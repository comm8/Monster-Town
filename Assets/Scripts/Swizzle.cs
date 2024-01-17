
using UnityEngine;

public static class Swizzle
{
    public static Vector3 SetX(this Vector3 v, float x)
    {
        return new Vector3(x, v.y, v.z);
    }

    public static Vector3 SetY(this Vector3 v, float y)
    {
        return new Vector3(v.x, y, v.z);
    }

    public static Vector3 SetZ(this Vector3 v, float z)
    {
        return new Vector3(v.x, v.y, z);
    }

    public static Vector2 SetX(this Vector2 v, float x)
    {
        return new Vector2(x, v.y);
    }

    public static Vector2 SetY(this Vector2 v, float y)
    {
        return new Vector2(v.x, y);
    }

    public static Vector3 SetZ(this Vector2 v, float z)
    {
        return new Vector3(v.x, v.y, z);
    }


    //swizzle of size 1

    public static Vector3 _xxx(this float a) { return new Vector3(a,a,a); }
    public static Vector3 _x0x(this float a) { return new Vector3(a, 0, a); }
    public static Vector3 _x1x(this float a) { return new Vector3(a, 1, a); }
    public static Vector3 _00x(this float a) { return new Vector3(0, 0, a); }
    public static Vector3 _x00(this float a) { return new Vector3(a, 0, 0); }
    public static Vector3 _x01(this float a) { return new Vector3(a, 0, 1); }
    public static Vector3 _x10(this float a) { return new Vector3(a, 1, 0); }
    public static Vector3 _x11(this float a) { return new Vector3(a, 1, 1); }
    public static Vector3 _10x(this float a) { return new Vector3(1, 0, a); }
    public static Vector3 _01x(this float a) { return new Vector3(0, 1, a); }

    public static Vector3 _0x0(this float a) { return new Vector3(0, a, 0); }

