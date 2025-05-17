
using Microsoft.Xna.Framework;

namespace TeamCherry.Project;

interface IRenderableObjectsProvider
{
    public IReadOnlyList<IRenderable> RenderableObjects { get; }
    public Matrix RenderTransform { get; }
}
