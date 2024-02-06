using Crystals.Core.Systems;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace Crystals.Content.Items.Crystals
{
    internal class RedCrystal : ModItem
    {
        public override string Texture => AssetDir.CrystalItems + Name;

        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            SlotPlacement.CrystalColor = Color.Red;

        }
    }
}
