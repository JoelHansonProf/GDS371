using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;

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
}