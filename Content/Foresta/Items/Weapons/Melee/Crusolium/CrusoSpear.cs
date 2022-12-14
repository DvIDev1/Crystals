using System.Collections.Generic;
using Crystals.Helpers;
using Humanizer;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Crystals.Content.Foresta.Items.Weapons.Melee.Crusolium
{
    public class CrusoSpear : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Crusolium Spear");
            Tooltip.SetDefault("A Powerful Spear when fully charged right click for an powerful strike");

            ItemID.Sets.SkipsInitialUseSound[Item.type] = true; 
            ItemID.Sets.Spears[Item.type] = true; 
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }
        
        
        public override void SetDefaults() 
        {
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.Size = new Vector2(42 , 45);
            Item.useAnimation = 24;
            Item.useTime = 36;
            Item.shootSpeed = 16f;
            Item.knockBack = KnockbackValue.LowWeakknockback;
            Item.damage = 21;
            Item.UseSound = SoundID.Item1;
            Item.rare = ItemRarityID.Green;

            Item.scale = 2.0f;
            Item.DamageType = DamageClass.MeleeNoSpeed;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<CrusoSpearProj>();
        }

        private static int maxhits = 10;

        private static int hits;
        
        
        public override bool CanUseItem(Player player) {
            return player.ownedProjectileCounts[Item.shoot] < 1;
        }

        public override bool? UseItem(Player player) 
        {
            if (!Main.dedServ && Item.UseSound.HasValue) 
            {
                SoundEngine.PlaySound(Item.UseSound.Value, player.Center);
            }

            return null;
        }

        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage,
	        ref float knockback)
        {
	        if (player.altFunctionUse == 2)
	        {
		        knockback = KnockbackValue.HighWeakknockback;
		        velocity = new Vector2(0);
		        if (player.direction == 1)
		        {
			       position = player.Center;
		        }
		        else
		        {
			        position = player.Center - new Vector2(336 , 34);
		        }
		        damage *= 4;
		        type = ModContent.ProjectileType<Slice>();
		        hits = 0;
	        }
        }

        public override bool AltFunctionUse(Player player)
        {
	        if (hits == maxhits)
	        {
		        return true;
	        }

	        return false;
        }

        
        class CrusoSpearProj : ModProjectile 
        {
		protected virtual float HoldoutRangeMin => 50f;
		protected virtual float HoldoutRangeMax => 120f;

		public override string Texture => "Crystals/Content/Foresta/Items/Weapons/Melee/Crusolium/CrusoSpearProj";

		public override void SetStaticDefaults() {
			DisplayName.SetDefault("Spear");
		}

		public override void SetDefaults() {
			Projectile.CloneDefaults(ProjectileID.Spear);
			Projectile.scale = 2.0f;
		}

		public override bool PreAI() {
			Player player = Main.player[Projectile.owner]; 
			int duration = player.itemAnimationMax; 

			player.heldProj = Projectile.whoAmI; 
			
			if (Projectile.timeLeft > duration) {
				Projectile.timeLeft = duration;
			}

			Projectile.velocity = Vector2.Normalize(Projectile.velocity);

			float halfDuration = duration * 0.5f;
			float progress;
			
			if (Projectile.timeLeft < halfDuration) {
				progress = Projectile.timeLeft / halfDuration;
			}
			else {
				progress = (duration - Projectile.timeLeft) / halfDuration;
			}
			
			Projectile.Center = player.MountedCenter + Vector2.SmoothStep(Projectile.velocity * HoldoutRangeMin, Projectile.velocity * HoldoutRangeMax, progress);
			
			if (Projectile.spriteDirection == -1) {
				
				Projectile.rotation += MathHelper.ToRadians(45f);
			}
			else {
				Projectile.rotation += MathHelper.ToRadians(135f);
			}

			if (hits == maxhits)
			{
				Dust.NewDustPerfect(Projectile.Center, 269);
			}
			
			return false;
		}

		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			if (hits == maxhits)
			{
				return;
			}
			hits++;
		}

        }

        class Slice : ModProjectile
        {
	        
	        public override void SetStaticDefaults()
	        {
		        DisplayName.SetDefault("Slice");
		        Main.projFrames[Projectile.type] = 4;
	        }
            
	        public override void SetDefaults()
	        {
		        Projectile.Size = new Vector2(336 , 34);
		        Projectile.DamageType = DamageClass.MeleeNoSpeed;
		        Projectile.ignoreWater = true;
		        Projectile.tileCollide = false;
		        Projectile.friendly = true;
		        Projectile.timeLeft = 20;
		        Projectile.penetrate = -1;
		        Projectile.ArmorPenetration = 100;
	        }

	        public List<NPC> hitted = new List<NPC>();
	        
	        public override void AI()
	        {
		        Player owner = Main.player[Projectile.owner];
		        owner.itemTime = 10;
		        owner.itemAnimation = 10;
		        if (owner.direction == 1)
		        {
			        Projectile.Center = new Vector2(owner.Center.X + Projectile.width/2 , owner.Center.Y);
		        }
		        else
		        {
			        Projectile.Center = new Vector2(owner.Center.X - Projectile.width/2 , owner.Center.Y);
		        }
		        Projectile.rotation = owner.bodyRotation;
		        
		        if (++Projectile.frameCounter >= 5)
		        {
			        Projectile.frameCounter = 0;
			        Projectile.frame = ++Projectile.frame % Main.projFrames[Projectile.type];
		        }

		        if (owner.direction == -1)
		        {
			        Projectile.spriteDirection = -1;
			        Projectile.direction = -1;
		        }
		        
		        Dust.NewDust(Projectile.position, Projectile.width , Projectile.height , 269);
	        }
	        
	        

	        public override bool PreKill(int timeLeft)
	        {
		        Player owner = Main.player[Projectile.owner];
		        if (owner.direction == 1)
		        {
			        Projectile.NewProjectile(Projectile.GetSource_None(), Projectile.Left, Vector2.Zero, ModContent.ProjectileType<Swipe>(),
				        Projectile.damage / 2, KnockbackValue.HighInsaneknockback , Projectile.owner);
		        }
		        else
		        {
			        Projectile.NewProjectile(Projectile.GetSource_None(), Projectile.Right, Vector2.Zero, ModContent.ProjectileType<Swipe>(),
				        Projectile.damage / 2, KnockbackValue.HighInsaneknockback , Projectile.owner);
		        }
		        
		        return true;
	        }

	        public override bool? CanHitNPC(NPC target)
	        {
		        if (hitted.Contains(target))
		        {
			        return false;
		        }

		        if (Projectile.frame == 1 || Projectile.frame == 2)
		        {
			        return true;
		        }

		        if (target.townNPC)
		        {
			        return false;
		        }
		        
		        return true;
	        }

	        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
	        {
		        Player owner = Main.player[Projectile.owner];
		        owner.AddBuff(ModContent.BuffType<GoldenBoots>() , 60 * 5);
		        hitted.Add(target);
	        }

	        class Swipe : ModProjectile
	        {
		        
		        public override void SetStaticDefaults()
		        {
			        DisplayName.SetDefault("Swipe");
			        Main.projFrames[Projectile.type] = 5;
		        }
            
		        public override void SetDefaults()
		        {
			        Projectile.Size = new Vector2(360 , 360);
			        Projectile.DamageType = DamageClass.MeleeNoSpeed;
			        Projectile.ignoreWater = true;
			        Projectile.tileCollide = false;
			        Projectile.friendly = true;
			        Projectile.timeLeft = 25;
			        Projectile.penetrate = -1;
			        Projectile.ArmorPenetration = 100;
		        }

		        public override void AI()
		        {
			        Player owner = Main.player[Projectile.owner];
			        owner.itemTime = 10;
			        owner.itemAnimation = 10;
			        if (owner.direction == 1)
			        {
				        Projectile.Center = new Vector2(owner.Center.X + Projectile.width/2 , owner.Center.Y);
			        }
			        else
			        {
				        Projectile.Center = new Vector2(owner.Center.X - Projectile.width/2 , owner.Center.Y);
			        }
			        Projectile.rotation = owner.bodyRotation;
			        
			        if (++Projectile.frameCounter >= 5)
			        {
				        Projectile.frameCounter = 0;
				        Projectile.frame = ++Projectile.frame % Main.projFrames[Projectile.type];
			        }

			        if (owner.direction == -1)
			        {
				        Projectile.spriteDirection = -1;
				        Projectile.direction = -1;
			        }
		        
			        Dust.NewDust(Projectile.position, Projectile.width , Projectile.height , 269);
		        }
		        
		        public List<NPC> hitted = new List<NPC>();
		        
		        public override bool? CanHitNPC(NPC target)
		        {
			        if (hitted.Contains(target))
			        {
				        return false;
			        }

			        if (Projectile.frame == 1 || Projectile.frame == 2 || Projectile.frame == 3)
			        {
				        return true;
			        }
			        
			        if (target.townNPC)
			        {
				        return false;
			        }

			        return true;
		        }

		        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		        {
			        hitted.Add(target);
		        }

	        }
	        
	        

	        class GoldenBoots : ModBuff
	        {
		        public override void SetStaticDefaults()
		        {
			        DisplayName.SetDefault("Golden Boots");
			        Description.SetDefault("Makes the User faster but also attracts more foes");
		        }

		        public override void Update(Player player, ref int buffIndex)
		        {
			        Dust.NewDust(player.position, player.width, player.height, 269, -player.velocity.X);
			        player.aggro += 100;
			        player.maxRunSpeed += 2.5f;
			        player.runAcceleration += 0.75f;
			        player.runSlowdown += 2.5f;
		        }
	        }
	        
        }
        
    }
}