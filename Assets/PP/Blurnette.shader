Shader "Custom/Blurnette"
{
    HLSLINCLUDE
        // StdLib.hlsl holds pre-configured vertex shaders (VertDefault), varying structs (VaryingsDefault), and most of the data you need to write common effects.
        #include "Packages/com.unity.postprocessing/PostProcessing/Shaders/StdLib.hlsl"
        TEXTURE2D_SAMPLER2D(_MainTex, sampler_MainTex);
        
        float Falloff = 0.5;
        float Intensity = 0.5;
        float EyeOffset;

        float4 Frag(VaryingsDefault i) : SV_Target
        {
            // calculate natural vignette
            float2 uv = i.texcoord;

            uv.x += EyeOffset;

            float2 coord = uv - 0.5;

            float rf = sqrt(dot(coord, coord)) * (Falloff / 2.0);
            float rf2_1 = rf * rf + 1.0;
            float e = 1.0 / (rf2_1 * rf2_1);

            // calculate the blur
            float Pi = 6.28318530718; // 2 * PI
            float Directions = 16.0;
            float Quality = 4.0;
            float Size = Intensity * (1.0 - e);
            // cap size at 2
            Size = min(Size, 0.01);
            Size = max(Size, 0.0);
            float2 Radius = Size/1;

            float4 Color = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, i.texcoord);
            for (float d = 0.0; d < Pi; d += Pi/Directions)
            {
                for (float j = 1.0/Quality; j <= 1.0; j += 1.0/Quality)
                {
                    float2 Offset = float2(cos(d), sin(d));
                    Color += SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, i.texcoord + Offset * Radius * j);
                }
            }
            Color /= Quality * Directions - 15.0;

            // with decreasing e, darken the color
            Color.rgb *= pow(e, 10.0);

            return Color;
        }
    ENDHLSL
    SubShader
    {
        Cull Off ZWrite Off ZTest Always
        Pass
        {
            HLSLPROGRAM
                #pragma vertex VertDefault
                #pragma fragment Frag
            ENDHLSL
        }
    }
}
