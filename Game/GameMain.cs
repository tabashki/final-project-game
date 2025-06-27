using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;

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
        gcm.RegisterLoader(new JsonAssetLoader<Player>());
        Content = gcm;

        gdm = new GraphicsDeviceManager(this);
        gdm.PreferredBackBufferWidth = 1280;
        gdm.PreferredBackBufferHeight = 720;
        gdm.SynchronizeWithVerticalRetrace = true;

        input = new InputManager();
    }

    protected override void Initialize()
    {
        base.Initialize();
    }

    protected override void LoadContent()
    {
        base.LoadContent();
        DebugDraw.Init(Content, GraphicsDevice);
        DebugDraw.Text($"use tilde (~) to toggle debug draw", Color.LightBlue, float.PositiveInfinity);

        font = Content.Load<SpriteFont>("Fonts/Dogica");
        renderer = new Renderer(GraphicsDevice, font);

        camera = new Camera(renderer.ViewportDimensions);

        SwitchLevel(1);
    }

    private void SwitchLevel(int levelNumber)
    {
        var playerTexture = Content.Load<Texture2D>("Sprites/guy");

        level = levelNumber switch
        {
            1 => Levels.CreateLevel1(playerTexture),
            2 => Levels.CreateLevel2(playerTexture),
            3 => Levels.CreateLevel3(playerTexture),
            _ => Levels.CreateLevel1(playerTexture),
        };

        renderer.RenderableProvider = level;

        var player = FindPlayerInLevel();
        if (player != null)
            camera.Follow(player);
    }

    private Player? FindPlayerInLevel()
    {
        foreach (var entity in level.GetRenderableObjects())
        {
            if (entity is Player p)
                return p;
        }
        return null;
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

        float deltaTimeMs = gameTime.DeltaTime() * 1e3f;
        float fps = 1e3f / deltaTimeMs;
        DebugDraw.Text($"dt: {deltaTimeMs:f3} ms (fps: {fps:f0})", Color.White);
        var player = FindPlayerInLevel();
        if (player != null)
            DebugDraw.Text($"position: {player.Position.X:f1}, {player.Position.Y:f1}", Color.White);
    }

    protected override void Update(GameTime gameTime)
    {
        input.Update();

        if (input.KeyJustPressed(Keys.D1))
            SwitchLevel(1);
        else if (input.KeyJustPressed(Keys.D2))
            SwitchLevel(2);
        else if (input.KeyJustPressed(Keys.D3))
            SwitchLevel(3);

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
        using var game = new GameMain();
        game.Run();
    }
}
