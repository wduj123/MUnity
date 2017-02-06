Shader "Custom/MyCircle" {
	Properties {
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_Radius ("Radius", Range(0,0.5)) = 0.5
	}

	SubShader {
		Tags
		{
			"Queue" = "Transparent"
		}

		Cull Off

		Pass{
			CGPROGRAM  
			#pragma vertex vert  
			#pragma fragment frag  
			#include "UnityCG.cginc"  
			struct vertexOutput {  
				float4 pos : SV_POSITION;  
				float4 col : TEXCOORD0;  
				fixed4 color : COLOR;
			};  

			sampler2D _MainTex;
			float _Radius;

			vertexOutput vert(appdata_full input)  
			{  
				vertexOutput output;  
				output.pos = mul(UNITY_MATRIX_MVP, input.vertex);  
				output.col = input.texcoord;  
				output.color = input.color;
				return output;  
			}  
			fixed4 frag(vertexOutput input) : COLOR  
			{  
				float radius = _Radius;
				if(abs(input.col.x-0.5) > abs(0.5-radius) && abs(input.col.y-0.5) > abs(0.5-radius))
				{
					clip(sqrt(pow(abs(input.col.x-0.5)-(0.5-radius),2) + pow(abs(input.col.y-0.5)-(0.5-radius),2)) > radius ? -1 : 1);
				}
				fixed4 col = tex2D(_MainTex,input.col)*input.color;
				return col;  
            }  
			ENDCG
		}  
	}
	FallBack "Diffuse"
}

