using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Crystals.Content.Items.Foresta.Weapons.Ranged.Fall
{
    public class Fall : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Fall");
            Tooltip.SetDefault("An Bow only used by the most skilled Huntresses");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }
        
        public override void SetDefaults()
        {
            Item.Size = new Vector2(14,40);Item.noMelee = true;
            Item.crit = 5;
            Item.DamageType = DamageClass.Ranged;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.damage = 13;
            Item.useTime = Item.useAnimation = 25;
            Item.knockBack = 0.04f;
            Item.rare = ItemRarityID.Green;
            Item.UseSound = SoundID.DD2_BookStaffCast;
            Item.autoReuse = true;

            Item.shoot = 1;
            Item.useAmmo = AmmoID.Arrow;
            Item.shootSpeed = 14.0f;
        }

        public int spreadMax = 22; //Maximal Projectile Spread
        public int spreadMin = -20; //Minimum Projectile Spread

        public int ArrowUse = 10; //The amount of uses you need for the Neon Arrow to be shot

        public int uses;

        public override bool RangedPrefix()
        {
            return true;
        }

        public override void OnConsumeAmmo(Item ammo, Player player)
        {
            uses++;
        }

        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage,
            ref float knockback)
        {
            if (type == ProjectileID.WoodenArrowFriendly)
            {
                type = ModContent.ProjectileType<LeafArrow>();
            }
            
            if (uses == ArrowUse)
            {
                type = ModContent.ProjectileType<NeonArrow>();
                knockback *= 2;
                damage *= 2;
                uses = 0;
            }
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type,
            int damage, float knockback)
        {
            int numberProjectiles = 3; // shoots *(Inserted Value)* of projectiles
            for (int index = 0; index < numberProjectiles; ++index)
            {
                Vector2 vector2_1 = new Vector2((float)((double)player.position.X + (double)player.width * 0.5 + (double)(Main.rand.Next(201) * -player.direction) + ((double)Main.mouseX + (double)Main.screenPosition.X - (double)player.position.X)), (float)((double)player.position.Y + (double)player.height * 0.5 - 600.0));   //this defines the projectile width, direction and position
                vector2_1.X = (float)(((double)vector2_1.X + (double)player.Center.X) / 2.0) + (float)Main.rand.Next(-200, 201);
                vector2_1.Y -= (float)(100 * index);
                float num12 = (float)Main.mouseX + Main.screenPosition.X - vector2_1.X;
                float num13 = (float)Main.mouseY + Main.screenPosition.Y - vector2_1.Y;
                if ((double)num13 < 0.0) num13 *= -1f;
                if ((double)num13 < 20.0) num13 = 20f;
                float num14 = (float)Math.Sqrt((double)num12 * (double)num12 + (double)num13 * (double)num13);
                float num15 = Item.shootSpeed / num14;
                float num16 = num12 * num15;
                float num17 = num13 * num15;
                float SpeedX = num16 + (float)Main.rand.Next(spreadMin, spreadMax) * 0.02f; //Projectile Spread
                float SpeedY = num17 + (float)Main.rand.Next(-40, 41) * 0.02f;
                Projectile.NewProjectile(Item.GetSource_None() ,vector2_1.X, vector2_1.Y, SpeedX, SpeedY, type, damage, knockback, Main.myPlayer, 0.0f, (float)Main.rand.Next(5));
            }
            return false;
        }

        class NeonArrow : ModProjectile
        {
            
            public override void SetStaticDefaults()
            {
                DisplayName.SetDefault("Neon Arrow");
            }
            
            public override void SetDefaults()
            {
                Projectile.width = 14;
                Projectile.height = 40;
                Projectile.friendly = true;
                Projectile.DamageType = DamageClass.Ranged;
                Projectile.ignoreWater = true;
                Projectile.tileCollide = true;
                Projectile.arrow = true;
                Projectile.CloneDefaults(ProjectileID.WoodenArrowFriendly);
                Projectile.aiStyle = ProjAIStyleID.Arrow;
                Projectile.penetrate = -1;
            }

            public int startVelo = 2; //Velocity Multiplier on Spawn !!Makes The Arrows Inaccurate!!
            public int BuffTime = 5; //The Time the Debuff will be applied for (InSeconds) !!Is Halved!!
            
            public override void OnSpawn(IEntitySource source)
            {
                Projectile.velocity *= 2;
            }

            public override void AI()
            {
                Dust.NewDustPerfect(Projectile.Center, 61);
            }

            public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
            {
                target.AddBuff(BuffID.Venom , BuffTime*15);
            }
        }
        
        class LeafArrow : ModProjectile
        {
            
            public override void SetStaticDefaults()
            {
                DisplayName.SetDefault("Leaf Arrow");
            }
            
            public override void SetDefaults()
            {
                Projectile.width = 14;
                Projectile.height = 40;
                Projectile.friendly = true;
                Projectile.DamageType = DamageClass.Ranged;
                Projectile.ignoreWater = true;
                Projectile.tileCollide = true;
                Projectile.arrow = true;
                Projectile.CloneDefaults(ProjectileID.WoodenArrowFriendly);
                Projectile.aiStyle = ProjAIStyleID.Arrow;
                Projectile.penetrate = 1;
            }
            
            public int BuffTime = 5; //The Time the Debuff will be applied for (InSeconds) 

            public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
            {
                target.AddBuff(BuffID.Poisoned , BuffTime*30);
            }
        }
    }
}