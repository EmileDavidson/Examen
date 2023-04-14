// Upgrade NOTE: upgraded instancing buffer 'GameLiquid' to new syntax.

// Made with Amplify Shader Editor v1.9.1.5
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Game/Liquid"
{
	Properties
	{
		_Float0("Float 0", Float) = 0
		_WobbleZ("WobbleZ", Float) = 0
		_WobbleX("WobbleX", Float) = 0
		_Vector2("Vector 2", Vector) = (0,0,0,0)
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
			float _WobbleZ_Instance = UNITY_ACCESS_INSTANCED_PROP(_WobbleZ_arr, _WobbleZ);
			float3 ase_vertex3Pos = mul( unity_WorldToObject, float4( i.worldPos , 1 ) );
			float3 rotatedValue19 = RotateAroundAxis( _Vector2, ase_vertex3Pos, float3(1,0,0), 90.0 );
			float _WobbleX_Instance = UNITY_ACCESS_INSTANCED_PROP(_WobbleX_arr, _WobbleX);
			float3 rotatedValue41 = RotateAroundAxis( _Vector2, ase_vertex3Pos, float3(0,0,1), 90.0 );
			float3 ase_worldPos = i.worldPos;
			float4 transform35 = mul(unity_ObjectToWorld,float4( 0,0,0,1 ));
			float3 temp_cast_2 = (step( ( float4( ( ( _WobbleZ_Instance * rotatedValue19 ) + ( _WobbleX_Instance * rotatedValue41 ) ) , 0.0 ) + ( float4( ase_worldPos , 0.0 ) - transform35 ) ).y , _Float0 )).xxx;
			o.Emission = temp_cast_2;
			o.Alpha = 1;
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
Node;AmplifyShaderEditor.StepOpNode;10;389.5388,93.25549;Inherit;True;2;0;FLOAT;0;False;1;FLOAT;0.03;False;1;FLOAT;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;2;643.6365,43.74874;Float;False;True;-1;0;ASEMaterialInspector;0;0;Unlit;Game/Liquid;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Off;0;False;;0;False;;False;0;False;;0;False;;False;0;Custom;0.5;True;True;0;False;TransparentCutout;;Transparent;All;12;all;True;True;True;True;0;False;;False;0;False;;255;False;;255;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;False;2;15;10;25;False;0.5;True;0;0;False;;0;False;;0;0;False;;0;False;;0;False;;0;False;;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;True;Relative;0;;0;-1;-1;-1;0;False;0;0;False;;-1;0;False;;0;0;0;False;0.1;False;;0;False;;False;15;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
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
WireConnection;8;0;23;0
WireConnection;6;0;3;0
WireConnection;6;1;35;0
WireConnection;23;0;46;0
WireConnection;23;1;6;0
WireConnection;10;0;8;1
WireConnection;10;1;11;0
WireConnection;2;2;10;0
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
ASEEND*/
//CHKSM=3F4040D1682C075E724E68DECA521DE272CC6C90