    // swizzle of size 2
    public static Vector2 _xx(this Vector2 a) { return new Vector2(a.x, a.x); }
    public static Vector2 _xy(this Vector2 a) { return new Vector2(a.x, a.y); }
    public static Vector2 _x0(this Vector2 a) { return new Vector2(a.x, 0); }
    public static Vector2 _x1(this Vector2 a) { return new Vector2(a.x, 1); }
    public static Vector2 _yx(this Vector2 a) { return new Vector2(a.y, a.x); }
    public static Vector2 _yy(this Vector2 a) { return new Vector2(a.y, a.y); }
    public static Vector2 _y0(this Vector2 a) { return new Vector2(a.y, 0); }
    public static Vector2 _y1(this Vector2 a) { return new Vector2(a.y, 1); }
    public static Vector2 _0x(this Vector2 a) { return new Vector2(0, a.x); }
    public static Vector2 _0y(this Vector2 a) { return new Vector2(0, a.y); }
    public static Vector2 _1x(this Vector2 a) { return new Vector2(1, a.x); }
    public static Vector2 _1y(this Vector2 a) { return new Vector2(1, a.y); }
    // swizzle of size 3
    public static Vector3 _xxx(this Vector2 a) { return new Vector3(a.x, a.x, a.x); }
    public static Vector3 _xxy(this Vector2 a) { return new Vector3(a.x, a.x, a.y); }
    public static Vector3 _xx0(this Vector2 a) { return new Vector3(a.x, a.x, 0); }
    public static Vector3 _xx1(this Vector2 a) { return new Vector3(a.x, a.x, 1); }
    public static Vector3 _xyx(this Vector2 a) { return new Vector3(a.x, a.y, a.x); }
    public static Vector3 _xyy(this Vector2 a) { return new Vector3(a.x, a.y, a.y); }
    public static Vector3 _xy0(this Vector2 a) { return new Vector3(a.x, a.y, 0); }
    public static Vector3 _xy1(this Vector2 a) { return new Vector3(a.x, a.y, 1); }
    public static Vector3 _x0x(this Vector2 a) { return new Vector3(a.x, 0, a.x); }
    public static Vector3 _x0y(this Vector2 a) { return new Vector3(a.x, 0, a.y); }
    public static Vector3 _x00(this Vector2 a) { return new Vector3(a.x, 0, 0); }
    public static Vector3 _x01(this Vector2 a) { return new Vector3(a.x, 0, 1); }
    public static Vector3 _x1x(this Vector2 a) { return new Vector3(a.x, 1, a.x); }
    public static Vector3 _x1y(this Vector2 a) { return new Vector3(a.x, 1, a.y); }
    public static Vector3 _x10(this Vector2 a) { return new Vector3(a.x, 1, 0); }
    public static Vector3 _x11(this Vector2 a) { return new Vector3(a.x, 1, 1); }
    public static Vector3 _yxx(this Vector2 a) { return new Vector3(a.y, a.x, a.x); }
    public static Vector3 _yxy(this Vector2 a) { return new Vector3(a.y, a.x, a.y); }
    public static Vector3 _yx0(this Vector2 a) { return new Vector3(a.y, a.x, 0); }
    public static Vector3 _yx1(this Vector2 a) { return new Vector3(a.y, a.x, 1); }
    public static Vector3 _yyx(this Vector2 a) { return new Vector3(a.y, a.y, a.x); }
    public static Vector3 _yyy(this Vector2 a) { return new Vector3(a.y, a.y, a.y); }
    public static Vector3 _yy0(this Vector2 a) { return new Vector3(a.y, a.y, 0); }
    public static Vector3 _yy1(this Vector2 a) { return new Vector3(a.y, a.y, 1); }
    public static Vector3 _y0x(this Vector2 a) { return new Vector3(a.y, 0, a.x); }
    public static Vector3 _y0y(this Vector2 a) { return new Vector3(a.y, 0, a.y); }
    public static Vector3 _y00(this Vector2 a) { return new Vector3(a.y, 0, 0); }
    public static Vector3 _y01(this Vector2 a) { return new Vector3(a.y, 0, 1); }
    public static Vector3 _y1x(this Vector2 a) { return new Vector3(a.y, 1, a.x); }
    public static Vector3 _y1y(this Vector2 a) { return new Vector3(a.y, 1, a.y); }
    public static Vector3 _y10(this Vector2 a) { return new Vector3(a.y, 1, 0); }
    public static Vector3 _y11(this Vector2 a) { return new Vector3(a.y, 1, 1); }
    public static Vector3 _0xx(this Vector2 a) { return new Vector3(0, a.x, a.x); }
    public static Vector3 _0xy(this Vector2 a) { return new Vector3(0, a.x, a.y); }
    public static Vector3 _0x0(this Vector2 a) { return new Vector3(0, a.x, 0); }
    public static Vector3 _0x1(this Vector2 a) { return new Vector3(0, a.x, 1); }
    public static Vector3 _0yx(this Vector2 a) { return new Vector3(0, a.y, a.x); }
    public static Vector3 _0yy(this Vector2 a) { return new Vector3(0, a.y, a.y); }
    public static Vector3 _0y0(this Vector2 a) { return new Vector3(0, a.y, 0); }
    public static Vector3 _0y1(this Vector2 a) { return new Vector3(0, a.y, 1); }
    public static Vector3 _00x(this Vector2 a) { return new Vector3(0, 0, a.x); }
    public static Vector3 _00y(this Vector2 a) { return new Vector3(0, 0, a.y); }
    public static Vector3 _01x(this Vector2 a) { return new Vector3(0, 1, a.x); }
    public static Vector3 _01y(this Vector2 a) { return new Vector3(0, 1, a.y); }
    public static Vector3 _1xx(this Vector2 a) { return new Vector3(1, a.x, a.x); }
    public static Vector3 _1xy(this Vector2 a) { return new Vector3(1, a.x, a.y); }
    public static Vector3 _1x0(this Vector2 a) { return new Vector3(1, a.x, 0); }
    public static Vector3 _1x1(this Vector2 a) { return new Vector3(1, a.x, 1); }
    public static Vector3 _1yx(this Vector2 a) { return new Vector3(1, a.y, a.x); }
    public static Vector3 _1yy(this Vector2 a) { return new Vector3(1, a.y, a.y); }
    public static Vector3 _1y0(this Vector2 a) { return new Vector3(1, a.y, 0); }
    public static Vector3 _1y1(this Vector2 a) { return new Vector3(1, a.y, 1); }
    public static Vector3 _10x(this Vector2 a) { return new Vector3(1, 0, a.x); }
    public static Vector3 _10y(this Vector2 a) { return new Vector3(1, 0, a.y); }
    public static Vector3 _11x(this Vector2 a) { return new Vector3(1, 1, a.x); }
    public static Vector3 _11y(this Vector2 a) { return new Vector3(1, 1, a.y); }


