#ifndef OCULARDEPTH_HLSL
#define OCULARDEPTH_HLSL

#include "Packages/com.jimmycushnie.noisynodes/NoiseShader/HLSL/SimplexNoise3D.hlsl" 

void CalculateOcularDepth_float( float3 RayOrigin, float3 Ray_Direction, float Steps, float Scene_Depth, float Step_Length, float Atmospheric_Density, out float Optical_Depth)
{

Optical_Depth = 0;

for(int i = 0; i < Steps; i++)
{
    Optical_Depth +=  snoise((RayOrigin + (Ray_Direction * i * Step_Length)) * 10) * Step_Length * 0.2;

    if(Scene_Depth >= (i * Step_Length))
    {
    break;
    }

}

Optical_Depth = (Optical_Depth * Steps)/Step_Length;
}

#endif


