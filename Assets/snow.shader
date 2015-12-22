Shader "Custom/snow" {
    Properties {
		_MainTex ("Base (RGB)", 2D) = "white" {}
    }
	SubShader {
   		Tags { "Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent" }
		ZWrite Off
		Cull Off
		Blend SrcAlpha OneMinusSrcAlpha // alpha blending
		
        Pass {
            CGPROGRAM

            #pragma vertex vert
            #pragma fragment frag
 			#pragma target 3.0
 			
 			#include "UnityCG.cginc"

            uniform sampler2D _MainTex;

 			struct appdata_custom {
				float4 vertex : POSITION;
				float2 texcoord : TEXCOORD0;
			};

 			struct v2f {
 				float4 pos:SV_POSITION;
				float2 uv:TEXCOORD0;
 			};
 			
 			float4x4 _PrevInvMatrix;
			float3   _TargetPosition;
			float    _Range;
			float    _RangeR;
			float    _Size;
			float3   _MoveTotal;
			float3   _CamUp;
   
            v2f vert(appdata_custom v)
            {
				float3 target = _TargetPosition;
				float3 trip;
				float3 mv = v.vertex.xyz;
				mv += _MoveTotal;
				trip = floor( ((target - mv)*_RangeR + 1) * 0.5 );
				trip *= (_Range * 2);
				mv += trip;

				float3 diff = _CamUp * _Size;
				float3 finalposition;
				float3 tv0 = mv;
				tv0.x += sin(mv.x*0.2) * sin(mv.y*0.3) * sin(mv.x*0.9) * sin(mv.y*0.8);
				tv0.z += cos(mv.x*0.1) * cos(mv.y*0.2) * cos(mv.x*0.8) * cos(mv.y*1.2);
				{
					float3 eyeVector = ObjSpaceViewDir(float4(tv0, 0));
					float3 sideVector = normalize(cross(eyeVector,diff));
					tv0 += (v.texcoord.x-0.5f)*sideVector * _Size;
					tv0 += (v.texcoord.y-0.5f)*diff;
					finalposition = tv0;
				}
            	v2f o;
			    o.pos = mul( UNITY_MATRIX_MVP, float4(finalposition,1));
				o.uv = MultiplyUV(UNITY_MATRIX_TEXTURE0, v.texcoord);
            	return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
				return tex2D(_MainTex, i.uv);
            }

            ENDCG
        }
    }
}
