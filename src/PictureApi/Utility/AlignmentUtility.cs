using System.Drawing;
using PictureApi.Models;
using SkiaSharp;

namespace PictureApi;

public static class AlignmentUtility
{
    public static PointF AlignText(SKPaint paint, TextOverlay overlay)
    {
        SKRect bounds = new();
        paint.MeasureText(overlay.Text, ref bounds);

        float x = overlay.Offset.X;
        float y = overlay.Offset.Y;

        switch (overlay.HorizontalAlignment)
        {
            case HorizontalAlignment.CENTERED:
                x -= bounds.MidX;
                break;
            case HorizontalAlignment.RIGHT:
                x -= bounds.Width;
                break;
        }

        switch (overlay.VerticalAlignment)
        {
            case VerticalAlignment.CENTERED:
                y -= bounds.MidY;
                break;
            case VerticalAlignment.BOTTOM:
                y -= bounds.Height;
                break;
        }

        return new PointF(x, y);
    }
}