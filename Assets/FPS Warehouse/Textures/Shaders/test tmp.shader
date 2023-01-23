// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Custom/HangarFloor"
{
	Properties
	{
		[HideInInspector] __dirty( "", Int ) = 1
		_concrete("concrete", 2D) = "white" {}
		_concrete2("concrete2", 2D) = "white" {}
		_concretemask("concrete mask", 2D) = "white" {}
		_concretedirt("concrete dirt", 2D) = "white" {}
		_puddlemask("puddle mask", 2D) = "white" {}
		_puddletexture("puddle texture", 2D) = "white" {}
		_concretetile("concrete tile", Range( 1 , 30)) = 1
		_Cubemap("Cubemap", CUBE) = "white" {}
		_Albedocolor("Albedo color", Color) = (1,1,1,0)
		_cubemapcolor("cubemap color", Color) = (1,1,1,0)
		_cubemapblurriness("cubemap blurriness", Range( 0 , 5)) = 0
		_Fresnelintensity(" Fresnel intensity", Range( 0 , 5)) = 0
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" }
		Cull Back
		CGINCLUDE
		#include "UnityPBSLighting.cginc"
		#include "Lighting.cginc"
		#pragma target 3.0
		#ifdef UNITY_PASS_SHADOWCASTER
			#undef INTERNAL_DATA
			#undef WorldReflectionVector
			#undef WorldNormalVector
			#define INTERNAL_DATA half3 internalSurfaceTtoW0; half3 internalSurfaceTtoW1; half3 internalSurfaceTtoW2;
			#define WorldReflectionVector(data,normal) reflect (data.worldRefl, half3(dot(data.internalSurfaceTtoW0,normal), dot(data.internalSurfaceTtoW1,normal), dot(data.internalSurfaceTtoW2,normal)))
			#define WorldNormalVector(data,normal) fixed3(dot(data.internalSurfaceTtoW0,normal), dot(data.internalSurfaceTtoW1,normal), dot(data.internalSurfaceTtoW2,normal))
		#endif
		struct Input
		{
			float3 worldPos;
			float3 worldNormal;
			INTERNAL_DATA
			float3 worldRefl;
			float2 uv_texcoord;
			float2 texcoord_0;
		};

		uniform float _Fresnelintensity;
		uniform samplerCUBE _Cubemap;
		uniform float _cubemapblurriness;
		uniform float4 _cubemapcolor;
		uniform sampler2D _puddletexture;
		uniform float4 _puddletexture_ST;
		uniform sampler2D _concrete;
		uniform float _concretetile;
		uniform sampler2D _concrete2;
		uniform sampler2D _concretemask;
		uniform float4 _concretemask_ST;
		uniform sampler2D _concretedirt;
		uniform float4 _concretedirt_ST;
		uniform float4 _Albedocolor;
		uniform sampler2D _puddlemask;
		uniform float4 _puddlemask_ST;

		void vertexDataFunc( inout appdata_full v, out Input o )
		{
			UNITY_INITIALIZE_OUTPUT( Input, o );
			float2 temp_cast_0 = _concretetile;
			o.texcoord_0.xy = v.texcoord.xy * temp_cast_0 + float2( 0,0 );
		}

		void surf( Input i , inout SurfaceOutputStandardSpecular o )
		{
			o.Normal = float3(0.5,0.5,1);
			float3 worldViewDir = normalize( UnityWorldSpaceViewDir( i.worldPos ) );
			float3 worldNormal = WorldNormalVector( i, float3( 0, 0, 1 ) );
			float fresnelFinalVal33 = (0.0 + _Fresnelintensity*pow( 1.0 - dot( worldNormal, worldViewDir ) , 10.0));
			float4 temp_cast_0 = fresnelFinalVal33;
			float3 worldrefVec14 = WorldReflectionVector( i , float3(0,0,1) );
			float2 uv_puddletexture = i.uv_texcoord * _puddletexture_ST.xy + _puddletexture_ST.zw;
			float2 uv_concretemask = i.uv_texcoord * _concretemask_ST.xy + _concretemask_ST.zw;
			float2 uv_concretedirt = i.uv_texcoord * _concretedirt_ST.xy + _concretedirt_ST.zw;
			float2 uv_puddlemask = i.uv_texcoord * _puddlemask_ST.xy + _puddlemask_ST.zw;
			o.Albedo = lerp( ( temp_cast_0 + ( ( texCUBElod( _Cubemap,float4( worldrefVec14, _cubemapblurriness)) * _cubemapcolor ) * tex2D( _puddletexture,uv_puddletexture) ) ) , ( ( lerp( tex2D( _concrete,i.texcoord_0) , tex2D( _concrete2,i.texcoord_0) , tex2D( _concretemask,uv_concretemask).x ) * tex2D( _concretedirt,uv_concretedirt) ) * _Albedocolor ) , tex2D( _puddlemask,uv_puddlemask).x ).rgb;
			o.Alpha = 1;
		}

		ENDCG
		CGPROGRAM
		#pragma surface surf StandardSpecular keepalpha vertex:vertexDataFunc 

		ENDCG
		Pass
		{
			Name "ShadowCaster"
			Tags{ "LightMode" = "ShadowCaster" }
			ZWrite On
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma target 3.0
			#pragma multi_compile_shadowcaster
			#pragma multi_compile UNITY_PASS_SHADOWCASTER
			#pragma skip_variants FOG_LINEAR FOG_EXP FOG_EXP2
			# include "HLSLSupport.cginc"
			#if ( SHADER_API_D3D11 || SHADER_API_GLCORE || SHADER_API_GLES3 )
				#define CAN_SKIP_VPOS
			#endif
			#include "UnityCG.cginc"
			#include "Lighting.cginc"
			#include "UnityPBSLighting.cginc"
			sampler3D _DitherMaskLOD;
			struct v2f
			{
				V2F_SHADOW_CASTER;
				float3 worldPos : TEXCOORD6;
				float4 tSpace0 : TEXCOORD1;
				float4 tSpace1 : TEXCOORD2;
				float4 tSpace2 : TEXCOORD3;
				float4 texcoords01 : TEXCOORD4;
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};
			v2f vert( appdata_full v )
			{
				v2f o;
				UNITY_SETUP_INSTANCE_ID( v );
				UNITY_INITIALIZE_OUTPUT( v2f, o );
				UNITY_TRANSFER_INSTANCE_ID( v, o );
				float3 worldPos = mul( unity_ObjectToWorld, v.vertex ).xyz;
				half3 worldNormal = UnityObjectToWorldNormal( v.normal );
				fixed3 worldTangent = UnityObjectToWorldDir( v.tangent.xyz );
				fixed tangentSign = v.tangent.w * unity_WorldTransformParams.w;
				fixed3 worldBinormal = cross( worldNormal, worldTangent ) * tangentSign;
				o.tSpace0 = float4( worldTangent.x, worldBinormal.x, worldNormal.x, worldPos.x );
				o.tSpace1 = float4( worldTangent.y, worldBinormal.y, worldNormal.y, worldPos.y );
				o.tSpace2 = float4( worldTangent.z, worldBinormal.z, worldNormal.z, worldPos.z );
				o.texcoords01 = float4( v.texcoord.xy, v.texcoord1.xy );
				o.worldPos = worldPos;
				TRANSFER_SHADOW_CASTER_NORMALOFFSET( o )
				return o;
			}
			fixed4 frag( v2f IN
			#if !defined( CAN_SKIP_VPOS )
			, UNITY_VPOS_TYPE vpos : VPOS
			#endif
			) : SV_Target
			{
				UNITY_SETUP_INSTANCE_ID( IN );
				Input surfIN;
				UNITY_INITIALIZE_OUTPUT( Input, surfIN );
				surfIN.uv_texcoord = IN.texcoords01.xy;
				float3 worldPos = float3( IN.tSpace0.w, IN.tSpace1.w, IN.tSpace2.w );
				fixed3 worldViewDir = normalize( UnityWorldSpaceViewDir( worldPos ) );
				surfIN.worldPos = worldPos;
				surfIN.worldNormal = float3( IN.tSpace0.z, IN.tSpace1.z, IN.tSpace2.z );
				surfIN.worldRefl = -worldViewDir;
				surfIN.internalSurfaceTtoW0 = IN.tSpace0.xyz;
				surfIN.internalSurfaceTtoW1 = IN.tSpace1.xyz;
				surfIN.internalSurfaceTtoW2 = IN.tSpace2.xyz;
				SurfaceOutputStandardSpecular o;
				UNITY_INITIALIZE_OUTPUT( SurfaceOutputStandardSpecular, o )
				surf( surfIN, o );
				#if defined( CAN_SKIP_VPOS )
				float2 vpos = IN.pos;
				#endif
				SHADOW_CASTER_FRAGMENT( IN )
			}
			ENDCG
		}
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=6001
2567;29;1666;974;884.4396;190.791;1.3;True;True
Node;AmplifyShaderEditor.RangedFloatNode;1;-1122.496,-366.2048;Float;False;Property;_concretetile;concrete tile;7;0;1;1;30;FLOAT
Node;AmplifyShaderEditor.RangedFloatNode;29;-358.8454,402.4617;Float;False;Property;_cubemapblurriness;cubemap blurriness;10;0;0;0;5;FLOAT
Node;AmplifyShaderEditor.TextureCoordinatesNode;2;-798.8865,-334.562;Float;False;0;-1;2;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;FLOAT2;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.WorldReflectionVector;14;-368.3989,257.1289;Float;False;0;FLOAT3;0,0,0;False;FLOAT3;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.SamplerNode;16;-78.84653,231.8625;Float;True;Property;_Cubemap;Cubemap;7;0;Assets/FPS Warehouse/Textures/Cubemap.png;True;0;False;white;Auto;False;Object;-1;MipLevel;Cube;0;SAMPLER2D;;False;1;FLOAT3;0,0,0;False;2;FLOAT;1.0;False;3;FLOAT3;0,0,0;False;4;FLOAT3;0,0,0;False;5;FLOAT;1.0;False;FLOAT4;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.ColorNode;24;-41.6459,446.0624;Float;False;Property;_cubemapcolor;cubemap color;8;0;1,1,1,0;COLOR;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.SamplerNode;3;-508.9691,-427.677;Float;True;Property;_concrete;concrete;0;0;Assets/FPS Warehouse/Textures/concrete 1 diffuseµ.tga;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;1.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;FLOAT4;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.SamplerNode;5;-508.5691,-219.4771;Float;True;Property;_concrete2;concrete2;1;0;Assets/FPS Warehouse/Textures/concrete 2 diffuse.tga;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;1.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;FLOAT4;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.SamplerNode;4;-502.3689,-10.47682;Float;True;Property;_concretemask;concrete mask;2;0;Assets/FPS Warehouse/Textures/Mask.tga;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;1.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;FLOAT4;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.SamplerNode;6;-77.96954,643.1251;Float;True;Property;_puddletexture;puddle texture;6;0;Assets/FPS Warehouse/Textures/oil.tga;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;1.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;FLOAT4;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.RangedFloatNode;35;241.66,255.4091;Float;False;Property;_Fresnelintensity; Fresnel intensity;11;0;0;0;5;FLOAT
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;23;285.054,358.4622;Float;False;0;FLOAT4;0.0,0,0,0;False;1;COLOR;0.0,0,0,0;False;COLOR
Node;AmplifyShaderEditor.SamplerNode;9;-129.569,3.023178;Float;True;Property;_concretedirt;concrete dirt;3;0;Assets/FPS Warehouse/Textures/Mask2.tga;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;1.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;FLOAT4;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.LerpOp;8;-130.4689,-239.0772;Float;True;0;FLOAT4;0.0,0,0,0;False;1;FLOAT4;0.0,0,0,0;False;2;FLOAT4;0.0;False;FLOAT4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;12;293.8307,-57.67712;Float;False;0;FLOAT4;0.0,0,0,0;False;1;FLOAT4;0.0;False;FLOAT4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;27;463.4541,426.3623;Float;False;0;COLOR;0,0,0,0;False;1;FLOAT4;0.0,0,0,0;False;FLOAT4
Node;AmplifyShaderEditor.ColorNode;21;200.4542,41.86231;Float;False;Property;_Albedocolor;Albedo color;8;0;1,1,1,0;COLOR;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.FresnelNode;33;527.7601,267.208;Float;False;0;FLOAT3;0,0,0;False;1;FLOAT;0.0;False;2;FLOAT;1.0;False;3;FLOAT;10.0;False;FLOAT
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;22;442.854,-4.337542;Float;False;0;FLOAT4;0,0,0,0;False;1;COLOR;0.0,0,0,0;False;COLOR
Node;AmplifyShaderEditor.SimpleAddOpNode;34;721.2603,347.3088;Float;False;0;FLOAT;0.0;False;1;FLOAT4;0.0;False;FLOAT4
Node;AmplifyShaderEditor.SamplerNode;7;371.131,569.324;Float;True;Property;_puddlemask;puddle mask;5;0;Assets/FPS Warehouse/Textures/mask3.tga;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;1.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;FLOAT4;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.Vector3Node;32;782.3353,626.9532;Float;True;Constant;_Vector0;Vector 0;11;0;0.5,0.5,1;FLOAT3;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.LerpOp;30;878.4539,345.2623;Float;True;0;FLOAT4;0.0;False;1;COLOR;0,0,0,0;False;2;FLOAT4;0.0;False;COLOR
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;1145.743,469.5166;Float;False;True;2;Float;ASEMaterialInspector;StandardSpecular;Custom/HangarFloor;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;0;False;0;0;Opaque;0.5;True;True;0;False;Opaque;Geometry;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;False;0;255;255;0;0;0;0;False;0;4;10;25;False;0.5;True;0;Zero;Zero;0;Zero;Zero;Add;Add;0;False;0;0,0,0,0;VertexOffset;False;Cylindrical;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT;0.0;False;5;FLOAT;0.0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0.0;False;9;FLOAT;0.0;False;10;OBJECT;0.0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;13;OBJECT;0.0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False
WireConnection;2;0;1;0
WireConnection;16;1;14;0
WireConnection;16;2;29;0
WireConnection;3;1;2;0
WireConnection;5;1;2;0
WireConnection;23;0;16;0
WireConnection;23;1;24;0
WireConnection;8;0;3;0
WireConnection;8;1;5;0
WireConnection;8;2;4;0
WireConnection;12;0;8;0
WireConnection;12;1;9;0
WireConnection;27;0;23;0
WireConnection;27;1;6;0
WireConnection;33;2;35;0
WireConnection;22;0;12;0
WireConnection;22;1;21;0
WireConnection;34;0;33;0
WireConnection;34;1;27;0
WireConnection;30;0;34;0
WireConnection;30;1;22;0
WireConnection;30;2;7;0
WireConnection;0;0;30;0
WireConnection;0;1;32;0
ASEEND*/
//CHKSM=327CC4BF1CCF4F7E325423193055B67923A6380F