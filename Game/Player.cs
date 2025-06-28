
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

    private AnimatedSprite? movementAnimation;
    private AnimatedSprite? attackAnimation;

    private bool isAttacking = false;
    private MouseState previousMouse;

    public override SpriteEffects SpriteEffects => flipHoriz ? SpriteEffects.FlipHorizontally : SpriteEffects.None;

    public override Rectangle? SourceRectangle =>
        isAttacking ? attackAnimation?.CurrentFrameRectangle : movementAnimation?.CurrentFrameRectangle;

    public override Vector2 RotationOrigin =>
        new Vector2(SourceRectangle?.Width ?? 0, SourceRectangle?.Height ?? 0) / 2f;

    private static Camera2D? camera;
    private static bool cameraTogglePressedLastFrame = false;

    public Player() : base(null) { }

    public Player(AnimatedSprite movementAnimation, AnimatedSprite attackAnimation)
        : base(movementAnimation.SpriteSheet)
    {
        this.movementAnimation = movementAnimation;
        this.attackAnimation = attackAnimation;
        this.Texture = movementAnimation.SpriteSheet;
        this.Scale = 0.05f; // Adjust as needed
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

        // === Camera Toggle ===
        var keyboard = Keyboard.GetState();
        bool toggleCamera = keyboard.IsKeyDown(Keys.C);

        if (toggleCamera && !cameraTogglePressedLastFrame)
        {
            camera ??= new Camera2D(200, 100);
            camera.IsEnabled = !camera.IsEnabled;
        }

        cameraTogglePressedLastFrame = toggleCamera;

        if (camera?.IsEnabled == true)
            camera.Update(Position);

        // === Attack Input ===
        MouseState currentMouse = Mouse.GetState();
        bool mouseJustPressed = currentMouse.LeftButton == ButtonState.Pressed &&
                                previousMouse.LeftButton == ButtonState.Released;

        if (mouseJustPressed && !isAttacking && attackAnimation != null)
        {
            isAttacking = true;
            attackAnimation.Reset();
        }

        // === Movement ===
        const float epsilonSq = 0.01f;
        const float speed = 12;
        float t = MathF.Min(speed * deltaTime, 1);

        targetVelocity = moveInput * MaxSpeed;
        Velocity = Vector2.Lerp(Velocity, targetVelocity, t);

        if (Velocity.LengthSquared() < epsilonSq)
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
        if (isAttacking && attackAnimation != null)
        {
            attackAnimation.Update(gameTime);
            Texture = attackAnimation.SpriteSheet;

            // Assuming row 0 of attack sheet is the correct row
            attackAnimation.SetRow(0);

            if (attackAnimation.HasEnded)
            {
                isAttacking = false;
                attackAnimation.Reset();
            }
        }
        else if (movementAnimation != null)
        {
            Texture = movementAnimation.SpriteSheet;

            if (MathF.Abs(Velocity.X) > epsilonSq)
                movementAnimation.SetRow(1); // left/right
            else if (Velocity.Y < -epsilonSq)
                movementAnimation.SetRow(3); // up
            else if (Velocity.Y > epsilonSq)
                movementAnimation.SetRow(0); // down
            else
                movementAnimation.SetRow(2); // idle

            movementAnimation.Update(gameTime);
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

        if (camera?.IsEnabled == true)
            DebugDraw.Rect(camera.GetViewRectangle(), Color.Red);

        previousMouse = currentMouse;
        base.Update(gameTime);
    }

    public static Matrix GetCameraTransform() => camera?.GetTransform() ?? Matrix.Identity;
}
