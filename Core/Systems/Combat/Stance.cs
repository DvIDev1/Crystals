﻿using Crystals.Core.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace Crystals.Core.Systems.Combat;

public class Stance : ModPlayer
{
    public int StanceHealthMax => (Player.statLifeMax + Player.statDefense.Positive * 2) / 4;

    public int StanceHealth;
    
    public bool Stunned;
    
    public int StunCooldown;

    public override void DrawEffects(PlayerDrawSet drawInfo, ref float r, ref float g, ref float b, ref float a, ref bool fullBright)
    {
        if(Stunned)
        {
            r = 0.4f;
            g = 0.4f;
            b = 0.4f;
        }
    }

    public override void PreUpdate()
    {
        if (Player.GetModPlayer<PlayerState>().CurrentState == States.Idle)
        {
            //TODO No instant regen
            StanceHealth = StanceHealthMax;
        }
        
        if (Stunned)
        {
            StunCooldown++;
        }
        
        if (StunCooldown >= 180)
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

    public override void OnHitByNPC(NPC npc, Player.HurtInfo hurtInfo)
    {
        DamageStance(hurtInfo.Damage / 2);
    }

    public override void OnHitByProjectile(Projectile proj, Player.HurtInfo hurtInfo)
    {
        DamageStance(hurtInfo.Damage / 3);
    }

    public override void ModifyHurt(ref Player.HurtModifiers modifiers)
    {
        
        if (Stunned)
        {
            modifiers.Knockback *= 2f;
            modifiers.FinalDamage *= 1.5f;
            Stunned = false;
            StanceHealth = StanceHealthMax;
            Player.GetModPlayer<Stamina>().StatStamina += 10;
        }
        
        if (StanceHealth <= 0 || Player.GetModPlayer<TechniqueHandler>().Blocking && 
            Player.GetModPlayer<Stamina>().StatStamina <= Player.GetModPlayer<TechniqueHandler>().CurrentTechnique.MinimalStamina())
        {
            Stunned = true;
            StunCooldown = 0;
        }
    }
    
    public float[] Timer = new[] { 0f, 1f, 2f };

    public int StunFrame = 0;
    
    public override void ModifyDrawInfo(ref PlayerDrawSet drawInfo)
    {
        
        //Timer & Animator 


        if (Stunned)
        {
            Timer[0]++;
            if (Timer[0] == 20f)
            {
                if (StunFrame != 8)
                {
                    StunFrame++;
                }
                else
                {
                    StunFrame = 0;
                }

                Timer[0] = 0;
            }
        
            StunFrame = (int)MathHelper.Clamp(StunFrame, 0, 8);
        }
        
        
        //Stun Bar
        if (Stunned)
        {
            Vector2 drawPos = drawInfo.drawPlayer.Top - Main.screenPosition + new Vector2(0 , drawInfo.drawPlayer.gfxOffY - 20);
            SpriteEffects spriteEffects = SpriteEffects.None;
            if (drawInfo.drawPlayer.gravDir == -1)
            {
                drawPos = drawInfo.drawPlayer.Bottom - Main.screenPosition + new Vector2(0 , drawInfo.drawPlayer.gfxOffY + 20);
                spriteEffects = SpriteEffects.FlipVertically;
            }

            
            Texture2D texture = ModContent.Request<Texture2D>(AssetDir.Mechanics + "StunBar").Value;
            Rectangle sourceRectangle = new Rectangle(0, 30 * StunFrame, 64, 30);
            Vector2 drawOrigin = sourceRectangle.Size() / 2f;

            Main.EntitySpriteDraw(texture , drawPos, sourceRectangle, 
                Color.White, drawInfo.drawPlayer.fullRotation , drawOrigin , 
                drawInfo.drawPlayer.Size / drawInfo.drawPlayer.DefaultSize  , spriteEffects);
        }
        
    }

}