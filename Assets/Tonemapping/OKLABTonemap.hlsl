#ifndef OKLABTONEMAP_INCLUDED
#define OKLABTONEMAP_INCLUDED

//https://skia.googlesource.com/skia/+/f95647aa03ca/tests/sksl/intrinsics/Inverse.hlsl?autodive=0%2F%2F%2F%2F%2F%2F%2F%2F%2F%2F

// Returns the determinant of a 2x2 matrix.
float spvDet2x2(float a1, float a2, float b1, float b2)
{
    return a1 * b2 - b1 * a2;
}
// Returns the inverse of a matrix, by using the algorithm of calculating the classical
// adjoint and dividing by the determinant. The contents of the matrix are changed.
float3x3 inverse(float3x3 m)
{
    float3x3 adj;	// The adjoint matrix (inverse after dividing by determinant)
    // Create the transpose of the cofactors, as the classical adjoint of the matrix.
    adj[0][0] =  spvDet2x2(m[1][1], m[1][2], m[2][1], m[2][2]);
    adj[0][1] = -spvDet2x2(m[0][1], m[0][2], m[2][1], m[2][2]);
    adj[0][2] =  spvDet2x2(m[0][1], m[0][2], m[1][1], m[1][2]);
    adj[1][0] = -spvDet2x2(m[1][0], m[1][2], m[2][0], m[2][2]);
    adj[1][1] =  spvDet2x2(m[0][0], m[0][2], m[2][0], m[2][2]);
    adj[1][2] = -spvDet2x2(m[0][0], m[0][2], m[1][0], m[1][2]);
    adj[2][0] =  spvDet2x2(m[1][0], m[1][1], m[2][0], m[2][1]);
    adj[2][1] = -spvDet2x2(m[0][0], m[0][1], m[2][0], m[2][1]);
    adj[2][2] =  spvDet2x2(m[0][0], m[0][1], m[1][0], m[1][1]);
    // Calculate the determinant as a combination of the cofactors of the first row.
    float det = (adj[0][0] * m[0][0]) + (adj[0][1] * m[1][0]) + (adj[0][2] * m[2][0]);
    // Divide the classical adjoint matrix by the determinant.
    // If determinant is zero, matrix is not invertable, so leave it unchanged.
    return (det != 0.0f) ? (adj * (1.0f / det)) : m;
}


/* float3 linear_srgb_to_oklab_float(float3 rgbInput)
{

float l = 0.4122214708f * rgbInput.r + 0.5363325363f * rgbInput.g + 0.0514459929f * rgbInput.b;
float m = 0.2119034982f * rgbInput.r + 0.6806995451f * rgbInput.g + 0.1073969566f * rgbInput.b;
float s = 0.0883024619f *rgbInput.r + 0.2817188376f *rgbInput.g + 0.6299787005f *rgbInput.b;

    l = pow(l,1/3.0f);
    m = pow(m,1/3.0f);
    s = pow(s,1/3.0f);

return  float3( 0.2104542553f*l + 0.7936177850f*m - 0.0040720468f*s,
                        1.9779984951f*l - 2.4285922050f*m + 0.4505937099f*s,
                        0.0259040371f*l + 0.7827717662f*m - 0.8086757660f*s);
}

float3 oklab_to_linear_srgb_float(float3 oklabInput)
{
    float l = oklabInput.r + 0.3963377774f * oklabInput.g + 0.2158037573f * oklabInput.b;
    float m = oklabInput.r - 0.1055613458f * oklabInput.g - 0.0638541728f * oklabInput.b;
    float s = oklabInput.r - 0.0894841775f * oklabInput.g - 1.2914855480f * oklabInput.b;

    l = l*l*l;
    m = m*m*m;
    s = s*s*s;

    return float3(
        +4.0767416621f * l - 3.3077115913f * m + 0.2309699292f * s,
		-1.2684380046f * l + 2.6097574011f * m - 0.3413193965f * s,
		-0.0041960863f * l - 0.7034186147f * m + 1.7076147010f * s);
} */

