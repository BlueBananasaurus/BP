sampler s0;

float2 dimensions;

texture TextureIn;
sampler TextureInSampler = sampler_state
{
    Texture = <TextureIn>;
};

texture Mask;
sampler MaskSampler = sampler_state
{
    Texture = <Mask>;
};

int kernelSize;

float4 HorizontalBlur(float2 coords: TEXCOORD0) : COLOR0
{
	float4 color = tex2D(s0, coords);
    float4 mask = tex2D(MaskSampler, coords);
	float4 temp = float4(0, 0, 0, 0);
	float divide = 0;

    [unroll(64)]
    for (int i = -kernelSize/2; i <= kernelSize/2; i++)
    {
        float4 TextureColors = tex2D(TextureInSampler, 
        float2(coords.x + (1.00 / dimensions.x) * i, coords.y));

        if ((coords.x + (1.0 / dimensions.x) * i) >=
            0.0 && (coords.x + (1.0 / dimensions.x) * i) <= 1.0)
        {
            temp += TextureColors * ((kernelSize / 2 + 1) * 2 - abs(i) * 2);
            divide += ((kernelSize/2 + 1)*2 - abs(i)*2);
        }
    }

    return float4(color + temp / divide);
}

float4 VerticalBlur(float2 coords: TEXCOORD0) : COLOR0
{
    float4 color = tex2D(s0, coords);
    float4 mask = tex2D(MaskSampler, coords);
    float4 temp = float4(0, 0, 0, 0);
    float divide = 0;

    [unroll(64)]
    for (int i = -kernelSize/2; i <= kernelSize/2; i++)
    {
        float4 TextureColors = tex2D(TextureInSampler, float2(coords.x, coords.y + (1.00 / dimensions.y) * i));

        if ((coords.y + (1.0 / dimensions.y) * i) >= 0.0 && (coords.y + (1.0 / dimensions.y) * i) <= 1.0)
        {
            temp += TextureColors * ((kernelSize / 2 + 1) * 2 - abs(i) * 2);
            divide += ((kernelSize/2 + 1)*2 - abs(i)*2);
        }
    }

    return float4(color + temp / divide) * float4(1 * mask.r, 1 * mask.r, 1 * mask.r, mask.r);
}

technique Technique1
{
	pass Pass1
	{
        PixelShader = compile ps_3_0 HorizontalBlur();
    }

	pass Pass2
	{
        PixelShader = compile ps_3_0 VerticalBlur();
    }
}


