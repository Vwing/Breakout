Shader "Unlit/Forcefield"
{
	Properties
	{
		[HideInInspector]_EnergyColor ("Energy Color", Color) = (1,1,1,0.5)
		[HideInInspector]_Visibility ("Visibility", Float) = 0
		[HideInInspector]_CollisionPoint("Collision Point", Vector) = (0.5,0.5,0,0)
		[HideInInspector]_CollisionTime("Collision Time", Float) = 0
		_MainTex ("Force Field Alpha Map", 2D) = "white" {}
	}
	SubShader
	{
		Tags 
		{
			"Queue"="Transparent" 
			"RenderType"="Opaque" 
		}
		
		LOD 100

		Pass
		{
         	ZWrite Off // don't write to depth buffer, in order not to occlude other objects
         	Blend SrcAlpha OneMinusSrcAlpha // use alpha blending
			
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			// make fog work
			#pragma multi_compile_fog
			
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				UNITY_FOG_COORDS(1)
				float4 vertex : SV_POSITION;
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;
			fixed4 _EnergyColor;
			float2 _CollisionPoint;
			fixed _CollisionTime;
			fixed _Visibility;
			
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				UNITY_TRANSFER_FOG(o,o.vertex);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				
					// sample the texture
					fixed4 col = tex2D(_MainTex, i.uv);					
					
					// Compute the distance between this fragment's uv coords and the collision point
					fixed dist = distance(i.uv, _CollisionPoint.xy);
					
					// Animation effect has four components. (1) there's an expanding blast radius that
					// reveals an expanding ring-shaped section of the sampled texture (with energy color multiplied).
					// (2) There's a secondary ring with a contracting blast radius, to be drawn solid (no texture sampling).
					// (3) We'll draw a border around the perimeter of the force field and fade it out.
					// (4) We'll fade in an "alpha boost" and fade it back out, so that parts of the force field
					// that'd otherwise be transparent will have some visibility for a while.
					
					
					fixed elapsedTime = _Time[1] - _CollisionTime;					
					fixed blastRadius = lerp(0.3, 1.0, elapsedTime * 2);
					fixed secondBlastRadius = lerp(1.0, 0, elapsedTime * 4);
					
					// The amount of alpha boost we give over the entire surface varies with time.
					// Visibility property is user-defined minimum alpha at any time, so we'll take the greater of.
					fixed addAlpha;
					addAlpha = max(_Visibility, 0.1 * (1 - smoothstep(0, 0.5, elapsedTime)));			

					// Ring-shaped section: 0.05 texture units wide: use the texture (and add alpha to fill in holes slightly)														
					if (dist < blastRadius && dist > blastRadius - 0.05) 
						col = fixed4(_EnergyColor.r, _EnergyColor.g, _EnergyColor.b, _EnergyColor.a * col.a * 1 + addAlpha);
					
					// Secondary ring: 0.02 texture units wide: don't use the texture (just Energy color @ 0.25 alpha)
					else if (dist < secondBlastRadius && dist > secondBlastRadius - 0.02)
						col = fixed4(_EnergyColor.r, _EnergyColor.g, _EnergyColor.b, addAlpha * 2.5);
					
					// Bright border around the edges of the force field
					// (Multiple of addAlpha to use animated fade)
					else if (i.uv.x < 0.01 || i.uv.x > 0.99 || i.uv.y < 0.01 || i.uv.y > 0.99)
						col = fixed4(_EnergyColor.r, _EnergyColor.g, _EnergyColor.b, addAlpha * 2);
					
					// If this pixel isn't part of a ring, just add some alpha for visibility:
					else 
						col = fixed4(_EnergyColor.r, _EnergyColor.g, _EnergyColor.b, _EnergyColor.a * addAlpha);	
				
				// apply fog
				UNITY_APPLY_FOG(i.fogCoord, col);				
				return col;
			}
			ENDCG
		}
	}
}
