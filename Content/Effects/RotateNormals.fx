sampler s0;

float2 flip;

float2x2 RotationMatrix(float rot)
{
    float c = cos(rot);
    float s = sin(rot);
 
    return float2x2(c, -s, s, c);
}

float angle;

float4 PixelShaderFunction(float2 coords : TEXCOORD0) : COLOR0
{
    float4 color = tex2D(s0, coords);

    float3 normalized = (color.rgb - float3(0.5f, 0.5f, 0)) * float3(flip.x, flip.y, 1);
    normalized.rg = mul(normalized.rg, RotationMatrix(angle)) + float2(0.5f, 0.5f);

    return float4(normalized.rgb, color.a);
}

technique Technique1
{
    pass Pass1
    {
        PixelShader = compile ps_2_0 PixelShaderFunction();
    }
}