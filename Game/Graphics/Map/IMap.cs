namespace TeamCherry.Project;

interface IMap
{
    public int MapWidth { get; set; }
    public int MapHeight { get; set; }
    public int TileWidth { get; set; }
    public int TileHeight { get; set; }
    public IReadOnlyList<ILayer>? Layers { get; set; }
    public IReadOnlyList<ITileset>? Tilesets { get; set; }
}