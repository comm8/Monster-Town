Shader "Unlit/General"
{

	SubShader{
		Tags{"RenderPipeline" = "UniversalPipeline"}

		Pass {
			Name "ForwardLit"
			Tags{"LightMode" = "UniversalForward"}


			HLSLPROGRAM

			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary.Lighting.hlsl"

			struct Attributes {
				float3 position : POSITION;
				};

				void Vertex(Attributes input)

			ENDHLSL

			}



		}
		 

}
