
using System.Text.Json.Serialization;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TeamCherry.Project;

public abstract class BaseSprite : IBatchRenderable
{
    public Texture2D Texture { get; protected set; }
    public Vector2 Position { get; protected set; } = Vector2.Zero;
    public Color Color { get; protected set; } = Color.White;
    public float Rotation { get; protected set; } = 0f;
    public Vector2 RotationOrigin { get; protected set; } = Vector2.Zero;
    public SpriteEffects SpriteEffects { get; protected set; } = SpriteEffects.None;
    public Rectangle? SourceRectangle { get; protected set; } = null;

    public BaseSprite(Texture2D texture)
    {
        Texture = texture;
    }
    
    public void DrawBatched(SpriteBatch spriteBatch, Rectangle viewport)
    {
        spriteBatch.Draw(
            Texture, Position, SourceRectangle,
            Color, Rotation, RotationOrigin,
            1f, SpriteEffects, 0
        );
    }
}