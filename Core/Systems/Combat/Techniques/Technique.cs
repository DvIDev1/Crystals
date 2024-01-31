using System.Threading;
using Terraria;

namespace Crystals.Core.Systems.Combat;

public abstract class Technique
{
    public virtual string Name()
    {
        return Name();
    }

    public virtual int StaminaUse()
    {
        return StaminaUse();
    }

    public virtual int StartTime()
    {
        return StartTime();
    }
    public virtual int Duration()
    {
        return Duration();
    }

    public float Time;

    public virtual void Update()
    {
        Time++;
    }

    public bool InUse;

    public virtual void StartTechnique()
    {
        Main.NewText(Name + " Technique Used");
    }

    public virtual void Dodge()
    {
        
    }
}