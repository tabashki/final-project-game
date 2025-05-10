using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

abstract class Entity : IRenderable
{
    public virtual Texture2D Texture { get; protected set; }
    public virtual Vector2 Position { get; protected set; } = Vector2.Zero;
    public virtual Vector2 Velocity { get; protected set; } = Vector2.Zero;
    public virtual Color Color => Color.White;
    public virtual Rectangle BoundingBox {
        get {
            var r = Texture.Bounds;
            int x = (int)MathF.Round(Position.X);
            int y = (int)MathF.Round(Position.Y);
            r.Offset(new Point(x, y));
            return r;
        }
    }

    public Entity(Texture2D texture)
    {
        Texture = texture;
    }

    public virtual void Update(GameTime gameTime)
    {
        Position += gameTime.DeltaTime() * Velocity;
    }
}