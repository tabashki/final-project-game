using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

public class AnimatedSprite
{
    private Texture2D spriteSheet;
    private int frameWidth, frameHeight;
    private int currentFrame;
    private int currentRow;

    private double timePerFrame;
    private double totalElapsed;

    private int columns, rows;
    private bool loop = true;
    private bool hasEnded = false;

    public bool HasEnded => hasEnded;

    public Texture2D SpriteSheet => spriteSheet;

    public Rectangle CurrentFrameRectangle =>
        new Rectangle(currentFrame * frameWidth, currentRow * frameHeight, frameWidth, frameHeight);

    public AnimatedSprite(Texture2D sheet, int cols, int rows, double fps = 10, bool loop = true)
    {
        spriteSheet = sheet;
        this.columns = cols;
        this.rows = rows;
        this.loop = loop;
        timePerFrame = 1.0 / fps;

        frameWidth = spriteSheet.Width / columns;
        frameHeight = spriteSheet.Height / rows;
    }

    public void SetRow(int rowIndex)
    {
        if (currentRow != rowIndex)
        {
            currentRow = rowIndex;
            currentFrame = 0;
            totalElapsed = 0;
            hasEnded = false; // for attack animations
        }
    }

    public void Update(GameTime gameTime)
    {
        if (hasEnded) return;

        totalElapsed += gameTime.ElapsedGameTime.TotalSeconds;

        if (totalElapsed >= timePerFrame)
        {
            totalElapsed -= timePerFrame;

            if (loop)
            {
                currentFrame = (currentFrame + 1) % columns;
            }
            else
            {
                currentFrame++;
                if (currentFrame >= columns)
                {
                    currentFrame = columns - 1; // stay on last frame
                    hasEnded = true;
                }
            }
        }
    }

    public void Draw(SpriteBatch spriteBatch, Vector2 position, bool flipHoriz = false)
    {
        Rectangle source = CurrentFrameRectangle;
        SpriteEffects effects = flipHoriz ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
        spriteBatch.Draw(spriteSheet, position, source, Color.White, 0f, Vector2.Zero, 1f, effects, 0f);
    }

    public void Reset()
    {
        currentFrame = 0;
        totalElapsed = 0;
        hasEnded = false;
    }
}
