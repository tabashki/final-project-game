namespace TeamCherry.Project;
using Microsoft.Xna.Framework.Graphics;

public interface ITileset
{
    public int TileCount { get; init; }
    public int Spacing { get; init; }
    public int TileWidth { get; init; }
    public int TileHeight { get; init; }
    public string? Name { get; init; }
    public int ImageMargin { get; init; }
    public int TilesPerRow { get; init; }
    public Texture2D? Texture { get; init; }
    public int ImageHeight { get; init; }
    public int ImageWidth { get; init; }
}