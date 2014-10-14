Shader "Custom/Alpha Blended Unlit" 
{
	Properties 
	{
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_Color ("Color (RGB)", Color) = (1, 1, 1, 1)
		_Alpha ("Alpha", Range(0, 1)) = 1
	}
	SubShader {
		Tags { "RenderType"="Transparent" }
		LOD 200
		Blend SrcAlpha OneMinusSrcAlpha
		
		CGPROGRAM
		#pragma surface surf CustomUnlit

		sampler2D _MainTex;
		float4 _Color;
		float _Alpha;

		struct Input 
		{
			float2 uv_MainTex;
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
			o.Albedo = c.rgb * _Color;
			o.Alpha = c.a * _Alpha;
		}
		ENDCG
	} 
	FallBack "Diffuse"
}
