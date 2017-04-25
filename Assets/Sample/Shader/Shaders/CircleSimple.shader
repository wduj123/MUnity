Shader "Custom/CircleSimple" {
	Properties {
		_MainTex ("Texture", 2D) = "white" {}
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

			sampler2D _MainTex;
			float _Radius;

			struct v2f {  
				float4 pos : SV_POSITION;  
				float4 texcoord : TEXCOORD0;  
				fixed4 color : COLOR;
			};  

			v2f vert(appdata_full input)  
			{  
				v2f output;  
				output.pos = mul(UNITY_MATRIX_MVP, input.vertex);  
				output.texcoord = input.texcoord;  
				output.color = input.color;
				return output;  
			}  

			fixed4 frag(v2f input) : COLOR  
			{  
				float radius = _Radius;
				if(abs(input.texcoord.x-0.5) > abs(0.5-radius) && abs(input.texcoord.y-0.5) > abs(0.5-radius))
				{
					// 剪切的为Texture 非 sprite
					clip(sqrt(pow(abs(input.texcoord.x-0.5)-(0.5-radius),2) + pow(abs(input.texcoord.y-0.5)-(0.5-radius),2)) > radius ? -1 : 1);
				}
				fixed4 col = tex2D(_MainTex,input.texcoord)*input.color;
				return col;  
            }  
			ENDCG
		}  
	}
	FallBack "Diffuse"
}

