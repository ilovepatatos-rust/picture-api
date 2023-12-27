using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;

namespace PictureApi.Binders;

/// <summary>
/// Allows handling of [JsonProperty("name")] as [FromBody] doesn't handle it
/// </summary>
public class JsonModelBinder : IModelBinder
{
    public async Task BindModelAsync(ModelBindingContext bindingContext)
    {
        if (bindingContext == null)
            throw new ArgumentNullException(nameof(bindingContext));

        var reader = new StreamReader(bindingContext.HttpContext.Request.Body);
        string json = await reader.ReadToEndAsync();

        try
        {
            var model = JsonConvert.DeserializeObject(json, bindingContext.ModelType);
            bindingContext.Result = ModelBindingResult.Success(model);
        }
        catch (JsonException)
        {
            bindingContext.ModelState.AddModelError(bindingContext.ModelName, "Invalid JSON!");
        }
    }
}