sampler s0;
texture normal;
float2 light;
float2 positionTex;
float4 lightColor;
float rotation;
float2 origin;
float2 size;

sampler NormalMap = sampler_state
{
    Texture = <normal>;
};

float2x2 RotationMatrix(float rotation)
{
    float c = cos(rotation);
    float s = sin(rotation);
 
    return float2x2(c, -s, s, c);
}

float4 PixelShaderFunction(float2 coords : TEXCOORD0) : COLOR0
{
    float4 color = tex2D(s0, coords);
    float4 normalColor = tex2D(NormalMap, coords);

    float2 pixelPos = float2((coords.x - (origin.x / size.x)) * size.x + positionTex.x - origin.x, (coords.y - (origin.y / size.y)) * size.y + positionTex.y - origin.y);
    pixelPos.xy = mul(pixelPos.xy, RotationMatrix(-rotation));

    float3 normalized = (normalColor.rgb - float3(0.5, 0.5, 0)) * float3(1, -1, 1);
    normalized.rg = mul(normalized.rg, RotationMatrix(-rotation));

    float dist = 1 / (distance(light, positionTex + pixelPos) + 128);
    float DistanceLoss = (distance(light, positionTex + pixelPos) + 128) / 128;

    float output = dot(float3(light.x - (positionTex.x + pixelPos.x), light.y - (positionTex.y + pixelPos.y), 128), normalized);

    float3 calculated = float3((color.r * output) * dist, (color.g * output) * dist, (color.b * output) * dist);

    return float4(calculated.r / DistanceLoss, calculated.g / DistanceLoss, calculated.b / DistanceLoss, normalColor.a) * lightColor;
}

technique Technique1
{
    pass Pass1
    {
        PixelShader = compile ps_2_0 PixelShaderFunction();
    }
}