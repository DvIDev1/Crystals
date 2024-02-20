using Crystals.Content.Techniques;
using Crystals.Core.Systems.Combat.Techniques;
using Microsoft.Xna.Framework.Input;
using Terraria;
using Terraria.ModLoader;

namespace Crystals.Core.Systems.Combat;

public class TechniqueHandler : ModPlayer
{
    
    public static ModKeybind UseTechniqueKey;

    public Technique CurrentTechnique => new Barrier();
    
    public override void Load()
    {
        UseTechniqueKey = KeybindLoader.RegisterKeybind(Mod, "Use Technique ", Keys.LeftControl);
    }

    
    
    
    /*public override void PreUpdateMovement()
    {
        if (UseTechniqueKey.JustPressed)
        {
            CurrentTechnique.StartTechnique();
            Main.NewText(CurrentTechnique.Time);
        }
        
        //CurrentTechnique.Update();        // ReSharper disable once CompareOfFloatsByEqualityOperator
        if (CurrentTechnique.Time == CurrentTechnique.StartTime() + CurrentTechnique.Duration())
        {
            Main.NewText("False");
        }
    }*/

    public bool Blocking;

    public override void SetControls()
    {
        if (Blocking)
        {
            Player.controlJump = false;
            Player.controlDown = false;
            Player.controlLeft = false;
            Player.controlRight = false;
            Player.controlUp = false;
            Player.controlUseItem = false;
            Player.controlUseTile = false;
            Player.controlThrow = false;
        }
    }

    public override void PostUpdate()
    {
        Main.NewText(Player.GetModPlayer<Stamina>().StatStamina);
        if (!Player.GetModPlayer<Stance>().Stunned && Player.GetModPlayer<Stamina>().StatStamina >= CurrentTechnique.MinimalStamina())
        {
            switch (CurrentTechnique.TechniqueType())
            {
            
                case TechniqueType.Hold:
                    if (UseTechniqueKey.Current)
                    {
                        Blocking = true;
                        CurrentTechnique.TimeInUse++;
                        Player.GetModPlayer<Stamina>().ReduceStamina(CurrentTechnique.StaminaUse() * 0.01f);
                    }
                    else Blocking = false;

                    if (UseTechniqueKey.JustReleased)
                    {
                        CurrentTechnique.TimeInUse = 0;
                    }
                
                    break;
            
                case TechniqueType.Press:
                    break;

            }
        }
        
    }

    public override void ModifyHitByNPC(NPC npc, ref Player.HurtModifiers modifiers)
    {
        if (Blocking && Player.GetModPlayer<Stamina>().StatStamina != 0) 
        {
            modifiers.Knockback *= CurrentTechnique.KnockBackReduction();
            modifiers.FinalDamage *= CurrentTechnique.DamageReduction();
            Player.GetModPlayer<Stamina>().ReduceStamina(npc.damage * CurrentTechnique.StaminaPunish());
        }
    }

    public override void ModifyHitByProjectile(Projectile proj, ref Player.HurtModifiers modifiers)
    {
        if (Blocking)
        {
            modifiers.Knockback *= CurrentTechnique.KnockBackReduction();
            modifiers.FinalDamage *= CurrentTechnique.DamageReduction();
            Player.GetModPlayer<Stamina>().ReduceStamina(proj.damage * CurrentTechnique.StaminaPunish());
        }
    }
}