sampler s0;

float R,G,B,A;

float4 PixelShaderFunction(float2 coords: TEXCOORD0) : COLOR0
{
	float4 color = tex2D(s0, coords);

	return float4(color.r * R* A, color.g * G* A, color.b * B* A, color.a * A);
}

technique Technique1
{
	pass Pass1
	{
		PixelShader = compile ps_2_0 PixelShaderFunction();
	}
}

