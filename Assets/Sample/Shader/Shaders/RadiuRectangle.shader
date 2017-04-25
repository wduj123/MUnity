// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'
Shader "Custom/RadiuRectangle" {
	Properties {
		_MainTex ("Texture", 2D) = "white" {}
		_Radius ("Radius", Range(0,0.5)) = 0.5
	}

	SubShader {
		Tags
		{
			"Queue" = "Transparent"
			"RenderType" = "Transparent"
		}

		Cull Off // 背面剔除

		Pass{// vertex Shader
			CGPROGRAM  
			#pragma vertex vert  
			#pragma fragment frag  
			#include "UnityCG.cginc"  

			sampler2D _MainTex;
			float _Radius;

			struct v2f {  
				float4 pos : SV_POSITION;  
				float4 texcoord : TEXCOORD0;  
				float4 texcoord1 : TEXCOORD1;
				fixed4 color : COLOR;
			};  

			v2f vert(appdata_full input)  
			{  
				v2f output;  
				output.pos = mul(UNITY_MATRIX_MVP, input.vertex);  
				output.texcoord1 = mul(unity_ObjectToWorld, input.vertex); 
				output.texcoord = input.texcoord;  
				output.color = input.color;
				return output;  
			}  

			// input.pos 为屏幕位置screenPos
			fixed4 frag(v2f input) : COLOR  
			{  
				float radius = _Radius;
				if(abs(input.texcoord.x-0.5) > abs(0.5-radius) && abs(input.texcoord.y-0.5) > abs(0.5-radius))
				{
					clip(sqrt(pow(abs(input.texcoord.x-0.5)-(0.5-radius),2) + pow(abs(input.texcoord.y-0.5)-(0.5-radius),2)) > radius ? -1 : 1);
				}
				clip(input.pos.x < 100 && input.pos.y < 100 ? -1 : 1); // 100为屏幕坐标
				if(input.texcoord1.x > 100)
				{
					//input.color = fixed4(0,0,0,0);
					clip(-1);
				}
				else if(input.texcoord1.x > 100 - 10)
				{
					
				}
				else
				{

				}
				//if(input.pos.x > 200)
				//{
				//	input.color = fixed4(0,0,0,1);
				//}
				fixed4 col = tex2D(_MainTex,input.texcoord)*input.color;
				col.a = 0.5f;
				return col;  
				
            }  
			ENDCG
		}  
		
	}
	FallBack "Diffuse"
}
