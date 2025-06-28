using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;
using System.Data;

namespace TeamCherry.Project;

class GameMain : Game, IRenderableObjectsProvider
{
    private GameContentManager gcm;
    private GraphicsDeviceManager gdm;
    private InputManager input;
    private Renderer renderer;
    private TileMap tilemap;
    private Player player;
    private SpriteFont font;
    private List<TileMap> Maps = new();
    private List<Entity> Entites = new();
    private Camera camera;

    public IReadOnlyList<IBatchedRenderable> RenderableObjects => CreateRenderList();
    public Matrix RenderTransform => camera.GetTransformMatrix();

    private IReadOnlyList<IBatchedRenderable> CreateRenderList()
    {
        List<IBatchedRenderable> lst = new();
        lst.AddRange(Maps);
        lst.AddRange(Entites);
        return lst.AsReadOnly();
    }
    
    private GameMain() : base()
    {
        gcm = new GameContentManager(Services);
        gcm.RegisterLoader(new JsonAssetLoader<Player>(true));
        gcm.RegisterLoader(new TiledAssetLoader());

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
    renderer.RenderableProvider = this;

        tilemap = Content.Load<TileMap>("Maps/TestingMultipleTilesets");
        DebugDraw.Text($"Loaded tilemap: {tilemap.PrintShort()}", Color.Aqua, 4f);
        Maps.Add(tilemap);
        
        player = Content.Load<Player>("Entities/Player");
        Texture2D playerTexture = Content.Load<Texture2D>("Sprites/Knight");
        //Texture2D spriteSheet = Content.Load<Texture2D>("Sprites/Knight-Walk_00-sheet");  -- use for a single spread shit
        string json = File.ReadAllText("Content/Json/spritesheet.json");
        Rectangle[] sourceRects = AsepriteSheet.LoadSourceRects(json);
        SpriteSheet playerSheet = new SpriteSheet(playerTexture, sourceRects);

    // === Load attack animation (14 columns × 1 rows) === 
    Texture2D attackSpriteSheet = Content.Load<Texture2D>("Sprites/KnightAttack");
    AnimatedSprite attackAnimation = new AnimatedSprite(attackSpriteSheet, cols: 14, rows: 1, fps: 28, loop: false);

        // Initialize player with idle + attack animation
        player = new Player(playerTexture, playerSheet, attackFrames);
        player.Texture = attackFrames[0];
        Entites.Add(player);

        camera = new Camera(renderer.ViewportDimensions);
        camera.Follow(player);
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
