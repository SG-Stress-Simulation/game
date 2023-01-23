// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

// Shader created with Shader Forge v1.26 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.26;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:6,dpts:2,wrdp:False,dith:0,rfrpo:False,rfrpn:Refraction,coma:15,ufog:False,aust:False,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False;n:type:ShaderForge.SFN_Final,id:3138,x:33046,y:32759,varname:node_3138,prsc:2|emission-7949-OUT;n:type:ShaderForge.SFN_ViewVector,id:4364,x:31961,y:32818,varname:node_4364,prsc:2;n:type:ShaderForge.SFN_Dot,id:644,x:32183,y:32922,varname:node_644,prsc:2,dt:0|A-4364-OUT,B-1078-OUT;n:type:ShaderForge.SFN_NormalVector,id:1078,x:31964,y:33018,prsc:2,pt:False;n:type:ShaderForge.SFN_Clamp01,id:3612,x:32341,y:32943,varname:node_3612,prsc:2|IN-644-OUT;n:type:ShaderForge.SFN_Power,id:3340,x:32477,y:33013,varname:node_3340,prsc:2|VAL-3612-OUT,EXP-3421-OUT;n:type:ShaderForge.SFN_Slider,id:3421,x:32163,y:33132,ptovrint:False,ptlb:node_3421,ptin:_node_3421,varname:node_3421,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:10,max:10;n:type:ShaderForge.SFN_Vector1,id:4355,x:32622,y:33013,varname:node_4355,prsc:2,v1:0.5;n:type:ShaderForge.SFN_Multiply,id:814,x:32428,y:32726,varname:node_814,prsc:2|A-217-RGB,B-3340-OUT;n:type:ShaderForge.SFN_Color,id:217,x:32162,y:32626,ptovrint:False,ptlb:node_217,ptin:_node_217,varname:node_217,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:0.736568,c3:0.404,c4:1;n:type:ShaderForge.SFN_Multiply,id:7949,x:32772,y:32879,varname:node_7949,prsc:2|A-814-OUT,B-4355-OUT,C-4355-OUT;proporder:3421-217;pass:END;sub:END;*/

Shader "Shader Forge/Raycast" {
    Properties {
        _node_3421 ("node_3421", Range(0, 10)) = 10
        _node_217 ("node_217", Color) = (1,0.736568,0.404,1)
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent"
            "RenderType"="Transparent"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            Blend One OneMinusSrcColor
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase
            #pragma exclude_renderers gles3 metal d3d11_9x xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            uniform float _node_3421;
            uniform float4 _node_217;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float4 posWorld : TEXCOORD0;
                float3 normalDir : TEXCOORD1;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = UnityObjectToClipPos(v.vertex );
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
////// Lighting:
////// Emissive:
                float node_4355 = 0.5;
                float3 emissive = ((_node_217.rgb*pow(saturate(dot(viewDirection,i.normalDir)),_node_3421))*node_4355*node_4355);
                float3 finalColor = emissive;
                return fixed4(finalColor,1);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
