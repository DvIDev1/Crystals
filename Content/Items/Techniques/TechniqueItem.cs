using Crystals.Core.Systems;
using Crystals.Core.Systems.Combat;
using Crystals.Core.Systems.Combat.Techniques;
using Terraria.ModLoader;

namespace Crystals.Content.Items.Techniques
{
    public abstract class TechniqueItem : ModItem
    {
        public override string Texture => AssetDir.Techniques + "Technique";

        public virtual void SaveTech()
        {
            TechniqueHandler.TechniqueItems.Add(Item , this);
        }
        
        public virtual Technique ItemTechnique()
        {
            SaveTech();
            return ItemTechnique();
        }
        
        public override void SetDefaults()
        {
            Item.width = 54;
            Item.height = 56;
        }

        protected TechniqueItem()
        {
        }
    }
}
