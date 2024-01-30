using Terraria;

namespace Crystals.Core.Systems.Combat.Techniques;

public class TestTechnique : Technique
{
    public override string Name() => "Test";

    public override int StaminaUse() => 0;

    public override int StartTime() => 0;

    public override int Duration() => 60;

    public override void StartTechnique()
    {
        InUse = true;
        Main.NewText(Name() + " Technique");
    }

    public override void Dodge()
    {
        base.Dodge();
    }
}