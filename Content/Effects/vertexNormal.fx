float4x4 World;
float4x4 View;
float4x4 Projection;

struct VertexShaderInput
{
    float4 Position : POSITION0;
    float2 TexCoord : TEXCOORD0;
    float3 tangent : NORMAL0;
};
 
struct VertexShaderOutput
{
    float4 Position : POSITION0;
    float3 tangent : TEXCOORD1;
    float2 TexCoord : TEXCOORD0;
};

texture NormalMap;
sampler2D textureSampler = sampler_state
{
    Texture = (NormalMap);
    MinFilter = Linear;
    MagFilter = Linear;
    AddressU = Clamp;
    AddressV = Clamp;
};

float2x2 RotationMatrix(float rot)
{
    float c = cos(rot);
    float s = sin(rot);
 
    return float2x2(c, -s, s, c);
}
 
VertexShaderOutput VertexShaderFunction(VertexShaderInput input)
{
    VertexShaderOutput output;
 
    float4 worldPosition = mul(input.Position, World);
    float4 viewPosition = mul(worldPosition, View);
    output.Position = mul(viewPosition, Projection);

    output.tangent = normalize(input.tangent);

    output.TexCoord = input.TexCoord;

    return output;
}
 
float4 PixelShaderFunction(VertexShaderOutput input) : COLOR0
{
    float4 normalMap = tex2D(textureSampler, input.TexCoord);
    float direction = atan2(input.tangent.y, input.tangent.x);

    float2 normalized = mul((normalMap.gr - float2(0.5, 0.5)) * float3(-1, -1, 1), RotationMatrix(direction));

    return float4(normalized.r + 0.5, normalized.g + 0.5, normalMap.b, normalMap.a);
}
 
technique Textured
{
    pass Pass1
    {
        VertexShader = compile vs_1_1 VertexShaderFunction();
        PixelShader = compile ps_2_0 PixelShaderFunction();
    }
}