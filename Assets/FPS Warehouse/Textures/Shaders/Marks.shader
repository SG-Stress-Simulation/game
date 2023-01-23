// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Custom/Marks"
{
	Properties
	{
		_Cutoff( "Mask Clip Value", Float ) = 0.5
		_Painttexturemask("Paint texture mask", 2D) = "white" {}
		_Paintcolor("Paint color", Color) = (1,1,1,0)
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "TransparentCutout"  "Queue" = "Transparent+0" "IsEmissive" = "true"  }
		Cull Back
		Blend DstColor Zero
		BlendOp Add
		CGPROGRAM
		#pragma target 3.0
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows 
		struct Input
		{
			float2 uv_texcoord;
		};

		uniform float4 _Paintcolor;
		uniform sampler2D _Painttexturemask;
		uniform float4 _Painttexturemask_ST;
		uniform float _Cutoff = 0.5;

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			o.Emission = _Paintcolor.rgb;
			o.Alpha = 1;
			float2 uv_Painttexturemask = i.uv_texcoord * _Painttexturemask_ST.xy + _Painttexturemask_ST.zw;
			clip( ( 1.0 - tex2D( _Painttexturemask, uv_Painttexturemask ) ).r - _Cutoff );
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=13801
207;177;1666;965;820.8009;258.3002;1;True;True
Node;AmplifyShaderEditor.SamplerNode;1;-441.6005,153.8995;Float;True;Property;_Painttexturemask;Paint texture mask;0;0;Assets/FPS Warehouse/Textures/A.tga;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;1.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;5;COLOR;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.ColorNode;2;-434.6005,-61.10046;Float;False;Property;_Paintcolor;Paint color;1;0;1,1,1,0;0;5;COLOR;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.OneMinusNode;3;-3.600517,161.8995;Float;True;1;0;COLOR;0.0;False;1;COLOR
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;349.8,-30.8;Float;False;True;2;Float;ASEMaterialInspector;0;0;Standard;Custom/Marks;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;0;False;0;0;Custom;0.5;True;True;0;True;TransparentCutout;Transparent;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;False;0;255;255;0;0;0;0;0;0;0;0;False;0;4;10;25;False;0.5;True;6;DstColor;Zero;0;Zero;Zero;Add;Add;0;False;0;0,0,0,0;VertexOffset;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;0;0;False;15;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0.0;False;4;FLOAT;0.0;False;5;FLOAT;0.0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0.0;False;9;FLOAT;0.0;False;10;FLOAT;0.0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;3;0;1;0
WireConnection;0;2;2;0
WireConnection;0;10;3;0
ASEEND*/
//CHKSM=D03B68CF862C1EF21ACEFA0638392547D45CDB81