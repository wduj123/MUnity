Shader "Custom/UVAnim" {
    Properties {
        _MainTex ("Water Texture1", 2D) = "white" {}
        _MainTex2("Water Texture2", 2D) = "white" {}

        _XScrollSpeed1("X ScrollSpeed 1", float) = 0.01
        _YScrollSpeed1("Y ScrollSpeed 1", float) = 0.01

        _XScrollSpeed2("X ScrollSpeed 2", float) = 0.01
        _YScrollSpeed2("Y ScrollSpeed 2", float) = 0.01
    }
    SubShader {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        #pragma surface surf Lambert

        sampler2D _MainTex;
        sampler2D _MainTex2;

        float _XScrollSpeed1;
        float _YScrollSpeed1;
        float _XScrollSpeed2;
        float _YScrollSpeed2;

        struct Input {
            float2 uv_MainTex;
        };

        void surf (Input IN, inout SurfaceOutput o) {
            float2 uvScrolled1 = IN.uv_MainTex;
            float2 uvScrolled2 = IN.uv_MainTex;

            uvScrolled1 += float2(_XScrollSpeed1 * _Time.y, _YScrollSpeed1 * _Time.y);
            uvScrolled2 += float2(_XScrollSpeed2 * _Time.y, _YScrollSpeed2 * _Time.y);

            uvScrolled1 = frac(uvScrolled1);
            uvScrolled2 = fmod(uvScrolled2,1);

            half4 water1 = tex2D (_MainTex, uvScrolled1);
            half4 water2 = tex2D (_MainTex2, uvScrolled2);

            o.Albedo = water1.rgb * 0.5 + water2.rgb * 0.5;
            o.Alpha = 1.0;
        }
        ENDCG
    } 
    FallBack "Diffuse"
}