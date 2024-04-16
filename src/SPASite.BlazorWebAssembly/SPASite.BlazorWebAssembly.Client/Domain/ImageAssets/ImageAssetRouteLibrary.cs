namespace SPASite.BlazorWebAssembly.Client.Domain;

/// <summary>
/// Route library for image assets
/// </summary>
public class ImageAssetRouteLibrary
{
    /// <summary>
    /// Gets the url for an image asset, with optional resizing parameters
    /// </summary>
    /// <param name="asset">asset to get the url for</param>
    /// <param name="width">width to resize the image to</param>
    /// <param name="height">height to resize the image to</param>
    public static string ImageAsset(ImageAssetRenderDetails? asset, int? width, int? height = null)
    {
        if (asset == null)
        {
            return string.Empty;
        }

        var settings = GetResizeSettings(asset, width, height);

        return GetUrl(asset, settings);
    }

    private static ImageResizeSettings GetResizeSettings(ImageAssetRenderDetails asset, int? width, int? height)
    {
        var settings = new ImageResizeSettings();

        if (width.HasValue)
        {
            settings.Width = width.Value;
        }

        if (height.HasValue)
        {
            settings.Height = height.Value;
        }

        SetDefaultCrop(asset, settings);

        return settings;
    }

    private static string GetUrl(ImageAssetRenderDetails asset, ImageResizeSettings? settings = null)
    {
        var url = asset.Url;

        if (settings != null && asset.FileExtension != "svg")
        {
            url += settings.ToQueryString();
        }

        return url;
    }

    private static void SetDefaultCrop(ImageAssetRenderDetails asset, ImageResizeSettings? settings)
    {
        if (settings == null)
        {
            return;
        }

        if (isDefinedAndChanged(settings.Width, asset.Width) || isDefinedAndChanged(settings.Height, asset.Height))
        {
            if (asset.DefaultAnchorLocation.HasValue)
            {
                settings.Anchor = asset.DefaultAnchorLocation.Value;
            }
        }
    }

    private static bool isDefinedAndChanged(int settingValue, int imageValue)
    {
        return settingValue > 0 && settingValue != imageValue;
    }
}
