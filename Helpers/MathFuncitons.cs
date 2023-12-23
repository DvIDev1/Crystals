using System;
using System.Reflection;

namespace Crytsals.Helpers;

public static class MathFunctions
{
    
    public static double SineWave(float x , float amplitude , float phase , float frequency)
    {
        double w = 2 * System.Math.PI * frequency;
        return amplitude * System.Math.Sin(w * x + phase);
    }

}