
using System.Text.Json.Serialization;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TeamCherry.Project;

public abstract class Entity : BaseSprite
{
    public virtual Vector2 Velocity { get; protected set; } = Vector2.Zero;

    public virtual Rectangle BoundingBox {
        get {
            int x = (int)MathF.Round(Position.X);
            int y = (int)MathF.Round(Position.Y);
            float w = Texture.Bounds.Width;
            float h = Texture.Bounds.Height;

            if (SourceRectangle != null)
            {
                w = SourceRectangle.Value.Width;
                h = SourceRectangle.Value.Height;
            }

            w *= Scale;
            h *= Scale;
            Rectangle r = new Rectangle(x, y, (int)MathF.Round(w), (int)MathF.Round(h));
            return r;
        }
    }
    
    public Entity(Texture2D texture) : base(texture)
    {
    }

    public virtual void Update(GameTime gameTime)
    {
        Position += gameTime.DeltaTime() * Velocity;
    }
}