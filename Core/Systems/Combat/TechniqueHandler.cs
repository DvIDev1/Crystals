using Crystals.Core.Systems.Combat.Techniques;
using Microsoft.Xna.Framework.Input;
using Terraria;
using Terraria.ModLoader;

namespace Crystals.Core.Systems.Combat;

public class TechniqueHandler : ModPlayer
{
    
    public static ModKeybind UseTechniqueKey;

    public Technique CurrentTechnique => new TestTechnique();
    
    public override void Load()
    {
        UseTechniqueKey = KeybindLoader.RegisterKeybind(Mod, "Use Technique ", Keys.LeftControl);
    }

    
    
    
    public override void PreUpdateMovement()
    {
        if (UseTechniqueKey.JustPressed)
        {
            CurrentTechnique.StartTechnique();
            Main.NewText(CurrentTechnique.Time);
        }
        
        CurrentTechnique.Update();        // ReSharper disable once CompareOfFloatsByEqualityOperator
        if (CurrentTechnique.Time == CurrentTechnique.StartTime() + CurrentTechnique.Duration())
        {
            Main.NewText("False");
        }
    }

    public override void PreUpdate()
    {
        
        
    }

    public override bool CanBeHitByNPC(NPC npc, ref int cooldownSlot)
    {
        return !CurrentTechnique.canDodge;
    }

    public override bool CanBeHitByProjectile(Projectile proj)
    {
        return !CurrentTechnique.canDodge;
    }
}