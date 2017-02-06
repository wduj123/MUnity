Shader "Custom/Clip" {
	Properties {
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_RangeX ("RangeX",float) = 0.5
	}
	SubShader {
		Tags 
		{
			"RenderType" = "Transparent" 
			"Queue" = "Transparent"
		}
		LOD 200
		CGPROGRAM
		#pragma surface surf Standard fullforwardshadows
		#pragma target 3.0

		sampler2D _MainTex;
		float _RangeX;

		struct Input {
			float2 uv_MainTex;
			float4 screenPos;
		};

		void surf (Input IN, inout SurfaceOutputStandard o) {
			clip(IN.screenPos.x < _RangeX ? -1 : 1);
			fixed4 c = tex2D (_MainTex, IN.uv_MainTex);
			o.Albedo = c.rgb;
			o.Alpha = c.a;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
