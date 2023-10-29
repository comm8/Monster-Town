			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary.Lighting.hlsl"

            struct interpolators {
            float4 positionCS : SV_POSITION;
            }
            
			struct Attributes
                {
				float3 position : POSITION; //Pos in OBJ space
				};

            Interpolators Vertex(Attributes input) {
            VertexPositionInputs posnInputs = GetVertexPositionInputs(input.PositionOS)


            output.positionCS = posnInputs.positionCS;

            return output;

            }



				void Vertex(Attributes input)
                {
                    VertexPositionInputs posnInputs = GetVertexPositionInputs(input.position);


                }
