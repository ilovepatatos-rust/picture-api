using Microsoft.AspNetCore.Mvc;
using PictureApi.Models;
using SkiaSharp;

namespace PictureApi.Controllers;

[ApiController]
[Route("[controller]")]
public class ImageEditController : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> EditImage([FromBody] ImageEditRequest request)
    {
        Console.WriteLine($"Received a write {request.Texts.Count} text on the image at {request.ImageUrl}");

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

            // Apply each text instruction
            foreach (var instruction in request.Texts)
            {
                using var paint = new SKPaint();

                // Load the font from the file
                var typeface = SKTypeface.FromFile($"./fonts/{instruction.Font}");
                paint.Typeface = typeface;
                paint.TextSize = instruction.Size;
                paint.Color = SKColors.White;

                // Draw the text onto the canvas
                canvas.DrawText(instruction.Value, instruction.Offset.X, instruction.Offset.Y, paint);
            }

            // Convert the surface to an image, then to a byte array
            using (var image = surface.Snapshot())
                using (var data = image.Encode(SKEncodedImageFormat.Png, 100))
                {
                    var imageArray = data.ToArray();

                    // Return the byte array
                    return File(imageArray, "image/png");
                }
        }
    }
}