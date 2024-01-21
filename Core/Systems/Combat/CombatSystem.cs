using System;
using Crytsals.Core.Systems;
using Crytsals.Helpers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Crystals.Core.Systems.Combat;

public class CombatSystem : ModPlayer
{
    
    
    #region Stance

    public int StanceHealthMax => (Player.statLifeMax + Player.statDefense.Positive) / 4;

    public int StanceHealth;

    public float LastHitTime;

    public bool Stunned;
    
    public int StunCooldown;

    public override void PreUpdate()
    {
        LastHitTime++;

        if (Stunned)
        {
            StunCooldown++;
        }
        
        if (StunCooldown >= 120)
        {
            StanceHealth = StanceHealthMax;
            Stunned = false;
            StunCooldown = 0;
        }
    }

    public override void SetControls()
    {
        if (Stunned)
        {
            Player.controlJump = false;
            Player.controlDown = false;
            Player.controlLeft = false;
            Player.controlRight = false;
            Player.controlUp = false;
            Player.controlUseItem = false;
            Player.controlUseTile = false;
            Player.controlThrow = false;
            Player.gravDir = 1f;
        }
    }

    #region StanceRecover

    public override void OnEnterWorld()
    {
        StanceHealth = StanceHealthMax;
    }

    public override void OnRespawn()
    {
        StanceHealth = StanceHealthMax;
    }

    public override void PlayerConnect()
    {
        StanceHealth = StanceHealthMax;
    }

    #endregion

    public void DamageStance(int amount)
    {
        StanceHealth = StanceHealth - amount >= 0 ? StanceHealth - amount : 0;
    }
    

    public override void OnHurt(Player.HurtInfo info)
    {

        LastHitTime = 0;
        DamageStance(info.Damage / 2);

    }

    public override void ModifyHurt(ref Player.HurtModifiers modifiers)
    {
        
        if (Stunned)
        {
            modifiers.Knockback *= 1.25f;
            modifiers.FinalDamage *= 1.5f;
            Stunned = false;
            StanceHealth = StanceHealthMax;
        }
        
        if (StanceHealth <= 0)
        {
            Stunned = true;
            StunCooldown = 0;
        }
    }

    #endregion

    #region Visuals

    public override void ModifyDrawInfo(ref PlayerDrawSet drawInfo)
    {
        
        //Stun Bar
        Vector2 drawPos = drawInfo.drawPlayer.Top;
        Vector2 drawOrigin = drawPos / 2;
            
        Main.EntitySpriteDraw(ModContent.Request<Texture2D>(AssetDir.Mechanics + "StunBar").Value , drawInfo.drawPlayer.Top , null , 
            Color.White, 0 , drawOrigin , 1f  , SpriteEffects.None);
        
    }

    #endregion
    
    
}