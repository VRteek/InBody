// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Mobile Xray Shader created by Bruno Rime 2012
// Free for personal and comercial use

Shader "Mobile/Mobile-XrayEffect" {
	Properties {
		_Color("_Color", Color) = (0,1,0,1)
	    _Inside("_Inside", Range(0,1) ) = 0
	    _Rim("_Rim", Range(0,2) ) = 1.2
	}
	SubShader {
		Tags { "Queue" = "Transparent" }
		LOD 80
		
	Pass {
		Name "Darken"
		Cull off
		Zwrite off
		Blend dstcolor zero
				
		CGPROGRAM
		
		#pragma vertex vert_surf
		#pragma fragment frag_surf
		#pragma fragmentoption ARB_precision_hint_fastest
		//#pragma multi_compile_fwdbase

		#include "HLSLSupport.cginc"
		#include "UnityCG.cginc"


		struct v2f_surf {
			  half4 pos 	: SV_POSITION;
			  fixed4 finalColor : COLOR;
		};
		
		uniform half4 _Color;
		uniform half _Rim;
		uniform half _Inside;

		v2f_surf vert_surf (appdata_base v) {
		v2f_surf o;
			
			o.pos = UnityObjectToClipPos (v.vertex);
			half3 uv = mul( (float3x3)UNITY_MATRIX_IT_MV, v.normal );
			uv = normalize(uv);
			o.finalColor = lerp(half4(1,1,1,1),_Color,saturate(max(1- pow (uv.z,_Rim),_Inside)));
			return o;
		}
		
		fixed4 frag_surf (v2f_surf IN) : COLOR {

			return IN.finalColor;
		}
 
	ENDCG
	}
	
	Pass {
		Name "Lighten"
		Cull off
		Zwrite off
		Blend oneminusdstcolor one
				
		CGPROGRAM
		
		#pragma vertex vert_surf
		#pragma fragment frag_surf
		#pragma fragmentoption ARB_precision_hint_fastest
		//#pragma multi_compile_fwdbase

		#include "HLSLSupport.cginc"
		#include "UnityCG.cginc"


		struct v2f_surf {
			  half4 pos 	: SV_POSITION;
			  fixed4 finalColor : COLOR;
		};
		
		uniform half4 _Color;
		uniform half _Rim;
		uniform half _Inside;

		v2f_surf vert_surf (appdata_base v) {
		v2f_surf o;
			
			o.pos = UnityObjectToClipPos (v.vertex);
			half3 uv = mul( (float3x3)UNITY_MATRIX_IT_MV, v.normal );
			uv = normalize(uv);
			o.finalColor = lerp(half4(0,0,0,0),_Color,saturate(max(1- pow (uv.z,_Rim),_Inside)));
			return o;
		}
		
		fixed4 frag_surf (v2f_surf IN) : COLOR {

			return IN.finalColor;
		}
 
	ENDCG
	}
}

FallBack "Mobile/VertexLit"
}