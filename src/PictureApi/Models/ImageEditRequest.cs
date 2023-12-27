using Newtonsoft.Json;

namespace PictureApi.Models;

[Serializable]
public class ImageEditRequest
{
    [JsonProperty("Background image url")]
    public string ImageUrl { get; set; } = "";

    [JsonProperty("Text overlays")]
    public List<TextOverlay> Texts { get; set; } = new();

    [JsonProperty("Image overlays")]
    public List<ImageOverlay> Images { get; set; } = new();
}