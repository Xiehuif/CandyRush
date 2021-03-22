Shader "Hidden/TrailShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Range("Rang",Range(0,1)) = 0
        [hdr]_EdgeColor("EdgeColor",Color) = (0,0,0,0)
        [hdr]_EdgeColor2("EdgeColor2",Color) = (0,0,0,0)
        _MainColor("MainColor",Color) = (0,0,0,0)
        _TrailColor("TrailColor",Color) = (0,0,0,0)
        _TimeControl("TimeControl",Vector) = (0,0,0,0)
        _Distance("DectDistance",float) = 0.5
    }
    SubShader
    {
        Tags {"Queue" = "Transparent"}
		Blend SrcAlpha OneMinusSrcAlpha
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
            float4 _MainColor,_TrailColor,_EdgeColor,_EdgeColor2,_TimeControl;
            float _Range;
            fixed4 frag (v2f i) : SV_Target
            {
                float control = i.uv[0].x * 2 -  _Range;
                float r = tex2D(_MainTex, i.uv[0]).a;
                fixed4 col = lerp(_MainColor,_TrailColor,control);
                col.a = r ;
                fixed la = tex2D(_MainTex,i.uv[1]).a;
                fixed ra = tex2D(_MainTex,i.uv[2]).a;
                fixed ta = tex2D(_MainTex,i.uv[3]).a;
                fixed ba = tex2D(_MainTex,i.uv[4]).a;
                fixed IsSide = lerp((la + ra + ta + ba)/4,1,r);
                fixed4 blendcol = lerp(_EdgeColor,_EdgeColor2,saturate(IsSide * 5));
                clip(IsSide);
                col = fixed4(lerp(blendcol,col,IsSide).rgb,IsSide);
                return col;
            }
            ENDCG
        }
    }
}
