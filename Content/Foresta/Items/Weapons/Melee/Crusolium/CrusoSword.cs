using Crystals.Helpers;
using Humanizer;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Crystals.Content.Foresta.Items.Weapons.Melee.Crusolium
{
    public class CrusoSword : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Crusolium Sword");
            Tooltip.SetDefault("Infused with Two Powers \nEvery " + maxHits.ToWords() + " hits the Sword is Empowered");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        private int maxHits = 3;
        private int hits;
        
        public override void SetDefaults()
        {
            Item.Size = new Vector2(32,27);
            Item.crit = 6;
            Item.DamageType = DamageClass.Melee;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.damage = 15;
            Item.useTime = 50;
            Item.scale = 2.0f;
            Item.useAnimation = 25;
            Item.knockBack = KnockbackValue.Averageknockback;
            Item.rare = ItemRarityID.Green;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;

            Item.value = Item.sellPrice(0, 0, 50, 0);
            Item.buyPrice(0, 75, 0, 0);
        }

        public override void ModifyHitNPC(Player player, NPC target, ref int damage, ref float knockBack, ref bool crit)
        {
            if (hits == maxHits)
            {
                damage *= 2;
                knockBack = KnockbackValue.HighInsaneknockback;
                crit = true;
                player.AddBuff(ModContent.BuffType<Bealux>() , 60*2);
                hits = 0;
            }
            hits++;
        }

        public override void Update(ref float gravity, ref float maxFallSpeed)
        {
            maxFallSpeed = 0;
        }

        public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale,
            int whoAmI)
        {
            scale = 1.5f;
            if (Main.rand.NextFloat() < 0.12f)
            {
                Dust dust;
                Vector2 position = Item.position;
                dust = Main.dust[Terraria.Dust.NewDust(position, Item.width, Item.height, 269, 0f, -10f, 0, new Color(255,255,255), 1f)];
            }
            return true;
        }

        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            if (hits == maxHits)
            {
                Dust.NewDust(hitbox.Location.ToVector2(), hitbox.Width, hitbox.Height, 269);
            }
        }

        public override bool MeleePrefix()
        {
            return true;
        }

        class Bealux : ModBuff
        {
            public override void SetStaticDefaults()
            {
                DisplayName.SetDefault("Bealux!");
                Description.SetDefault("Gives you the Energy of the Light");
            }

            public override void Update(Player player, ref int buffIndex)
            {
                player.runAcceleration += 0.75f;
                player.runSlowdown += 2.5f;
                player.GetAttackSpeed(DamageClass.Melee) += 0.5f;
                player.maxRunSpeed += 2.5f;
                player.endurance += 0.10f;
                player.aggro += 5;
                player.GetArmorPenetration(DamageClass.Melee) += 0.10f;
                Dust.NewDust(player.position, player.width, player.height, 269, -player.velocity.X);
            }
        }
        
        
    }
}