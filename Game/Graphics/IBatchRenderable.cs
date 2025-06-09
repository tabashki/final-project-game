
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TeamCherry.Project;

interface IBatchRenderable
{
    public void DrawBatched(SpriteBatch spriteBatch, Rectangle viewport);
}
