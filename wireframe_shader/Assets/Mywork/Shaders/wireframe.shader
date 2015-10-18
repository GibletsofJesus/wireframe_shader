Shader "Custom/wireframe" {

	Properties 
	{
		_SubBaseTex("Sub Base Texture", 2D) = "white" {}
		_SubAlpha("Sub Texture Alpha", range(0, 1)) = 1
		_SubTint("Sub Texture Tint Color", Color) = (1, 1, 1, 1)
		
		_BaseTex("Base Texture", 2D) = "white" {}
		_Alpha("Base Texture Alpha", range(0, 1)) = 1
		_Tint("Base Texture Tint Color", Color) = (1, 1, 1, 1)
		
		_MainTex("WF Texture", 2D) = "white" {}
		_Fill ("WF Texture Alpha", range(0,1)) = 0 //Bools are not supported in Shader Lab. YES, REALLY.
		_Color ("WF Color", Color) = (1,1,1,1)
		_Thickness ("WF Thickness", range(0,10)) = 1
	}

	SubShader
		{

			Tags{ "RenderType" = "Transparent" "Queue" = "Transparent" }

			CGPROGRAM
				#pragma surface surf Lambert alpha

				sampler2D _SubBaseTex;
				fixed4 _SubTint;
				float _SubAlpha;

				struct Input {
					float2 uv_SubBaseTex;
				};

				void surf(Input IN, inout SurfaceOutput o) {
					fixed4 c = tex2D(_SubBaseTex, IN.uv_SubBaseTex)* _SubTint;
					o.Albedo = c.rgb * _SubTint;
					//o.Alpha = c.a;
					o.Alpha = _SubAlpha;
				}
		
			ENDCG
			
			CGPROGRAM
				#pragma surface surf Lambert alpha

				sampler2D _BaseTex;
				fixed4 _Color, _Tint;
				float _Alpha;

				struct Input {
					float2 uv_BaseTex;
				};

				void surf(Input IN, inout SurfaceOutput o) {
					fixed4 c = tex2D(_BaseTex, IN.uv_BaseTex) * _Tint;
					o.Albedo = _Tint;//c.rgb * _Tint;
					o.Alpha = c.a;
					//o.Alpha = _Alpha;
				}
		
			ENDCG

		Pass
		{
			Tags { "RenderType"="Transparent" "Queue"="Transparent" }

			Blend SrcAlpha OneMinusSrcAlpha //Alpha blending 
			//ZWrite Off //Culling disabled
			LOD 200
			
			CGPROGRAM
				#pragma target 5.0
				#include "UnityCG.cginc"
				#include "wireframeFunctions.cginc"
				#pragma vertex vert
				#pragma fragment frag
				#pragma geometry geom

				// Vertex Shader
				wf_v2g vert(appdata_base v)
				{
					//v.color.a = 0.2f;
					return wf_vert(v);
				}
				
				// Geometry Shader
				[maxvertexcount(3)]
				void geom(triangle wf_v2g p[3], inout TriangleStream<wf_g2f> triStream)
				{
					wf_geom( p, triStream);
				}
				
				// Fragment Shader
				float4 frag(wf_g2f input) : COLOR
				{	
					//return wf_frag(input);
					float4 col = wf_frag(input);
					if( col.a < 0.5f ) discard;
					else col.a = 1.0f;
					
					return col;
				}
			
			ENDCG
		}
	}
} 
