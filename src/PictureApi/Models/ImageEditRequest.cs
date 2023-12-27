namespace PictureApi.Models;

[Serializable]
public class ImageEditRequest
{
    public string ImageUrl { get; set; } = "";
    public List<TextOverlay> Texts { get; set; } = new();
    public List<ImageOverlay> Images { get; set; } = new();
}