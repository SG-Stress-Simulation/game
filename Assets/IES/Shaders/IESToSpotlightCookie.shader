Shader "Hidden/IES/IESToSpotlightCookie"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		// The height of the spot above the surface is defined by the field of view of the spot.
		_SpotHeight("Spot height", Float) = 0
	}
	SubShader
	{
		// No culling or depth
		Cull Off ZWrite Off ZTest Always

		Pass
		{
			CGPROGRAM
			#pragma multi_compile FULL_HORIZONTAL HALF_HORIZONTAL QUAD_HORIZONTAL
			#pragma multi_compile TOP_VERTICAL
			#pragma multi_compile VIGNETTE
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"

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
				o.vertex = UnityObjectToClipPos(v.vertex); //mul(UNITY_MATRIX_MVP,*)
				o.uv = v.uv;
				return o;
			}
			
			sampler2D _MainTex;
			float _SpotHeight;

			fixed4 frag (v2f i) : SV_Target
			{
				// The uv coordinates are being used as coordinates on the plane on which the half sphere is being projected onto.
				// The origin of that plane is at 0.5, 0.5.
				float2 uvFromCenter = i.uv - float2(0.5, 0.5);
#ifdef FULL_HORIZONTAL
				float u = atan2(uvFromCenter.y, uvFromCenter.x);
				// Normalize.
				u = (u + 3.14159) / (2 * 3.14159);
#elif HALF_HORIZONTAL
				float u = atan2(abs(uvFromCenter.y), uvFromCenter.x);
				// Normalize.
				u /= 3.14159;
#elif QUAD_HORIZONTAL
				float u = atan2(abs(uvFromCenter.y), abs(uvFromCenter.x));
				// Normalize.
				u /= 3.14159 / 2;
#else
				// If no horizontal angles are specified, there is just a single vertical slice lathed around the entire sphere.
				float u = 0;
#endif

				float uvFromCenterLength = length(uvFromCenter);
				// The v coordinate is defined by the angle from the spot light towards the surface.
				float v = atan2(_SpotHeight, uvFromCenterLength);
				// Normalize.
				v /= 3.14159 / 2;
				
				// If the values are on the upper half of the sphere, 1 - x the vertical coordinate.
#ifdef TOP_VERTICAL
				v = 1 - v;
#endif

				fixed4 candela = tex2D(_MainTex, float2(u,v));
				// Forcefully fade edges to black, since a certain threshold is allowed to give more texture space to the most lit areas.
#if VIGNETTE
				candela = lerp(candela, 0, pow(uvFromCenterLength / 0.5, 4));
#endif
				candela.a = candela.r; // Only the alpha value matters for cookies.
				return candela;
			}
			ENDCG
		}
	}
}
