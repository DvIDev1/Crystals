using System.Collections.Generic;
using Crystals.Core.Systems.TrailSystem;
using Crystals.Helpers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Crystals.Content.Foresta.Items.Weapons.Magic.Photosynthesia
{
    public class Photosynthesia : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Photosynthesia");
            Tooltip.SetDefault("An Ancient book from The Witch that allows you to control Leaves \nLeaves return automatically when mana is missing or when you Reached " + maxProjs + " leaves" + "\nRight Click to return Leaves");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.Size = new Vector2(64,64);Item.noMelee = true;
            Item.crit = 13;
            Item.DamageType = DamageClass.Magic;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.damage = 19;
            Item.useTime = 19;
            Item.useAnimation = 19;
            Item.knockBack = KnockbackValue.Noknockback;
            Item.mana = 14;
            Item.rare = ItemRarityID.Green;
            Item.UseSound = SoundID.DD2_BookStaffCast;
            Item.scale = 0.5f;
            Item.autoReuse = true;

            Item.value = Item.sellPrice(0, 1, 0, 0);
            Item.buyPrice(0, 1, 75, 0);
            Item.shoot = ModContent.ProjectileType<MagicLeaves>();
            Item.shootSpeed = 14.0f;
        }

        private int projcount = 2; // The amount of Projectiles shot
        private int spread = 22; //The spread from 0 to *(The value you inserted)* when the Projectile is shot 
        private int maxProjs = 12; // Maximal amount of Projectiles the Owner of the Weapon is allowed to shoot/ is allowed to be in the world fot he Owner

        public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale,
            int whoAmI)
        {
            Lighting.AddLight(Item.Center, TorchID.Green);
            return true;
        }

        public override bool MagicPrefix()
        {
            return true;
        }

        public List<Projectile> projectiles = new List<Projectile>();

        public override void OnMissingMana(Player player, int neededMana)
        {
            foreach (var projs in projectiles)
            {
                projs.ai[1] = 1;
            }
        }

        public override bool CanUseItem(Player player)
        {
            if (player.ownedProjectileCounts[ModContent.ProjectileType<MagicLeaves>()] >= maxProjs)
            {
                foreach (var projs in projectiles)
                {
                    projs.ai[1] = 1;
                }
                return false;
            }
            return true;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type,
            int damage, float knockback)
        {
            
            if (player.altFunctionUse == 2)
            {
                return false;
            }
            
            for (int i = 0; i < projcount; i++) 

            {
                Vector2 perturbedSpeed = velocity.RotatedByRandom(MathHelper.ToRadians(spread)); //I recommend the Spread to stay between 12 to 24
                projectiles.Add(Projectile.NewProjectileDirect(Item.GetSource_None() , position , perturbedSpeed , type , damage , knockback , player.whoAmI)); //create the projectile
            }
            return false;
        }

        public override bool AltFunctionUse(Player player)
        {
            foreach (var projs in projectiles)
            {
                projs.ai[1] = 1;
            }
            return true;
        }

        class MagicLeaves : ModProjectile
        {
            
            public override void SetStaticDefaults()
            {
                DisplayName.SetDefault("Magic Leaves");
            }
            
            public override void SetDefaults()
            {
                Projectile.Size = new Vector2(18 , 28);
                Projectile.friendly = true;
                Projectile.DamageType = DamageClass.Magic;
                Projectile.ignoreWater = true;
                Projectile.tileCollide = true;
                Projectile.friendly = true;
                Projectile.penetrate = 3; //Maximal amount of times the Projectiles can damage anything (If t)
            }
            
            private bool collided;
            private int hits;
            
            private bool hit = false;
            private NPC lastHit;
            public bool deadleaf = false;
            private bool ret;
            
            private float Distance = 50f; //The Distance from which the returning Projectiles Vanish
            

            public override void AI()
            {
                ret = Projectile.ai[1] == 1;
                if (Projectile.ai[1] != 1)
                {
                    if (!collided)
                    {
                        Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;
                    }
                    Projectile.ai[0] += 1f;
                    if (Projectile.ai[0] >= 15f)
                    {
                        Projectile.ai[0] = 15f;
                        Projectile.velocity.Y = Projectile.velocity.Y + 0.1f;
                    }
                    if (Projectile.velocity.Y > 16f)
                    {
                        Projectile.velocity.Y = 16f;
                    }
                }
                else if(deadleaf != true)
                {
                    Player owner = Main.player[Projectile.owner];
                    Projectile.tileCollide = false;
                    Projectile.penetrate = -1;
                    if (Projectile.Distance(owner.Center) <= Distance)
                    {
                        for (int i = 0; i < 10; i++)
                        {
                            Vector2 pos = owner.Center + Vector2.One.RotatedBy(MathHelper.TwoPi / 10 * i) * (owner.width + owner.height)/2;
                            int dust = Dust.NewDust(pos, 16, 16, 61);
                            deadleaf = true;
                        }
                        
                    }

                    if (Projectile.Distance(owner.Center) >= 1500f)
                    {
                        Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;
                        Projectile.velocity = Projectile.DirectionTo(owner.Center) * Projectile.Distance(owner.Center) * 0.01f;
                    }else {
                        Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;
                        Projectile.velocity += Projectile.DirectionTo(owner.Center) /3; 
                    }
                    
                }
                else
                {
                    Projectile.alpha +=  2;
                    Projectile.tileCollide = true;
                    Projectile.ai[0] += 1f;
                    if (!collided) Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;
                    if (Projectile.ai[0] >= 15f)
                    {
                        Projectile.ai[0] = 15f;
                        Projectile.velocity.Y = Projectile.velocity.Y + 0.8f;
                    }
                    if (Projectile.velocity.Y > 16f)
                    {
                        Projectile.velocity.Y = 16f;
                    }
                    if (Projectile.alpha >= 250)
                    {
                        Projectile.Kill();
                    }
                }
            }
            
            public PrimitiveTrail trail = new PrimitiveTrail();
            public List<Vector2> oldPositions = new List<Vector2>();
            public override bool PreDraw(ref Color lightColor)
            {
                Main.spriteBatch.End();
                Main.spriteBatch.Begin();

                lightColor = Color.White;

                Color color = Color.SeaGreen;

                Vector2 pos = (Projectile.Center).RotatedBy(Projectile.rotation, Projectile.Center);

                oldPositions.Add(pos);
                while (oldPositions.Count > 30)
                    oldPositions.RemoveAt(0);

                trail.Draw(color, pos, oldPositions, 1.4f);

                Main.spriteBatch.End();
                Main.spriteBatch.Begin();
                return true;
            }

            public override bool OnTileCollide(Vector2 oldVelocity)
            {
                collided = true;
                Projectile.velocity = Vector2.Zero;
                return false;
            }

            public override void Kill(int timeLeft)
            {
                Player owner = Main.player[Projectile.owner];
                for (int i = 0; i < 10; i++)
                {
                    Vector2 pos = Projectile.Center + Vector2.One.RotatedBy(MathHelper.TwoPi / 10 * i) * (Projectile.width + Projectile.height)/2;
                    int dust = Dust.NewDust(pos, 16, 16, 61);
                }
            }


            public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
            {
                hits++;
                damage /= hits;
                if (ret)
                {
                    for (int i = 0; i < 10; i++)
                    {
                        Vector2 pos = target.Center + Vector2.One.RotatedBy(MathHelper.TwoPi / 10 * i) * (target.width + target.height)/2;
                        int dust = Dust.NewDust(pos, 16, 16, 61);
                        deadleaf = true;
                    }
                }

            }

            public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac)
            {
                width /= 2;
                height /= 2;
                return true;
            }

            public override bool? CanCutTiles()
            {
                return true;
            }
        }
    }
}