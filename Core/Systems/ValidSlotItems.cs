using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Crystals.Content.Items.Crystals;
using Crystals.Content.Items.Techniques;
using Terraria;
using Terraria.ModLoader;

namespace Crystals.Core.Systems
{
    internal class ValidSlotItems
    {
        public static int[] ValidCrystals = { ModContent.ItemType<GreenCrystal>(), ModContent.ItemType<RedCrystal>() };
        public static int[] ValidTechniques = {ModContent.ItemType<TechniqueItem>() , ModContent.ItemType<BarrierItem>()};
        
    }
}
