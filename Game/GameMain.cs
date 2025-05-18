using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;

namespace TeamCherry.Project;

class GameMain : Game, IRenderableObjectsProvider
{
    private GraphicsDeviceManager gdm;
    private InputManager input;
    private Renderer renderer;
    private Player player;
    private SpriteFont font;
    private List<Entity> Entites = new();
    private Camera camera;

    public IReadOnlyList<IRenderable> RenderableObjects => Entites.AsReadOnly();
    public Matrix RenderTransform => Matrix.Identity;

    private GameMain()
    {
        gdm = new GraphicsDeviceManager(this);
        gdm.PreferredBackBufferWidth = 1280;
        gdm.PreferredBackBufferHeight = 720;
        gdm.SynchronizeWithVerticalRetrace = true;

        input = new InputManager();

        Content.RootDirectory = "Content";
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
        renderer.RenderableProvider = this;

        player = new Player(Content.Load<Texture2D>("Sprites/Guy"));
        Entites.Add(player);

        camera = new Camera(renderer.RenderTarget.Width, renderer.RenderTarget.Height);
        camera.Zoom = 0.25f; // camera size
        renderer.Camera = camera;
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
            // Toggle debug drawing with tilde (~) key
            DebugDraw.Enabled = !DebugDraw.Enabled;
        }

        float deltaTimeMs = gameTime.DeltaTime() * 1e3f;
        float fps = 1e3f / deltaTimeMs;
        DebugDraw.Text($"dt: {deltaTimeMs:f3} ms (fps: {fps:f0})", Color.White);
        DebugDraw.Text($"position: {player.Position.X:f1}, {player.Position.Y:f1}", Color.White);
    }

    protected override void Update(GameTime gameTime)
    {
        input.Update();

        player.SetMoveInput(input.MoveInput);
        player.Update(gameTime);

        // Make camera follow the player's center
        var playerCenter = player.Position + new Vector2(player.Texture.Width / 2f, player.Texture.Height / 2f);
        camera.Follow(playerCenter);

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