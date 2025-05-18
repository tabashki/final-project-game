using Microsoft.Xna.Framework;

namespace TeamCherry.Project;

class Camera
{
    private Vector2 position;
    private readonly int viewportWidth;
    private readonly int viewportHeight;

    private float zoom = 1f;
    public float Zoom
    {
        get => zoom;
        set => zoom = MathHelper.Clamp(value, 0.1f, 20f);
    }

    public Camera(int viewportWidth, int viewportHeight)
    {
        this.viewportWidth = viewportWidth;
        this.viewportHeight = viewportHeight;
        Zoom = 10f;
    }

    public void Follow(Vector2 targetCenter)
    {
        position = targetCenter;
    }

    public Matrix GetTransformMatrix()
    {
        var scale = Matrix.CreateScale(Zoom); 
        var translate = Matrix.CreateTranslation(position.X, position.Y, 0); 
        var offset = Matrix.CreateTranslation(viewportWidth / 2f, viewportHeight / 2f, 0); 
        return translate * scale * offset;
    }

}
