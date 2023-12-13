using System.Drawing;

namespace PictureApi.Models;

[Serializable]
public class Text
{
    public string Value { get; set; }
    public PointF Offset { get; set; }
    public float Size { get; set; }
    public string Font { get; set; }
}