// Upgrade NOTE: upgraded instancing buffer 'GameLiquid' to new syntax.

// Made with Amplify Shader Editor v1.9.1.5
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Game/Liquid"
{
	Properties
	{
		_Float0("Float 0", Float) = 0
		_WobbleZ("WobbleZ", Float) = 0
		_MainColor("MainColor", Color) = (0,0,0,0)
		_WobbleX("WobbleX", Float) = 0
		_PivotOffset("PivotOffset", Vector) = (0,0,0,0)
		_ObjectRotation("ObjectRotation", Vector) = (0,0,0,0)
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Transparent"  "Queue" = "Transparent+0" "IgnoreProjector" = "True" "IsEmissive" = "true"  }
		Cull Off
		CGINCLUDE
		#include "UnityShaderVariables.cginc"
		#include "UnityPBSLighting.cginc"
		#include "Lighting.cginc"
		#pragma target 2.0
		#pragma multi_compile_instancing
		struct Input
		{
			float3 worldPos;
		};

		uniform float4 _MainColor;
		uniform float3 _PivotOffset;
		uniform float4 _ObjectRotation;
		uniform float _Float0;

		UNITY_INSTANCING_BUFFER_START(GameLiquid)
			UNITY_DEFINE_INSTANCED_PROP(float, _WobbleZ)
#define _WobbleZ_arr GameLiquid
			UNITY_DEFINE_INSTANCED_PROP(float, _WobbleX)
#define _WobbleX_arr GameLiquid
		UNITY_INSTANCING_BUFFER_END(GameLiquid)


		float3 RotateAroundAxis( float3 center, float3 original, float3 u, float angle )
		{
			original -= center;
			float C = cos( angle );
			float S = sin( angle );
			float t = 1 - C;
			float m00 = t * u.x * u.x + C;
			float m01 = t * u.x * u.y - S * u.z;
			float m02 = t * u.x * u.z + S * u.y;
			float m10 = t * u.x * u.y + S * u.z;
			float m11 = t * u.y * u.y + C;
			float m12 = t * u.y * u.z - S * u.x;
			float m20 = t * u.x * u.z - S * u.y;
			float m21 = t * u.y * u.z + S * u.x;
			float m22 = t * u.z * u.z + C;
			float3x3 finalMatrix = float3x3( m00, m01, m02, m10, m11, m12, m20, m21, m22 );
			return mul( finalMatrix, original ) + center;
		}


		inline half4 LightingUnlit( SurfaceOutput s, half3 lightDir, half atten )
		{
			return half4 ( 0, 0, 0, s.Alpha );
		}

		void surf( Input i , inout SurfaceOutput o )
		{
			o.Emission = _MainColor.rgb;
			float _WobbleZ_Instance = UNITY_ACCESS_INSTANCED_PROP(_WobbleZ_arr, _WobbleZ);
			float3 ase_vertex3Pos = mul( unity_WorldToObject, float4( i.worldPos , 1 ) );
			float3 rotatedValue19 = RotateAroundAxis( _PivotOffset, ase_vertex3Pos, float3(1,0,0), 90.0 );
			float _WobbleX_Instance = UNITY_ACCESS_INSTANCED_PROP(_WobbleX_arr, _WobbleX);
			float3 rotatedValue41 = RotateAroundAxis( _PivotOffset, ase_vertex3Pos, float3(0,0,1), 90.0 );
			float3 ase_worldPos = i.worldPos;
			float4 transform35 = mul(unity_ObjectToWorld,float4( 0,0,0,1 ));
			float4 temp_cast_3 = (180.0).xxxx;
			float4 break56 = (float4( 0,0,0,0 ) + (_ObjectRotation - float4( 0,0,0,0 )) * (float4( 1,1,1,1 ) - float4( 0,0,0,0 )) / (temp_cast_3 - float4( 0,0,0,0 )));
			float temp_output_1_0_g3 = break56.x;
			float temp_output_1_0_g2 = break56.z;
			float temp_output_10_0 = step( ( ( float4( ( ( _WobbleZ_Instance * rotatedValue19 ) + ( _WobbleX_Instance * rotatedValue41 ) ) , 0.0 ) + ( float4( ase_worldPos , 0.0 ) - transform35 ) ).y + (0.0 + (saturate( ( saturate( (0.0 + (( ( abs( ( ( temp_output_1_0_g3 - floor( ( temp_output_1_0_g3 + 0.5 ) ) ) * 2 ) ) * 2 ) - 1.0 ) - -1.0) * (1.0 - 0.0) / (1.0 - -1.0)) ) + saturate( (0.0 + (( ( abs( ( ( temp_output_1_0_g2 - floor( ( temp_output_1_0_g2 + 0.5 ) ) ) * 2 ) ) * 2 ) - 1.0 ) - -1.0) * (1.0 - 0.0) / (1.0 - -1.0)) ) ) ) - 0.0) * (0.12 - 0.0) / (0.5 - 0.0)) ) , _Float0 );
			o.Alpha = ( temp_output_10_0 * _MainColor.a );
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
			#pragma target 2.0
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
				float3 worldPos : TEXCOORD1;
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
				float3 worldPos = mul( unity_ObjectToWorld, v.vertex ).xyz;
				half3 worldNormal = UnityObjectToWorldNormal( v.normal );
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
				float3 worldPos = IN.worldPos;
				half3 worldViewDir = normalize( UnityWorldSpaceViewDir( worldPos ) );
				surfIN.worldPos = worldPos;
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
Node;AmplifyShaderEditor.BreakToComponentsNode;8;6.916167,76.70583;Inherit;False;FLOAT4;1;0;FLOAT4;0,0,0,0;False;16;FLOAT;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT;5;FLOAT;6;FLOAT;7;FLOAT;8;FLOAT;9;FLOAT;10;FLOAT;11;FLOAT;12;FLOAT;13;FLOAT;14;FLOAT;15
Node;AmplifyShaderEditor.WorldPosInputsNode;3;-968.8868,110.9455;Inherit;False;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.SimpleAddOpNode;20;-221.53,190.1727;Inherit;True;2;2;0;FLOAT4;0,0,0,0;False;1;FLOAT4;0,0,0,0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;6;-477.6406,97.913;Inherit;False;2;0;FLOAT3;0,0,0;False;1;FLOAT4;0,0,0,0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.ObjectToWorldTransfNode;35;-777.8566,324.03;Inherit;False;1;0;FLOAT4;0,0,0,1;False;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleAddOpNode;23;-229.3339,27.00799;Inherit;True;2;2;0;FLOAT3;0,0,0;False;1;FLOAT4;0,0,0,0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.RangedFloatNode;11;202.0122,195.401;Inherit;False;Property;_Float0;Float 0;0;0;Create;True;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;44;-691.8548,-418.3708;Inherit;False;InstancedProperty;_WobbleX;WobbleX;3;0;Create;True;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;43;-297.2771,-356.8912;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SimpleAddOpNode;46;-121.9996,-429.8643;Inherit;False;2;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.Vector3Node;47;-1037.545,-757.8741;Inherit;False;Constant;_Vector0;Vector 0;4;0;Create;True;0;0;0;False;0;False;1,0,0;0,0,0;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.Vector3Node;48;-932.4658,-349.2534;Inherit;False;Constant;_Vector1;Vector 0;4;0;Create;True;0;0;0;False;0;False;0,0,1;0,0,0;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.RotateAboutAxisNode;19;-781.8366,-640.2727;Inherit;False;False;4;0;FLOAT3;1,0,0;False;1;FLOAT;90;False;2;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RotateAboutAxisNode;41;-702.7023,-214.2945;Inherit;False;False;4;0;FLOAT3;0,0,1;False;1;FLOAT;90;False;2;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.PosVertexDataNode;42;-971.3917,-66.81476;Inherit;False;0;0;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;40;-357.7119,-676.2928;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RangedFloatNode;26;-788.3737,-814.5279;Inherit;False;InstancedProperty;_WobbleZ;WobbleZ;1;0;Create;True;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.PosVertexDataNode;24;-1003.644,-517.1858;Inherit;False;0;0;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;2;1111.386,202.4876;Float;False;True;-1;0;ASEMaterialInspector;0;0;Unlit;Game/Liquid;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;False;False;False;False;False;False;Off;0;False;;0;False;;False;0;False;;0;False;;False;0;Transparent;0.5;True;True;0;True;Transparent;;Transparent;All;12;all;True;True;True;True;0;False;;False;0;False;;255;False;;255;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;False;2;15;10;25;False;0.5;True;2;5;False;;10;False;;0;0;False;;0;False;;0;False;;0;False;;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;True;Relative;0;;0;-1;-1;-1;0;False;0;0;False;;-1;0;False;;0;0;0;False;0.1;False;;0;False;;False;15;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
Node;AmplifyShaderEditor.BreakToComponentsNode;56;-851.6461,705.4271;Inherit;False;FLOAT4;1;0;FLOAT4;0,0,0,0;False;16;FLOAT;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT;5;FLOAT;6;FLOAT;7;FLOAT;8;FLOAT;9;FLOAT;10;FLOAT;11;FLOAT;12;FLOAT;13;FLOAT;14;FLOAT;15
Node;AmplifyShaderEditor.FunctionNode;61;-674.9325,921.7888;Inherit;True;Triangle Wave;-1;;2;51ec3c8d117f3ec4fa3742c3e00d535b;0;1;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TFHCRemapNode;62;-430.1322,913.2887;Inherit;True;5;0;FLOAT;0;False;1;FLOAT;-1;False;2;FLOAT;1;False;3;FLOAT;0;False;4;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;58;-684.567,705.7893;Inherit;True;Triangle Wave;-1;;3;51ec3c8d117f3ec4fa3742c3e00d535b;0;1;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SaturateNode;65;-169.984,740.254;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SaturateNode;66;-129.9841,897.254;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TFHCRemapNode;57;-461.7668,697.2892;Inherit;True;5;0;FLOAT;0;False;1;FLOAT;-1;False;2;FLOAT;1;False;3;FLOAT;0;False;4;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.TFHCRemapNode;53;-1039.646,706.4271;Inherit;False;5;0;FLOAT4;0,0,0,0;False;1;FLOAT4;0,0,0,0;False;2;FLOAT4;1,1,1,1;False;3;FLOAT4;0,0,0,0;False;4;FLOAT4;1,1,1,1;False;1;FLOAT4;0
Node;AmplifyShaderEditor.SaturateNode;67;281.557,825.0078;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;63;122.6556,828.4591;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.StepOpNode;10;784.2261,119.1693;Inherit;True;2;0;FLOAT;0;False;1;FLOAT;0.03;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;68;504.9127,112.3471;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;54;-1273.646,869.4272;Inherit;False;Constant;_Float1;Float 1;6;0;Create;True;0;0;0;False;0;False;180;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;16;564.1454,-271.9736;Inherit;False;Property;_MainColor;MainColor;2;0;Create;True;0;0;0;False;0;False;0,0,0,0;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TFHCRemapNode;70;426.8687,441.6987;Inherit;False;5;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0.5;False;3;FLOAT;0;False;4;FLOAT;0.12;False;1;FLOAT;0
Node;AmplifyShaderEditor.Vector4Node;51;-1273.693,653.7673;Inherit;False;Property;_ObjectRotation;ObjectRotation;5;0;Create;True;0;0;0;False;0;False;0,0,0,0;0,0,0,0;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.Vector3Node;49;-1136.886,-175.5325;Inherit;False;Property;_PivotOffset;PivotOffset;4;0;Create;True;0;0;0;False;0;False;0,0,0;0,0,0;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;72;980.8812,469.8116;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
WireConnection;8;0;23;0
WireConnection;6;0;3;0
WireConnection;6;1;35;0
WireConnection;23;0;46;0
WireConnection;23;1;6;0
WireConnection;43;0;44;0
WireConnection;43;1;41;0
WireConnection;46;0;40;0
WireConnection;46;1;43;0
WireConnection;19;0;47;0
WireConnection;19;2;49;0
WireConnection;19;3;24;0
WireConnection;41;0;48;0
WireConnection;41;2;49;0
WireConnection;41;3;42;0
WireConnection;40;0;26;0
WireConnection;40;1;19;0
WireConnection;2;2;16;0
WireConnection;2;9;72;0
WireConnection;2;10;10;0
WireConnection;56;0;53;0
WireConnection;61;1;56;2
WireConnection;62;0;61;0
WireConnection;58;1;56;0
WireConnection;65;0;57;0
WireConnection;66;0;62;0
WireConnection;57;0;58;0
WireConnection;53;0;51;0
WireConnection;53;2;54;0
WireConnection;67;0;63;0
WireConnection;63;0;65;0
WireConnection;63;1;66;0
WireConnection;10;0;68;0
WireConnection;10;1;11;0
WireConnection;68;0;8;1
WireConnection;68;1;70;0
WireConnection;70;0;67;0
WireConnection;72;0;10;0
WireConnection;72;1;16;4
ASEEND*/
//CHKSM=D3103253F2052B624C5ED92E73778A30C9A33A64