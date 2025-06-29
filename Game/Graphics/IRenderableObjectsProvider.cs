
using Microsoft.Xna.Framework;

namespace TeamCherry.Project;

interface IRenderableObjectsProvider
{
    public IReadOnlyList<IBatchedRenderable> RenderableObjects { get; }
    public Matrix RenderTransform { get; }
}
