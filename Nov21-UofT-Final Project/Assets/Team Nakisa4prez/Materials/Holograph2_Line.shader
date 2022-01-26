// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Holograph2_Line"
{
	Properties
	{
		_Cutoff( "Mask Clip Value", Float ) = 0.5
		_Slice("Slice", Range( -3 , 3)) = 1.327637
		_Range("Range", Range( -3 , 3)) = 3
		[HDR]_HoloColor("Holo Color", Color) = (2,0,1.380392,0)
		_Tint("Tint", Color) = (0,0,0,0)
		_AmbientOcclusion("Ambient Occlusion", 2D) = "white" {}
		_Albedo("Albedo", 2D) = "white" {}
		_Normal("Normal", 2D) = "bump" {}
		_Metallic("Metallic", Range( 0 , 1)) = 0
		_NoiseScale("Noise Scale", Range( 0 , 10)) = 7.055524
		_Smoothness("Smoothness", Range( 0 , 1)) = 0
		_Linethickness("Line thickness", Range( 0 , 1)) = 0.271226
		_VertOffsetStrength("VertOffset Strength", Range( 0 , 1)) = 0
		_Speed("Speed", Float) = 1
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Transparent"  "Queue" = "Transparent+0" "IgnoreProjector" = "True" "IsEmissive" = "true"  }
		Cull Back
		Blend SrcAlpha OneMinusSrcAlpha
		
		CGPROGRAM
		#include "UnityShaderVariables.cginc"
		#pragma target 3.0
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows exclude_path:deferred vertex:vertexDataFunc 
		struct Input
		{
			float2 uv_texcoord;
			float3 worldPos;
		};

		uniform float _Slice;
		uniform float _Range;
		uniform float _VertOffsetStrength;
		uniform float _NoiseScale;
		uniform float _Speed;
		uniform float _Linethickness;
		uniform sampler2D _Normal;
		uniform float4 _Normal_ST;
		uniform sampler2D _Albedo;
		uniform float4 _Albedo_ST;
		uniform sampler2D _AmbientOcclusion;
		uniform float4 _AmbientOcclusion_ST;
		uniform float4 _Tint;
		uniform float4 _HoloColor;
		uniform float _Metallic;
		uniform float _Smoothness;
		uniform float _Cutoff = 0.5;

		void vertexDataFunc( inout appdata_full v, out Input o )
		{
			UNITY_INITIALIZE_OUTPUT( Input, o );
			float3 ase_vertex3Pos = v.vertex.xyz;
			float Y_Slice23 = saturate( ( ( ase_vertex3Pos.y + _Slice ) / _Range ) );
			float2 temp_cast_0 = (_NoiseScale).xx;
			float2 panner75 = ( ( _Time.y * _Speed ) * float2( 0,-1 ) + float2( 0,0 ));
			float2 uv_TexCoord2 = v.texcoord.xy * temp_cast_0 + panner75;
			float Noise16 = step( frac( uv_TexCoord2.y ) , _Linethickness );
			float3 VertexOffset64 = ( ( ( ase_vertex3Pos * Y_Slice23 ) * _VertOffsetStrength ) * Noise16 );
			v.vertex.xyz += VertexOffset64;
			v.vertex.w = 1;
		}

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 uv_Normal = i.uv_texcoord * _Normal_ST.xy + _Normal_ST.zw;
			float3 Normalmap56 = UnpackNormal( tex2D( _Normal, uv_Normal ) );
			o.Normal = Normalmap56;
			float2 uv_Albedo = i.uv_texcoord * _Albedo_ST.xy + _Albedo_ST.zw;
			float2 uv_AmbientOcclusion = i.uv_texcoord * _AmbientOcclusion_ST.xy + _AmbientOcclusion_ST.zw;
			float4 Albedo53 = ( tex2D( _Albedo, uv_Albedo ) * tex2D( _AmbientOcclusion, uv_AmbientOcclusion ) * _Tint );
			o.Albedo = Albedo53.rgb;
			float2 temp_cast_1 = (_NoiseScale).xx;
			float2 panner75 = ( ( _Time.y * _Speed ) * float2( 0,-1 ) + float2( 0,0 ));
			float2 uv_TexCoord2 = i.uv_texcoord * temp_cast_1 + panner75;
			float Noise16 = step( frac( uv_TexCoord2.y ) , _Linethickness );
			float3 ase_vertex3Pos = mul( unity_WorldToObject, float4( i.worldPos , 1 ) );
			float Y_Slice23 = saturate( ( ( ase_vertex3Pos.y + _Slice ) / _Range ) );
			float4 Emission44 = ( ( Noise16 * Y_Slice23 ) * _HoloColor );
			o.Emission = Emission44.rgb;
			o.Metallic = _Metallic;
			o.Smoothness = _Smoothness;
			o.Alpha = 1;
			float Opacitymask32 = ( ( ( 1.0 - Y_Slice23 ) * Noise16 ) - ( Y_Slice23 * 1.0 ) );
			clip( Opacitymask32 - _Cutoff );
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=18934
174;419;1793;828;3655.909;1175.918;1.652297;True;False
Node;AmplifyShaderEditor.CommentaryNode;34;-2859.992,-964.4686;Inherit;False;1683.795;659.4248;this is the noise network;10;2;16;71;72;73;74;75;76;77;78;Noise network;1,1,1,1;0;0
Node;AmplifyShaderEditor.CommentaryNode;28;-2856.459,-198.9217;Inherit;False;1493.302;534.9465;The slice network;7;21;20;22;26;25;27;23;Slice (Y Gradient);1,1,1,1;0;0
Node;AmplifyShaderEditor.RangedFloatNode;77;-2765.322,-448.9065;Inherit;False;Property;_Speed;Speed;13;0;Create;True;0;0;0;False;0;False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleTimeNode;76;-2773.583,-600.9174;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;78;-2570.35,-529.8691;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.PosVertexDataNode;20;-2806.46,-108.3468;Inherit;True;0;0;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;21;-2642.468,101.3205;Inherit;False;Property;_Slice;Slice;1;0;Create;True;0;0;0;False;0;False;1.327637;-1.647059;-3;3;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;22;-2461.01,-148.9216;Inherit;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.PannerNode;75;-2375.379,-556.3057;Inherit;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,-1;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;26;-2307.251,220.0246;Inherit;False;Property;_Range;Range;2;0;Create;True;0;0;0;False;0;False;3;0;-3;3;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;72;-2570.351,-693.4465;Inherit;False;Property;_NoiseScale;Noise Scale;9;0;Create;True;0;0;0;False;0;False;7.055524;0;0;10;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleDivideOpNode;25;-2137.738,-11.55658;Inherit;True;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;2;-2225.375,-886.4844;Inherit;True;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SaturateNode;27;-1803.694,77.68217;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;74;-2000.306,-549.6966;Inherit;False;Property;_Linethickness;Line thickness;11;0;Create;True;0;0;0;False;0;False;0.271226;0.271226;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.FractNode;71;-1963.957,-873.5467;Inherit;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;40;-1297.164,-188.4393;Inherit;False;1191.149;768.7531;Comment;8;24;29;30;31;35;36;37;32;Opacity mask;1,1,1,1;0;0
Node;AmplifyShaderEditor.StepOpNode;73;-1707.85,-830.5873;Inherit;True;2;0;FLOAT;0;False;1;FLOAT;0.23;False;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;70;81.81966,12.66746;Inherit;False;1053.936;585.3355;Comment;8;61;62;63;66;67;69;68;64;Vert offset;1,1,1,1;0;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;23;-1587.154,-13.89651;Inherit;False;Y_Slice;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;47;-972.2576,-932.3337;Inherit;False;892.947;578.165;Comment;6;41;42;43;45;46;44;Emission;1,1,1,1;0;0
Node;AmplifyShaderEditor.GetLocalVarNode;24;-1247.164,-132.6553;Inherit;True;23;Y_Slice;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;16;-1400.194,-905.4542;Inherit;False;Noise;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;62;145.4142,285.1219;Inherit;True;23;Y_Slice;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.PosVertexDataNode;61;131.8196,62.66751;Inherit;True;0;0;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;63;391.5847,135.3329;Inherit;False;2;2;0;FLOAT3;0,0,0;False;1;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.GetLocalVarNode;31;-860.5339,104.5357;Inherit;True;16;Noise;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;60;91.26893,-1018.878;Inherit;False;1053.179;732.9686;Comment;7;50;49;52;53;51;55;56;Base material;1,1,1,1;0;0
Node;AmplifyShaderEditor.GetLocalVarNode;42;-922.2576,-590.5667;Inherit;True;23;Y_Slice;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;67;397.0581,344.7246;Inherit;False;Property;_VertOffsetStrength;VertOffset Strength;12;0;Create;True;0;0;0;False;0;False;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;35;-1225.472,342.5321;Inherit;True;23;Y_Slice;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;29;-996.3176,-136.4391;Inherit;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;41;-891.8448,-875.0916;Inherit;True;16;Noise;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;30;-813.3169,-138.4391;Inherit;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;43;-650.0759,-608.1687;Inherit;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;45;-625.129,-882.3337;Inherit;False;Property;_HoloColor;Holo Color;3;1;[HDR];Create;True;0;0;0;False;0;False;2,0,1.380392,0;2,0,1.380392,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;66;552.5615,136.366;Inherit;False;2;2;0;FLOAT3;0,0,0;False;1;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.GetLocalVarNode;69;701.6119,368.0025;Inherit;True;16;Noise;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;36;-946.8333,326.3132;Inherit;True;2;2;0;FLOAT;0;False;1;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;49;229.7443,-521.5646;Inherit;False;Property;_Tint;Tint;4;0;Create;True;0;0;0;False;0;False;0,0,0,0;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;52;141.2689,-968.8784;Inherit;True;Property;_Albedo;Albedo;6;0;Create;True;0;0;0;False;0;False;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;50;145.3233,-755.1763;Inherit;True;Property;_AmbientOcclusion;Ambient Occlusion;5;0;Create;True;0;0;0;False;0;False;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;51;525.3323,-840.5465;Inherit;True;3;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;68;752.0473,172.08;Inherit;False;2;2;0;FLOAT3;0,0,0;False;1;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;46;-390.6632,-611.0891;Inherit;True;2;2;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;37;-568.533,63.17579;Inherit;True;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;55;523.6565,-547.6462;Inherit;True;Property;_Normal;Normal;7;0;Create;True;0;0;0;False;0;False;-1;None;None;True;0;False;bump;Auto;True;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RegisterLocalVarNode;53;755.5631,-947.4496;Inherit;False;Albedo;-1;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;44;-303.3106,-880.0342;Inherit;True;Emission;-1;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;64;911.7559,180.653;Inherit;False;VertexOffset;-1;True;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;32;-330.013,76.92665;Inherit;False;Opacitymask;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;56;920.4477,-544.9098;Inherit;True;Normalmap;-1;True;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.GetLocalVarNode;57;1459.916,-888.4891;Inherit;False;56;Normalmap;1;0;OBJECT;;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RangedFloatNode;58;1376.116,-698.089;Inherit;False;Property;_Metallic;Metallic;8;0;Create;True;0;0;0;False;0;False;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;54;1469.418,-1006.947;Inherit;False;53;Albedo;1;0;OBJECT;;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;59;1379.917,-607.489;Inherit;False;Property;_Smoothness;Smoothness;10;0;Create;True;0;0;0;False;0;False;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;65;1454.055,-351.4631;Inherit;False;64;VertexOffset;1;0;OBJECT;;False;1;FLOAT3;0
Node;AmplifyShaderEditor.GetLocalVarNode;33;1457.824,-476.4863;Inherit;False;32;Opacitymask;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;17;1472.953,-775.2742;Inherit;False;44;Emission;1;0;OBJECT;;False;1;COLOR;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;1762.318,-853.384;Float;False;True;-1;2;ASEMaterialInspector;0;0;Standard;Holograph2_Line;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Custom;0.5;True;True;0;True;Transparent;;Transparent;ForwardOnly;18;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;2;5;False;-1;10;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;True;Relative;0;;0;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;False;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;78;0;76;0
WireConnection;78;1;77;0
WireConnection;22;0;20;2
WireConnection;22;1;21;0
WireConnection;75;1;78;0
WireConnection;25;0;22;0
WireConnection;25;1;26;0
WireConnection;2;0;72;0
WireConnection;2;1;75;0
WireConnection;27;0;25;0
WireConnection;71;0;2;2
WireConnection;73;0;71;0
WireConnection;73;1;74;0
WireConnection;23;0;27;0
WireConnection;16;0;73;0
WireConnection;63;0;61;0
WireConnection;63;1;62;0
WireConnection;29;0;24;0
WireConnection;30;0;29;0
WireConnection;30;1;31;0
WireConnection;43;0;41;0
WireConnection;43;1;42;0
WireConnection;66;0;63;0
WireConnection;66;1;67;0
WireConnection;36;0;35;0
WireConnection;51;0;52;0
WireConnection;51;1;50;0
WireConnection;51;2;49;0
WireConnection;68;0;66;0
WireConnection;68;1;69;0
WireConnection;46;0;43;0
WireConnection;46;1;45;0
WireConnection;37;0;30;0
WireConnection;37;1;36;0
WireConnection;53;0;51;0
WireConnection;44;0;46;0
WireConnection;64;0;68;0
WireConnection;32;0;37;0
WireConnection;56;0;55;0
WireConnection;0;0;54;0
WireConnection;0;1;57;0
WireConnection;0;2;17;0
WireConnection;0;3;58;0
WireConnection;0;4;59;0
WireConnection;0;10;33;0
WireConnection;0;11;65;0
ASEEND*/
//CHKSM=C15647E20928E8081072BC2CA21E6A47179F5A1E