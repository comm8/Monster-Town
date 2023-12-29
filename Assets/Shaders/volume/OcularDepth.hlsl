#ifndef OCULARDEPTH_HLSL
#define OCULARDEPTH_HLSL

#include "Packages/com.jimmycushnie.noisynodes/NoiseShader/HLSL/ClassicNoise3D.hlsl" 

void CalculateOcularDepth_float( float3 RayOrigin, float3 Ray_Direction, float Steps, float Scene_Depth, float Step_Length, float Atmospheric_Density, out float Optical_Depth)
{

Optical_Depth = 0;

if(Scene_Depth < (Steps * Step_Length ))
{
    Steps = Scene_Depth/Step_Length;
}

for(int i = 0; i < Steps; i++)
{
    Optical_Depth += cnoise((RayOrigin + (Ray_Direction * i))) * Step_Length;
}

Optical_Depth = (Optical_Depth * Steps)/Step_Length;
 
 Optical_Depth = (cnoise((RayOrigin + Ray_Direction) * 0.1) + 1.15)/2.3;
}
#endif


