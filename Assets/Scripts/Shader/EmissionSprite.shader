Shader "Custom/EmissionSprite"
{
    Properties{
  _Cutoff("Cutoff", Range(0,1)) = 0.5
  _Color("Color", Color) = (1,1,1,1)
  _MainTex("Albedo (RGB)", 2D) = "white" {}
  _Glossiness("Smoothness", Range(0,1)) = 0.5
  _Metallic("Metallic", Range(0,1)) = 0.0
  _EmissionColor("Emission Color", Color) = (1.0, 1.0, 1.0, 1.0)
  _EmissionTex("Emission Texture", 2D) = "white" {}
    }
        SubShader{
         Tags { "Queue" = "AlphaTest" "RenderType" = "TransparentCutout" }
         LOD 200

         Cull Off

         CGPROGRAM
         #pragma surface surf Standard alphatest:_Cutoff addshadow fullforwardshadows
         #pragma target 3.0

         sampler2D _MainTex;
         sampler2D _EmissionTex;

         struct Input {
          float2 uv_MainTex;
         };

         float e;

         float _EMissionOOFN;
         half _Glossiness;
         half _Metallic;
         half4 _EmissionColor;
         fixed4 _Color;

         int x;
         float random()
         {
             return (48271 * x) % 2147483647;
         }

         void surf(Input IN, inout SurfaceOutputStandard o) {
             fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;

             e = 2;

             o.Albedo = c.rgb;
             o.Metallic = _Metallic;
             o.Smoothness = _Glossiness;
             o.Alpha = c.a;
             o.Emission = _EmissionColor * e;
         }
         ENDCG

             // ここから追加
             Cull Front

             CGPROGRAM

             #pragma surface surf Standard alphatest:_Cutoff fullforwardshadows vertex:vert
             #pragma target 3.0

             sampler2D _MainTex;
             sampler2D _EmissionTex;

             struct Input {
              float2 uv_MainTex;
             };

             // 法線を反転させて裏面の影の描写がきちんと行われるようにする
             void vert(inout appdata_full v) {
              v.normal.xyz = v.normal * -1;
             }

             float e;
             half _Glossiness;
             half _Metallic;
             half4 _EmissionColor;
             fixed4 _Color;

             void surf(Input IN, inout SurfaceOutputStandard o) {
              fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;

              e = 2;

              o.Albedo = c.rgb;
              o.Metallic = _Metallic;
              o.Smoothness = _Glossiness;
              o.Alpha = c.a;
              o.Emission = _EmissionColor * e;
             }
             ENDCG
  }
      FallBack "Diffuse"
}