// Copyright(c) 2022 Björn Ottosson
//
// Permission is hereby granted, free of charge, to any person obtaining a copy of
// this softwareand associated documentation files(the "Software"), to deal in
// the Software without restriction, including without limitation the rights to
// use, copy, modify, merge, publish, distribute, sublicense, and /or sell copies
// of the Software, and to permit persons to whom the Software is furnished to do
// so, subject to the following conditions :
// The above copyright noticeand this permission notice shall be included in all
// copies or substantial portions of the Software.
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.
/* 
const static float softness_scale = 0.1; // controls softness of RGB clipping
const static float offset = 0.85; // controls how colors desaturate as they brighten. 0 results in that colors never fluoresce, 1 in very saturated colors 
const static float chroma_scale = 1.2; // overall scale of chroma */

// Origin: https://knarkowicz.wordpress.com/2016/01/06/aces-filmic-tone-mapping-curve/
// Using this since it was easy to differentiate, same technique would work for any curve 
 float3 s_curve(float3 x)
            {
                float a = 2.51;
                float b = 0.03;
                float c = 2.43;
                float d = 0.59;
                float e = 0.14;
                x = max(x, 0.0);
                return saturate((x * (a * x + b)) / (x * (c * x + d) + e));
            }


// derivative of s-curve
float3 d_s_curve(float3 x)
            {
                float a = 2.51;
                float b = 0.03;
                float c = 2.43;
                float d = 0.59;
                float e = 0.14;
                x = max(x, 0.0);
                float3 r = (x * (c * x + d) + e);
                return (a * x * (d * x + 2.0 * e) + b * (e - c * x * x)) / (r * r);
            }

float3 tonemap_per_channel(float3 c)
{
    return s_curve(c);
}

float2 findCenterAndPurity(float3 x)
{
    // Matrix derived for (c_smooth+s_smooth) to be an approximation of the macadam limit
    // this makes it some kind of g0-like estimate
    float3x3 M = float3x3(
        2.26775149, -1.43293879,  0.1651873,
        -0.98535505,  2.1260072, -0.14065215,
        -0.02501605, -0.26349465,  1.2885107);

    x = mul(x,M);
    
    float x_min = min(x.r,min(x.g,x.b));
    float x_max = max(x.r,max(x.g,x.b));
    
    float c = 0.5*(x_max+x_min);
    float s = (x_max-x_min);
    
    // math trickery to create values close to c and s, but without producing hard edges
    float3 y = (x-c)/s;
    float c_smooth = c + dot(y*y*y, float3(1.0/3.0,1.0/3.0,1.0/3.0))*s;
    float s_smooth = sqrt(dot(x-c_smooth,x-c_smooth)/2.0);
    return float2(c_smooth, s_smooth);
}

float3 toLms(float3 c)
{
    float3x3 rgbToLms = float3x3(
        0.4122214708, 0.5363325363, 0.0514459929,
        0.2119034982, 0.6806995451, 0.1073969566,
        0.0883024619, 0.2817188376, 0.6299787005);

    float3 lms_ = mul(c,rgbToLms);
    return sign(lms_)*pow(abs(lms_), float3(1.0/3.0,1.0/3.0,1.0/3.0));
}

float calculateC(float3 lms)
{
    // Most of this could be precomputed
    // Creating a transform that maps R,G,B in the target gamut to have same distance from grey axis

    float3 lmsR = toLms(float3(1.0,0.0,0.0));
    float3 lmsG = toLms(float3(0.0,1.0,0.0));
    float3 lmsB = toLms(float3(0.0,0.0,1.0));
    
    float3 uDir = (lmsR - lmsG)/sqrt(2.0);
    float3 vDir = (lmsR + lmsG - 2.0*lmsB)/sqrt(6.0);
    
    float3x3 to_uv = inverse(float3x3(
    1.0, uDir.x, vDir.x,
    1.0, uDir.y, vDir.y,
    1.0, uDir.z, vDir.z
    ));
    
    float3 _uv = mul(lms,to_uv);
    
    return sqrt(_uv.y*_uv.y + _uv.z*_uv.z);
    
    float a = 1.9779984951f*lms.x - 2.4285922050f*lms.y + 0.4505937099f*lms.z;
    float b = 0.0259040371f*lms.x + 0.7827717662f*lms.y - 0.8086757660f*lms.z;

    return sqrt(a*a + b*b);
}

