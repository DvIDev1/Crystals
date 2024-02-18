using Terraria;
using Terraria.ModLoader;

namespace Crystals.Core.Systems.Combat;

public class PlayerState : ModPlayer
{

    public States CurrentState = States.Idle;

    public float LastHitTime;
    
    public override void PostUpdate()
    {
        LastHitTime++;
        
        if (LastHitTime <= 60*35)
        {
            CurrentState = States.Combat;
            return;
        }

        CurrentState = States.Idle;
    }

    public override void OnHitByNPC(NPC npc, Player.HurtInfo hurtInfo)
    {
        LastHitTime = 0;
    }

    public override void OnHitByProjectile(Projectile proj, Player.HurtInfo hurtInfo)
    {
        LastHitTime = 0;
    }
}