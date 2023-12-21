using System.Drawing;

namespace PictureApi.Models;

[Serializable]
public class ImageOverlay
{
    public string Url { get; set; }
    public PointF Offset { get; set; }
    public SizeF Size { get; set; }
}