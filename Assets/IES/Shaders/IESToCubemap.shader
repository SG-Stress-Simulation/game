Shader "IES/IESToCubemap"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
	}
	SubShader
	{
		Tags { "RenderType"="Opaque" }
		LOD 100

		Pass
		{
			CGPROGRAM
			#pragma multi_compile BOTTOM_VERTICAL TOP_VERTICAL FULL_VERTICAL
			#pragma multi_compile FULL_HORIZONTAL HALF_HORIZONTAL QUAD_HORIZONTAL
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
				float4 position : TEXCOORD1;
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;
			
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex); // mul(UNITY_MATRIX_MVP,*)
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				o.position = mul(unity_ObjectToWorld, v.vertex);
				return o;
			}

			fixed4 frag (v2f i) : SV_Target
			{
					
#ifdef FULL_HORIZONTAL
				// Calculate the degree in 0 - 2 pi range on the XZ plane.
				float atanU = (atan2(i.position.z, i.position.x) + 3.14159);
				// Normalize to the 0-1 range.
				float u = atanU / (2 * 3.14159);
#elif HALF_HORIZONTAL
				// If 180 degrees are specified, the full 0-1 u range is used for each half of the sphere.
				// Calculate the degree in 0 - pi range on the +-X+Z plane.
				float atanU = atan2(abs(i.position.z), i.position.x);
				// Normalize to the 0-1 range.
				float u = atanU / 3.14159;
#elif QUAD_HORIZONTAL
				// If 90 degrees are specified, the full 0-1 u range is used for each quadrant of the sphere.
				// Calculate the degree in 0 - half pi range on the +X+Z plane.
				float atanU = atan2(abs(i.position.z), abs(i.position.x));
				// atanU to the 0-1 range.
				float u = atanU / (3.14159 / 2);
#else
				// If no horizontal angles are specified, there is just a single vertical slice lathed around the entire sphere.
				fixed u = 0;
#endif
			
				// The v position is defied by the sine on the XY plane.
				// atanV is in -pi/2 - pi/2 range.
				float atanV = atan2(i.position.y, length(i.position.xz));
#ifdef BOTTOM_VERTICAL
				// The full texture should be used for the bottom half of the sphere.
				float v = -atanV / (3.14159 / 2);
				fixed4 candela = tex2D(_MainTex, fixed2(u, 1 - v));
				candela *= step(i.position.y, 0);
#elif TOP_VERTICAL
				// The full texture should be used for the bottom top of the sphere.
				float v = atanV / (3.14159 / 2);
				fixed4 candela = tex2D(_MainTex, fixed2(u, v));
				candela *= step(0, i.position.y);
#elif FULL_VERTICAL
				// Normalize to the 0-1 range.
				float v = (atanV + 3.14159 / 2) / 3.14159;
				// If the full 180 vertical degrees are supplied, the texture is used to fill the entirety of the photometric sphere.
				fixed4 candela = tex2D(_MainTex, fixed2(u, v));
#endif

				candela.a = candela.r; // Only the alpha value matters for cookies.
				return candela;
			}
			ENDCG
		}
	}
}