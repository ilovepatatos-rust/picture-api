using System.Drawing;
using PictureApi.Models;
using SkiaSharp;

namespace PictureApi;

public static class AlignmentUtility
{
    public static PointF AlignText(SKPaint paint, TextOverlay instruction)
    {
        SKRect bounds = new();
        paint.MeasureText(instruction.Text, ref bounds);

        float x = instruction.Offset.X;
        float y = instruction.Offset.Y;

        switch (instruction.HorizontalAlignment)
        {
            case HorizontalAlignment.CENTERED:
                x -= bounds.MidX;
                break;
            case HorizontalAlignment.RIGHT:
                x -= bounds.Width;
                break;
        }

        switch (instruction.VerticalAlignment)
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