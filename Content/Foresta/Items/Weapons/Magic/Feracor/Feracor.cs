using System;
using Crystals.Helpers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace Crystals.Content.Foresta.Items.Weapons.Magic.Feracor
{
    public class Feracor : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Feracor");
            Tooltip.SetDefault("An Ancient Staff that uses the Power of Nature to to Charge up \nHold the Item to Charge At max Charge shoot an Embowered Projectile");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }
        
        public override void SetDefaults()
        {
            Item.Size = new Vector2(33,33);Item.noMelee = true;
            Item.crit = 29;
            Item.DamageType = DamageClass.Magic;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.damage = 51;
            Item.useTime = 80;
            Item.useAnimation = 80;
            Item.reuseDelay = 20;
            Item.knockBack = KnockbackValue.LowAverageknockback;
            Item.mana = 40;
            Item.scale = 1.5f;
            Item.rare = ItemRarityID.Green;
            Item.UseSound = SoundID.Item8;
            Item.autoReuse = false;
            Item.shootSpeed = 20.0f;
            Item.ArmorPenetration = 30;
            Item.shoot = ModContent.ProjectileType<EnergyBlast>();
            Item.value = Item.sellPrice(0, 10, 0, 0);
            Item.value = Item.buyPrice(0, 25, 0, 0);
        }

        private static int charge;

        private int MaxCharge = 1000;

        public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale,
            int whoAmI)
        {
            if (Main.rand.NextFloat() < 0.23f)
            {
                for (int i = 0; i < charge/ (MaxCharge / 4); i++)
                {
                    Vector2 pos = Item.Center + Vector2.One.RotatedBy((MathHelper.TwoPi / 4 * i) + 90f) * (Item.width + Item.height) /2;
                    Dust dust;
                    Vector2 position = pos;
                    dust = Dust.NewDustPerfect(pos , 107);
                }
            }
            return true;
        }

        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage,
            ref float knockback)
        {
            if (charge >= MaxCharge)
            {
                damage *= 2;
                knockback *= 2;
                type = ModContent.ProjectileType<BigEnergyBlast>();
                charge -= MaxCharge;
            }
            else if (charge >= MaxCharge/4)
            {
                charge -= MaxCharge/4;
            }
        }
        
        public override bool CanUseItem(Player player)
        {
            return charge >= MaxCharge/4;
        }

        public override void HoldItem(Player player)
        {
            if (Main.rand.NextFloat() < 0.23f)
            {
                for (int i = 0; i < charge/ (MaxCharge / 4); i++)
                {
                    Vector2 pos = player.Center + Vector2.One.RotatedBy((MathHelper.TwoPi / 4 * i) + 90f) * (player.width + player.height);
                    Dust dust;
                    Vector2 position = pos;
                    dust = Dust.NewDustPerfect(pos , 107);
                    dust.shader = GameShaders.Armor.GetSecondaryShader(18, player);
                }
            }

            if (charge <= MaxCharge)
            {
                charge++;
            }

        }

        class EnergyBlast : ModProjectile
        {

            public override void SetStaticDefaults()
            {
                DisplayName.SetDefault("Energy Blast");
                Main.projFrames[Projectile.type] = 4;
            }

            public override void SetDefaults()
            {
                Projectile.width = 14;
                Projectile.height = 9;
                Projectile.scale = 1.5f;
                Projectile.friendly = true;
                Projectile.ignoreWater = true;
                Projectile.tileCollide = true;
            }

            public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
            {
                if (crit)
                {
                    charge += 300;
                }
            }

            public override void AI()
            {
                Projectile.rotation = Projectile.velocity.ToRotation();
                Dust dust;
                Vector2 position = Projectile.position;
                dust = Main.dust[Terraria.Dust.NewDust(position, Projectile.width, Projectile.height, 107, 0f, 0f, 0, new Color(255,255,255), 1f)];
                if (++Projectile.frameCounter >= 6)
                {
                    Projectile.frameCounter = 0;
                    Projectile.frame = ++Projectile.frame % Main.projFrames[Projectile.type];
                }
            }
        }
        
        class BigEnergyBlast : ModProjectile
        {
            
            public override void SetStaticDefaults()
            {
                DisplayName.SetDefault("Big Energy Blast");
                Main.projFrames[Projectile.type] = 6;
            }
            
            public override void SetDefaults()
            {
                Projectile.width = 38; 
                Projectile.height = 25;
                Projectile.friendly = true;
                Projectile.ignoreWater = true;
                Projectile.penetrate = 1;
                Projectile.tileCollide = true;
            }

            public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
            {
                if (!target.active)
                {
                    Projectile.penetrate++;
                    return;
                }
                Player owner = Main.player[Projectile.owner];
                for (int i = 0; i < 4; i++)
                {
                    Vector2 pos = owner.Center + Vector2.One.RotatedBy((MathHelper.TwoPi / 4 * i) + 90f) * (owner.width + owner.height);
                    pos.Y -= owner.height;
                    Terraria.Projectile proj = Terraria.Projectile.NewProjectileDirect(owner.GetSource_OnHit(target), pos, Projectile.oldVelocity,
                        ModContent.ProjectileType<EnergyBlast>(), Projectile.damage / 2, Projectile.knockBack / 2,
                        owner.whoAmI);
                    proj.velocity = proj.DirectionTo(target.Center) * 20f;
                    proj.CritChance = 25;
                }

            }

            public override bool OnTileCollide(Vector2 oldVelocity)
            {
                Player owner = Main.player[Projectile.owner];
                for (int i = 0; i < 4; i++)
                {
                    Vector2 pos = owner.Center + Vector2.One.RotatedBy((MathHelper.TwoPi / 4 * i) + 90f) * (owner.width + owner.height);
                    pos.Y -= owner.height;
                    Terraria.Projectile proj = Terraria.Projectile.NewProjectileDirect(Projectile.GetSource_None(), pos, Projectile.oldVelocity,
                        ModContent.ProjectileType<EnergyBlast>(), Projectile.damage / 2, Projectile.knockBack / 2,
                        owner.whoAmI);
                    proj.velocity = proj.DirectionTo(Projectile.Center) * 20f;
                }
                return true;
            }

            public override void AI()
            {
                Projectile.rotation = Projectile.velocity.ToRotation();
                Dust dust;
                Vector2 position = Projectile.position;
                Player owner = Main.player[Projectile.owner];
                dust = Main.dust[Terraria.Dust.NewDust(position, Projectile.width, Projectile.height, 107, 0f, 0f, 0, new Color(255,255,255), 1f)];
                if (++Projectile.frameCounter >= 8)
                {
                    Projectile.frameCounter = 0;
                    Projectile.frame = ++Projectile.frame % Main.projFrames[Projectile.type];
                }

                if (Projectile.Distance(owner.Center) >= 1000)
                {
                    for (int i = 0; i < 10; i++)
                    {
                        Vector2 pos = owner.Center + Vector2.One.RotatedBy((MathHelper.TwoPi / 10 * i) + 90f) * 1000;
                        Dust.NewDustPerfect(pos, 107);
                        Projectile.Kill();
                    }  
                }
            }

            


        }
        
    }
}