using System;
using Crytsals.Helpers;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace Crytsals.Core.Systems.Combat;

public class CombatSystem : ModPlayer
{

    public float Stamina = 100;

    public float Balance = 0f;

    public float LastHitTime = 0;
    
    public const float RecoverSpeed = 0.05f;

    public static bool ImGrayDabaDee;

    public override void DrawEffects(PlayerDrawSet drawInfo, ref float r, ref float g, ref float b, ref float a, ref bool fullBright)
    {
        if(ImGrayDabaDee == true)
        {
            r = 0.2f;
            g = 0.2f;
            b = 0.2f;
        }
        else
        {
            fullBright = true;
        }
    }

    public override void PreUpdate()
    {
        LastHitTime += RecoverSpeed;
        LastHitTime = MathHelper.Clamp(LastHitTime, 0, 1);
        //Balance = MathHelper.Lerp(Balance, 0, LastHitTime);
        
        Main.NewText(Player.fullRotation  / 2);
    }

    public override void OnHitByNPC(NPC npc, Player.HurtInfo hurtInfo)
    {
        LastHitTime = 0;
        Balance += ((hurtInfo.Damage * (hurtInfo.Knockback * 0.2f)) * hurtInfo.HitDirection);

    }

    public override void OnHitByProjectile(Projectile proj, Player.HurtInfo hurtInfo)
    {
        LastHitTime = 0;
        Balance += (hurtInfo.Damage * (hurtInfo.Knockback * 0.1f)) * hurtInfo.HitDirection;
    }

    public override void ModifyDrawInfo(ref PlayerDrawSet drawInfo)
    {
        drawInfo.drawPlayer.fullRotation = 0f.AngleLerp(Balance, EaseFunctions.EaseIn(LastHitTime));
    }
}