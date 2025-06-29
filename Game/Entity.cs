using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Text.Json.Serialization;


namespace TeamCherry.Project
{
    [JsonPolymorphic(TypeDiscriminatorPropertyName = "$type")]
    [JsonDerivedType(typeof(Player), "Player")]
    public abstract class Entity : BaseSprite
    {
        public virtual Vector2 Velocity { get; protected set; } = Vector2.Zero;

        public Level? Level { get; private set; }

        public virtual Rectangle BoundingBox
        {
            get
            {
                var r = Texture.Bounds;
                int x = (int)System.MathF.Round(Position.X);
                int y = (int)System.MathF.Round(Position.Y);
                r.Offset(new Point(x, y));
                return r;
            }
        }
        public void SetTexture(Texture2D texture)
        {
            Texture = texture;
        }

        public Entity(Texture2D texture) : base(texture) { }

        public void SetLevel(Level level)
        {
            Level = level;
        }

        public virtual void Update(GameTime gameTime)
        {
            Position += gameTime.DeltaTime() * Velocity;
        }

        internal void SetPosition(Vector2 position)
        {
            Position = position;
        }
    }
}
