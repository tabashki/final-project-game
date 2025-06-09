//-----------------------------------------------------------------------------
// DebugDraw.fx
//-----------------------------------------------------------------------------

#include "Macros.fxh"

BEGIN_CONSTANTS

float4x4 ViewportTransform  _vs(c0) _cb(c0);

END_CONSTANTS


void VSDebugDraw(inout float4 color    : COLOR0,
                 inout float4 position : SV_Position)
{
    float4 pos = float4(position.xyz, 1);
    position = mul(pos, ViewportTransform);
}

float4 PSDebugDraw(float4 color : COLOR0) : SV_Target0
{
    return color;
}

technique DebugDraw
{
    pass
    {
        VertexShader = compile vs_2_0 VSDebugDraw();
        PixelShader  = compile ps_2_0 PSDebugDraw();
    }
}
