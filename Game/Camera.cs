using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Media;

namespace TeamCherry.Project;

public class Camera
{
    private Vector2 position;
    private Vector2 viewportDimensions;
    private Entity? follow = null;
    

    private float zoom = 1f;
    public float Zoom
    {
        get => zoom;
        set => zoom = MathHelper.Clamp(value, 0.1f, 20f);
    }

    public Camera(Vector2 viewportDims)
    {
        this.viewportDimensions = viewportDims;
    }

    public void Follow(Entity targetEntity)
    { 
        follow = targetEntity;
    }

    public void Unfollow()
    {
        follow = null;
    }

    public void Update(GameTime gameTime)
    {
        if (follow != null) 
        {
            var halfWidth = new Vector2(follow.BoundingBox.Width, follow.BoundingBox.Height) / 2f;
            position = follow.Position + halfWidth;
        }
    }

    public Matrix GetTransformMatrix()
    {
        var matrix = Matrix.CreateTranslation(new Vector3(-position, 0));
        matrix *= Matrix.CreateScale(new Vector3(zoom, zoom, 1));
        matrix *= Matrix.CreateTranslation(new Vector3(viewportDimensions / 2f, 0));
        return matrix;
    }

    internal void Follow(object player)
    {
        throw new NotImplementedException();
    }
}
