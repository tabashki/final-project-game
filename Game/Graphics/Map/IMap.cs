namespace TeamCherry.Project;

interface IMap
{
    public int MapWidth { get; init; }
    public int MapHeight { get; init; }
    public int TileWidth { get; init; }
    public int TileHeight { get; init; }
    public IReadOnlyList<ILayer>? Layers { get; init; }
    public IReadOnlyList<ITileset>? Tilesets { get; init; }
}