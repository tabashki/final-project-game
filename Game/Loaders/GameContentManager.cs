
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace TeamCherry.Project;

public class GameContentManager : ContentManager
{
    // Duplicate of `loadedAssets` from the base class, which is sadly private
    private Dictionary<string, object> loadedAssetCache = new(StringComparer.OrdinalIgnoreCase);
    private Dictionary<Type, IAssetLoader> assetLoaders = new();

    private Stream? TryOpenAssetStream(string assetName, string[] supportedExtensions)
    {
        foreach (string ext in supportedExtensions)
        {
            string path = Path.Combine(RootDirectory, assetName) + ext;
            try
            {
                return TitleContainer.OpenStream(path);
            }
            catch { }
        }
        return null;
    }

    public GameContentManager(IServiceProvider serviceProvider)
        : base(serviceProvider, "Content")
    {
    }

    public void RegisterLoader(IAssetLoader loader)
    {
        var assetType = loader.AssetType;
        if (assetLoaders.ContainsKey(assetType))
        {
            throw new ArgumentException($"Cannot register duplicate loaders for the same asset type: {assetType}");
        }
        assetLoaders.Add(assetType, loader);
    }

    public virtual T Load<T>(string assetName, bool ignoreCache)
    {
        if (string.IsNullOrEmpty(assetName))
        {
            throw new ArgumentNullException("assetName");
        }
        Type assetType = typeof(T);

        if (assetLoaders.TryGetValue(assetType, out var loader))
        {
            ignoreCache |= loader.IgnoreCache;
            if (!ignoreCache)
            {
                if (loadedAssetCache.TryGetValue(assetName, out var cachedAsset))
                {
                    return (T)cachedAsset;
                }
            }

            Stream? stream = TryOpenAssetStream(assetName, loader.SupportedExtensions);
            if (stream == null)
            {
                throw new ContentLoadException($"Failed to open asset: {assetName}");
            }

            var asset = loader.LoadFromStream(stream, this);
            if (!ignoreCache)
            {
                loadedAssetCache.Add(assetName, asset);
            }
            return (T)asset;
        }

        return base.Load<T>(assetName);
    }

    public override T Load<T>(string assetName)
    {
        return Load<T>(assetName, false);
    }

    public override void Unload()
    {
        assetLoaders.Clear();
        loadedAssetCache.Clear();
        base.Unload();
    }
}