
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TeamCherry.Project;

interface IBatchedRenderable
{
    public void DrawBatched(SpriteBatch spriteBatch, Rectangle visibleRegion);
}
