
namespace TeamCherry.Project;

public interface IAssetLoader
{
    public Type AssetType { get; }
    // When true, forces the content manager to always load a fresh instance of this asset instead
    // of trying to add it to the cache. VERY IMPORTANT for asset types that have state (e.g. Levels)
    public bool IgnoreCache { get; }
    public string[] SupportedExtensions { get; }

    public object LoadFromStream(Stream stream, GameContentManager contentManager);
}