float2 calculateMC(float3 c)
{
    float3 lms = toLms(c);
    
    float M = findCenterAndPurity(lms).x; 
    
    return float2(M, calculateC(lms));
}

float2 expandShape(float3 rgb, float2 ST)
{
    float2 MC = calculateMC(rgb);
    float2 STnew = float2((MC.x)/MC.y, (1.0-MC.x)/MC.y);
    STnew = (STnew + 3.0*STnew*STnew*MC.y);
    
    return float2(min(ST.x, STnew.x), min(ST.y, STnew.y));
}

float expandScale(float3 rgb, float2 ST, float scale)
{
    float2 MC = calculateMC(rgb);
    float Cnew = (1.0/((ST.x/(MC.x)) + (ST.y/(1.0-MC.x))));

    return max(MC.y/Cnew, scale);
}

float2 approximateShape(float softness_scale)
{
    float m = -softness_scale*0.2;
    float s = 1.0 + (softness_scale*0.2+softness_scale*0.8);
    
    float2 ST = float2(1000.0,1000.0);
    ST = expandShape(m+s*float3(1.0,0.0,0.0), ST);
    ST = expandShape(m+s*float3(1.0,1.0,0.0), ST);
    ST = expandShape(m+s*float3(0.0,1.0,0.0), ST);
    ST = expandShape(m+s*float3(0.0,1.0,1.0), ST);
    ST = expandShape(m+s*float3(0.0,0.0,1.0), ST);
    ST = expandShape(m+s*float3(1.0,0.0,1.0), ST);
    
    float scale = 0.0;
    scale = expandScale(m+s*float3(1.0,0.0,0.0), ST, scale);
    scale = expandScale(m+s*float3(1.0,1.0,0.0), ST, scale);
    scale = expandScale(m+s*float3(0.0,1.0,0.0), ST, scale);
    scale = expandScale(m+s*float3(0.0,1.0,1.0), ST, scale);
    scale = expandScale(m+s*float3(0.0,0.0,1.0), ST, scale);
    scale = expandScale(m+s*float3(1.0,0.0,1.0), ST, scale);
    
    return ST/scale;
}

float3 tonemap_hue_preserving(float3 c, float offset, float chroma_scale, float softness_scale)
{
    float3x3 toLms = float3x3(
        0.4122214708, 0.5363325363, 0.0514459929,
        0.2119034982, 0.6806995451, 0.1073969566,
        0.0883024619, 0.2817188376, 0.6299787005);
        
    float3x3 fromLms = float3x3(
        4.0767416621f , -3.3077115913, 0.2309699292,
        -1.2684380046f , 2.6097574011, -0.3413193965,
        -0.0041960863f , -0.7034186147, 1.7076147010);
        
    float3 lms_ = mul(toLms,c);
    float3 lms = sign(lms_)*pow(abs(lms_), float3(1.0/3.0,1.0/3.0,1.0/3.0));
    
    float2 MP = findCenterAndPurity(lms);
    
    // apply tone curve
    
    // Approach 1: scale chroma based on derivative of chrome curve
    if (true)
    { 
        float I = (MP.x+(1.0-offset)*MP.y);

        // Remove comment to see what the results are with Oklab L
        //I = dot(lms, float3(0.2104542553f, 0.7936177850f, - 0.0040720468f));
        
        lms = lms*I*I;
        I = I*I*I;
        float3 dLms = lms - I;

        float Icurve = s_curve(float3(I,I,I)).x;
        //Icurve = max(Icurve, 1e-5);
        lms = 1.0f + chroma_scale*dLms*d_s_curve(float3(I,I,I))/Icurve;     
        I = pow(Icurve, 1.0/3.0);

        lms = lms*I;
    }
    
    // Approach 2: Separate color into a whiteness/blackness part, apply scale to them independendtly
    if (false)
    {
        lms = chroma_scale*(lms - MP.x) + MP.x;
    
        float invBlackness = (MP.x+MP.y);
        float whiteness = (MP.x-MP.y);
        
        float invBlacknessC = pow(s_curve(float3(invBlackness*invBlackness*invBlackness,invBlackness*invBlackness*invBlackness,invBlackness*invBlackness*invBlackness)).x, 1.0/3.0);
        float whitenessC = pow(s_curve(float3(whiteness*whiteness*whiteness,whiteness*whiteness*whiteness,whiteness*whiteness*whiteness)).x, 1.0/3.0);
        
        lms = (invBlacknessC+whitenessC)/2.0 + (lms-(invBlackness+whiteness)/2.0)*(invBlacknessC-whitenessC)/(invBlackness-whiteness);
    }
    
    
    // compress to a smooth approximation of the target gamut
    { 
        float M = findCenterAndPurity(lms).x;
        float2 ST = approximateShape(softness_scale); // this can be precomputed, only depends on RGB gamut
        float C_smooth_gamut = (1.0)/((ST.x/(M)) + (ST.y/(1.0-M)));
        float C = calculateC(lms);

        lms = (lms-M)/sqrt(C*C/C_smooth_gamut/C_smooth_gamut+1.0) + M;
    }
    
    float3 rgb = mul(fromLms,lms*lms*lms);

    return rgb;
}

