
using System.Text.Json.Serialization;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TeamCherry.Project;

abstract class Entity : BaseSprite
{
    public virtual Vector2 Velocity { get; protected set; } = Vector2.Zero;
    
    public virtual Rectangle BoundingBox {
        get {
            var r = Texture.Bounds;
            int x = (int)MathF.Round(Position.X);
            int y = (int)MathF.Round(Position.Y);
            r.Offset(new Point(x, y));
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