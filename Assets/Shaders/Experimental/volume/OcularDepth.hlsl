#ifndef OCULARDEPTH_HLSL
#define OCULARDEPTH_HLSL

#include "Packages/com.jimmycushnie.noisynodes/NoiseShader/HLSL/ClassicNoise3D.hlsl" 

void CalculateOcularDepth_float( float3 RayOrigin, float3 Ray_Direction, float Steps, float Sample_radius, float Scene_Depth, float Atmospheric_Density, float Noise_Scale, out float Optical_Depth)
{

Optical_Depth = 0;

float Step_Length = 0;

if(Scene_Depth < Sample_radius)
{
    Step_Length = Scene_Depth/Steps;
}
else
{
    Step_Length = Sample_radius/Steps;
}

for(int i = 0; i < Steps; i++)
{
 Optical_Depth += cnoise( (RayOrigin + (Ray_Direction * (i * Step_Length)) )  * Noise_Scale);
}

Optical_Depth = (Optical_Depth + 1.15)/2.3; //account for weird stuff going on with cnoise function

Optical_Depth = (Optical_Depth * Step_Length);


Optical_Depth = exp(-Optical_Depth * 0.1);
// transmittance = exp(-(sunRayOpticalDepth + viewRayOpticalDepth) * scatteringCoefficients)
}
#endif


