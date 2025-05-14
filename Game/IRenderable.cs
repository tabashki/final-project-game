
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TeamCherry.Project;

interface IRenderable
{
    public Texture2D Texture { get; }
    public Vector2 Position { get => Vector2.Zero; }
    public Color Color { get => Color.White; }
    public float Rotation { get => 0f; }
    public Vector2 RotationOrigin { get => Vector2.Zero; }
    public SpriteEffects SpriteEffects { get => SpriteEffects.None; }
}