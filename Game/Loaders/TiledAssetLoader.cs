namespace TeamCherry.Project;

class TiledAssetLoader : JsonAssetLoader<TileMap>
{
    public override string[] SupportedExtensions => [".tmj"];
}