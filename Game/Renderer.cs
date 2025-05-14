
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

    public List<IRenderable> RenderObjects;

    public Renderer(GraphicsDevice graphicsDevice, SpriteFont defaultFont)
    {
        device = graphicsDevice;
        font = defaultFont;
        renderTarget = new RenderTarget2D(device,
            device.Viewport.Width / viewportScale,
            device.Viewport.Height / viewportScale);
        batch = new SpriteBatch(device);
        RenderObjects = new List<IRenderable>();
    }

    public void Draw(GameTime gameTime)
    {
        device.SetRenderTarget(renderTarget);
        device.Clear(clearColor);

        batch.Begin(sortMode, blendState, pointClamp, depthStencil, rasterizerState);
        foreach (var renderable in RenderObjects)
        {
            batch.Draw(renderable.Texture, renderable.Position, null,
                renderable.Color,renderable.Rotation, renderable.RotationOrigin,
                1f, renderable.SpriteEffects, 0);
        }
        batch.End();

        device.SetRenderTarget(null);

        int w = device.PresentationParameters.BackBufferWidth;
        int h = device.PresentationParameters.BackBufferHeight;
        var rect = new Rectangle(0, 0, w, h);

        // Render low-res render target onto backbuffer with chunky pixels
        batch.Begin(sortMode, blendState, pointClamp, depthStencil, rasterizerState);
        batch.Draw(renderTarget, rect, Color.White);
        batch.End();

        DebugDraw.DrawToDevice(device, viewportScale, gameTime.DeltaTime());
    }

    public void Dispose()
    {
        renderTarget.Dispose();
        batch.Dispose();
    }
}