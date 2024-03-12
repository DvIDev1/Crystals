using Crystals.Content.Techniques;
using Crystals.Core.Systems;
using Crystals.Core.Systems.Combat;
using Crystals.Core.Systems.Combat.Techniques;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Crystals.Content.Items.Techniques;

internal class BarrierItem : TechniqueItem
{
    public override string Texture => AssetDir.Techniques + Name;

    public override Technique ItemTechnique() => new Barrier();

    public override void SetDefaults()
    {
        SaveTech();
        Item.width = 54;
        Item.height = 56;
        Item.rare = ItemRarityID.Blue;
    }
}