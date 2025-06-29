using System.Text.Json.Serialization;
using Microsoft.Xna.Framework.Graphics;

namespace TeamCherry.Project;

public class Tileset: ITileset
{
    // TMJ properties
    [JsonPropertyName("firstgid")]
    public int FirstGID { get; init; }  // First global ID of tileset

    [JsonPropertyName("tilecount")]
    public int TileCount { get; init; }

    [JsonPropertyName("spacing")]
    public int Spacing { get; init; }

    [JsonPropertyName("tilewidth")]
    public int TileWidth { get; init; }

    [JsonPropertyName("tileheight")]
    public int TileHeight { get; init; }

    [JsonPropertyName("name")]
    public string? Name { get; init; }

    [JsonPropertyName("margin")]
    public int ImageMargin { get; init; }

    [JsonPropertyName("columns")]
    public int TilesPerRow { get; init; }

    [JsonPropertyName("image")]
    public Texture2D? Texture { get; init; }

    [JsonPropertyName("imageheight")]
    public int ImageHeight { get; init; }

    [JsonPropertyName("imagewidth")]
    public int ImageWidth { get; init; }

    [JsonPropertyName("tiles")]
    public List<Tile>? Tiles { get; init; }
}
