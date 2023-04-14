// Upgrade NOTE: upgraded instancing buffer 'GameLiquid' to new syntax.

// Made with Amplify Shader Editor v1.9.1.5
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Game/Liquid"
{
	Properties
	{
		_Cutoff( "Mask Clip Value", Float ) = 0.5
		_Float0("Float 0", Float) = 0
		_WobbleZ("WobbleZ", Float) = 0
		_WobbleX("WobbleX", Float) = 0
		_Vector2("Vector 2", Vector) = (0,0,0,0)
		_ObjectRotation("ObjectRotation", Vector) = (0,0,0,0)
		_Vector3("Vector 3", Vector) = (0,1,0,0)
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "TransparentCutout"  "Queue" = "Transparent+0" "IsEmissive" = "true"  }
		Cull Off
		CGPROGRAM
		#include "UnityShaderVariables.cginc"
		#pragma target 2.0
		#pragma multi_compile_instancing
		#pragma surface surf Unlit keepalpha addshadow fullforwardshadows 
		struct Input
		{
			float3 worldPos;
		};

		uniform float3 _Vector2;
		uniform float4 _ObjectRotation;
		uniform float2 _Vector3;
		uniform float _Float0;
		uniform float _Cutoff = 0.5;

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
			float4 color16 = IsGammaSpace() ? float4(0,0,0,0) : float4(0,0,0,0);
			o.Emission = color16.rgb;
			o.Alpha = 1;
			float _WobbleZ_Instance = UNITY_ACCESS_INSTANCED_PROP(_WobbleZ_arr, _WobbleZ);
			float3 ase_vertex3Pos = mul( unity_WorldToObject, float4( i.worldPos , 1 ) );
			float3 rotatedValue19 = RotateAroundAxis( _Vector2, ase_vertex3Pos, float3(1,0,0), 90.0 );
			float _WobbleX_Instance = UNITY_ACCESS_INSTANCED_PROP(_WobbleX_arr, _WobbleX);
			float3 rotatedValue41 = RotateAroundAxis( _Vector2, ase_vertex3Pos, float3(0,0,1), 90.0 );
			float3 ase_worldPos = i.worldPos;
			float4 transform35 = mul(unity_ObjectToWorld,float4( 0,0,0,1 ));
			float4 temp_cast_3 = (180.0).xxxx;
			float4 break56 = (float4( 0,0,0,0 ) + (_ObjectRotation - float4( 0,0,0,0 )) * (float4( 1,1,1,1 ) - float4( 0,0,0,0 )) / (temp_cast_3 - float4( 0,0,0,0 )));
			float temp_output_1_0_g1 = break56.x;
			float temp_output_1_0_g2 = break56.z;
			clip( step( ( ( float4( ( ( _WobbleZ_Instance * rotatedValue19 ) + ( _WobbleX_Instance * rotatedValue41 ) ) , 0.0 ) + ( float4( ase_worldPos , 0.0 ) - transform35 ) ).y + (_Vector3.x + (saturate( ( saturate( (0.0 + (( ( abs( ( ( temp_output_1_0_g1 - floor( ( temp_output_1_0_g1 + 0.5 ) ) ) * 2 ) ) * 2 ) - 1.0 ) - -1.0) * (1.0 - 0.0) / (1.0 - -1.0)) ) + saturate( (0.0 + (( ( abs( ( ( temp_output_1_0_g2 - floor( ( temp_output_1_0_g2 + 0.5 ) ) ) * 2 ) ) * 2 ) - 1.0 ) - -1.0) * (1.0 - 0.0) / (1.0 - -1.0)) ) ) ) - 0.0) * (_Vector3.y - _Vector3.x) / (0.5 - 0.0)) ) , _Float0 ) - _Cutoff );
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=19105
Node;AmplifyShaderEditor.ColorNode;16;234.2724,-145.5465;Inherit;False;Constant;_MainColor;MainColor;2;0;Create;True;0;0;0;False;0;False;0,0,0,0;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.BreakToComponentsNode;8;6.916167,76.70583;Inherit;False;FLOAT4;1;0;FLOAT4;0,0,0,0;False;16;FLOAT;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT;5;FLOAT;6;FLOAT;7;FLOAT;8;FLOAT;9;FLOAT;10;FLOAT;11;FLOAT;12;FLOAT;13;FLOAT;14;FLOAT;15
Node;AmplifyShaderEditor.WorldPosInputsNode;3;-968.8868,110.9455;Inherit;False;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.SimpleAddOpNode;20;-221.53,190.1727;Inherit;True;2;2;0;FLOAT4;0,0,0,0;False;1;FLOAT4;0,0,0,0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;6;-477.6406,97.913;Inherit;False;2;0;FLOAT3;0,0,0;False;1;FLOAT4;0,0,0,0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.ObjectToWorldTransfNode;35;-777.8566,324.03;Inherit;False;1;0;FLOAT4;0,0,0,1;False;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleAddOpNode;23;-229.3339,27.00799;Inherit;True;2;2;0;FLOAT3;0,0,0;False;1;FLOAT4;0,0,0,0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.RangedFloatNode;11;202.0122,195.401;Inherit;False;Property;_Float0;Float 0;1;0;Create;True;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;44;-691.8548,-418.3708;Inherit;False;InstancedProperty;_WobbleX;WobbleX;3;0;Create;True;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;43;-297.2771,-356.8912;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SimpleAddOpNode;46;-121.9996,-429.8643;Inherit;False;2;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.Vector3Node;47;-1037.545,-757.8741;Inherit;False;Constant;_Vector0;Vector 0;4;0;Create;True;0;0;0;False;0;False;1,0,0;0,0,0;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.Vector3Node;48;-932.4658,-349.2534;Inherit;False;Constant;_Vector1;Vector 0;4;0;Create;True;0;0;0;False;0;False;0,0,1;0,0,0;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.RotateAboutAxisNode;19;-781.8366,-640.2727;Inherit;False;False;4;0;FLOAT3;1,0,0;False;1;FLOAT;90;False;2;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RotateAboutAxisNode;41;-702.7023,-214.2945;Inherit;False;False;4;0;FLOAT3;0,0,1;False;1;FLOAT;90;False;2;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.PosVertexDataNode;42;-971.3917,-66.81476;Inherit;False;0;0;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.Vector3Node;49;-1117.886,-197.5325;Inherit;False;Property;_Vector2;Vector 2;4;0;Create;True;0;0;0;False;0;False;0,0,0;0,0,0;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;40;-357.7119,-676.2928;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RangedFloatNode;26;-788.3737,-814.5279;Inherit;False;InstancedProperty;_WobbleZ;WobbleZ;2;0;Create;True;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.PosVertexDataNode;24;-1003.644,-517.1858;Inherit;False;0;0;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;2;1111.386,202.4876;Float;False;True;-1;0;ASEMaterialInspector;0;0;Unlit;Game/Liquid;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Off;0;False;;0;False;;False;0;False;;0;False;;False;0;Custom;0.5;True;True;0;False;TransparentCutout;;Transparent;All;12;all;True;True;True;True;0;False;;False;0;False;;255;False;;255;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;False;2;15;10;25;False;0.5;True;0;0;False;;0;False;;0;0;False;;0;False;;0;False;;0;False;;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;True;Relative;0;;0;-1;-1;-1;0;False;0;0;False;;-1;0;False;;0;0;0;False;0.1;False;;0;False;;False;15;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
Node;AmplifyShaderEditor.BreakToComponentsNode;56;-851.6461,705.4271;Inherit;False;FLOAT4;1;0;FLOAT4;0,0,0,0;False;16;FLOAT;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT;5;FLOAT;6;FLOAT;7;FLOAT;8;FLOAT;9;FLOAT;10;FLOAT;11;FLOAT;12;FLOAT;13;FLOAT;14;FLOAT;15
Node;AmplifyShaderEditor.Vector4Node;51;-1273.693,653.7673;Inherit;False;Property;_ObjectRotation;ObjectRotation;5;0;Create;True;0;0;0;False;0;False;0,0,0,0;0,0,0,0;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.FunctionNode;61;-674.9325,921.7888;Inherit;True;Triangle Wave;-1;;2;51ec3c8d117f3ec4fa3742c3e00d535b;0;1;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TFHCRemapNode;62;-430.1322,913.2887;Inherit;True;5;0;FLOAT;0;False;1;FLOAT;-1;False;2;FLOAT;1;False;3;FLOAT;0;False;4;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;58;-684.567,705.7893;Inherit;True;Triangle Wave;-1;;1;51ec3c8d117f3ec4fa3742c3e00d535b;0;1;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SaturateNode;65;-169.984,740.254;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SaturateNode;66;-129.9841,897.254;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TFHCRemapNode;57;-461.7668,697.2892;Inherit;True;5;0;FLOAT;0;False;1;FLOAT;-1;False;2;FLOAT;1;False;3;FLOAT;0;False;4;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.TFHCRemapNode;53;-1039.646,706.4271;Inherit;False;5;0;FLOAT4;0,0,0,0;False;1;FLOAT4;0,0,0,0;False;2;FLOAT4;1,1,1,1;False;3;FLOAT4;0,0,0,0;False;4;FLOAT4;1,1,1,1;False;1;FLOAT4;0
Node;AmplifyShaderEditor.SaturateNode;67;281.557,825.0078;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;63;122.6556,828.4591;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.StepOpNode;10;784.2261,119.1693;Inherit;True;2;0;FLOAT;0;False;1;FLOAT;0.03;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;68;504.9127,112.3471;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TFHCRemapNode;70;426.8687,441.6987;Inherit;False;5;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0.5;False;3;FLOAT;0;False;4;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.Vector2Node;71;214.8686,481.6987;Inherit;False;Property;_Vector3;Vector 3;6;0;Create;True;0;0;0;False;0;False;0,1;0,1;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.RangedFloatNode;54;-1273.646,869.4272;Inherit;False;Constant;_Float1;Float 1;6;0;Create;True;0;0;0;False;0;False;180;0;0;0;0;1;FLOAT;0
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
WireConnection;70;3;71;1
WireConnection;70;4;71;2
ASEEND*/
//CHKSM=36997634A0EA5F9D4527551F9888EFFB27822355