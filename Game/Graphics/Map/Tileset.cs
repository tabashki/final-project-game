using System.Text.Json.Serialization;
using Microsoft.Xna.Framework.Graphics;

namespace TeamCherry.Project;

public class Tileset: ITileset
{
    // TMJ properties
    [JsonPropertyName("firstgid")]
    public int FirstGID { get; set; }  // First global ID of tileset

    [JsonPropertyName("tilecount")]
    public int TileCount { get; set; }

    [JsonPropertyName("spacing")]
    public int Spacing { get; set; }

    [JsonPropertyName("tilewidth")]
    public int TileWidth { get; set; }

    [JsonPropertyName("tileheight")]
    public int TileHeight { get; set; }

    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("margin")]
    public int ImageMargin { get; set; }

    [JsonPropertyName("columns")]
    public int TilesPerRow { get; set; }

    [JsonPropertyName("image")]
    public Texture2D? Texture { get; set; }

    [JsonPropertyName("imageheight")]
    public int ImageHeight { get; set; }

    [JsonPropertyName("imagewidth")]
    public int ImageWidth { get; set; }

    [JsonPropertyName("tiles")]
    public List<Tile>? Tiles { get; set; }
}