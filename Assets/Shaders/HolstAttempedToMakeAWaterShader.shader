Shader "Custom/HolstAttempedToMakeAWaterShader" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_NormalMap("Normal Map", 2D) = "bump" {}
		_NormalMap2("Normal Map 2", 2D) = "bump" {}
		_FadeDistance("Fade Distance", Range(0, 200)) = 50
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
		_Metallic ("Metallic", Range(0,1)) = 0.0
		_XScrollSpeed("X Scroll Speed", Float) = 1
		_YScrollSpeed("Y Scroll Speed", Float) = 1
	}
	SubShader {
			Tags{ "Queue" = "Transparent" "RenderType" = "Transparent" }
		LOD 200
		
		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard alpha

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		sampler2D _NormalMap;
		sampler2D _NormalMap2;
		sampler2D _CameraDepthTexture;
		
		struct Input {
			float2 uv_NormalMap;
			float2 uv_NormalMap2;
			float3 worldPos;
		};

		half _Glossiness;
		half _Metallic;
		fixed4 _Color;
		half _FadeDistance;
		float _XScrollSpeed;
		float _YScrollSpeed;

		void surf (Input IN, inout SurfaceOutputStandard o) {



			// Albedo comes from a texture tinted by color
			o.Albedo = _Color;
			// Metallic and smoothness come from slider variables
			o.Metallic = _Metallic;
			o.Smoothness = _Glossiness;
			
			o.Alpha = 0.1;

			fixed2 scrollUV = IN.uv_NormalMap;
			fixed xScrollValue = _XScrollSpeed * _Time.x;
			fixed yScrollValue = _YScrollSpeed * _Time.x;
			scrollUV += fixed2(xScrollValue, yScrollValue);

			fixed2 scrollUV2 = IN.uv_NormalMap;
			scrollUV2 += fixed2(-xScrollValue*1.5, -yScrollValue/2);


			o.Normal = UnpackNormal(tex2D(_NormalMap, scrollUV) + tex2D(_NormalMap2, scrollUV2) * 2 - 1)/10;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
