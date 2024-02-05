using Crystals.Core.Systems;
using Crystals.Core.Systems;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;

namespace Crystals.Content.NewFolder
{
    internal class GreenCrystal : ModItem
    {
        public override string Texture => AssetDir.CrystalItems + Name;
        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            SlotPlacement.CrystalColor = Color.Green;
        }
    }
}
