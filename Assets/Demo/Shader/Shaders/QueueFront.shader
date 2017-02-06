Shader "Custom/QueueFront"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
	}
	SubShader
	{

		Tags { 
			"Queue" = "Transparent+1"
			"RenderType"="Transparent" 
		}
		LOD 100
		ZWrite On
		Cull Off
		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
				float4 color : COLOR;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float4 color : COLOR;
				float4 vertex : SV_POSITION;
			};

			sampler2D _MainTex;
			
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
				o.uv = v.uv;
				o.color = v.color;
				return o;
			}
			
			fixed4 frag (v2f i) : COLOR
			{
				fixed4 col = tex2D(_MainTex, i.uv);
				clip(col.a < 0.8 ? -1 : 1);
				return col;
			}
			ENDCG
		}
	}
}
