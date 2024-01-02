Shader "Shaders/General" {

    Properties
    {
	[Header(Surface options)]
    
	[MainColor] _ColorTint("Tint", Color) = (1,1,1,1)
    [MainTexture] _ColorMap ("Color", 2D) = "white" {}
	}


    SubShader{
        	
            // No culling or depth
            //Cull Off ZWrite Off ZTest Always

        Tags{"RenderPipeline" = "UniversalPipeline"}
        

        // Shaders can have several passes which are used to render different data about the material
        // Each pass has it's own vertex and fragment function and shader variant keywords

        Pass {
            Name "ForwardLit" // For debugging
            Tags{"LightMode" = "UniversalForward"} // Pass specific tags. 
            // "UniversalForward" tells Unity this is the main lighting pass of this shader

            HLSLPROGRAM
                     // Register our programmable stage functions
                    #pragma vertex Vertex
                     #pragma fragment Fragment

                     // Include our code file
                     #include "GeneralForwardLitPass.hlsl"
                     ENDHLSL
        }
    }
}