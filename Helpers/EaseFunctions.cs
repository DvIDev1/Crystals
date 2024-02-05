using System;
using Microsoft.Xna.Framework;

namespace Crystals.Helpers;

public class EaseFunctions
{
    public static float EaseIn(float t)
    {
        return t * t;
    }
    
    public static float EaseOutQuad(float x){
        return 1 - (1 - x) * (1 - x);
    }
    
    public static float EaseInOutQuad(float x) {
        return x < 0.5 ? 2 * x * x : 1 - (float)Math.Pow(-2 * x + 2, 2) / 2;
    }
}