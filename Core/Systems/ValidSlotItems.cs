using Crystals.Content.NewFolder;
using Crystals.Content.NewFolder1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;

namespace Crystals.Core.Systems
{
    internal class ValidSlotItems
    {
        public static int[] ValidCrystals = { ModContent.ItemType<GreenCrystal>(), ModContent.ItemType<RedCrystal>() };
        public static int[] ValidTechniques = { ModContent.ItemType<Technique>()};
        
    }
}
