using SkiaSharp;

namespace PictureApi;

public static class FontUtility
{
    public static SKTypeface ToFont(string fontName)
    {
        //TODO: Add font validation and fallback if not found
        return SKTypeface.FromFile($"./fonts/{fontName}");
    }
}