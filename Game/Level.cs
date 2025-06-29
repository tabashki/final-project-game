using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TeamCherry.Project;

public class Level : IRenderableObjectsProvider
{
    [JsonInclude]
    public List<Entity> Entities { get; private set; } = new();

    [JsonInclude]
    public Vector2 PlayerStart { get; private set; }

    public Player? Player { get; private set; }

    public IReadOnlyList<IBatchedRenderable> RenderableObjects => Entities;
    public Matrix RenderTransform => Camera?.GetTransformMatrix() ?? Matrix.Identity;

    public Camera? Camera { get; private set; }

    public void LoadContent(ContentManager content)
    {
        // Use PlayerStart from JSON or fallback position if PlayerStart is Vector2.Zero
        Vector2 spawnPosition = PlayerStart == Vector2.Zero ? new Vector2(100, 150) : PlayerStart;

        for (int i = 0; i < Entities.Count; i++)
        {
            var entity = Entities[i];

            if (entity is Player player)
            {
                Player = player;

                // Load and assign texture to the player
                player.SetTexture(content.Load<Texture2D>("Sprites/guy"));

                // Set player position to spawnPosition
                player.SetPosition(spawnPosition);
            }
            else
            {
         
                entity.SetTexture(content.Load<Texture2D>("Sprites/guy"));
            }

            // Link entity to this level
            entity.SetLevel(this);
        }

        // Create camera with viewport size, then make it follow the player
        Camera = new Camera(new Vector2(1280, 720)); // Adjust viewport size if necessary

        if (Player != null)
        {
            Camera.Follow(Player);
        }
    }


    public void Update(GameTime gameTime)
    {
        foreach (var entity in Entities)
        {
            entity.Update(gameTime);
        }

        Camera?.Update(gameTime);
    }

    public void DebugUpdate(GameTime gameTime)
    {
        if (Player != null)
        {
            DebugDraw.Text($"position: {Player.Position.X:F1}, {Player.Position.Y:F1}", Color.White);
        }
    }
}

