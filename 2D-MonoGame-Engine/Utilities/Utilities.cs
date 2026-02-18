using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Vector2 = System.Numerics.Vector2;

namespace _2D_MonoGame_Engine.Utilities;

public static class Utilities
{
    
    public static void ShuffleList<T>(this List<T> list)
    {
        var orderedEnumerable = list.OrderBy(x => Random.Shared.Next());
        
        list.Clear();
        list.AddRange(orderedEnumerable);
    }

    public static void AddToString(this string str, string toAdd)
    {
        str += toAdd;
    }

    public static T ClearAction<T>(this Action<T> action)
    {
        return default;
    }

    
    public static Vector2 MultiplyVectorByVector(this Vector2 vector, Vector2 multiplier)
    {
        return vector * multiplier;
    }

    public static float OneValueDistance(this float value1, float value2)
    {
        return MathF.Abs(value1 - value2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector2 RoundVector(this Vector2 vector)
    {
        var x = MathF.Round(vector.X);
        var y = MathF.Round(vector.Y);
        
        vector.X = x;
        vector.Y = y;
        
        return vector;
    }


    public static bool IsTileEmpty(this Rectangle rect, Color[] textureData, int width)
    {
        //Check the pixel y coordinates based on current y location and where the rectangle
        for (int pixelY = rect.Y; pixelY < rect.Bottom; pixelY++)
        {
            //Check the pixel x coordinates
            for (int pixelX = rect.X; pixelX < rect.Right; pixelX++)
            {
                //Get the pixel
             Color pixelColor = textureData[pixelY * width + pixelX];

             //If the alpha is greater then 0, we have a non transparent pixel
             if (pixelColor.A > 0) return true;

            }
        }

        return false;
    }
}