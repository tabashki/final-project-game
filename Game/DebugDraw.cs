
using System.Diagnostics;
using System.Reflection;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace TeamCherry.Project;

public static class DebugDraw
{
    const int MAX_VERTS = 2048;
    const int MAX_LINES = 128;
    private static int vertexCount = 0;
    private static int textLineCount = 0;
    private static int availVerts => MAX_VERTS - vertexCount;
    private static readonly float RcpSqrt2 = 1 / MathF.Sqrt(2);
    private static Effect? debugDrawEffect = null;
    private static EffectPass? effectPass = null;
    private static SpriteBatch? fontBatch = null;
    private static SpriteFont? debugDrawFont = null;
    private const float extraLineSpacing = 3f;
    private const float fontScale = 2f;

    private static VertexPositionColor[] vertices = new VertexPositionColor[MAX_VERTS];
    private class TextLineEntry
    {
        public string Text = string.Empty;
        public Color Color = Color.White;
        public float LifeTime = 0;
    }
    private static List<TextLineEntry> textLines = new();

    public static bool Enabled { get; set; } = true;

    [Conditional("DEBUG")]
    public static void Line(Vector2 start, Vector2 end, Color color)
    {
        if (availVerts < 2)
        {
            return;
        }
        vertices[vertexCount++] = new VertexPositionColor(new Vector3(start, 0), color);
        vertices[vertexCount++] = new VertexPositionColor(new Vector3(end, 0), color);
    }

    [Conditional("DEBUG")]
    public static void Arrow(Vector2 start, Vector2 end, float headSize, Color color)
    {
        if (availVerts < 8)
        {
            return;
        }

        Vector2 n = (end - start).NormalizedSafe();

        vertices[vertexCount++] = new VertexPositionColor(new Vector3(start, 0), color);
        vertices[vertexCount++] = new VertexPositionColor(new Vector3(end, 0), color);

        Vector2 right = new Vector2(
            -RcpSqrt2 * n.X - RcpSqrt2 * n.Y,
             RcpSqrt2 * n.X - RcpSqrt2 * n.Y
        ) * headSize + end;
        vertices[vertexCount++] = new VertexPositionColor(new Vector3(end, 0), color);
        vertices[vertexCount++] = new VertexPositionColor(new Vector3(right, 0), color);

        Vector2 left = new Vector2(
            -RcpSqrt2 * n.X + RcpSqrt2 * n.Y,
            -RcpSqrt2 * n.X - RcpSqrt2 * n.Y
        ) * headSize + end;
        vertices[vertexCount++] = new VertexPositionColor(new Vector3(end, 0), color);
        vertices[vertexCount++] = new VertexPositionColor(new Vector3(left, 0), color);

        vertices[vertexCount++] = new VertexPositionColor(new Vector3(left, 0), color);
        vertices[vertexCount++] = new VertexPositionColor(new Vector3(right, 0), color);
    }

    [Conditional("DEBUG")]
    public static void Rect(Rectangle rect, Color color)
    {
        if (availVerts < 8)
        {
            return;
        }

        Vector2[] verts = [
            new Vector2(rect.X,              rect.Y),
            new Vector2(rect.X + rect.Width, rect.Y),
            new Vector2(rect.X + rect.Width, rect.Y + rect.Height),
            new Vector2(rect.X,              rect.Y + rect.Height),
        ];

        Line(verts[0], verts[1], color);
        Line(verts[1], verts[2], color);
        Line(verts[2], verts[3], color);
        Line(verts[3], verts[0], color);
    }

    [Conditional("DEBUG")]
    public static void Text(string text, Color color, float lifeTime = 0)
    {
        var lines = text.Split('\n');
        var remainingLines = MAX_LINES - textLineCount;

        if (lines.Length <= remainingLines)
        {
            foreach (var l in lines)
            {
                textLines.Add(new TextLineEntry{Text = l, Color = color, LifeTime = lifeTime});
                textLineCount++;
            }
        }
    }

    [Conditional("DEBUG")]
    public static void DrawToDevice(GraphicsDevice device, float viewportScale, float deltaTime)
    {
        if (Enabled)
        {
            if (vertexCount > 0 && debugDrawEffect != null && effectPass != null)
            {
                var vb = new DynamicVertexBuffer(device, typeof(VertexPositionColor), vertexCount, BufferUsage.WriteOnly);
                vb.SetData(vertices, 0, vertexCount);

                var viewportDims = new Vector2(
                    viewportScale / device.Viewport.Width, viewportScale / device.Viewport.Height
                );
                debugDrawEffect.Parameters["ViewportRcpDims"].SetValue(viewportDims);

                device.BlendState = BlendState.AlphaBlend;
                device.SamplerStates[0] = SamplerState.LinearWrap;
                device.DepthStencilState = DepthStencilState.None;
                device.RasterizerState = RasterizerState.CullNone;
                device.Indices = null;
                effectPass.Apply();
                device.SetVertexBuffer(vb);
                device.DrawPrimitives(PrimitiveType.LineList, 0, vertexCount / 2);

                vb.Dispose();
            }
            if (textLineCount > 0 && fontBatch != null && debugDrawFont != null)
            {
                float lineHeight = (debugDrawFont.LineSpacing + extraLineSpacing) * fontScale;
                Vector2 pos = Vector2.One;

                fontBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend,
                    SamplerState.PointClamp, DepthStencilState.None, RasterizerState.CullNone);
                foreach (var entry in textLines)
                {
                    fontBatch.DrawString(debugDrawFont, entry.Text, pos, entry.Color,
                        0, Vector2.Zero, fontScale, SpriteEffects.None, 0);
                    pos.Y += lineHeight;
                }
                fontBatch.End();
            }
        }

        // Update line lifetime logic regardless of enablement
        for (int i = 0; i < textLines.Count;)
        {
            TextLineEntry entry = textLines.ElementAt(i);
            entry.LifeTime -= deltaTime;
            if (entry.LifeTime <= 0)
            {
                textLines.RemoveAt(i);
                textLineCount--;
            }
            else
            {
                i++;
            }
        }

        vertexCount = 0;
    }

    public static void Init(ContentManager content, GraphicsDevice device)
    {
        debugDrawEffect = content.Load<Effect>("Effects/DebugDraw");
        debugDrawFont = content.Load<SpriteFont>("Fonts/Dogica");
        effectPass = debugDrawEffect.CurrentTechnique.Passes[0];
        fontBatch = new SpriteBatch(device);
    }
}