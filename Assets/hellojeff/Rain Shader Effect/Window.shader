Shader "Unlit/Window"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Size ("Size", float) = 1
        _T ("Time", float) = 1
        _Distortion("Distortion", range(-5, 5)) = 0
        _Blur("Blur", range(0, 1)) = 1
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" "Queue"="Transparent" }
        LOD 100

        GrabPass { "_GrabTexture" }

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

            #define S(a, b, t) smoothstep(a, b, t)

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 grabUv : TEXCOORD1;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            sampler2D _GrabTexture;
            float4 _MainTex_ST;
            float _Size;
            float _T;
            float _Distortion;
            float _Blur;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.grabUv = UNITY_PROJ_COORD(ComputeGrabScreenPos(o.vertex));

                if (_ProjectionParams.x == 1)
                {
                    o.grabUv.y = 1 - o.grabUv.y;
                }

                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            float N21(float2 p) {
                p = frac(p * float2(123.34, 345.45));
                p += dot(p, p + 34.345);
                return frac(p.x * p.y);
            }

            float3 Layer(float2 UV, float t) {
                float2 aspect = float2(2, 1);
                float2 uv = UV * _Size * aspect;
                uv.y += t * .25;
                float2 gv = frac(uv) - 0.5;

                // create id for each cell
                float2 id = floor(uv);
                float n = N21(id);
                t += n * 6.2831;

                // wiggle for x
                float w = UV.y * 10;

                float x = (n - 0.5) * .8;
                x += (0.4 - abs(x)) * sin(3*w) * pow(sin(w), 6) * .45;

                float y = -sin(t + sin(t + sin(t) * .5))*.45;
                y -= (gv.x  - x) * (gv.x - x);

                // drop
                float2 dropPos = (gv - float2(x, y)) / aspect;
                float drop = S(.05, .03, length(dropPos));

                // trail dots
                float2 trailPos = (gv - float2(x, t * .25)) / aspect;
                trailPos.y = (frac(trailPos.y * 8) - 0.5) /  8;
                float trail = S(.03, .01, length(trailPos));

                // trail path
                float fogtrail = S(-0.05, 0.05, dropPos.y);
                fogtrail *= S(0.5, y, gv.y);
                trail *= fogtrail;

                fogtrail *= S(.05, .04, abs(dropPos.x));

                // col += fogtrail * .5;
                // col += trail;
                // col += drop;

                float2 offs = drop * dropPos + trail * trailPos;

                return float3(offs, fogtrail);
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float t = fmod(_Time.y + _T, 7200);

                float4 col = 0;

                float3 drops = Layer(i.uv, t);
                drops += Layer(i.uv * 1.23 + 7.54, t);
                drops += Layer(i.uv * 1.32 + 1.54, t);
                drops += Layer(i.uv * 1.56 + 3.54, t);

                float fade = 1 - saturate(fwidth(i.uv) * 60);

                float blur = _Blur * 7 * (1 - drops.z * fade);
                blur *= .01;
                
                float2 projUv = i.grabUv.xy / i.grabUv.w;
                projUv += drops.xy * _Distortion;

                // average 
                const float numSamples = 32;
                float a = N21(i.uv) * 6.2831;
                for (float i = 0; i < numSamples; i++) {
                    float2 offs = float2(sin(a), cos(a)) * blur;
                    float d = frac(sin((i+1) * 536.1) * 5000.0);
                    d = sqrt(d);
                    offs *= d;

                    col += tex2D(_GrabTexture, projUv + offs);
                    a++;
                }
                col /= numSamples;

                return col*0.9;
            }
            ENDCG
        }
    }
}
