using System;
using System.Data;
using System.Threading;
using Terraria;

namespace Crystals.Core.Systems.Combat.Techniques;

public abstract class Technique
{
    public virtual string Name()
    {
        return Name();
    }

    public virtual float StaminaUse()
    {
        return StaminaUse();
    }
    
    public virtual float StaminaPunish()
    {
        return StaminaPunish();
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

    public virtual float MinimalStamina()
    {
        return MinimalStamina();
    }

    public int Time;

    public virtual void Block() { }

    public int TimeInUse;

    public virtual void PreUpdate() { }

    public virtual void PostUpdate() { }

    public virtual float KnockBackReduction() => 1f;

    public virtual float DamageReduction() => 1f;
    
    public virtual TechniqueType TechniqueType()
    {
        throw new NoNullAllowedException();
    }

    public bool Started()
    {
        if (StartValue == 0)
        {
            return false;
        }
        else return true;
    }

    private int StartValue;

    public int UpdateStarted(int i)
    {
        StartValue = i;
        return UpdateStarted(i);
    }  
    
    public void Update()
    {
        //PreUpdate();
        //Time++;
        //canDodge = Time >= StartTime();
        //PostUpdate();
    }

    public virtual void OnStartTechnique()
    {
        
    }
    
    public void StartTechnique()
    {
        OnStartTechnique();
        UpdateStarted(1);
    }

    public virtual void Dodge()
    {
        
    }
}