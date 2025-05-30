using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Media;

namespace TeamCherry.Project;

class Camera
{
    private Vector2 position;
    private Vector2 viewportDimensions;

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

    public void Follow(Vector2 targetCenter)
    {
        position = targetCenter;
    }

    public Matrix GetTransformMatrix()
    {
        var matrix = Matrix.CreateTranslation(new Vector3(-position, 0));
        matrix *= Matrix.CreateScale(new Vector3(zoom, zoom, 1));
        matrix *= Matrix.CreateTranslation(new Vector3(viewportDimensions / 2f, 0));
        return matrix;
    }

}
