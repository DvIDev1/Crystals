using Crystals.Core.Systems;
using Terraria.ModLoader;

namespace Crystals.Content.Items.Techniques
{
    internal class Technique : ModItem
    {
        public override string Texture => AssetDir.Techniques + Name;
        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
        }
    }
}
