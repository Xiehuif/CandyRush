Shader "Hidden/Blur"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _OffsetX("Offset_X",float) = 0
        _OffsetY("Offset_Y",float) = 0
        _Radius("Radius",float) = 0
        _BlurSize("BlurSize",float) = 1
        _IterNumber("IterNum",Int) = 2
        _DirectionX("DirectionX",Range(0,1)) = 0
        _DirectionY("DirectionY",Range(0,1)) = 0
    }
    SubShader
    {
        // No culling or depth
        Cull Off ZWrite Off ZTest Always
        CGINCLUDE
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
        float _DirectionX,_DirectionY,_OffsetX,_OffsetY,_Radius,_BlurSize;
        half Circle(float2 uv)
        {
            float2 new_uv = uv * 2 - 1;
            new_uv.x += _OffsetX;
            new_uv.y += _OffsetY;
            return dot(new_uv,new_uv);
        }
        v2f VerBlur(appdata v)
        {
            v2f o;
            o.vertex = UnityObjectToClipPos(v.vertex);
            o.uv[0] = v.uv;
            o.uv[1] = v.uv + _BlurSize * float2(0,1 * _MainTex_TexelSize.y + _DirectionY);
            o.uv[2] = v.uv + _BlurSize * float2(0,-1 * _MainTex_TexelSize.y+ _DirectionY);
            o.uv[3] = v.uv + _BlurSize * float2(0,2 * _MainTex_TexelSize.y+ _DirectionY);
            o.uv[4] = v.uv + _BlurSize * float2(0,-2 * _MainTex_TexelSize.y+ _DirectionY);
            return o;
        }
        v2f HorBlur(appdata v)
        {
            v2f o;
            o.vertex = UnityObjectToClipPos(v.vertex);
            o.uv[0] = v.uv;
            o.uv[1] = v.uv + _BlurSize * float2(1* _MainTex_TexelSize.x + _DirectionX,0);
            o.uv[2] = v.uv + _BlurSize * float2(-1 * _MainTex_TexelSize.x + _DirectionX,0);
            o.uv[3] = v.uv + _BlurSize * float2(2 * _MainTex_TexelSize.x + _DirectionX,0);
            o.uv[4] = v.uv + _BlurSize * float2(-2 * _MainTex_TexelSize.x + _DirectionX,0);
            return o;
        }
        fixed4 frag(v2f i):SV_Target
        {
            half IsNormal = Circle(i.uv[0]) > pow(_Radius,2) ? 0 : 1;
            half weight[3] ={0.4026,0.2442,0.0545};
            fixed3 sum = tex2D(_MainTex,i.uv[0])*weight[0];
            for(int iter = 1;iter <= 2;iter++)
            {
                sum += tex2D(_MainTex,i.uv[2 * iter - 1]) * weight[iter];
                sum += tex2D(_MainTex,i.uv[2 * iter]) * weight[iter];
            }
            return lerp(fixed4(sum,1.f),tex2D(_MainTex,i.uv[0]),IsNormal);
        }
        ENDCG
        Pass
        {
            CGPROGRAM
            #pragma vertex VerBlur
            #pragma fragment frag
            ENDCG
        }
        Pass
        {
            CGPROGRAM
            #pragma vertex HorBlur
            #pragma fragment frag
            ENDCG
        }
    }
}
