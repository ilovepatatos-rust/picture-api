using System.Drawing;
using Microsoft.AspNetCore.Mvc;
using PictureApi.Binders;
using PictureApi.Models;
using SkiaSharp;

namespace PictureApi.Controllers;

[ApiController]
[Route("[controller]")]
public class ImageEditController : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> AddOverlays([ModelBinder(typeof(JsonModelBinder))] ImageEditRequest request)
    {
        Console.WriteLine(
            $"Received a request to write {request.Texts.Count} texts and {request.Images.Count} image overlays on the image at {request.ImageUrl}");

        if (string.IsNullOrEmpty(request.ImageUrl))
            return BadRequest("No background image URL provided");

        // Download the image from the URL provided in the request
        byte[] imageBytes;
        using (var httpClient = new HttpClient())
        {
            imageBytes = await httpClient.GetByteArrayAsync(request.ImageUrl);
        }

        // Load the image into a SKBitmap
        SKBitmap bitmap;
        using (var stream = new MemoryStream(imageBytes))
        {
            bitmap = SKBitmap.Decode(stream);
        }

        // Create a new image with the same dimensions for drawing
        using (var surface = SKSurface.Create(new SKImageInfo(bitmap.Width, bitmap.Height)))
        {
            // Draw the original bitmap onto the surface
            var canvas = surface.Canvas;
            canvas.DrawBitmap(bitmap, 0, 0);

            if (request.Images != null)
                await ApplyImages(canvas, request.Images);

            if (request.Texts != null)
                ApplyTexts(canvas, request.Texts);

            // Convert the surface to an image, then to a byte array
            using (var image = surface.Snapshot())
            {
                using (var data = image.Encode(SKEncodedImageFormat.Png, 100))
                {
                    var imageArray = data.ToArray();

                    // Return the byte array
                    return File(imageArray, "image/png");
                }
            }
        }
    }

    private static async Task ApplyImages(SKCanvas canvas, List<ImageOverlay> images)
    {
        foreach (var overlay in images)
        {
            if (string.IsNullOrEmpty(overlay.Url))
                continue;

            byte[] overlayImageBytes;
            using (var httpClient = new HttpClient())
            {
                overlayImageBytes = await httpClient.GetByteArrayAsync(overlay.Url);
            }

            SKBitmap overlayBitmap;
            using (var stream = new MemoryStream(overlayImageBytes))
            {
                overlayBitmap = SKBitmap.Decode(stream);
            }

            // Resize if necessary
            SKBitmap resizedOverlayBitmap = overlayBitmap;
            if (overlay.Size.Width > 0 && overlay.Size.Height > 0)
            {
                resizedOverlayBitmap = new SKBitmap((int)overlay.Size.Width, (int)overlay.Size.Height);

                using var resizeCanvas = new SKCanvas(resizedOverlayBitmap);
                resizeCanvas.DrawBitmap(overlayBitmap,
                    new SKRect(0, 0, overlayBitmap.Width, overlayBitmap.Height),
                    new SKRect(0, 0, overlay.Size.Width, overlay.Size.Height));
            }

            canvas.DrawBitmap(resizedOverlayBitmap, overlay.Offset.X, overlay.Offset.Y);
        }
    }

    private static void ApplyTexts(SKCanvas canvas, List<TextOverlay> texts)
    {
        foreach (var overlay in texts)
        {
            if (string.IsNullOrEmpty(overlay.Text) || overlay.Size <= 0)
                continue;

            using var paint = new SKPaint();

            paint.Color = ColorUtility.ToColor(overlay.HexString);
            paint.TextSize = overlay.Size;
            paint.Typeface = FontUtility.ToFont(overlay.Font);

            PointF offset = AlignmentUtility.AlignText(paint, overlay);
            canvas.DrawText(overlay.Text, offset.X, offset.Y, paint);
        }
    }
}