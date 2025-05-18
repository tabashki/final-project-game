
namespace TeamCherry.Project;

interface IAssetLoader
{
    public Type AssetType { get; }
    public string[] SupportedExtensions { get; }

    public object LoadFromStream(Stream stream, GameContentManager contentManager);
}