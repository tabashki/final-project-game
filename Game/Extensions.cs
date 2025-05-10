
using Microsoft.Xna.Framework;

public static class GameTimeExtensions
{
    public static float DeltaTime(this GameTime gameTime)
    {
        return (float)gameTime.ElapsedGameTime.TotalSeconds;
    }
}

public static class Vector2Extensions
{
    public static Vector2 NormalizedSafe(this Vector2 v)
    {
        if (v.LengthSquared() > 0)
        {
            Vector2 temp = v;
            temp.Normalize();
            return temp;
        }
        return Vector2.Zero;
    }
}
