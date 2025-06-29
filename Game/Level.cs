using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace TeamCherry.Project;

public class Level : IRenderableObjectsProvider
{
    private readonly List<Entity> entities = new();

    public IReadOnlyList<IBatchedRenderable> RenderableObjects => entities;
    public Matrix RenderTransform => Matrix.Identity;

    public void AddEntity(Entity entity)
    {
        entity.SetLevel(this); 
        entities.Add(entity);
    }

    public void Update(GameTime gameTime)
    {
        foreach (var entity in entities)
        {
            entity.Update(gameTime);
        }
    }

    public IEnumerable<Entity> GetRenderableObjects() => entities;
}
