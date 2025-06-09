
using Microsoft.Xna.Framework;

namespace TeamCherry.Project;

interface IRenderableObjectsProvider
{
    public IReadOnlyList<IBatchRenderable> RenderableObjects { get; }
    public Matrix RenderTransform { get; }
}
