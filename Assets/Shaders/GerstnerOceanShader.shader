Shader "Custom/GerstnerOcean"
{
    Properties
    {
        _Amplitude1 ("Wave Height1", Float) = 0.5
        _Frequency1 ("Wave Frequency1", Float) = 1.0
        _Direction1 ("Wave Direction1", Vector) = (1,0,1,0)
        _Speed1 ("Wave Speed1", Float) = 1.0
        
        _Frequency2 ("Wave Frequency2", Float) = 1.0
        _Amplitude2 ("Wave Height2", Float) = 0.5
        _Speed2 ("Wave Speed2", Float) = 1.0
        _Direction2 ("Wave Direction2", Vector) = (1,0,1,0)
        
        _Color ("Water Color", Color) = (0,0.5,1,1)
        _SeedA ("Random Seed A", Float) = 1.0
        _SeedB ("Random Seed B", Float) = 1.0
    }

    SubShader
    {
        Tags {
            "RenderType"="Transparent"
            "Queue"="Transparent"
        }

        Pass
        {
            Blend SrcAlpha OneMinusSrcAlpha
            
            CGPROGRAM
// Upgrade NOTE: excluded shader from DX11; has structs without semantics (struct v2f members height)
#pragma exclude_renderers d3d11
            #pragma vertex vert
            #pragma fragment frag
            
            
            float _SeedA;
            float _SeedB;

            
            float4 _Direction1;
            float _Amplitude1;
            float _Frequency1;
            float _Speed1;

            float4 _Direction2;
            float _Amplitude2;
            float _Frequency2;
            float _Speed2;

            float4 _Color;

            struct appdata
            {
                float4 vertex : POSITION;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                // float height : TEXCOORD0;
            };

            float random()
            {
                return frac(sin(dot(float2(_SeedA, _SeedB), float2(12.9898, 78.233))) * 43758.5453);
                
            }

            float3 wave(float3 vertex, float3 dir, float amp, float freq, float speed)
            {
                float2 dire = normalize(dir.xz);
                float wave = dot(vertex.xz, dire) * freq + _Time.y * speed;
                
                float height = sin(wave) * amp;
                float offset = cos(wave) * amp;
                
                float3 v = vertex;
                v.x += dir.x * offset;
                v.z += dir.y * offset;
                v.y += height;
                
                return v;
            }

            v2f vert(appdata v)
            {
                v2f o;

                // float2 dir = normalize(_Direction.xz);
                // float wave = dot(v.vertex.xz, dir) * _Frequency + _Time.y * _Speed;
                //
                // float height = sin(wave) * _Amplitude;
                // float offset = cos(wave) * _Amplitude;
                //
                // v.vertex.x += dir.x * offset;
                // v.vertex.z += dir.y * offset;
                // v.vertex.y += height;
                
                float3 wave1 = wave(v.vertex, _Direction1, _Amplitude1, _Frequency1, _Speed1);
                float3 wave2 = wave(v.vertex, _Direction2, _Amplitude2, _Frequency2, _Speed2);
                
                float3 final_v = wave1 + wave2;
                
                o.vertex = UnityObjectToClipPos(final_v);
                // o.height = offset;
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                float4 modifiedColor = _Color;
                // if (i.height > .5)
                // {
                //     modifiedColor.a = i.height/2;    
                // }
                
                // modifiedColor.r += i.height/10;
                // modifiedColor.g += i.height/10;
                // modifiedColor.b += i.height/10;
                // modifiedColor.a += i.height*5;
                // if (modifiedColor.r > 0.1) modifiedColor.r = 0.1;
                // if (modifiedColor.g > 0.18) modifiedColor.g = 0.18;
                // if (modifiedColor.b > 0.23) modifiedColor.b = 0.23;
                return modifiedColor;
            }

            ENDCG
        }
    }
}

//wave1 = Gerstner(direction1, amplitude1) 
//wave2 = Gerstner(direction2, amplitude2) 
//wave3 = Gerstner(direction3, amplitude3) 
//finalHeight = wave1 + wave2 + wave3 