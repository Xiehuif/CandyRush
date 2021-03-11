Shader "Hidden/Slider"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Range("Range",float) = 0
        _ReachColor("ReachColor",Color) = (0,0,0,0)
        _EdgeColor("EdgeColor",Color) = (0,0,0,0)
        _Distance("DectDistance",float) = 0.5
    }
    SubShader
    {
        Tags{"Queue" = "Transparent"}
        Cull Off ZWrite Off ZTest Always

        Pass
        {
            CGPROGRAM
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
                float2 uv[5] : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };
            
            sampler2D _MainTex;
            float4 _MainTex_TexelSize;
            float _Distance;
            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv[0] = v.uv;
                o.uv[1] = v.uv + float2(1,1) * _MainTex_TexelSize.xy * _Distance;
                o.uv[2] = v.uv + float2(-1,-1) * _MainTex_TexelSize.xy * _Distance;
                o.uv[3] = v.uv + float2(-1,1) * _MainTex_TexelSize.xy * _Distance;
                o.uv[4] = v.uv + float2(1,-1) * _MainTex_TexelSize.xy * _Distance;
                return o;
            }

            float _Range;
            fixed4 _ReachColor;
            fixed4 _EdgeColor;
            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex,i.uv[0]);
                clip(col.a);
                fixed l = tex2D(_MainTex,i.uv[1]).a;
                fixed r = tex2D(_MainTex,i.uv[2]).a;
                fixed t = tex2D(_MainTex,i.uv[3]).a;
                fixed b = tex2D(_MainTex,i.uv[4]).a;
                fixed IsSide = l * r * t * b;
                int HasReach = i.uv[0].x > _Range ? 1 : 0;
                fixed4 tmpCol = fixed4(lerp(_ReachColor,tex2D(_MainTex,i.uv[0]),HasReach).rgb,1);
                return lerp(_EdgeColor,tmpCol,IsSide);
            }
            ENDCG
        }
    }
}
