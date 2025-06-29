using System.Text.Json.Serialization; 

namespace TeamCherry.Project;

public class Tile
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("properties")]
    public List<TileProperty>? Properties { get; set; }
}