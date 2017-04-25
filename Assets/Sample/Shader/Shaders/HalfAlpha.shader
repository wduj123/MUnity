Shader "Custom/HalfAlpha" {
    Properties {
		_MainTex ("Texture", 2D) = "white" {}
		_Alpha("Alpha",Float) = 0.5
    }
    SubShader {
        Tags { 
			"RenderType" = "Transparent" 
			"Queue" = "Overlay"
		}
        Cull Off
		Blend One One
        CGPROGRAM
        #pragma surface surf Lambert
        struct Input {
            float2 uv_MainTex;
            float2 uv_BumpMap;
            float3 worldPos;
        };
        sampler2D _MainTex;
        sampler2D _BumpMap;
        void surf (Input IN, inout SurfaceOutput o) {
            //clip (frac(IN.worldPos.y) - 0.5);
			if(IN.worldPos.x > 100)
			{
				o.Albedo = tex2D (_MainTex, IN.uv_MainTex).rgb * 0.1;
			}
			else
			{
				o.Albedo = tex2D (_MainTex, IN.uv_MainTex);
			}
            
			//o.Albedo = o.Albedo * 0.5;
        }
        ENDCG
    } 
    FallBack "Diffuse"
}