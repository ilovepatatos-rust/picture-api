using SkiaSharp;

namespace PictureApi;

public static class ColorUtility
{
    private const uint WHITE = uint.MaxValue;

    public static SKColor ToColor(string hex, uint fallback = WHITE)
    {
        return SKColor.TryParse(hex, out var color) ? color : new SKColor(fallback);
    }
}