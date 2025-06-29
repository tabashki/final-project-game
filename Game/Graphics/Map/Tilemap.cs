using System.Text.Json;
using System.Text.Json.Serialization;  // For  [JsonPropertyName]
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TeamCherry.Project;


public class TileMap : IMap, IBatchedRenderable
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
    public List<TileLayer>? Layers { get; set; }
    IReadOnlyList<ILayer>? IMap.Layers
    {
        get => Layers;
        set => Layers = value?.Cast<TileLayer>().ToList();
    }
    [JsonPropertyName("tilesets")]
    public List<Tileset>? Tilesets { get; set; }
    IReadOnlyList<ITileset>? IMap.Tilesets
    {
        get => Tilesets;
        set => Tilesets = value?.Cast<Tileset>().ToList();
    }


    public string PrintShort()
    {
        string msg = $"Map size: {this.MapWidth}x{this.MapHeight}\n" +
                     $"Tile size: {this.TileWidth}x{this.TileHeight}\n" +
                     $"Number of loaded layers: {this.Layers?.Count ?? 0}";
        return msg;
    }

    public int GetTileID(TileLayer layer, int x, int y)
    {
        int tileIndex = x + y * layer.LayerWidth;
        return layer.Data[tileIndex];
    }

    public bool isTileCollidable(int tileId)
    {
        foreach (Tileset tileset in Tilesets)
        {
            if (tileset.FirstGID <= tileId && tileId < tileset.FirstGID + tileset.TileCount)
            {
                int localId = tileId - tileset.FirstGID;
                foreach (Tile t in tileset.Tiles)
                {
                    if (localId == t.Id)
                    {
                        foreach (TileProperty prop in t.Properties)
                        {
                            if (prop.Name == "collidable" && prop.Type == "bool" && (bool)prop.Value)
                            {
                                return true;
                            }
                        }
                    }
                }
            }
        }
        return false;
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
        // throw new Exception($"The tileset for id: \"{id}\" could not be loaded");
        return null;  // Currently needed to check if id == 0. TODO: Make checking for id == 0 unnecessary!
    }

    public void DrawBatched(SpriteBatch spriteBatch, Rectangle visibleRegion)
    {
        if (Layers == null)
        {
            throw new Exception($"TileMap.Layers is null");
        }
        foreach (TileLayer layer in Layers)
            {
                for (int layerY = visibleRegion.Y / this.TileHeight; layerY <= (visibleRegion.Y + visibleRegion.Height) / this.TileHeight; layerY++)
                {
                    for (int layerX = visibleRegion.X / this.TileWidth; layerX <= (visibleRegion.X + visibleRegion.Width) / this.TileWidth; layerX++)
                    {
                        if (layerX < 0 || layerY < 0 || layerX >= MapWidth || layerY >= MapHeight)
                        {
                            continue;
                        }

                        int tileId = GetTileID(layer, layerX, layerY);
                        Tileset tileset = this.GetTilesetForID(tileId);
                        if (tileset == null)  // If id == 0 the tileset is null. If there is no tileset, continue
                        {
                            continue;
                        }

                        int tilesetX = (tileId - tileset.FirstGID) % tileset.TilesPerRow;
                        int tilesetY = (tileId - tileset.FirstGID) / tileset.TilesPerRow;

                        int pixelX = tileset.ImageMargin + tilesetX * tileset.Spacing + tilesetX * tileset.TileWidth;
                        int pixelY = tileset.ImageMargin + tilesetY * tileset.Spacing + tilesetY * tileset.TileHeight;

                        Rectangle textureLocation = new Rectangle(pixelX, pixelY, tileset.TileWidth, tileset.TileHeight);
                        Vector2 position = new Vector2(layerX * tileset.TileWidth, layerY * tileset.TileHeight);
                        // TODO: Check layerDepth in spriteBatch.Draw. Maybe use if there are problems with layer ordering?
                        spriteBatch.Draw(tileset.Texture, position, textureLocation, Color.White, 0f, Vector2.Zero, 1.01f, SpriteEffects.None, 0);
                    }
                }
            }
    }
}

