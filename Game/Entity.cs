using System.Text.Json.Serialization;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TeamCherry.Project;

public abstract class Entity : BaseSprite
{
    public virtual Vector2 Velocity { get; protected set; } = Vector2.Zero;

    public Level? Level { get; private set; } 

    public virtual Rectangle BoundingBox
    {
        get
        {
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

    internal void SetLevel(Level level)
    {
        Level = level;
    }

    public void SetPosition(Vector2 position)
    {
        Position = position;
    }

    public virtual void Update(GameTime gameTime)
    {
        Position += gameTime.DeltaTime() * Velocity;
    }
}