float3 softSaturate(float3 x, float3 a, float softness_scale)
{
    a = clamp(a, 0.0,softness_scale);
    a = 1.0+a;
    x = min(x, a);
    float3 b = (a-1.0)*sqrt(a/(2.0-a));
    return 1.0 - (sqrt((x-a)*(x-a) + b*b) - b)/(sqrt(a*a+b*b)-b);
}

float3 softClipColor(float3 color, float softness_scale)
{
    // soft clip of rgb values to avoid artifacts of hard clipping
    // causes hues distortions, but is a smooth mapping
    // not quite sure this mapping is easy to invert, but should be possible to construct similar ones that do
    
    float grey = 0.2f;
        
    float3 x = color-grey;

    float3 xsgn = sign(x);
    float3 xscale = 0.5 + xsgn*(0.5-grey);

    x = x/xscale;


    float maxRGB = max(color.r, max(color.g, color.b));
    float minRGB = min(color.r, min(color.g, color.b));
 
    float softness_0 = maxRGB/(1.0+softness_scale)*softness_scale; 
    float softness_1 = (1.0-minRGB)/(1.0+softness_scale)*softness_scale;
    
    float3 softness = float3(0.5,0.5,0.5)*(softness_0+softness_1 + xsgn*(softness_1 - softness_0));

    return grey + xscale*xsgn*softSaturate(abs(x), softness, softness_scale);
}

void ToneMap_float(float3 color, float offset, float chroma_scale, float softness_scale,  out float3 Out)
{

if (all(color == 0.0))
{
    Out = float3(0,0,0);
    return;
}


color = tonemap_hue_preserving(color, offset, chroma_scale, softness_scale);
color = softClipColor(color, softness_scale);
//Out = pow(color,1.0/2.2);
Out=color;
}

/* void ConvertRGBOKLAB2_float(float3 colorInput, float contrast, float brightness, float chroma, out float3 colorOutput)
{

float3 oklab = linear_srgb_to_oklab_float(colorInput);

oklab = float3(oklab.r, pow(pow(oklab.g,2) +pow(oklab.b,2),1/2.0), atan2(oklab.b, oklab.g));


oklab.r = brightness*pow(oklab.r,contrast);
//lCh mode

oklab = float3(oklab.r, oklab.g * chroma, oklab.b);


oklab = float3(oklab.r, oklab.g * cos(oklab.b), oklab.g * sin(oklab.b));

colorOutput = oklab_to_linear_srgb_float(oklab);
} */


    #endif

