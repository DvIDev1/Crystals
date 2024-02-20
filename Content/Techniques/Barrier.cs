using Crystals.Core.Systems.Combat.Techniques;
using Terraria;

namespace Crystals.Content.Techniques;

public class Barrier : Technique
{
    public override string Name() => "Barrier";

    public override float StaminaUse() => 0;

    public override int StartTime() => 0;

    public override int Duration() => 180;

    public override float KnockBackReduction() => .5f;

    public override float DamageReduction() => .25f;

    public override float StaminaPunish() => 0.5f;

    public override float MinimalStamina() => 0;

    public override TechniqueType TechniqueType() => Core.Systems.Combat.Techniques.TechniqueType.Hold;

    public override void OnStartTechnique()
    {
        Main.NewText("Started");
    }

    public override void Block()
    {
        base.Block();
    }
}