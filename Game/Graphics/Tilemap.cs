using System.Text.Json;
using System.Text.Json.Serialization;  // For  [JsonPropertyName]
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TeamCherry.Project;


class TileMap : IBatchedRenderable
{
    // TMJ properties
    [JsonPropertyName("width")]
    public int MapWidth { get; set; }

    [JsonPropertyName("height")]
    public int MapHeight { get; set; }

    [JsonPropertyName("tilewidth")]
    public int TileWidth { get; set; }

    [JsonPropertyName("tileheight")]
    public int TileHeight { get; set; }

    [JsonPropertyName("layers")]
    public List<TileMapLayer>? Layers { get; set; }

    [JsonPropertyName("tilesets")]
    public List<Tileset>? Tilesets { get; set; }


    // Methods
    public string PrintShort()
    {
        string msg = $"Map size: {this.MapWidth}x{this.MapHeight}\n" +
                     $"Tile size: {this.TileWidth}x{this.TileHeight}\n" +
                     $"Number of loaded layers: {this.Layers?.Count ?? 0}";
        return msg;
    }

    public string PrintLong()
    {
        throw new NotImplementedException();
    }

    public Tileset? GetTilesetForID(int id)
    {
        if (Tilesets != null)
        {
            foreach (Tileset tileset in Tilesets)
            {
                if (tileset.FirstGID <= id && id < tileset.FirstGID + tileset.TileCount)
                {
                    return tileset;
                }
            }
        }
        return null;
    }

    public void DrawBatched(SpriteBatch spriteBatch, Rectangle visibleRegion)
    {
        Vector2 position = Vector2.Zero;
        for (int i = 0; i < Tilesets.Count; i++)
        {
            if (i != 0)
            {
                position.X += Tilesets[i - 1].ImageWidth;
            }
            spriteBatch.Draw(
                    Tilesets[i].Texture, position, null,
                    Color.White, 0f, Vector2.Zero,
                    1f, SpriteEffects.None, 0
                );
        }
    }
}


class TileMapLayer
{
    // TMJ properties
    [JsonPropertyName("name")]
    public string? Name { get; set; }
  
    [JsonPropertyName("width")]
    public int LayerWidth { get; set; }  // Should usually be same as MapWidth

    [JsonPropertyName("height")]
    public int LayerHeight { get; set; }  // Should usually be same as MapHeight

    [JsonPropertyName("data")]
    public int[]? Data { get; set; }

    [JsonPropertyName("type")]
    public string? Type { get; set; }

    [JsonPropertyName("visible")]
    public bool Visible { get; set; }

    [JsonPropertyName("id")]
    public int Id { get; set; }
}


class Tileset
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
}

