sampler s0;

float2 Mult, Div, Time, pos;

texture Texture;
sampler TextureSampler = sampler_state
{
    Texture = <Texture>;
};

float4 PixelShaderFunction(float2 coords: TEXCOORD0) : COLOR0
{
	float2 uv = coords.xy;
    
    uv.x += cos(((uv.x + (pos.x/ 1920.0)) * Mult.x) + Time.x) / Div.x;
    uv.y += cos(((uv.y + (pos.y / 1080.0)) * Mult.y) + Time.y) / Div.y;

    float minus = 0.0;
    float4 mask = tex2D(TextureSampler, coords);
    float4 noChange = tex2D(s0, coords);
	float4 color = tex2D(s0, uv);
    color.rgb = float3(color.r - minus - 0.1, color.g - minus - 0.1, color.b - minus);

    if(mask.r > 0.5)
        return color;
    else
        return noChange;
}

technique Technique1
{
	pass Pass1
	{
		PixelShader = compile ps_3_0 PixelShaderFunction();
	}
}

