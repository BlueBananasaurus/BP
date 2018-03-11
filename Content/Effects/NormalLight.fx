sampler s0;
texture normal;
float2 light;
float2 positionTex;
float4 lightColor;
float2 size;

sampler NormalMap = sampler_state
{
    Texture = <normal>;
};

float4 PixelShaderFunction(float2 coords : TEXCOORD0) : COLOR0
{
    float4 color = tex2D(s0, coords);
    float4 normalColor = tex2D(NormalMap, coords);
    float2 pixelPos = float2((coords.x) * size.x + positionTex.x, (coords.y) * size.y + positionTex.y);
    float3 normalized = (normalColor.rgb - float3(0.5, 0.5, 0)) * float3(1, -1, 1);
    float dist = 1 / (distance(light, pixelPos) + 32);
    float DistanceLoss = (distance(light, pixelPos) + 128) / 128;
    float output = dot(float3(light.x - pixelPos.x, light.y - pixelPos.y, 128), normalized);
    float3 calculated = float3((color.r * output) * dist, (color.g * output) * dist, (color.b * output) * dist);
    float3 temp = float3(calculated.r / DistanceLoss, calculated.g / DistanceLoss, calculated.b / DistanceLoss) * lightColor.rgb;

    return float4(temp.rgb, normalColor.a);
}

technique Technique1
{
    pass Pass1
    {
        PixelShader = compile ps_2_0 PixelShaderFunction();
    }
}