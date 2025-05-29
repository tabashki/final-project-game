
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;

namespace TeamCherry.Project;

class Renderer : IDisposable
{
    private GraphicsDevice device;
    private RenderTarget2D renderTarget;
    private SpriteBatch batch;
    private SpriteFont font;

    private static readonly Color clearColor = new Color(0.4f, 0.4f, 0.4f);
    private static readonly SpriteSortMode sortMode = SpriteSortMode.Deferred;
    private static readonly BlendState blendState = BlendState.AlphaBlend;
    private static readonly SamplerState pointClamp = SamplerState.PointClamp;
    private static readonly DepthStencilState depthStencil = DepthStencilState.None;
    private static readonly RasterizerState rasterizerState = RasterizerState.CullCounterClockwise;
    private const int viewportScale = 5;

    public IRenderableObjectsProvider? RenderableProvider { get; set; } = null;
    public Vector2 ViewportDimensions => new Vector2(renderTarget.Width, renderTarget.Height);

    public Renderer(GraphicsDevice graphicsDevice, SpriteFont defaultFont)
    {
        device = graphicsDevice;
        font = defaultFont;
        renderTarget = new RenderTarget2D(device,
            device.Viewport.Width / viewportScale,
            device.Viewport.Height / viewportScale);
        batch = new SpriteBatch(device);
    }

    public void Draw(GameTime gameTime)
    {
        device.SetRenderTarget(renderTarget);
        device.Clear(clearColor);

        Matrix transform = Matrix.Identity;
        if (RenderableProvider != null)
        {
            transform = RenderableProvider.RenderTransform;

            batch.Begin(sortMode, blendState, pointClamp, depthStencil,
                rasterizerState, null, transform);
            foreach (var renderable in RenderableProvider.RenderableObjects)
            {
                batch.Draw(renderable.Texture, renderable.Position, null,
                    renderable.Color, renderable.Rotation, renderable.RotationOrigin,
                    1f, renderable.SpriteEffects, 0);
            }
            batch.End();
        }

        device.SetRenderTarget(null);

        int w = device.PresentationParameters.BackBufferWidth;
        int h = device.PresentationParameters.BackBufferHeight;
        var rect = new Rectangle(0, 0, w, h);

        // Render low-res render target onto backbuffer with chunky pixels
        batch.Begin(sortMode, blendState, pointClamp, depthStencil, rasterizerState);
        batch.Draw(renderTarget, rect, Color.White);
        batch.End();

        Matrix debugTransform = transform;
        debugTransform *= Matrix.CreateScale(new Vector3(2f / renderTarget.Width, -2f / renderTarget.Height, 1));
        debugTransform *= Matrix.CreateTranslation(new Vector3(-1, 1, 0));

        DebugDraw.DrawToDevice(device, debugTransform, gameTime.DeltaTime());
    }

    public void Dispose()
    {
        renderTarget.Dispose();
        batch.Dispose();
    }
}