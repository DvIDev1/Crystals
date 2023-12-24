using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crytsals.Core.Systems
{
    internal class AssetDir
    {
        public const string Assets = "Crystals/Assets/";
        public const string Shaders = Assets + "Shaders/";

        public const string Misc = Assets + "Misc/";
        public const string NPCs = Assets + "NPCs/";
        public const string Tiles = Assets + "Tiles/";
        public const string Dust = Assets + "Dust/";
        public const string Projectiles = Assets + "Projectiles/";
        public const string Unknown = Assets + "Unknown/";
        public const string Items = Assets + "Items/";

        #region Items
        public const string Accessories = Items + "Accessories/";
        public const string Armors = Items + "Armors/";
        public const string BannersItem = TilesItem + "BannersItem";
        public const string Consumables = Items + "Consumables/";
        public const string Pet = Items + "Pet/";
        public const string TilesItem = Items + "TilesItem/";
        public const string Tools = Items + "Tools/";
        public const string Vanity = Items + "Vanity/";
        public const string Weapons = Items + "Weapons/";

        #endregion

        public const string FX = Shaders + "FX/";
        public const string Textures = Shaders + "Textures/";

        public const string UI = Misc + "UI/";
        public const string Menus = Misc + "Menus/";

        public const string Banners = Tiles + "Banners/";
    }
}
