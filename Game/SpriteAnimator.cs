using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class SpriteAnimator
{
    private Texture2D spriteSheet;
    private int frameWidth;
    private int frameHeight;
    private int rows;
    private int columns;

    private int currentFrame;
    private int startFrame;
    private int endFrame;

    private double timePerFrame;
    private double totalElapsed;

    public bool IsLooping { get; set; } = true;
    public bool IsPlaying { get; set; } = true;

    public SpriteAnimator(Texture2D sheet, int frameWidth, int frameHeight, int rows, int columns, double frameDuration = 0.1)
    {
        spriteSheet = sheet;
        this.frameWidth = frameWidth;
        this.frameHeight = frameHeight;
        this.rows = rows;
        this.columns = columns;
        this.timePerFrame = frameDuration;

        SetAnimation(0, columns * rows - 1);
    }

    public void SetAnimation(int startFrame, int endFrame)
    {
        this.startFrame = startFrame;
        this.endFrame = endFrame;
        currentFrame = startFrame;
        totalElapsed = 0;
    }

    public void Update(GameTime gameTime)
    {
        if (!IsPlaying) return;

        totalElapsed += gameTime.ElapsedGameTime.TotalSeconds;

        if (totalElapsed >= timePerFrame)
        {
            currentFrame++;
            if (currentFrame > endFrame)
            {
                currentFrame = IsLooping ? startFrame : endFrame;
            }
            totalElapsed = 0;
        }
    }

    public void Draw(SpriteBatch spriteBatch, Vector2 position, SpriteEffects effects = SpriteEffects.None)
    {
        int row = currentFrame / columns;
        int col = currentFrame % columns;

        Rectangle sourceRect = new Rectangle(col * frameWidth, row * frameHeight, frameWidth, frameHeight);
        spriteBatch.Draw(spriteSheet, position, sourceRect, Color.White, 0f, Vector2.Zero, 1f, effects, 0f);
    }
}
