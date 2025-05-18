using Newtonsoft.Json;
using Microsoft.Xna.Framework;

namespace TeamCherry.Project;

    public class FrameData
    {
        public Frame frame { get; set; }
    }

    public class Frame
    {
        public int x { get; set; }
        public int y { get; set; }
        public int w { get; set; }
        public int h { get; set; }
    }

    public class AsepriteSheet
    {
        public List<FrameData> frames { get; set; }

        public static Rectangle[] LoadSourceRects(string json)
        {
            var sheet = JsonConvert.DeserializeObject<AsepriteSheet>(json);
            return sheet.frames.Select(f => new Rectangle(f.frame.x, f.frame.y, f.frame.w, f.frame.h)).ToArray();
        }
    }


