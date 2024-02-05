using Crystals.Core.Systems;
using System.Diagnostics.Metrics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Crystals.Content
{
    internal class Testarang : ModItem 
    {
        public static bool Perfect;
        public override string Texture => AssetDir.Weapons + Name;
        public override void SetDefaults()
        {
            Item.useStyle = ItemUseStyleID.Swing;
            Item.damage = 17;
            Item.knockBack = 8f;
            Item.width = 14;
            Item.height = 28;
            Item.UseSound = SoundID.Item1;
            Item.useAnimation = 20;
            Item.useTime = 20;
            Item.noUseGraphic = true;
            
        }
        public override void RightClick(Player player)
        {
            Item.shootSpeed = 10f;
            Item.shoot = ModContent.ProjectileType<TestarangProjectile>();
            Item.consumable = true;

        }
    }
}