    // swizzle of size 2
    public static Vector2 _xx(this Vector3 a) { return new Vector2(a.x, a.x); }
    public static Vector2 _xy(this Vector3 a) { return new Vector2(a.x, a.y); }
    public static Vector2 _xz(this Vector3 a) { return new Vector2(a.x, a.z); }
    public static Vector2 _x0(this Vector3 a) { return new Vector2(a.x, 0); }
    public static Vector2 _x1(this Vector3 a) { return new Vector2(a.x, 1); }
    public static Vector2 _yx(this Vector3 a) { return new Vector2(a.y, a.x); }
    public static Vector2 _yy(this Vector3 a) { return new Vector2(a.y, a.y); }
    public static Vector2 _yz(this Vector3 a) { return new Vector2(a.y, a.z); }
    public static Vector2 _y0(this Vector3 a) { return new Vector2(a.y, 0); }
    public static Vector2 _y1(this Vector3 a) { return new Vector2(a.y, 1); }
    public static Vector2 _zx(this Vector3 a) { return new Vector2(a.z, a.x); }
    public static Vector2 _zy(this Vector3 a) { return new Vector2(a.z, a.y); }
    public static Vector2 _zz(this Vector3 a) { return new Vector2(a.z, a.z); }
    public static Vector2 _z0(this Vector3 a) { return new Vector2(a.z, 0); }
    public static Vector2 _z1(this Vector3 a) { return new Vector2(a.z, 1); }
    public static Vector2 _0x(this Vector3 a) { return new Vector2(0, a.x); }
    public static Vector2 _0y(this Vector3 a) { return new Vector2(0, a.y); }
    public static Vector2 _0z(this Vector3 a) { return new Vector2(0, a.z); }
    public static Vector2 _1x(this Vector3 a) { return new Vector2(1, a.x); }
    public static Vector2 _1y(this Vector3 a) { return new Vector2(1, a.y); }
    public static Vector2 _1z(this Vector3 a) { return new Vector2(1, a.z); }
    //  of size 3
    public static Vector3 _xxx(this Vector3 a) { return new Vector3(a.x, a.x, a.x); }
    public static Vector3 _xxy(this Vector3 a) { return new Vector3(a.x, a.x, a.y); }
    public static Vector3 _xxz(this Vector3 a) { return new Vector3(a.x, a.x, a.z); }
    public static Vector3 _xx0(this Vector3 a) { return new Vector3(a.x, a.x, 0); }
    public static Vector3 _xx1(this Vector3 a) { return new Vector3(a.x, a.x, 1); }
    public static Vector3 _xyx(this Vector3 a) { return new Vector3(a.x, a.y, a.x); }
    public static Vector3 _xyy(this Vector3 a) { return new Vector3(a.x, a.y, a.y); }
    public static Vector3 _xyz(this Vector3 a) { return new Vector3(a.x, a.y, a.z); }
    public static Vector3 _xy0(this Vector3 a) { return new Vector3(a.x, a.y, 0); }
    public static Vector3 _xy1(this Vector3 a) { return new Vector3(a.x, a.y, 1); }
    public static Vector3 _xzx(this Vector3 a) { return new Vector3(a.x, a.z, a.x); }
    public static Vector3 _xzy(this Vector3 a) { return new Vector3(a.x, a.z, a.y); }
    public static Vector3 _xzz(this Vector3 a) { return new Vector3(a.x, a.z, a.z); }
    public static Vector3 _xz0(this Vector3 a) { return new Vector3(a.x, a.z, 0); }
    public static Vector3 _xz1(this Vector3 a) { return new Vector3(a.x, a.z, 1); }
    public static Vector3 _x0x(this Vector3 a) { return new Vector3(a.x, 0, a.x); }
    public static Vector3 _x0y(this Vector3 a) { return new Vector3(a.x, 0, a.y); }
    public static Vector3 _x0z(this Vector3 a) { return new Vector3(a.x, 0, a.z); }
    public static Vector3 _x00(this Vector3 a) { return new Vector3(a.x, 0, 0); }
    public static Vector3 _x01(this Vector3 a) { return new Vector3(a.x, 0, 1); }
    public static Vector3 _x1x(this Vector3 a) { return new Vector3(a.x, 1, a.x); }
    public static Vector3 _x1y(this Vector3 a) { return new Vector3(a.x, 1, a.y); }
    public static Vector3 _x1z(this Vector3 a) { return new Vector3(a.x, 1, a.z); }
    public static Vector3 _x10(this Vector3 a) { return new Vector3(a.x, 1, 0); }
    public static Vector3 _x11(this Vector3 a) { return new Vector3(a.x, 1, 1); }
    public static Vector3 _yxx(this Vector3 a) { return new Vector3(a.y, a.x, a.x); }
    public static Vector3 _yxy(this Vector3 a) { return new Vector3(a.y, a.x, a.y); }
    public static Vector3 _yxz(this Vector3 a) { return new Vector3(a.y, a.x, a.z); }
    public static Vector3 _yx0(this Vector3 a) { return new Vector3(a.y, a.x, 0); }
    public static Vector3 _yx1(this Vector3 a) { return new Vector3(a.y, a.x, 1); }
    public static Vector3 _yyx(this Vector3 a) { return new Vector3(a.y, a.y, a.x); }
    public static Vector3 _yyy(this Vector3 a) { return new Vector3(a.y, a.y, a.y); }
    public static Vector3 _yyz(this Vector3 a) { return new Vector3(a.y, a.y, a.z); }
    public static Vector3 _yy0(this Vector3 a) { return new Vector3(a.y, a.y, 0); }
    public static Vector3 _yy1(this Vector3 a) { return new Vector3(a.y, a.y, 1); }
    public static Vector3 _yzx(this Vector3 a) { return new Vector3(a.y, a.z, a.x); }
    public static Vector3 _yzy(this Vector3 a) { return new Vector3(a.y, a.z, a.y); }
    public static Vector3 _yzz(this Vector3 a) { return new Vector3(a.y, a.z, a.z); }
    public static Vector3 _yz0(this Vector3 a) { return new Vector3(a.y, a.z, 0); }
    public static Vector3 _yz1(this Vector3 a) { return new Vector3(a.y, a.z, 1); }
    public static Vector3 _y0x(this Vector3 a) { return new Vector3(a.y, 0, a.x); }
    public static Vector3 _y0y(this Vector3 a) { return new Vector3(a.y, 0, a.y); }
    public static Vector3 _y0z(this Vector3 a) { return new Vector3(a.y, 0, a.z); }
    public static Vector3 _y00(this Vector3 a) { return new Vector3(a.y, 0, 0); }
    public static Vector3 _y01(this Vector3 a) { return new Vector3(a.y, 0, 1); }
    public static Vector3 _y1x(this Vector3 a) { return new Vector3(a.y, 1, a.x); }
    public static Vector3 _y1y(this Vector3 a) { return new Vector3(a.y, 1, a.y); }
    public static Vector3 _y1z(this Vector3 a) { return new Vector3(a.y, 1, a.z); }
    public static Vector3 _y10(this Vector3 a) { return new Vector3(a.y, 1, 0); }
    public static Vector3 _y11(this Vector3 a) { return new Vector3(a.y, 1, 1); }
    public static Vector3 _zxx(this Vector3 a) { return new Vector3(a.z, a.x, a.x); }
    public static Vector3 _zxy(this Vector3 a) { return new Vector3(a.z, a.x, a.y); }
    public static Vector3 _zxz(this Vector3 a) { return new Vector3(a.z, a.x, a.z); }
    public static Vector3 _zx0(this Vector3 a) { return new Vector3(a.z, a.x, 0); }
    public static Vector3 _zx1(this Vector3 a) { return new Vector3(a.z, a.x, 1); }
    public static Vector3 _zyx(this Vector3 a) { return new Vector3(a.z, a.y, a.x); }
    public static Vector3 _zyy(this Vector3 a) { return new Vector3(a.z, a.y, a.y); }
    public static Vector3 _zyz(this Vector3 a) { return new Vector3(a.z, a.y, a.z); }
    public static Vector3 _zy0(this Vector3 a) { return new Vector3(a.z, a.y, 0); }
    public static Vector3 _zy1(this Vector3 a) { return new Vector3(a.z, a.y, 1); }
    public static Vector3 _zzx(this Vector3 a) { return new Vector3(a.z, a.z, a.x); }
    public static Vector3 _zzy(this Vector3 a) { return new Vector3(a.z, a.z, a.y); }
    public static Vector3 _zzz(this Vector3 a) { return new Vector3(a.z, a.z, a.z); }
    public static Vector3 _zz0(this Vector3 a) { return new Vector3(a.z, a.z, 0); }
    public static Vector3 _zz1(this Vector3 a) { return new Vector3(a.z, a.z, 1); }
    public static Vector3 _z0x(this Vector3 a) { return new Vector3(a.z, 0, a.x); }
    public static Vector3 _z0y(this Vector3 a) { return new Vector3(a.z, 0, a.y); }
    public static Vector3 _z0z(this Vector3 a) { return new Vector3(a.z, 0, a.z); }
    public static Vector3 _z00(this Vector3 a) { return new Vector3(a.z, 0, 0); }
    public static Vector3 _z01(this Vector3 a) { return new Vector3(a.z, 0, 1); }
    public static Vector3 _z1x(this Vector3 a) { return new Vector3(a.z, 1, a.x); }
    public static Vector3 _z1y(this Vector3 a) { return new Vector3(a.z, 1, a.y); }
    public static Vector3 _z1z(this Vector3 a) { return new Vector3(a.z, 1, a.z); }
    public static Vector3 _z10(this Vector3 a) { return new Vector3(a.z, 1, 0); }
    public static Vector3 _z11(this Vector3 a) { return new Vector3(a.z, 1, 1); }
    public static Vector3 _0xx(this Vector3 a) { return new Vector3(0, a.x, a.x); }
    public static Vector3 _0xy(this Vector3 a) { return new Vector3(0, a.x, a.y); }
    public static Vector3 _0xz(this Vector3 a) { return new Vector3(0, a.x, a.z); }
    public static Vector3 _0x0(this Vector3 a) { return new Vector3(0, a.x, 0); }
    public static Vector3 _0x1(this Vector3 a) { return new Vector3(0, a.x, 1); }
    public static Vector3 _0yx(this Vector3 a) { return new Vector3(0, a.y, a.x); }
    public static Vector3 _0yy(this Vector3 a) { return new Vector3(0, a.y, a.y); }
    public static Vector3 _0yz(this Vector3 a) { return new Vector3(0, a.y, a.z); }
    public static Vector3 _0y0(this Vector3 a) { return new Vector3(0, a.y, 0); }
    public static Vector3 _0y1(this Vector3 a) { return new Vector3(0, a.y, 1); }
    public static Vector3 _0zx(this Vector3 a) { return new Vector3(0, a.z, a.x); }
    public static Vector3 _0zy(this Vector3 a) { return new Vector3(0, a.z, a.y); }
    public static Vector3 _0zz(this Vector3 a) { return new Vector3(0, a.z, a.z); }
    public static Vector3 _0z0(this Vector3 a) { return new Vector3(0, a.z, 0); }
    public static Vector3 _0z1(this Vector3 a) { return new Vector3(0, a.z, 1); }
    public static Vector3 _00x(this Vector3 a) { return new Vector3(0, 0, a.x); }
    public static Vector3 _00y(this Vector3 a) { return new Vector3(0, 0, a.y); }
    public static Vector3 _00z(this Vector3 a) { return new Vector3(0, 0, a.z); }
    public static Vector3 _01x(this Vector3 a) { return new Vector3(0, 1, a.x); }
    public static Vector3 _01y(this Vector3 a) { return new Vector3(0, 1, a.y); }
    public static Vector3 _01z(this Vector3 a) { return new Vector3(0, 1, a.z); }
    public static Vector3 _1xx(this Vector3 a) { return new Vector3(1, a.x, a.x); }
    public static Vector3 _1xy(this Vector3 a) { return new Vector3(1, a.x, a.y); }
    public static Vector3 _1xz(this Vector3 a) { return new Vector3(1, a.x, a.z); }
    public static Vector3 _1x0(this Vector3 a) { return new Vector3(1, a.x, 0); }
    public static Vector3 _1x1(this Vector3 a) { return new Vector3(1, a.x, 1); }
    public static Vector3 _1yx(this Vector3 a) { return new Vector3(1, a.y, a.x); }
    public static Vector3 _1yy(this Vector3 a) { return new Vector3(1, a.y, a.y); }
    public static Vector3 _1yz(this Vector3 a) { return new Vector3(1, a.y, a.z); }
    public static Vector3 _1y0(this Vector3 a) { return new Vector3(1, a.y, 0); }
    public static Vector3 _1y1(this Vector3 a) { return new Vector3(1, a.y, 1); }
    public static Vector3 _1zx(this Vector3 a) { return new Vector3(1, a.z, a.x); }
    public static Vector3 _1zy(this Vector3 a) { return new Vector3(1, a.z, a.y); }
    public static Vector3 _1zz(this Vector3 a) { return new Vector3(1, a.z, a.z); }
    public static Vector3 _1z0(this Vector3 a) { return new Vector3(1, a.z, 0); }
    public static Vector3 _1z1(this Vector3 a) { return new Vector3(1, a.z, 1); }
    public static Vector3 _10x(this Vector3 a) { return new Vector3(1, 0, a.x); }
    public static Vector3 _10y(this Vector3 a) { return new Vector3(1, 0, a.y); }
    public static Vector3 _10z(this Vector3 a) { return new Vector3(1, 0, a.z); }
    public static Vector3 _11x(this Vector3 a) { return new Vector3(1, 1, a.x); }
    public static Vector3 _11y(this Vector3 a) { return new Vector3(1, 1, a.y); }
    public static Vector3 _11z(this Vector3 a) { return new Vector3(1, 1, a.z); }
}
