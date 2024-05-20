using System;
using Unity.Burst;
using UnityEngine;


public static class VectorExtensions
{
    /// <summary>
    /// Returns a modified version of the vector with one or more components modified
    /// </summary>
    /// <param name="x">X component to modify</param>
    /// <param name="y">Y component to modify</param>
    /// <returns>A modified version of the vector with the passed component configurations</returns>
    [BurstCompile]
    public static Vector2 With(this Vector2 point, float? x = null, float? y = null)
    {
        // transform.position = transform.position.Modify(y: 0)
        return new Vector2(x == null ? point.x : x.Value, y == null ? point.y : y.Value);
    }

    /// <summary>
    /// Get a modified version of the vector with one or more components modified
    /// </summary>
    /// <param name="x">X component to modify</param>
    /// <param name="y">Y component to modify</param>
    /// <param name="z">Z component to modify</param>
    /// <returns>A modified version of the vector with the passed component configurations</returns>
    [BurstCompile]
    public static Vector3 With(this Vector3 point, float? x = null, float? y = null, float? z = null)
    {
        // transform.position = transform.position.Modify(z: 0)
        return new Vector3(x == null ? point.x : x.Value, y == null ? point.y : y.Value, z == null ? point.z : z.Value);
    }

    /// <summary>
    /// Get a modified version of the vector with one or more components modified
    /// </summary>
    /// <param name="x">X component to modify</param>
    /// <param name="y">Y component to modify</param>
    /// <param name="z">Z component to modify</param>
    /// <param name="w">W component to modify</param>
    /// <returns>A modified version of the vector with the passed component configurations</returns>

    [BurstCompile]
    public static Vector4 With(this Vector4 point, float? x = null, float? y = null, float? z = null, float? w = null)
    {
        // transform.position = transform.position.Modify(w: 0)
        return new Vector4(x == null ? point.x : x.Value,
            y == null ? point.y : y.Value,
            z == null ? point.z : z.Value,
            w == null ? point.w : w.Value);
    }


    [BurstCompile]
    public static Vector2 Swizzle2(this Vector3 point, string swizzlePattern)
    {
        if (swizzlePattern.Length != 2)
            throw new ArgumentException("Swizzle pattern must be 2 characters long for Vector2.");

        Vector2 result = Vector2.zero;
        for (int i = 0; i < swizzlePattern.Length; i++)
        {
            char c = swizzlePattern[i];
            switch (c)
            {
                case 'x':
                    result[i] = point.x;
                    break;
                case 'y':
                    result[i] = point.y;
                    break;
                case 'z':
                    result[i] = point.z;
                    break;
                case '0':
                    result[i] = 0;
                    break;
                case '1':
                    result[i] = 1;
                    break;
                default:
                    throw new ArgumentException("Invalid swizzle pattern.");
            }
        }
        return result;
    }


    [BurstCompile]
    public static Vector3 Swizzle3(this Vector3 point, string swizzlePattern)
    {
        if (swizzlePattern.Length != 3)
            throw new ArgumentException("Swizzle pattern must be 3 characters long for Vector3.");

        Vector3 result = Vector3.zero;
        for (int i = 0; i < swizzlePattern.Length; i++)
        {
            char c = swizzlePattern[i];
            switch (c)
            {
                case 'x':
                    result[i] = point.x;
                    break;
                case 'y':
                    result[i] = point.y;
                    break;
                case 'z':
                    result[i] = point.z;
                    break;
                case '0':
                    result[i] = 0;
                    break;
                case '1':
                    result[i] = 1;
                    break;
                default:
                    throw new ArgumentException("Invalid swizzle pattern.");
            }
        }
        return result;
    }


    [BurstCompile]
    public static Vector2 Swizzle2(this Vector2 point, string swizzlePattern)
    {
        if (swizzlePattern.Length != 2)
            throw new ArgumentException("Swizzle pattern must be 2 characters long for Vector2.");

        Vector2 result = Vector2.zero;
        for (int i = 0; i < swizzlePattern.Length; i++)
        {
            char c = swizzlePattern[i];
            switch (c)
            {
                case 'x':
                    result[i] = point.x;
                    break;
                case 'y':
                    result[i] = point.y;
                    break;
                case '0':
                    result[i] = 0;
                    break;
                case '1':
                    result[i] = 1;
                    break;
                default:
                    throw new ArgumentException("Invalid swizzle pattern.");
            }
        }
        return result;
    }


    [BurstCompile]
    public static Vector3 Swizzle3(this Vector2 point, string swizzlePattern)
    {
        if (swizzlePattern.Length != 3)
            throw new ArgumentException("Swizzle pattern must be 3 characters long for Vector3.");

        Vector3 result = Vector3.zero;
        for (int i = 0; i < swizzlePattern.Length; i++)
        {
            char c = swizzlePattern[i];
            switch (c)
            {
                case 'x':
                    result[i] = point.x;
                    break;
                case 'y':
                    result[i] = point.y;
                    break;
                case '0':
                    result[i] = 0;
                    break;
                case '1':
                    result[i] = 1;
                    break;
                default:
                    throw new ArgumentException("Invalid swizzle pattern.");
            }
        }
        return result;
    }
}