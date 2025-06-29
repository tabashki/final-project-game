using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;
using System.Collections.Generic;

namespace TeamCherry.Project;

class GameMain : Game
{
    private GameContentManager gcm;
    private GraphicsDeviceManager gdm;
    private InputManager input;
    private Renderer renderer;
    private SpriteFont font;
    private Level level;
    private Camera camera;

    private GameMain() : base()
    {
        gcm = new GameContentManager(Services);
        gcm.RegisterLoader(new JsonAssetLoader<Level>());
        Content = gcm;

        gdm = new GraphicsDeviceManager(this);
        gdm.PreferredBackBufferWidth = 1280;
        gdm.PreferredBackBufferHeight = 720;
        gdm.SynchronizeWithVerticalRetrace = true;

        input = new InputManager();
    }

    protected override void LoadContent()
    {
        base.LoadContent();
        DebugDraw.Init(Content, GraphicsDevice);
        DebugDraw.Text($"use tilde (~) to toggle debug draw", Color.LightBlue, float.PositiveInfinity);

        font = Content.Load<SpriteFont>("Fonts/Dogica");
        renderer = new Renderer(GraphicsDevice, font);

        LoadLevel(1);
    }

    private void LoadLevel(int levelNumber)
    {
        string levelPath = $"Levels/Level{levelNumber}";
        level = Content.Load<Level>(levelPath);
        level.LoadContent(Content);

        if (level.Player != null)
        {
            Debug.WriteLine($"Player loaded at position: {level.Player.Position}");
        }
        else
        {
            Debug.WriteLine("Player not found in level!");
        }

        camera = level.Camera ?? new Camera(renderer.ViewportDimensions);
        renderer.RenderableProvider = level;
    }



    protected override void UnloadContent()
    {
        renderer.Dispose();
        base.UnloadContent();
    }

    [Conditional("DEBUG")]
    protected void DebugUpdate(GameTime gameTime)
    {
        if (input.KeyJustPressed(Keys.OemTilde))
        {
            DebugDraw.Enabled = !DebugDraw.Enabled;
        }

        level.DebugUpdate(gameTime);
    }

    protected override void Update(GameTime gameTime)
    {
        input.Update();

        if (input.KeyJustPressed(Keys.D1))
        {
            LoadLevel(1);
        }
        else if (input.KeyJustPressed(Keys.D2))
        {
            LoadLevel(2);
        }
        else if (input.KeyJustPressed(Keys.D3))
        {
            LoadLevel(3);
        }

        if (level.Player != null)
        {
            level.Player.SetMoveInput(input.MoveInput);
        }

        level.Update(gameTime);
        camera.Update(gameTime);

        DebugUpdate(gameTime);
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        renderer.Draw(gameTime);
        base.Draw(gameTime);
    }

    public static void Main()
    {
        using (var game = new GameMain())
        {
            game.Run();
        }
    }
}
