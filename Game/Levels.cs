using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace TeamCherry.Project;

public static class Levels
{
    public static Level CreateLevel1(Texture2D playerTexture)
    {
        var level = new Level();
        Player player = new Player(playerTexture);
        player.SetPosition(new Vector2(100, 100));
        level.AddEntity(player);
        return level;
    }

    public static Level CreateLevel2(Texture2D playerTexture)
    {
        var level = new Level();
        Player player = new Player(playerTexture);
        player.SetPosition(new Vector2(80, 75));
        level.AddEntity(player);
        return level;
    }
    public static Level CreateLevel3(Texture2D playerTexture)
    {
        var level = new Level();
        Player player = new Player(playerTexture);
        player.SetPosition(new Vector2(40, 65));
        level.AddEntity(player);
        return level;
    }

}

