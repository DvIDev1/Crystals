using Crystals.Core.Systems;
using Crystals.Core.Systems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;

namespace Crystals.Content.NewFolder1
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
