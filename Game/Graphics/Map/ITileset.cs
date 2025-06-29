namespace TeamCherry.Project;
using Microsoft.Xna.Framework.Graphics;

public interface ITileset
{
    public int TileCount { get; set; }
    public int Spacing { get; set; }
    public int TileWidth { get; set; }
    public int TileHeight { get; set; }
    public string? Name { get; set; }
    public int ImageMargin { get; set; }
    public int TilesPerRow { get; set; }
    public Texture2D? Texture { get; set; }
    public int ImageHeight { get; set; }
    public int ImageWidth { get; set; }
}