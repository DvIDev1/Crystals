using System;
using System.Collections.Generic;
using Crystals.Helpers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using Projectile = IL.Terraria.Projectile;

namespace Crystals.Content.Foresta.Items.Weapons.Melee.Sunwirl
{
    public class Sunwirl : ModItem
    {
        public override void SetStaticDefaults() {
            DisplayName.SetDefault("Sunwirl");
            Tooltip.SetDefault("An Yo-Yo infused with Sun Energy ignites enemies");
            
            ItemID.Sets.Yoyo[Item.type] = true;
            ItemID.Sets.GamepadExtraRange[Item.type] = 15;
            ItemID.Sets.GamepadSmartQuickReach[Item.type] = true;
        }
        
        public override void SetDefaults() {
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.Size = new Vector2(30 , 26);
            Item.useAnimation = 25;
            Item.useTime = 25;
            Item.shootSpeed = 16f;
            Item.knockBack = KnockbackValue.Weakknockback;
            Item.damage = 19;
            Item.rare = ItemRarityID.LightRed;

            Item.DamageType = DamageClass.MeleeNoSpeed;
            Item.channel = true;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.value = Item.sellPrice(0, 5, 75, 0);
            Item.buyPrice(0, 7, 25, 0);
            Item.UseSound = SoundID.Item1;
            Item.shoot = ModContent.ProjectileType<SunwirlYoyo>();
        }

        public override bool MeleePrefix()
        {
            return true;
        }

        public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale,
            int whoAmI)
        {
            if (Main.rand.NextFloat() < 0.09f)
            {
                Dust dust;
                dust = Main.dust[Terraria.Dust.NewDust(Item.position, Item.width, Item.height, 6, 0f, 0f, 0, new Color(255,255,255), 1f)];
            }

            return true;
        }

        class SunwirlYoyo : ModProjectile
        {
            public override string Texture => "Crystals/Content/Foresta/Items/Weapons/Melee/Sunwirl/Sunwirl";

            public override void SetStaticDefaults() {
                // Vanilla values range from 3f(Wood) to 16f(Chik), and defaults to -1f. Leaving as -1 will make the time infinite.
                ProjectileID.Sets.YoyosLifeTimeMultiplier[Projectile.type] = 7;
                // Vanilla values range from 130f(Wood) to 400f(Terrarian), and defaults to 200f
                ProjectileID.Sets.YoyosMaximumRange[Projectile.type] = 300f;
                // Vanilla values range from 9f(Wood) to 17.5f(Terrarian), and defaults to 10f
                ProjectileID.Sets.YoyosTopSpeed[Projectile.type] = 12f;
            }

            public override void SetDefaults()
            {
                Projectile.extraUpdates = 0;
                Projectile.Size = new Vector2(30, 26); 
                Projectile.aiStyle = 99;
                Projectile.friendly = true;
                Projectile.penetrate = -1;
                Projectile.DamageType = DamageClass.MeleeNoSpeed;
                Projectile.scale = 1f;
            }

            private int hits;
            private int maxhits = 3;

            public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
            {
                hits++;
                Player owner = Main.player[Projectile.owner];

                if (crit)
                {
                    target.AddBuff(BuffID.OnFire , 60*3);
                }
                
                if (hits == maxhits)
                {

                    if (target.HasBuff(ModContent.BuffType<Flammable>()))
                    {
                        if (target.life <= target.lifeMax * 0.3)
                        {
                            target.AddBuff(BuffID.OnFire3 , 60*3);
                        }
                        else
                        {
                            target.AddBuff(BuffID.OnFire , 60*5);
                        }
                    }

                    if (!target.HasBuff(BuffID.OnFire) || !target.HasBuff(BuffID.OnFire3))
                    {
                        target.AddBuff(ModContent.BuffType<Flammable>() , 60*7);
                    }
                    SoundEngine.PlaySound(SoundID.Item34, Projectile.Center);
                    hits = 0;
                }
            }

            class Flammable : ModBuff
            {
                public override void SetStaticDefaults()
                {
                    DisplayName.SetDefault("Flammable");
                    Description.SetDefault("If you get damaged again you will burn");
                    Main.debuff[Type] = true;
                    BuffID.Sets.NurseCannotRemoveDebuff[Type] = true;
                }

