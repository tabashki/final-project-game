
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

class Player : Entity
{
    private const float maxVelocity = 100f;
    private Vector2 targetVelocity = Vector2.Zero;
    private Vector2 moveInput = Vector2.Zero;
    private bool flipHoriz = false;

    public virtual SpriteEffects SpriteEffects => (flipHoriz) ? SpriteEffects.FlipHorizontally : SpriteEffects.None;

    public Player(Texture2D texture) : base(texture) {}

    public void SetMoveInput(Vector2 input)
    {
        moveInput = input;
        if (moveInput.LengthSquared() > 1)
        {
            moveInput.Normalize();
        }
    }

    public override void Update(GameTime gameTime)
    {
        const float epsilonSq = 0.01f;
        float deltaTime = gameTime.DeltaTime();

        const float speed = 12;
        float t = MathF.Min(speed * deltaTime, 1);

        targetVelocity = moveInput * maxVelocity;
        Velocity = Vector2.Lerp(Velocity, targetVelocity, t);

        if (Velocity.LengthSquared() < epsilonSq)
        {
            Velocity = Vector2.Zero;
        }
        flipHoriz = Velocity.X < 0;

        Vector2 center = Position + new Vector2(Texture.Width / 2f, Texture.Height / 2f);
        DebugDraw.Arrow(center, center + moveInput * 20, 3, Color.Blue);
        DebugDraw.Arrow(center, center + Velocity * 0.2f, 3, Color.White);
        DebugDraw.Rect(BoundingBox, Color.Yellow);

        base.Update(gameTime);
    }
}