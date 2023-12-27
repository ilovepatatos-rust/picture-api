using System.Drawing;
using Newtonsoft.Json;

namespace PictureApi.Models;

[Serializable]
public class ImageOverlay
{
    [JsonProperty("Image url")]
    public string Url { get; set; }

    [JsonProperty("Offset")]
    public PointF Offset { get; set; }

    [JsonProperty("Size")]
    public SizeF Size { get; set; }
}