
using System.Text.Json.Serialization;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TeamCherry.Project;

public class Player : Entity
{
    [JsonInclude]
    public float MaxSpeed { get; private set; } = 100f;

    private Vector2 targetVelocity = Vector2.Zero;
    private Vector2 moveInput = Vector2.Zero;

    internal AnimatedSprite? MovementAnimation;
    internal AnimatedSprite? AttackAnimation;

    private bool isAttacking = false;
    private MouseState previousMouse;

    public Player(Texture2D texture) : base(texture)
    {
        Scale = 0.1f; // Due to large player sprites
    }

    public void SetMoveInput(Vector2 input)
    {
        moveInput = input;
        if (moveInput.LengthSquared() > 1)
            moveInput.Normalize();
    }

    public override void Update(GameTime gameTime)
    {
        float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

        // === Attack Input ===
        MouseState currentMouse = Mouse.GetState();
        bool mouseJustPressed = currentMouse.LeftButton == ButtonState.Pressed &&
                                previousMouse.LeftButton == ButtonState.Released;

        if (mouseJustPressed && !isAttacking && AttackAnimation != null)
        {
            isAttacking = true;
            AttackAnimation.Reset();
        }

        // === Movement ===
        const float epsilonSq = 0.01f;
        const float speed = 12;
        float t = MathF.Min(speed * deltaTime, 1);

        targetVelocity = moveInput * MaxSpeed;
        Velocity = Vector2.Lerp(Velocity, targetVelocity, t);

        if (Velocity.LengthSquared() < epsilonSq)
        {
            Velocity = Vector2.Zero;
        }

        if (Velocity.X < 0)
        {
            SpriteEffects = SpriteEffects.FlipHorizontally;
        }
        else if (Velocity.X > 0)
        {
            SpriteEffects = SpriteEffects.None;
        }

        Position += Velocity * deltaTime;

        // === Animation Handling ===
        if (isAttacking && AttackAnimation != null)
        {
            AttackAnimation.Update(gameTime);
            Texture = AttackAnimation.SpriteSheet;

            // Assuming row 0 of attack sheet is the correct row
            AttackAnimation.SetRow(0);

            if (AttackAnimation.HasEnded)
            {
                isAttacking = false;
                AttackAnimation.Reset();
            }
        }
        else if (MovementAnimation != null)
        {
            Texture = MovementAnimation.SpriteSheet;

            if (MathF.Abs(Velocity.X) > epsilonSq)
                MovementAnimation.SetRow(1); // left/right
            else if (Velocity.Y < -epsilonSq)
                MovementAnimation.SetRow(3); // up
            else if (Velocity.Y > epsilonSq)
                MovementAnimation.SetRow(0); // down
            else
                MovementAnimation.SetRow(2); // idle

            MovementAnimation.Update(gameTime);
        }

        // === Debug Visuals ===
        if (Texture != null)
        {
            Vector2 center = Position + new Vector2(
                SourceRectangle?.Width ?? Texture.Width,
                SourceRectangle?.Height ?? Texture.Height
            ) / 2f;

            DebugDraw.Arrow(center, center + moveInput * 20, 3, Color.Blue);
            DebugDraw.Arrow(center, center + Velocity * 0.2f, 3, Color.White);
            DebugDraw.Rect(BoundingBox, Color.Yellow);

            if (isAttacking)
                DebugDraw.Text("ATTACKING", Color.Red, 0.1f);
        }

        SourceRectangle = isAttacking ? AttackAnimation?.CurrentFrameRectangle
           : MovementAnimation?.CurrentFrameRectangle;
        previousMouse = currentMouse;
        base.Update(gameTime);
    }
}
