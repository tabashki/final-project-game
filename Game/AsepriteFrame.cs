using System;
using System.Drawing;
using Newtonsoft.Json;
using Microsoft.Xna.Framework;
using System.Collections.Generic;


namespace TeamCherry.Project;

public class AsepriteFrame
{
    [JsonProperty("filename")]
    public string Filename { get; set; }

    [JsonProperty("frame")]
    public Microsoft.Xna.Framework.Rectangle Frame { get; set; }

    [JsonProperty("duration")]
    public int Duration { get; set; }
}

public class AsepriteFrameTag
{
    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("from")]
    public int From { get; set; }

    [JsonProperty("to")]
    public int To { get; set; }

    [JsonProperty("direction")]
    public string Direction { get; set; }
}

public class AsepriteMeta
{
    [JsonProperty("image")]
    public string Image { get; set; }

    [JsonProperty("size")]
    public Size Size { get; set; }

    [JsonProperty("frameTags")]
    public List<AsepriteFrameTag> FrameTags { get; set; }
}

public class AsepriteData
{
    [JsonProperty("frames")]
    public List<AsepriteFrame> Frames { get; set; }

    [JsonProperty("meta")]
    public AsepriteMeta Meta { get; set; }
}

