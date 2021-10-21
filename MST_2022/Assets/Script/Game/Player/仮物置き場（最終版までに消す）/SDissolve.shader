//==============================================================================
//	[SDissolve.shader]
//	・じわっと消えるシェーダー
//--------------------------------------------------------------------------------
//	2021.10.17 @Fujiwara Aiko
//================================================================================
//	History
//		2021.10.17 Fujiwara Aiko
//			スクリプト追加
//
//============================================================================

Shader "Custom/SDissolve"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0
		_DisolveTex("DisolveTex (RGB)", 2D) = "white" {}	// ディゾルブテクスチャ
		_Threshold("Threshold", Range(0,1)) = 0.0			// 閾値
		_RampTex("RampTex (RGB)", 2D) = "white" {}		// ランプテクスチャ
    }
    SubShader
    {
        Tags 
		{ 
			"Queue" = "Transparent"
			"RenderType"="Transparent"
			"Projector" = "True"
		}

        LOD 200

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard alpha:fade

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        sampler2D _MainTex;
		sampler2D _DisolveTex;
		sampler2D _RampTex;

        struct Input
        {
            float2 uv_MainTex;
        };

        half _Glossiness;
        half _Metallic;
		half _Threshold;
        fixed4 _Color;

        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
        UNITY_INSTANCING_BUFFER_START(Props)
            // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
			fixed4 m = tex2D(_DisolveTex, IN.uv_MainTex);
			half g = m.r * 0.2 + m.g * 0.7 + m.b * 0.1;

			// 閾値 -0.1f以下は消える
			if (g < _Threshold - 0.1f) {

				discard;
			}

			// 閾値周辺が変な感じの色が付く
			if (g < _Threshold) {
				// Albedo comes from a texture tinted by color
				fixed4 c = tex2D(_RampTex, fixed2((_Threshold - g) * 10.0f, 0.5f));
				o.Albedo = c.rgb;
				// Metallic and smoothness come from slider variables
				o.Metallic = _Metallic;
				o.Smoothness = _Glossiness;
				o.Alpha = c.a;
				return;
			}

            // Albedo comes from a texture tinted by color
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
            o.Albedo = c.rgb;
            // Metallic and smoothness come from slider variables
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
            o.Alpha = c.a;
        }
        ENDCG
    }
    FallBack "Standard"		// 注：影を徐々に消したい場合は、まずStandardシェーダのFadeを選択してから、このカスタムシェーダに切り替える
}
