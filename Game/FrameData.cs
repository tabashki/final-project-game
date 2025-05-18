using Newtonsoft.Json;
using Microsoft.Xna.Framework;

namespace TeamCherry.Project;

public class FrameRect
{
    public int x { get; set; }
    public int y { get; set; }
    public int w { get; set; }
    public int h { get; set; }

    public Rectangle ToRectangle() => new Rectangle(x, y, w, h);
}

public class FrameData
{
    public string filename { get; set; }
    public FrameRect frame { get; set; }
    public bool rotated { get; set; }
    public bool trimmed { get; set; }
    public FrameRect spriteSourceSize { get; set; }
    public Size sourceSize { get; set; }
}

public class AsepriteSheet
{
    public List<FrameData> frames { get; set; }

    public static Rectangle[] LoadSourceRects(string json)
    {
        var sheet = JsonConvert.DeserializeObject<AsepriteSheet>(json);
        return sheet.frames.Select(f => f.frame.ToRectangle()).ToArray();
    }
}

public class Size
{
    public int w { get; set; }
    public int h { get; set; }
}




