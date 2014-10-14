Shader "Custom/Additive Unlit Sky" 
{
	Properties 
	{
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_TintColor ("Color (RGB)", Color) = (1, 1, 1, 1)
		_Alpha ("Alpha", Range(0, 1)) = 1
	}
	SubShader {
		Tags { "Queue"="Geometry-100" }
		LOD 200
		Blend SrcAlpha One
		ZWrite Off
		ZTest Always
		Colormask RGB
		
		CGPROGRAM
		#pragma surface surf CustomUnlit

		sampler2D _MainTex;
		float4 _TintColor;
		float _Alpha;

		struct Input 
		{
			float2 uv_MainTex;
			float4 color : COLOR;
		};

		inline fixed4 LightingCustomUnlit(SurfaceOutput s, fixed3 lightDir, fixed atten)
		{
			fixed4 c;
        	c.rgb = s.Albedo; 
        	c.a = s.Alpha;
        	return c;
		}

		void surf (Input IN, inout SurfaceOutput o) 
		{
			half4 c = tex2D (_MainTex, IN.uv_MainTex);
			o.Albedo = c.rgb * _TintColor * IN.color.rgb;
			o.Alpha = c.a * _Alpha * IN.color.a;
		}
		ENDCG
	} 
	FallBack "Diffuse"
}