                public override void Update(NPC npc, ref int buffIndex)
                {
                    if (Main.rand.NextFloat() < 0.26744187f)
                    {
                        Dust dust;
                        Vector2 position = npc.position;
                        dust = Main.dust[Terraria.Dust.NewDust(position, npc.width, npc.height, 303, Main.rand.NextFloat(-5.348837f , 5.348837f), Main.rand.NextFloat( -2f, -4f ), 0, new Color(255,255,255), 0.5813954f)];
                        dust.noGravity = true;
                        dust.fadeIn = 1.5697675f;
                    }
                    
                    if (npc.HasBuff(BuffID.OnFire) || npc.HasBuff(BuffID.OnFire3) && npc.HasBuff<Flammable>())
                    {
                        if (Main.rand.NextFloat() < 0.15f)
                        {
                            Vector2 position = npc.position;
                            if (Main.rand.NextBool())
                            {
                                Terraria.Projectile.NewProjectile(
                                    npc.GetSource_Buff(npc.FindBuffIndex(ModContent.BuffType<Flammable>())),
                                    position.X, position.Y, Main.rand.NextFloat(-2.5f, 2.5f),
                                    Main.rand.NextFloat(-3f, -5 ), ModContent.ProjectileType<White_Smoke>() , 1 , 0 , Main.LocalPlayer.whoAmI);
                            }
                            else
                            {
                                Terraria.Projectile.NewProjectile(
                                    npc.GetSource_Buff(npc.FindBuffIndex(ModContent.BuffType<Flammable>())),
                                    position.X, position.Y, Main.rand.NextFloat(-2.5f, 2.5f),
                                    Main.rand.NextFloat(-3f, -5 ), ModContent.ProjectileType<Smoke>() , 1 , 0 , Main.LocalPlayer.whoAmI);
                            }
                        }
                    }

                }


                class Smoke : ModProjectile
                {
                    
                    public override void SetStaticDefaults()
                    {
                        Main.projFrames[Projectile.type] = 4;
                        DisplayName.SetDefault("Black Smoke");
                    }

                    public override void SetDefaults()
                    {
                        Projectile.width = 16; 
                        Projectile.height = 17;
                        Projectile.friendly = true;
                        Projectile.ignoreWater = false;
                        Projectile.tileCollide = false;
                        Projectile.timeLeft = 60;
                    }

                    public override void AI()
                    {
                        
                        
                        if (Projectile.ai[0] >= 60f)
                            Projectile.Kill();
                        
                        if (++Projectile.frameCounter >= 5) {
                            Projectile.frameCounter = 0;
                            if (++Projectile.frame >= Main.projFrames[Projectile.type])
                                Projectile.frame = 0;
                        }
                    }

                    public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
                    {
                        target.AddBuff(ModContent.BuffType<Flammable>() , 60*5);
                        damage = 0;
                        knockback = 0;
                    }
                    
                    public override bool? CanHitNPC(NPC target)
                    {
                        return target.HasBuff<Flammable>() != true;
                    }
                    
                    
                }
                
                class White_Smoke : ModProjectile
                {
                    public override void SetStaticDefaults()
                    {
                        Main.projFrames[Projectile.type] = 4;
                        DisplayName.SetDefault("White Smoke");
                    }

                    public override void SetDefaults()
                    {
                        Projectile.width = 16; 
                        Projectile.height = 17;
                        Projectile.friendly = true;
                        Projectile.ignoreWater = false;
                        Projectile.tileCollide = false;
                        Projectile.timeLeft = 60;
                        Projectile.penetrate = -1;
                    }

                    public override void AI()
                    {
                        if (Projectile.ai[0] >= 60f)
                            Projectile.Kill();
                        
                        if (++Projectile.frameCounter >= 5) {
                            Projectile.frameCounter = 0;
                            if (++Projectile.frame >= Main.projFrames[Projectile.type])
                                Projectile.frame = 0;
                        }
                    }

                    public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
                    {
                        target.AddBuff(ModContent.BuffType<Flammable>() , 60*5);
                        damage = 0;
                        knockback = 0;
                    }

                    public override bool? CanHitNPC(NPC target)
                    {
                        return target.HasBuff<Flammable>() != true;
                    }
                }
            }
            
            public override void PostAI()
            {
                if (Main.rand.NextBool())
                {
                    Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, 6);
                }
            }

        }
        
    }
}