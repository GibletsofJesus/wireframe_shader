//Algorithms and shaders based on code from this journal
//http://cgg-journal.com/2008-2/06/index.html

#ifndef WIREFRAMEFUNCTIONS
#define WIREFRAMEFUNCTIONS

#include "UnityCG.cginc"

// DATA STRUCTURES //
// Vertex to Geometry
struct wf_v2g
{
	float4	pos		: POSITION;		// vertex position
	//float4	color	: COLOR;
	float2  uv		: TEXCOORD0;	// vertex uv coordinate
};

// Geometry to  wf_fragment
struct wf_g2f
{
	float4	pos		: POSITION;		// fragment position
	//float4  color	: COLOR;
	float2	uv		: TEXCOORD0;	// fragment uv coordinate
	float3  dist	: TEXCOORD1;	// distance to each edge of the triangle
};

// PARAMETERS //

//float4 _Texture_ST;			// For the Main Tex UV transform
float _Thickness = 1;		// Thickness of the wireframe line rendering
float4 _Color = {1,1,1,1};	// Color of the line
float4 _MainTex_ST;			// For the Main Tex UV transform
sampler2D _MainTex;			// Texture used for the line
float _Fill;

// SHADER PROGRAMS //
// Vertex Shader

//appdata_base: vertex consists of position, normal and one texture coordinate.
//appdata_tan: vertex consists of position, tangent, normal and one texture coordinate.
//appdata_full: vertex consists of position, tangent, normal, four texture coordinates and color.

wf_v2g wf_vert(appdata_base v)
{
	wf_v2g output; //This is our output
	//output.color.a = 0.1f;
	output.pos =  mul(UNITY_MATRIX_MVP, v.vertex); //pos = (Current model * view * projection matrix) * vertex data
	output.uv = TRANSFORM_TEX (v.texcoord, _MainTex);//v.texcoord;

	return output;
}

// Geometry Shader
[maxvertexcount(3)]
void wf_geom(triangle wf_v2g p[3], inout TriangleStream<wf_g2f> triStream)
{
	//points in screen space
	
	//_ScreenParams is standard ShaderLab built in value
	float2 p0 = _ScreenParams.xy * p[0].pos.xy / p[0].pos.w;
	float2 p1 = _ScreenParams.xy * p[1].pos.xy / p[1].pos.w;
	float2 p2 = _ScreenParams.xy * p[2].pos.xy / p[2].pos.w;
	
	//edge vectors
	float2 v0 = p2 - p1;
	float2 v1 = p2 - p0;
	float2 v2 = p1 - p0;

	//area of the triangle
	
	//abs is kinda like a toUpper, but for numbers.
 	float area = abs(v1.x*v2.y - v1.y * v2.x);

	//values based on distance to the edges
	float dist0 = area / length(v0);
	float dist1 = area / length(v1);
	float dist2 = area / length(v2);
	
	wf_g2f pIn;
	
	//add the first point
	pIn.pos = p[0].pos;
	pIn.uv = p[0].uv;
	pIn.dist = float3(dist0,0,0);
	triStream.Append(pIn);

	//add the second point
	pIn.pos =  p[1].pos;
	pIn.uv = p[1].uv;
	pIn.dist = float3(0,dist1,0);
	triStream.Append(pIn);
	
	//add the third point
	pIn.pos = p[2].pos;
	pIn.uv = p[2].uv;
	pIn.dist = float3(0,0,dist2);
	triStream.Append(pIn);
}

// Fragment Shader
float4 wf_frag(wf_g2f input) : COLOR
{			
	//find the smallest distance
	float val = min( input.dist.x, min( input.dist.y, input.dist.z));
	
	//calculate power to 2 to thin the line
	val = exp2( -1/_Thickness * val * val );
		
	//blend between the lines and the negative space to give illusion of anti aliasing
	//float4 targetColor = _Color;// *tex2D(_MainTex, input.uv);
	//float4 transCol = tex2D( _MainTex, input.uv);

	float4 targetColor = _Color * tex2D(_MainTex, input.uv);
	float4 transCol = _Color * tex2D(_MainTex, input.uv);
	transCol.a = _Fill;

	return val * targetColor + ( 1 - val ) * transCol;
}


#endif