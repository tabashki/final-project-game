using Microsoft.Xna.Framework;

namespace TeamCherry.Project;

public class Camera2D
{
    public Vector2 Position { get; private set; }
    public int ViewWidth { get; }
    public int ViewHeight { get; }
    public bool IsEnabled { get; set; }

    private float followLerp = 0.1f; // follow factor

    public Camera2D(int viewWidth, int viewHeight, float followLerp = 0.1f)
    {
        ViewWidth = viewWidth;
        ViewHeight = viewHeight;
        Position = Vector2.Zero;
        this.followLerp = MathHelper.Clamp(followLerp, 0.01f, 1f);
    }

    public void Update(Vector2 target)
    {
        Position = Vector2.Lerp(Position, target, followLerp);
    }

    public Matrix GetTransform()
    {
        return Matrix.CreateTranslation(new Vector3(
            -Position.X + ViewWidth / 2f,
            -Position.Y + ViewHeight / 2f,
            0));
    }

    public Rectangle GetViewRectangle()
    {
        int x = (int)(Position.X - ViewWidth / 2f);
        int y = (int)(Position.Y - ViewHeight / 2f);
        return new Rectangle(x, y, ViewWidth, ViewHeight);
    }
}
