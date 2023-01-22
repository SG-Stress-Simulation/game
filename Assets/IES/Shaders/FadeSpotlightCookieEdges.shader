Shader "Hidden/IES/FadeSpotlightCookieEdges"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
	}
		SubShader
	{
		// No culling or depth
		Cull Off ZWrite Off ZTest Always

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			#include "UnityCG.cginc"

			uniform float _HorizontalFadeDistance = 0.5;
			uniform float _VerticalFadeDistance = 0.5;
			uniform float _VerticalCenter = 0.5;

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
			};

			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex); // mul(UNITY_MATRIX_MVP,*)
				o.uv = v.uv;
				return o;
			}
			
			sampler2D _MainTex;

			fixed4 frag (v2f i) : SV_Target
			{
				fixed4 candela = tex2D(_MainTex, i.uv);
				
				// Forcefully fade edges to black so the cookie doesn't have a sharp cutoff.
				float2 uvFromCenter = i.uv - float2(0.5, 1 - _VerticalCenter);
				float ellipse = pow(uvFromCenter.x, 2) / pow(_HorizontalFadeDistance, 2) + pow(uvFromCenter.y, 2) / pow(_VerticalFadeDistance, 2);
				// The ellipse values go from 0 at the center to 1 on the rim - use it to fade the edges to black by raising the ellipse values to a power.
				candela = lerp(candela, 0, pow(ellipse, lerp(2,8,pow(1-candela,6))));
				return candela;
			}
			ENDCG
		}
	}
}