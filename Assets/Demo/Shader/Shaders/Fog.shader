Shader "Custom/Fog" { // 局部雾化
	Properties {
		_blurSizeXY("BlurSizeXY", Range(0,10)) = 2
	}
    SubShader {
        Tags { "Queue" = "Transparent" }
        GrabPass { }
        Pass {
			CGPROGRAM
			#pragma debug
			#pragma vertex vert
			#pragma fragment frag 
			#include "UnityCG.cginc"
			#pragma target 3.0
			 
			sampler2D _GrabTexture : register(s0);
			float _blurSizeXY;
			 
			struct data {
			    float4 vertex : POSITION;
			    float3 normal : NORMAL;
			};
			 
			struct v2f {
			    float4 position : POSITION;
			    float4 screenPos : TEXCOORD0;
			};
			 
			v2f vert(data i){
			    v2f o;
			    o.position = mul(UNITY_MATRIX_MVP, i.vertex);
			    o.screenPos = o.position;
			    return o;
			}
			 
			fixed4 frag( v2f input ) : COLOR
			{
				float depth= _blurSizeXY*0.00025;
				float2 screenPos = input.screenPos.xy / input.screenPos.w;
			    screenPos.x = (screenPos.x + 1) * 0.5;
			    screenPos.y = 1-(screenPos.y + 1) * 0.5;
			    fixed4 sum = fixed4(0.0h,0.0h,0.0h,0.0h);
				float circleCount = 10;
				for(int i = 0;i < circleCount;i++)
				{
					float factor = i == 0 ? 0 : 1 * ((circleCount / 2) / ((circleCount - 1) * circleCount / 2));
					float uvCount = 16;
					for(int j = 0;j < uvCount;j++)
					{
						float angle = radians(j / uvCount * 360);
						float2 pos = float2(sin(angle),cos(angle)) * depth * i;
						sum += tex2D( _GrabTexture, screenPos + pos) * factor / uvCount;
					}
				}
				return sum;
			}
			ENDCG
        }
    }
	FallBack Off
}
