//-----------------------------------------------------------------------------
// DebugDraw.fx
//-----------------------------------------------------------------------------

#include "Macros.fxh"

BEGIN_CONSTANTS

float2 ViewportRcpDims  _vs(c0) _cb(c0);

END_CONSTANTS


void VSDebugDraw(inout float4 color    : COLOR0,
                 inout float4 position : SV_Position)
{
    float2 screenPos = position.xy * ViewportRcpDims * 2 - 1;
    screenPos.y = -screenPos.y;
    position.xy = screenPos;
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
