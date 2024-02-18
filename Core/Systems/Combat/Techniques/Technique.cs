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

    public int Time;

    public virtual float DamageReduction()
    {
        return DamageReduction(); 
    }

    public virtual void PreUpdate() { }

    public virtual void PostUpdate() { }
    
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