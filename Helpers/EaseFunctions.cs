
using System;

namespace Crystals.Helpers
{
    public class EaseFunctions
    {
        public static float EaseIn(float t)
        {
            return t * t;
        }
        
        static float Flip(float x)
        {
            return 1 - x;
        }
        
        
        public static float easeInOutQuad(float x) {
            return x < 0.5 ? 2 * x * x : 1 - (float)Math.Pow(-2 * x + 2, 2) / 2;
        }
        
    }
}