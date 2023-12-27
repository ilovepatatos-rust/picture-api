using System.Drawing;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace PictureApi.Models;

[Serializable]
public class TextOverlay
{
    [JsonProperty("Text")]
    public string Text { get; set; }

    [JsonProperty("Size")]
    public float Size { get; set; }

    [JsonProperty("Font")]
    public string Font { get; set; }

    [JsonProperty("Hex")]
    public string HexString { get; set; } = "#FFFFFF";

    [JsonConverter(typeof(StringEnumConverter))]
    [JsonProperty("Horizontal alignment [ LEFT | CENTER | RIGHT ]")]
    public HorizontalAlignment HorizontalAlignment { get; set; } = HorizontalAlignment.LEFT;

    [JsonConverter(typeof(StringEnumConverter))]
    [JsonProperty("Vertical alignment [ TOP | CENTER | BOTTOM ]")]
    public VerticalAlignment VerticalAlignment { get; set; } = VerticalAlignment.CENTERED;

    [JsonProperty("Offset")]
    public PointF Offset { get; set; }
}