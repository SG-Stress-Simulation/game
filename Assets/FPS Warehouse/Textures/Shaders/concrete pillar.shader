// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

// Shader created with Shader Forge v1.26 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.26;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,lico:1,lgpr:1,limd:3,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:True,hqlp:False,rprd:True,enco:False,rmgx:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False;n:type:ShaderForge.SFN_Final,id:2865,x:34367,y:32664,varname:node_2865,prsc:2|diff-6343-OUT,spec-358-OUT,gloss-1813-OUT,normal-5964-RGB;n:type:ShaderForge.SFN_Multiply,id:6343,x:33709,y:32944,varname:node_6343,prsc:2|A-1975-OUT,B-6665-RGB;n:type:ShaderForge.SFN_Color,id:6665,x:33487,y:33072,ptovrint:False,ptlb:Color,ptin:_Color,varname:_Color,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:1,c3:1,c4:1;n:type:ShaderForge.SFN_Tex2d,id:7736,x:31446,y:32551,ptovrint:True,ptlb:Base Color,ptin:_MainTex,varname:_MainTex,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Tex2d,id:5964,x:33168,y:32822,ptovrint:True,ptlb:Normal Map,ptin:_BumpMap,varname:_BumpMap,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:3,isnm:True;n:type:ShaderForge.SFN_Slider,id:358,x:33105,y:32733,ptovrint:False,ptlb:Metallic,ptin:_Metallic,varname:node_358,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0,max:1;n:type:ShaderForge.SFN_Slider,id:1813,x:33090,y:32754,ptovrint:False,ptlb:Gloss,ptin:_Gloss,varname:_Metallic_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0,max:1;n:type:ShaderForge.SFN_Tex2d,id:9691,x:31460,y:32728,ptovrint:False,ptlb:node_9691,ptin:_node_9691,varname:node_9691,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Lerp,id:9412,x:31834,y:32700,varname:node_9412,prsc:2|A-9577-OUT,B-9691-RGB,T-6528-RGB;n:type:ShaderForge.SFN_Tex2d,id:6528,x:31487,y:32920,ptovrint:False,ptlb:node_6528,ptin:_node_6528,varname:node_6528,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:9e2677997077c0c4a8ae70d4b32aa44b,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Tex2d,id:8607,x:31834,y:32477,ptovrint:False,ptlb:node_8607,ptin:_node_8607,varname:node_8607,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:b893b08474b159a4baa4cc32b27874b3,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Multiply,id:7936,x:32038,y:32556,varname:node_7936,prsc:2|A-8607-RGB,B-9412-OUT;n:type:ShaderForge.SFN_Cubemap,id:9125,x:31780,y:33208,ptovrint:False,ptlb:node_9125,ptin:_node_9125,varname:node_9125,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,cube:05279d4a58a8039479777249c5885a9d,pvfc:0|MIP-1831-OUT;n:type:ShaderForge.SFN_Lerp,id:7441,x:32665,y:32846,varname:node_7441,prsc:2|A-4148-OUT,B-6628-OUT,T-3497-OUT;n:type:ShaderForge.SFN_Tex2d,id:836,x:31720,y:33593,ptovrint:False,ptlb:node_836,ptin:_node_836,varname:node_836,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:b42435d08453a3241959e23e23a0937f,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Multiply,id:6628,x:32276,y:32891,varname:node_6628,prsc:2|A-4450-OUT,B-9090-OUT;n:type:ShaderForge.SFN_Slider,id:4450,x:31647,y:33032,ptovrint:False,ptlb:cube map power,ptin:_cubemappower,varname:node_4450,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:1.595005,max:5;n:type:ShaderForge.SFN_OneMinus,id:3497,x:32104,y:33471,varname:node_3497,prsc:2|IN-836-R;n:type:ShaderForge.SFN_Slider,id:1831,x:31414,y:33234,ptovrint:False,ptlb:mip map blur,ptin:_mipmapblur,varname:node_1831,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:1.765784,max:10;n:type:ShaderForge.SFN_Multiply,id:8930,x:32121,y:33671,varname:node_8930,prsc:2|A-836-RGB,B-5723-OUT;n:type:ShaderForge.SFN_OneMinus,id:9950,x:32313,y:33641,varname:node_9950,prsc:2|IN-8930-OUT;n:type:ShaderForge.SFN_Add,id:9243,x:32512,y:33616,varname:node_9243,prsc:2|A-9950-OUT,B-4100-OUT;n:type:ShaderForge.SFN_Clamp01,id:1453,x:32680,y:33616,varname:node_1453,prsc:2|IN-9243-OUT;n:type:ShaderForge.SFN_Vector1,id:4100,x:32350,y:33776,varname:node_4100,prsc:2,v1:1;n:type:ShaderForge.SFN_Subtract,id:5926,x:32836,y:33464,varname:node_5926,prsc:2|A-3497-OUT,B-1453-OUT;n:type:ShaderForge.SFN_OneMinus,id:64,x:33014,y:33464,varname:node_64,prsc:2|IN-5926-OUT;n:type:ShaderForge.SFN_Vector1,id:5723,x:31891,y:33722,varname:node_5723,prsc:2,v1:1;n:type:ShaderForge.SFN_Multiply,id:1975,x:32886,y:33035,varname:node_1975,prsc:2|A-7441-OUT,B-2751-OUT;n:type:ShaderForge.SFN_Add,id:6475,x:33222,y:33505,varname:node_6475,prsc:2|A-3470-OUT,B-64-OUT;n:type:ShaderForge.SFN_Vector1,id:3470,x:33143,y:33430,varname:node_3470,prsc:2,v1:0.3;n:type:ShaderForge.SFN_Clamp01,id:2751,x:33084,y:33267,varname:node_2751,prsc:2|IN-6475-OUT;n:type:ShaderForge.SFN_Tex2d,id:1784,x:31976,y:33185,ptovrint:False,ptlb:node_1784,ptin:_node_1784,varname:node_1784,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:60a84529908d17c468b4ce2e3c322914,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Multiply,id:169,x:32141,y:33050,varname:node_169,prsc:2|A-9125-RGB,B-1784-RGB;n:type:ShaderForge.SFN_Tex2d,id:8107,x:32021,y:32700,ptovrint:False,ptlb:node_8107,ptin:_node_8107,varname:node_8107,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Lerp,id:4148,x:32276,y:32640,varname:node_4148,prsc:2|A-7936-OUT,B-8107-RGB,T-3284-OUT;n:type:ShaderForge.SFN_Multiply,id:9577,x:31662,y:32543,varname:node_9577,prsc:2|A-1633-OUT,B-7736-RGB;n:type:ShaderForge.SFN_Vector1,id:1633,x:31474,y:32469,varname:node_1633,prsc:2,v1:0.7;n:type:ShaderForge.SFN_Tex2d,id:200,x:31710,y:32893,ptovrint:False,ptlb:node_200,ptin:_node_200,varname:node_200,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:b893b08474b159a4baa4cc32b27874b3,ntxv:0,isnm:False;n:type:ShaderForge.SFN_OneMinus,id:5756,x:31891,y:32893,varname:node_5756,prsc:2|IN-200-RGB;n:type:ShaderForge.SFN_Multiply,id:3284,x:32051,y:32879,varname:node_3284,prsc:2|A-689-OUT,B-5756-OUT;n:type:ShaderForge.SFN_Slider,id:689,x:31677,y:32817,ptovrint:False,ptlb:node_689,ptin:_node_689,varname:node_689,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:3.200371,max:5;n:type:ShaderForge.SFN_Fresnel,id:2224,x:32280,y:33191,varname:node_2224,prsc:2|EXP-4917-OUT;n:type:ShaderForge.SFN_Add,id:9090,x:32489,y:33083,varname:node_9090,prsc:2|A-169-OUT,B-203-OUT;n:type:ShaderForge.SFN_Slider,id:4917,x:31976,y:33387,ptovrint:False,ptlb:node_4917,ptin:_node_4917,varname:node_4917,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:3.868841,max:5;n:type:ShaderForge.SFN_Multiply,id:203,x:32489,y:33258,varname:node_203,prsc:2|A-2224-OUT,B-1222-OUT;n:type:ShaderForge.SFN_Vector1,id:1222,x:32341,y:33315,varname:node_1222,prsc:2,v1:0.5;proporder:5964-6665-7736-358-1813-6528-9691-8607-9125-836-4450-1831-1784-8107-200-689-4917;pass:END;sub:END;*/

