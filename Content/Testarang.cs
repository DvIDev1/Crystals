using Crystals.Core.Systems;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Crystals.Content
{
    internal class Testarang : ModItem
    {

        public override string Texture => AssetDir.Weapons + Name;
        public override void SetDefaults()
        {

            Item.channel = true;

            Item.useStyle = ItemUseStyleID.Swing;
            Item.damage = 17;
            Item.knockBack = 8f;
            Item.width = 14;
            Item.height = 28;
            Item.UseSound = SoundID.Item1;
            Item.useAnimation = 20;
            Item.useTime = 20;
            //Item.noUseGraphic = true;
            Item.shoot = ModContent.ProjectileType<TestarangProjectile>();
            //Item.consumable = true;
        }

    }
}