Shader "Custom/Depth Cleaner"
{
	SubShader
	{
		Pass
		{
			Cull Off
			ZWrite On
			ZTest Always
			Colormask 0
		}
	}
}