Shader "Shader Forge/Concrete floor" {
    Properties {
        _BumpMap ("Normal Map", 2D) = "bump" {}
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Base Color", 2D) = "white" {}
        _Metallic ("Metallic", Range(0, 1)) = 0
        _Gloss ("Gloss", Range(0, 1)) = 0
        _node_6528 ("node_6528", 2D) = "white" {}
        _node_9691 ("node_9691", 2D) = "white" {}
        _node_8607 ("node_8607", 2D) = "white" {}
        _node_9125 ("node_9125", Cube) = "_Skybox" {}
        _node_836 ("node_836", 2D) = "white" {}
        _cubemappower ("cube map power", Range(0, 5)) = 1.595005
        _mipmapblur ("mip map blur", Range(0, 10)) = 1.765784
        _node_1784 ("node_1784", 2D) = "white" {}
        _node_8107 ("node_8107", 2D) = "white" {}
        _node_200 ("node_200", 2D) = "white" {}
        _node_689 ("node_689", Range(0, 5)) = 3.200371
        _node_4917 ("node_4917", Range(0, 5)) = 3.868841
    }
    SubShader {
        Tags {
            "RenderType"="Opaque"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #define SHOULD_SAMPLE_SH ( defined (LIGHTMAP_OFF) && defined(DYNAMICLIGHTMAP_OFF) )
            #define _GLOSSYENV 1
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #include "Lighting.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma multi_compile LIGHTMAP_OFF LIGHTMAP_ON
            #pragma multi_compile DIRLIGHTMAP_OFF DIRLIGHTMAP_COMBINED DIRLIGHTMAP_SEPARATE
            #pragma multi_compile DYNAMICLIGHTMAP_OFF DYNAMICLIGHTMAP_ON
            #pragma multi_compile_fog
            #pragma exclude_renderers gles3 metal d3d11_9x xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            #pragma glsl
            uniform float4 _Color;
            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
            uniform sampler2D _BumpMap; uniform float4 _BumpMap_ST;
            uniform float _Metallic;
            uniform float _Gloss;
            uniform sampler2D _node_9691; uniform float4 _node_9691_ST;
            uniform sampler2D _node_6528; uniform float4 _node_6528_ST;
            uniform sampler2D _node_8607; uniform float4 _node_8607_ST;
            uniform samplerCUBE _node_9125;
            uniform sampler2D _node_836; uniform float4 _node_836_ST;
            uniform float _cubemappower;
            uniform float _mipmapblur;
            uniform sampler2D _node_1784; uniform float4 _node_1784_ST;
            uniform sampler2D _node_8107; uniform float4 _node_8107_ST;
            uniform sampler2D _node_200; uniform float4 _node_200_ST;
            uniform float _node_689;
            uniform float _node_4917;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
                float2 texcoord0 : TEXCOORD0;
                float2 texcoord1 : TEXCOORD1;
                float2 texcoord2 : TEXCOORD2;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float2 uv1 : TEXCOORD1;
                float2 uv2 : TEXCOORD2;
                float4 posWorld : TEXCOORD3;
                float3 normalDir : TEXCOORD4;
                float3 tangentDir : TEXCOORD5;
                float3 bitangentDir : TEXCOORD6;
                LIGHTING_COORDS(7,8)
                UNITY_FOG_COORDS(9)
                #if defined(LIGHTMAP_ON) || defined(UNITY_SHOULD_SAMPLE_SH)
                    float4 ambientOrLightmapUV : TEXCOORD10;
                #endif
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.uv1 = v.texcoord1;
                o.uv2 = v.texcoord2;
                #ifdef LIGHTMAP_ON
                    o.ambientOrLightmapUV.xy = v.texcoord1.xy * unity_LightmapST.xy + unity_LightmapST.zw;
                    o.ambientOrLightmapUV.zw = 0;
                #elif UNITY_SHOULD_SAMPLE_SH
                #endif
                #ifdef DYNAMICLIGHTMAP_ON
                    o.ambientOrLightmapUV.zw = v.texcoord2.xy * unity_DynamicLightmapST.xy + unity_DynamicLightmapST.zw;
                #endif
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.tangentDir = normalize( mul( unity_ObjectToWorld, float4( v.tangent.xyz, 0.0 ) ).xyz );
                o.bitangentDir = normalize(cross(o.normalDir, o.tangentDir) * v.tangent.w);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = UnityObjectToClipPos(v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3x3 tangentTransform = float3x3( i.tangentDir, i.bitangentDir, i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 _BumpMap_var = UnpackNormal(tex2D(_BumpMap,TRANSFORM_TEX(i.uv0, _BumpMap)));
                float3 normalLocal = _BumpMap_var.rgb;
                float3 normalDirection = normalize(mul( normalLocal, tangentTransform )); // Perturbed normals
                float3 viewReflectDirection = reflect( -viewDirection, normalDirection );
                float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
                float3 lightColor = _LightColor0.rgb;
                float3 halfDirection = normalize(viewDirection+lightDirection);
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 attenColor = attenuation * _LightColor0.xyz;
                float Pi = 3.141592654;
                float InvPi = 0.31830988618;
///////// Gloss:
                float gloss = _Gloss;
                float specPow = exp2( gloss * 10.0+1.0);
/////// GI Data:
                UnityLight light;
                #ifdef LIGHTMAP_OFF
                    light.color = lightColor;
                    light.dir = lightDirection;
                    light.ndotl = LambertTerm (normalDirection, light.dir);
                #else
                    light.color = half3(0.f, 0.f, 0.f);
                    light.ndotl = 0.0f;
                    light.dir = half3(0.f, 0.f, 0.f);
                #endif
                UnityGIInput d;
                d.light = light;
                d.worldPos = i.posWorld.xyz;
                d.worldViewDir = viewDirection;
                d.atten = attenuation;
                #if defined(LIGHTMAP_ON) || defined(DYNAMICLIGHTMAP_ON)
                    d.ambient = 0;
                    d.lightmapUV = i.ambientOrLightmapUV;
                #else
                    d.ambient = i.ambientOrLightmapUV;
                #endif
                d.boxMax[0] = unity_SpecCube0_BoxMax;
                d.boxMin[0] = unity_SpecCube0_BoxMin;
                d.probePosition[0] = unity_SpecCube0_ProbePosition;
                d.probeHDR[0] = unity_SpecCube0_HDR;
                d.boxMax[1] = unity_SpecCube1_BoxMax;
                d.boxMin[1] = unity_SpecCube1_BoxMin;
                d.probePosition[1] = unity_SpecCube1_ProbePosition;
                d.probeHDR[1] = unity_SpecCube1_HDR;
                Unity_GlossyEnvironmentData ugls_en_data;
                ugls_en_data.roughness = 1.0 - gloss;
                ugls_en_data.reflUVW = viewReflectDirection;
                UnityGI gi = UnityGlobalIllumination(d, 1, normalDirection, ugls_en_data );
                lightDirection = gi.light.dir;
                lightColor = gi.light.color;
////// Specular:
                float NdotL = max(0, dot( normalDirection, lightDirection ));
                float LdotH = max(0.0,dot(lightDirection, halfDirection));
                float4 _node_8607_var = tex2D(_node_8607,TRANSFORM_TEX(i.uv0, _node_8607));
                float4 _MainTex_var = tex2D(_MainTex,TRANSFORM_TEX(i.uv0, _MainTex));
                float4 _node_9691_var = tex2D(_node_9691,TRANSFORM_TEX(i.uv0, _node_9691));
                float4 _node_6528_var = tex2D(_node_6528,TRANSFORM_TEX(i.uv0, _node_6528));
                float4 _node_8107_var = tex2D(_node_8107,TRANSFORM_TEX(i.uv0, _node_8107));
                float4 _node_200_var = tex2D(_node_200,TRANSFORM_TEX(i.uv0, _node_200));
                float4 _node_1784_var = tex2D(_node_1784,TRANSFORM_TEX(i.uv0, _node_1784));
                float4 _node_836_var = tex2D(_node_836,TRANSFORM_TEX(i.uv0, _node_836));
                float node_3497 = (1.0 - _node_836_var.r);
                float3 diffuseColor = ((lerp(lerp((_node_8607_var.rgb*lerp((0.7*_MainTex_var.rgb),_node_9691_var.rgb,_node_6528_var.rgb)),_node_8107_var.rgb,(_node_689*(1.0 - _node_200_var.rgb))),(_cubemappower*((texCUBElod(_node_9125,float4(viewReflectDirection,_mipmapblur)).rgb*_node_1784_var.rgb)+(pow(1.0-max(0,dot(normalDirection, viewDirection)),_node_4917)*0.5))),node_3497)*saturate((0.3+(1.0 - (node_3497-saturate(((1.0 - (_node_836_var.rgb*1.0))+1.0)))))))*_Color.rgb); // Need this for specular when using metallic
                float specularMonochrome;
                float3 specularColor;
                diffuseColor = DiffuseAndSpecularFromMetallic( diffuseColor, _Metallic, specularColor, specularMonochrome );
                specularMonochrome = 1-specularMonochrome;
                float NdotV = max(0.0,dot( normalDirection, viewDirection ));
                float NdotH = max(0.0,dot( normalDirection, halfDirection ));
                float VdotH = max(0.0,dot( viewDirection, halfDirection ));
                float visTerm = SmithBeckmannVisibilityTerm( NdotL, NdotV, 1.0-gloss );
                float normTerm = max(0.0, NDFBlinnPhongNormalizedTerm(NdotH, RoughnessToSpecPower(1.0-gloss)));
                float specularPBL = max(0, (NdotL*visTerm*normTerm) * (UNITY_PI / 4) );
                float3 directSpecular = 1 * pow(max(0,dot(halfDirection,normalDirection)),specPow)*specularPBL*lightColor*FresnelTerm(specularColor, LdotH);
                half grazingTerm = saturate( gloss + specularMonochrome );
                float3 indirectSpecular = (gi.indirect.specular);
                indirectSpecular *= FresnelLerp (specularColor, grazingTerm, NdotV);
                float3 specular = (directSpecular + indirectSpecular);
/////// Diffuse:
                NdotL = max(0.0,dot( normalDirection, lightDirection ));
                half fd90 = 0.5 + 2 * LdotH * LdotH * (1-gloss);
                float3 directDiffuse = ((1 +(fd90 - 1)*pow((1.00001-NdotL), 5)) * (1 + (fd90 - 1)*pow((1.00001-NdotV), 5)) * NdotL) * attenColor;
                float3 indirectDiffuse = float3(0,0,0);
                indirectDiffuse += gi.indirect.diffuse;
                float3 diffuse = (directDiffuse + indirectDiffuse) * diffuseColor;
/// Final Color:
                float3 finalColor = diffuse + specular;
                fixed4 finalRGBA = fixed4(finalColor,1);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
        Pass {
            Name "FORWARD_DELTA"
            Tags {
                "LightMode"="ForwardAdd"
            }
            Blend One One
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDADD
            #define SHOULD_SAMPLE_SH ( defined (LIGHTMAP_OFF) && defined(DYNAMICLIGHTMAP_OFF) )
            #define _GLOSSYENV 1
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #include "Lighting.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #pragma multi_compile_fwdadd_fullshadows
            #pragma multi_compile LIGHTMAP_OFF LIGHTMAP_ON
            #pragma multi_compile DIRLIGHTMAP_OFF DIRLIGHTMAP_COMBINED DIRLIGHTMAP_SEPARATE
            #pragma multi_compile DYNAMICLIGHTMAP_OFF DYNAMICLIGHTMAP_ON
            #pragma multi_compile_fog
            #pragma exclude_renderers gles3 metal d3d11_9x xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            #pragma glsl
            uniform float4 _Color;
            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
            uniform sampler2D _BumpMap; uniform float4 _BumpMap_ST;
            uniform float _Metallic;
            uniform float _Gloss;
            uniform sampler2D _node_9691; uniform float4 _node_9691_ST;
            uniform sampler2D _node_6528; uniform float4 _node_6528_ST;
            uniform sampler2D _node_8607; uniform float4 _node_8607_ST;
            uniform samplerCUBE _node_9125;
            uniform sampler2D _node_836; uniform float4 _node_836_ST;
            uniform float _cubemappower;
            uniform float _mipmapblur;
            uniform sampler2D _node_1784; uniform float4 _node_1784_ST;
            uniform sampler2D _node_8107; uniform float4 _node_8107_ST;
            uniform sampler2D _node_200; uniform float4 _node_200_ST;
            uniform float _node_689;
            uniform float _node_4917;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
                float2 texcoord0 : TEXCOORD0;
                float2 texcoord1 : TEXCOORD1;
                float2 texcoord2 : TEXCOORD2;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float2 uv1 : TEXCOORD1;
                float2 uv2 : TEXCOORD2;
                float4 posWorld : TEXCOORD3;
                float3 normalDir : TEXCOORD4;
                float3 tangentDir : TEXCOORD5;
                float3 bitangentDir : TEXCOORD6;
                LIGHTING_COORDS(7,8)
                UNITY_FOG_COORDS(9)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.uv1 = v.texcoord1;
                o.uv2 = v.texcoord2;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.tangentDir = normalize( mul( unity_ObjectToWorld, float4( v.tangent.xyz, 0.0 ) ).xyz );
                o.bitangentDir = normalize(cross(o.normalDir, o.tangentDir) * v.tangent.w);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = UnityObjectToClipPos(v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3x3 tangentTransform = float3x3( i.tangentDir, i.bitangentDir, i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 _BumpMap_var = UnpackNormal(tex2D(_BumpMap,TRANSFORM_TEX(i.uv0, _BumpMap)));
                float3 normalLocal = _BumpMap_var.rgb;
                float3 normalDirection = normalize(mul( normalLocal, tangentTransform )); // Perturbed normals
                float3 viewReflectDirection = reflect( -viewDirection, normalDirection );
                float3 lightDirection = normalize(lerp(_WorldSpaceLightPos0.xyz, _WorldSpaceLightPos0.xyz - i.posWorld.xyz,_WorldSpaceLightPos0.w));
                float3 lightColor = _LightColor0.rgb;
                float3 halfDirection = normalize(viewDirection+lightDirection);
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 attenColor = attenuation * _LightColor0.xyz;
                float Pi = 3.141592654;
                float InvPi = 0.31830988618;
///////// Gloss:
                float gloss = _Gloss;
                float specPow = exp2( gloss * 10.0+1.0);
////// Specular:
                float NdotL = max(0, dot( normalDirection, lightDirection ));
                float LdotH = max(0.0,dot(lightDirection, halfDirection));
                float4 _node_8607_var = tex2D(_node_8607,TRANSFORM_TEX(i.uv0, _node_8607));
                float4 _MainTex_var = tex2D(_MainTex,TRANSFORM_TEX(i.uv0, _MainTex));
                float4 _node_9691_var = tex2D(_node_9691,TRANSFORM_TEX(i.uv0, _node_9691));
                float4 _node_6528_var = tex2D(_node_6528,TRANSFORM_TEX(i.uv0, _node_6528));
                float4 _node_8107_var = tex2D(_node_8107,TRANSFORM_TEX(i.uv0, _node_8107));
                float4 _node_200_var = tex2D(_node_200,TRANSFORM_TEX(i.uv0, _node_200));
                float4 _node_1784_var = tex2D(_node_1784,TRANSFORM_TEX(i.uv0, _node_1784));
                float4 _node_836_var = tex2D(_node_836,TRANSFORM_TEX(i.uv0, _node_836));
                float node_3497 = (1.0 - _node_836_var.r);
                float3 diffuseColor = ((lerp(lerp((_node_8607_var.rgb*lerp((0.7*_MainTex_var.rgb),_node_9691_var.rgb,_node_6528_var.rgb)),_node_8107_var.rgb,(_node_689*(1.0 - _node_200_var.rgb))),(_cubemappower*((texCUBElod(_node_9125,float4(viewReflectDirection,_mipmapblur)).rgb*_node_1784_var.rgb)+(pow(1.0-max(0,dot(normalDirection, viewDirection)),_node_4917)*0.5))),node_3497)*saturate((0.3+(1.0 - (node_3497-saturate(((1.0 - (_node_836_var.rgb*1.0))+1.0)))))))*_Color.rgb); // Need this for specular when using metallic
                float specularMonochrome;
                float3 specularColor;
                diffuseColor = DiffuseAndSpecularFromMetallic( diffuseColor, _Metallic, specularColor, specularMonochrome );
                specularMonochrome = 1-specularMonochrome;
                float NdotV = max(0.0,dot( normalDirection, viewDirection ));
                float NdotH = max(0.0,dot( normalDirection, halfDirection ));
                float VdotH = max(0.0,dot( viewDirection, halfDirection ));
                float visTerm = SmithBeckmannVisibilityTerm( NdotL, NdotV, 1.0-gloss );
                float normTerm = max(0.0, NDFBlinnPhongNormalizedTerm(NdotH, RoughnessToSpecPower(1.0-gloss)));
                float specularPBL = max(0, (NdotL*visTerm*normTerm) * (UNITY_PI / 4) );
                float3 directSpecular = attenColor * pow(max(0,dot(halfDirection,normalDirection)),specPow)*specularPBL*lightColor*FresnelTerm(specularColor, LdotH);
                float3 specular = directSpecular;
/////// Diffuse:
                NdotL = max(0.0,dot( normalDirection, lightDirection ));
                half fd90 = 0.5 + 2 * LdotH * LdotH * (1-gloss);
                float3 directDiffuse = ((1 +(fd90 - 1)*pow((1.00001-NdotL), 5)) * (1 + (fd90 - 1)*pow((1.00001-NdotV), 5)) * NdotL) * attenColor;
                float3 diffuse = directDiffuse * diffuseColor;
/// Final Color:
                float3 finalColor = diffuse + specular;
                fixed4 finalRGBA = fixed4(finalColor * 1,0);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
        Pass {
            Name "Meta"
            Tags {
                "LightMode"="Meta"
            }
            Cull Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_META 1
            #define SHOULD_SAMPLE_SH ( defined (LIGHTMAP_OFF) && defined(DYNAMICLIGHTMAP_OFF) )
            #define _GLOSSYENV 1
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #include "UnityMetaPass.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
            #pragma multi_compile LIGHTMAP_OFF LIGHTMAP_ON
            #pragma multi_compile DIRLIGHTMAP_OFF DIRLIGHTMAP_COMBINED DIRLIGHTMAP_SEPARATE
            #pragma multi_compile DYNAMICLIGHTMAP_OFF DYNAMICLIGHTMAP_ON
            #pragma multi_compile_fog
            #pragma exclude_renderers gles3 metal d3d11_9x xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            #pragma glsl
            uniform float4 _Color;
            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
            uniform float _Metallic;
            uniform float _Gloss;
            uniform sampler2D _node_9691; uniform float4 _node_9691_ST;
            uniform sampler2D _node_6528; uniform float4 _node_6528_ST;
            uniform sampler2D _node_8607; uniform float4 _node_8607_ST;
            uniform samplerCUBE _node_9125;
            uniform sampler2D _node_836; uniform float4 _node_836_ST;
            uniform float _cubemappower;
            uniform float _mipmapblur;
            uniform sampler2D _node_1784; uniform float4 _node_1784_ST;
            uniform sampler2D _node_8107; uniform float4 _node_8107_ST;
            uniform sampler2D _node_200; uniform float4 _node_200_ST;
            uniform float _node_689;
            uniform float _node_4917;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
                float2 texcoord1 : TEXCOORD1;
                float2 texcoord2 : TEXCOORD2;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float2 uv1 : TEXCOORD1;
                float2 uv2 : TEXCOORD2;
                float4 posWorld : TEXCOORD3;
                float3 normalDir : TEXCOORD4;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.uv1 = v.texcoord1;
                o.uv2 = v.texcoord2;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = UnityMetaVertexPosition(v.vertex, v.texcoord1.xy, v.texcoord2.xy, unity_LightmapST, unity_DynamicLightmapST );
                return o;
            }
            float4 frag(VertexOutput i) : SV_Target {
                i.normalDir = normalize(i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
                float3 viewReflectDirection = reflect( -viewDirection, normalDirection );
                UnityMetaInput o;
                UNITY_INITIALIZE_OUTPUT( UnityMetaInput, o );
                
                o.Emission = 0;
                
                float4 _node_8607_var = tex2D(_node_8607,TRANSFORM_TEX(i.uv0, _node_8607));
                float4 _MainTex_var = tex2D(_MainTex,TRANSFORM_TEX(i.uv0, _MainTex));
                float4 _node_9691_var = tex2D(_node_9691,TRANSFORM_TEX(i.uv0, _node_9691));
                float4 _node_6528_var = tex2D(_node_6528,TRANSFORM_TEX(i.uv0, _node_6528));
                float4 _node_8107_var = tex2D(_node_8107,TRANSFORM_TEX(i.uv0, _node_8107));
                float4 _node_200_var = tex2D(_node_200,TRANSFORM_TEX(i.uv0, _node_200));
                float4 _node_1784_var = tex2D(_node_1784,TRANSFORM_TEX(i.uv0, _node_1784));
                float4 _node_836_var = tex2D(_node_836,TRANSFORM_TEX(i.uv0, _node_836));
                float node_3497 = (1.0 - _node_836_var.r);
                float3 diffColor = ((lerp(lerp((_node_8607_var.rgb*lerp((0.7*_MainTex_var.rgb),_node_9691_var.rgb,_node_6528_var.rgb)),_node_8107_var.rgb,(_node_689*(1.0 - _node_200_var.rgb))),(_cubemappower*((texCUBElod(_node_9125,float4(viewReflectDirection,_mipmapblur)).rgb*_node_1784_var.rgb)+(pow(1.0-max(0,dot(normalDirection, viewDirection)),_node_4917)*0.5))),node_3497)*saturate((0.3+(1.0 - (node_3497-saturate(((1.0 - (_node_836_var.rgb*1.0))+1.0)))))))*_Color.rgb);
                float specularMonochrome;
                float3 specColor;
                diffColor = DiffuseAndSpecularFromMetallic( diffColor, _Metallic, specColor, specularMonochrome );
                float roughness = 1.0 - _Gloss;
                o.Albedo = diffColor + specColor * roughness * roughness * 0.5;
                
                return UnityMetaFragment( o );
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
