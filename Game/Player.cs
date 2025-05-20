
using System.Text.Json.Serialization;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TeamCherry.Project;

public class Player : Entity
{
    [JsonInclude]
    public float MaxSpeed { get; private set; } = 100f;

    private Vector2 targetVelocity = Vector2.Zero;
    private Vector2 moveInput = Vector2.Zero;

    private Texture2D[]? attackFrames;
    private int currentFrame = 0;
    private float animationTimer = 0f;
    private float animationSpeed = 0.1f; // Seconds per frame

    private SpriteSheet? spriteSheet;

    [JsonInclude]
    public int SpriteIndex { get; set; } = 0;

    public override SpriteEffects SpriteEffects => flipHoriz ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
    public override Rectangle? SourceRectangle => spriteSheet?.GetFrame(SpriteIndex);

    // Parameterless constructor for deserialization
    public Player() : base(null) { }

    public Player(Texture2D texture, SpriteSheet sheet) : base(texture)
    {
        spriteSheet = sheet;
    }

    public Player(Texture2D texture) : base(texture) { }

    public Player(Texture2D idleTexture, SpriteSheet idleSheet, Texture2D[] attackFrames)
        : base(idleTexture)
    {
        this.spriteSheet = idleSheet;
        this.attackFrames = attackFrames;
        Texture = attackFrames[0];
    }

    public void AssignAttackFrames(Texture2D[] frames)
    {
        attackFrames = frames;
        currentFrame = 0;
        animationTimer = 0f;
        Texture = attackFrames[0];
    }

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
        float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

        // Basic movement
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

        // Animate attack frames
        if (attackFrames != null && attackFrames.Length > 0)
        {
            animationTimer += deltaTime;
            if (animationTimer >= animationSpeed)
            {
                animationTimer = 0f;
                currentFrame = (currentFrame + 1) % attackFrames.Length;
                Texture = attackFrames[currentFrame];
            }
        }

        // Debug visuals
        if (Texture != null)
        {
            Vector2 center = Position + new Vector2(
                (SourceRectangle?.Width ?? Texture.Width) / 2f,
                (SourceRectangle?.Height ?? Texture.Height) / 2f
            );

            DebugDraw.Arrow(center, center + moveInput * 20, 3, Color.Blue);
            DebugDraw.Arrow(center, center + Velocity * 0.2f, 3, Color.White);
            DebugDraw.Rect(BoundingBox, Color.Yellow);
        }

        base.Update(gameTime);
    }
}
