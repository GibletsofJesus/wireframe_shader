Shader "Custom/wireframe" 
{

	Properties 
	{
		_Opacity ("Opacity", range(0,1)) = 1
		_Fill ("Wireframe fill %", range(0,1)) = 0 //Bools are not supported in Shader Lab. YES, REALLY.
		_Color ("Wireframe Color", Color) = (1,1,1,1)
		_MainTex ("Main Texture", 2D) = "white" {}
		_Thickness ("Thickness", range(0,10)) = 1
	}

	SubShader 
	{
		
			Tags { "RenderType"="Transparent" "Queue"="Transparent" }

				 CGPROGRAM
				     #pragma surface surf Lambert alpha
 
					 sampler2D _MainTex;
					 fixed4 _Color;
					 float _Opacity;
					 
					 struct Input {
					     float2 uv_MainTex;
					 };
					 
					 void surf (Input IN, inout SurfaceOutput o) {
					     fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
					     o.Albedo = c.rgb;
					     o.Alpha = _Opacity;
					 }
			    ENDCG
			    
		Pass
		{
			Tags { "RenderType"="Transparent" "Queue"="Transparent" }

			Blend SrcAlpha OneMinusSrcAlpha //Alpha blending 
			ZWrite Off //Culling disabled
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
					return wf_frag(input);
				}
			
			ENDCG
		}
	}
} 
