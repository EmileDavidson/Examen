// Made with Amplify Shader Editor v1.9.1.5
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Game/Unlit Transparant"
{
	Properties
	{
		[NoScaleOffset]_MainTexture("MainTexture", 2D) = "white" {}
		_MixColor("MixColor", Color) = (1,1,1,1)
		[NoScaleOffset][SingleLineTexture]_RoughnessMap("RoughnessMap", 2D) = "white" {}
		_ShineSize("ShineSize", Float) = 0.95
		_ShineStranght("ShineStranght", Float) = 0.3
		_Alpha("Alpha", Float) = 0
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Transparent"  "Queue" = "Transparent+0" "IgnoreProjector" = "True" "IsEmissive" = "true"  }
		Cull Back
		CGINCLUDE
		#include "UnityCG.cginc"
		#include "UnityPBSLighting.cginc"
		#include "Lighting.cginc"
		#pragma target 3.0
		struct Input
		{
			float2 uv_texcoord;
			float3 worldPos;
			float3 worldNormal;
		};

		uniform float4 _MixColor;
		uniform sampler2D _MainTexture;
		uniform float _ShineSize;
		uniform float _ShineStranght;
		uniform sampler2D _RoughnessMap;
		uniform float _Alpha;

		inline half4 LightingUnlit( SurfaceOutput s, half3 lightDir, half atten )
		{
			return half4 ( 0, 0, 0, s.Alpha );
		}

		void surf( Input i , inout SurfaceOutput o )
		{
			float2 uv_MainTexture4 = i.uv_texcoord;
			float4 tex2DNode4 = tex2D( _MainTexture, uv_MainTexture4 );
			float4 MainTexture45 = ( ( _MixColor * _MixColor.a ) * tex2DNode4 );
			float4 color15 = IsGammaSpace() ? float4(0.09803922,0.04313726,0.4901961,0.6862745) : float4(0.009721218,0.003346536,0.2050788,0.6862745);
			float3 ase_worldPos = i.worldPos;
			#if defined(LIGHTMAP_ON) && UNITY_VERSION < 560 //aseld
			float3 ase_worldlightDir = 0;
			#else //aseld
			float3 ase_worldlightDir = normalize( UnityWorldSpaceLightDir( ase_worldPos ) );
			#endif //aseld
			float3 ase_worldNormal = i.worldNormal;
			float dotResult8 = dot( ase_worldlightDir , ase_worldNormal );
			float temp_output_3_0_g1 = ( dotResult8 - 0.46 );
			float Shadow38 = saturate( ( temp_output_3_0_g1 / fwidth( temp_output_3_0_g1 ) ) );
			float dotResult28 = dot( ase_worldlightDir , ase_worldNormal );
			float temp_output_3_0_g2 = ( dotResult28 - _ShineSize );
			float Shine42 = ( saturate( ( temp_output_3_0_g2 / fwidth( temp_output_3_0_g2 ) ) ) * _ShineStranght );
			float2 uv_RoughnessMap35 = i.uv_texcoord;
			o.Emission = ( ( ( ( ( MainTexture45 * ( 1.0 - color15.a ) ) + ( ( MainTexture45 * color15 ) * color15.a ) ) * ( 1.0 - Shadow38 ) ) + ( Shadow38 * MainTexture45 ) ) + ( Shine42 * tex2D( _RoughnessMap, uv_RoughnessMap35 ).r ) ).rgb;
			o.Alpha = saturate( (_Alpha + (tex2DNode4.a - 0.0) * (1.0 - _Alpha) / (1.0 - 0.0)) );
		}

		ENDCG
		CGPROGRAM
		#pragma surface surf Unlit alpha:fade keepalpha fullforwardshadows 

		ENDCG
		Pass
		{
			Name "ShadowCaster"
			Tags{ "LightMode" = "ShadowCaster" }
			ZWrite On
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma target 3.0
			#pragma multi_compile_shadowcaster
			#pragma multi_compile UNITY_PASS_SHADOWCASTER
			#pragma skip_variants FOG_LINEAR FOG_EXP FOG_EXP2
			#include "HLSLSupport.cginc"
			#if ( SHADER_API_D3D11 || SHADER_API_GLCORE || SHADER_API_GLES || SHADER_API_GLES3 || SHADER_API_METAL || SHADER_API_VULKAN )
				#define CAN_SKIP_VPOS
			#endif
			#include "UnityCG.cginc"
			#include "Lighting.cginc"
			#include "UnityPBSLighting.cginc"
			sampler3D _DitherMaskLOD;
			struct v2f
			{
				V2F_SHADOW_CASTER;
				float2 customPack1 : TEXCOORD1;
				float3 worldPos : TEXCOORD2;
				float3 worldNormal : TEXCOORD3;
				UNITY_VERTEX_INPUT_INSTANCE_ID
				UNITY_VERTEX_OUTPUT_STEREO
			};
			v2f vert( appdata_full v )
			{
				v2f o;
				UNITY_SETUP_INSTANCE_ID( v );
				UNITY_INITIALIZE_OUTPUT( v2f, o );
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO( o );
				UNITY_TRANSFER_INSTANCE_ID( v, o );
				Input customInputData;
				float3 worldPos = mul( unity_ObjectToWorld, v.vertex ).xyz;
				half3 worldNormal = UnityObjectToWorldNormal( v.normal );
				o.worldNormal = worldNormal;
				o.customPack1.xy = customInputData.uv_texcoord;
				o.customPack1.xy = v.texcoord;
				o.worldPos = worldPos;
				TRANSFER_SHADOW_CASTER_NORMALOFFSET( o )
				return o;
			}
			half4 frag( v2f IN
			#if !defined( CAN_SKIP_VPOS )
			, UNITY_VPOS_TYPE vpos : VPOS
			#endif
			) : SV_Target
			{
				UNITY_SETUP_INSTANCE_ID( IN );
				Input surfIN;
				UNITY_INITIALIZE_OUTPUT( Input, surfIN );
				surfIN.uv_texcoord = IN.customPack1.xy;
				float3 worldPos = IN.worldPos;
				half3 worldViewDir = normalize( UnityWorldSpaceViewDir( worldPos ) );
				surfIN.worldPos = worldPos;
				surfIN.worldNormal = IN.worldNormal;
				SurfaceOutput o;
				UNITY_INITIALIZE_OUTPUT( SurfaceOutput, o )
				surf( surfIN, o );
				#if defined( CAN_SKIP_VPOS )
				float2 vpos = IN.pos;
				#endif
				half alphaRef = tex3D( _DitherMaskLOD, float3( vpos.xy * 0.25, o.Alpha * 0.9375 ) ).a;
				clip( alphaRef - 0.01 );
				SHADOW_CASTER_FRAGMENT( IN )
			}
			ENDCG
		}
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=19105
Node;AmplifyShaderEditor.CommentaryNode;48;-2224,-224;Inherit;False;913.9999;472.0002;Comment;5;1;2;3;4;45;MainTexture;1,1,1,1;0;0
Node;AmplifyShaderEditor.CommentaryNode;44;-2224,384;Inherit;False;1032.821;461.5133;Comment;8;26;42;33;30;32;29;27;28;Shine;1,1,1,1;0;0
Node;AmplifyShaderEditor.CommentaryNode;40;-2224,944;Inherit;False;914;457;Comment;6;20;21;8;7;9;38;Shadow;1,1,1,1;0;0
Node;AmplifyShaderEditor.FunctionNode;20;-1744,1024;Inherit;False;Step Antialiasing;-1;;1;2a825e80dfb3290468194f83380797bd;0;2;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.DotProductOpNode;8;-1920,1072;Inherit;False;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.WorldSpaceLightDirHlpNode;7;-2176,1072;Inherit;False;False;1;0;FLOAT;0;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.WorldNormalVector;9;-2144,1216;Inherit;False;False;1;0;FLOAT3;0,0,1;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.RegisterLocalVarNode;38;-1536,1008;Inherit;False;Shadow;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.DotProductOpNode;28;-1936,528;Inherit;False;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.WorldSpaceLightDirHlpNode;27;-2176,528;Inherit;False;False;1;0;FLOAT;0;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;32;-1584,480;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;30;-1776,480;Inherit;False;Step Antialiasing;-1;;2;2a825e80dfb3290468194f83380797bd;0;2;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;33;-1760,592;Inherit;False;Property;_ShineStranght;ShineStranght;4;0;Create;True;0;0;0;False;0;False;0.3;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;42;-1424,480;Inherit;False;Shine;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.WorldNormalVector;26;-2176,672;Inherit;False;False;1;0;FLOAT3;0,0,1;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.RangedFloatNode;29;-1968,448;Inherit;False;Property;_ShineSize;ShineSize;3;0;Create;True;0;0;0;False;0;False;0.95;0.95;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;2;-1856,-176;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;3;-1680,-80;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;45;-1536,-80;Inherit;False;MainTexture;-1;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;1;-2176,-176;Inherit;False;Property;_MixColor;MixColor;1;0;Create;True;0;0;0;False;0;False;1,1,1,1;1,1,1,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;4;-2016,16;Inherit;True;Property;_MainTexture;MainTexture;0;1;[NoScaleOffset];Create;True;0;0;0;False;0;False;-1;None;4ecd1ce458f93a248b8547e898b1153e;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;11;144,-64;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.OneMinusNode;12;-32,32;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;10;-32,128;Inherit;False;2;2;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.OneMinusNode;24;-880,-144;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;16;-608,-144;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleAddOpNode;25;-256,-240;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.GetLocalVarNode;46;-880,-240;Inherit;False;45;MainTexture;1;0;OBJECT;;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;22;-608,-240;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;14;-464,-48;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleAddOpNode;31;656,32;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;800,-16;Float;False;True;-1;2;ASEMaterialInspector;0;0;Unlit;Game/Unlit Transparant;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;False;False;False;False;False;False;Back;0;False;;0;False;;False;0;False;;0;False;;False;0;Transparent;0.5;True;True;0;True;Transparent;;Transparent;All;12;all;True;True;True;True;0;False;;False;0;False;;255;False;;255;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;False;2;15;10;25;False;0.5;True;2;5;False;;10;False;;0;0;False;;0;False;;0;False;;0;False;;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;True;Relative;0;;1;-1;-1;-1;0;False;0;0;False;;-1;0;False;;0;0;0;False;0.1;False;;0;False;;False;15;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
Node;AmplifyShaderEditor.GetLocalVarNode;37;-256,32;Inherit;False;38;Shadow;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;47;-256,144;Inherit;False;45;MainTexture;1;0;OBJECT;;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleAddOpNode;13;464,-64;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;34;448,192;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;43;272,192;Inherit;False;42;Shine;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;21;-2032,992;Inherit;False;Constant;_GlobalShadowStrenght;GlobalShadowStrenght;2;0;Create;True;0;0;0;False;0;False;0.46;0.3;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;15;-1136,-128;Inherit;False;Constant;_ShadowColor;ShadowColor;2;0;Create;True;0;0;0;False;0;False;0.09803922,0.04313726,0.4901961,0.6862745;0.09803922,0.04313726,0.4901961,0.6862745;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SaturateNode;52;656,208;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;35;144,288;Inherit;True;Property;_RoughnessMap;RoughnessMap;2;2;[NoScaleOffset];[SingleLineTexture];Create;True;0;0;0;False;0;False;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TFHCRemapNode;56;272,528;Inherit;False;5;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;3;FLOAT;0;False;4;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;53;80,592;Inherit;False;Property;_Alpha;Alpha;5;0;Create;True;0;0;0;False;0;False;0;-2.37;0;0;0;1;FLOAT;0
WireConnection;20;1;21;0
WireConnection;20;2;8;0
WireConnection;8;0;7;0
WireConnection;8;1;9;0
WireConnection;38;0;20;0
WireConnection;28;0;27;0
WireConnection;28;1;26;0
WireConnection;32;0;30;0
WireConnection;32;1;33;0
WireConnection;30;1;29;0
WireConnection;30;2;28;0
WireConnection;42;0;32;0
WireConnection;2;0;1;0
WireConnection;2;1;1;4
WireConnection;3;0;2;0
WireConnection;3;1;4;0
WireConnection;45;0;3;0
WireConnection;11;0;25;0
WireConnection;11;1;12;0
WireConnection;12;0;37;0
WireConnection;10;0;37;0
WireConnection;10;1;47;0
WireConnection;24;0;15;4
WireConnection;16;0;46;0
WireConnection;16;1;15;0
WireConnection;25;0;22;0
WireConnection;25;1;14;0
WireConnection;22;0;46;0
WireConnection;22;1;24;0
WireConnection;14;0;16;0
WireConnection;14;1;15;4
WireConnection;31;0;13;0
WireConnection;31;1;34;0
WireConnection;0;2;31;0
WireConnection;0;9;52;0
WireConnection;13;0;11;0
WireConnection;13;1;10;0
WireConnection;34;0;43;0
WireConnection;34;1;35;1
WireConnection;52;0;56;0
WireConnection;56;0;4;4
WireConnection;56;3;53;0
ASEEND*/
//CHKSM=D04E23CBF823BD7CCD91D487946A92CB2D799BA8