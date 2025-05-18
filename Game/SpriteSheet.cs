using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Diagnostics;

public class SpriteSheet
{
    private readonly Texture2D texture;
    private readonly int cellWidth;
    private readonly int cellHeight;
    private readonly Rectangle[] sourceRectangles;

    public SpriteSheet(Texture2D texture, int cellWidth, int cellHeight)
    {
        this.texture = texture ?? throw new ArgumentNullException(nameof(texture));
        this.cellWidth = cellWidth;
        this.cellHeight = cellHeight;

        if (cellWidth <= 0 || cellHeight <= 0)
            throw new ArgumentException("cellWidth and cellHeight must be positive and non-zero.");

        int texWidth = texture.Width;
        int texHeight = texture.Height;

        Debug.WriteLine($"[SpriteSheet] Texture: {texWidth}x{texHeight}, Cell: {cellWidth}x{cellHeight}");

        int columns = texWidth / cellWidth;
        int rows = texHeight / cellHeight;

        if (columns == 0 || rows == 0)
        {
            Debug.WriteLine("SpriteSheet: Texture size smaller than cell size or zero-sized cells.");
        }

        int totalFrames = columns * rows;
        sourceRectangles = new Rectangle[Math.Max(totalFrames, 1)];

        if (totalFrames == 0)
        {
            // Fallback frame: full texture
            sourceRectangles[0] = new Rectangle(0, 0, texWidth, texHeight);
        }
        else
        {
            for (int y = 0; y < rows; y++)
            {
                for (int x = 0; x < columns; x++)
                {
                    int index = y * columns + x;
                    sourceRectangles[index] = new Rectangle(x * cellWidth, y * cellHeight, cellWidth, cellHeight);
                }
            }
        }

        Debug.WriteLine($"SpriteSheet initialized: {sourceRectangles.Length} frame(s), {columns} col x {rows} row");
    }

    public int FrameCount => sourceRectangles.Length;

    public Rectangle GetFrame(int index)
    {
        if (index < 0 || index >= sourceRectangles.Length)
            throw new ArgumentOutOfRangeException(nameof(index), "Frame index out of range.");
        return sourceRectangles[index];
    }
}
