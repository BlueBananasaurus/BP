sampler s0;
texture lightMask;
sampler lightSampler = sampler_state
{
    Texture = <lightMask>;
};

float4 PixelShaderFunction(float2 coords: TEXCOORD0) : COLOR0
{
    float4 color = tex2D(s0, coords);
    float4 lightColor = tex2D(lightSampler, coords);

    //float4 temp = floor(lightColor * 8) / 8;

    //return temp * color;

    return lightColor * color;
}

technique Technique1
{
	pass Pass1
	{
		PixelShader = compile ps_2_0 PixelShaderFunction();
	}
}
