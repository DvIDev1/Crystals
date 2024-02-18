using Terraria;

namespace Crystals.Core.Systems.Combat.Techniques;

public class TestTechnique : Technique
{
    public override string Name() => "Test";

    public override int StaminaUse() => 0;

    public override int StartTime() => 0;

    public override int Duration() => 180;

    public override TechniqueType TechniqueType() => Techniques.TechniqueType.Hold;

    public override void OnStartTechnique()
    {
        Main.NewText("Started");
    }

    public override void Dodge()
    {
        Main.NewText("Dodged");
    }
}