
using Microsoft.Xna.Framework;

namespace TeamCherry.Project;

public interface IRenderableObjectsProvider
{
    public IReadOnlyList<IBatchedRenderable> RenderableObjects { get; }
    public Matrix RenderTransform { get; }
}
