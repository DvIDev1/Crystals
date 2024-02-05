using System.Threading;
using Terraria;

namespace Crystals.Core.Systems.Combat.Techniques;

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

    public bool canDodge;

    public virtual int Cooldown()
    {
        return Cooldown();
    }

    public float Time
    {
        get => StartTime() + Duration() + Cooldown();
        set => throw new System.NotImplementedException();
    }

    public virtual void PreUpdate() { }

    public virtual void PostUpdate() { }
    
    public void Update()
    {
        PreUpdate();
        Time++;
        canDodge = Time >= StartTime();
        PostUpdate();
    }

    public virtual void OnStartTechnique()
    {
        
    }
    
    public void StartTechnique()
    {
        OnStartTechnique();
    }

    public virtual void Dodge()
    {
        
    }
}