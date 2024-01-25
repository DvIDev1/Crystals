using System;
using System.Drawing;
using Crytsals.Core.Systems;
using Crytsals.Helpers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Color = Microsoft.Xna.Framework.Color;
using Rectangle = Microsoft.Xna.Framework.Rectangle;

namespace Crystals.Core.Systems.Combat;

public class CombatSystem : ModPlayer
{
    
    
    #region Stance

    public int StanceHealthMax => (Player.statLifeMax + Player.statDefense.Positive * 2) / 4;

    public int StanceHealth;

    public float LastHitTime;

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
        LastHitTime++;

        if (LastHitTime >= 60*35)
        {
            StanceHealth = StanceHealthMax;
        }
        
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
        LastHitTime = 0;
        DamageStance(hurtInfo.Damage / 2);
    }

    public override void OnHitByProjectile(Projectile proj, Player.HurtInfo hurtInfo)
    {
        LastHitTime = 0;
        DamageStance(hurtInfo.Damage / 4);
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

    #endregion
    
    
}