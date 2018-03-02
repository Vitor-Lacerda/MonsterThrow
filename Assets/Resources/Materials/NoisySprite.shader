Shader "Custom/NoisySprite"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_NoiseTex("Noise Texture", 2D) = "black" {}
		_Magnitude("Magnitude", Range(0,0.1)) = 0
	}
	SubShader
	{


		Tags {"Queue"="Transparent" "RenderType"="Transparent" }

		ZWrite Off
		Blend SrcAlpha OneMinusSrcAlpha


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
				float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
			};

			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv;
				return o;
			}
			
			sampler2D _MainTex;
			sampler2D _NoiseTex;
			float _Magnitude;

			fixed4 frag (v2f i) : SV_Target
			{
				float2 noiseValue = tex2D(_NoiseTex, i.uv).rg;
				noiseValue = (noiseValue*2)-1;
				noiseValue = noiseValue*_Magnitude;

				fixed4 nCol = tex2D(_MainTex, i.uv+noiseValue);

				return nCol;
			}
			ENDCG
		}
	}
}
