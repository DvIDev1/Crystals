using Microsoft.Xna.Framework.Input;
using Terraria;
using Terraria.ModLoader;

namespace Crystals.Core.Systems.Combat.Techniques;

public class Techniques : ModPlayer
{
    
    public static ModKeybind UseTechniqueKey;

    public Technique CurrentTechnique => new TestTechnique();
    
    public override void Load()
    {
        UseTechniqueKey = KeybindLoader.RegisterKeybind(Mod, "Use Technique ", Keys.LeftControl);
    }

    public override void PreUpdate()
    {
        if (UseTechniqueKey.JustPressed)
        {
            CurrentTechnique.StartTechnique();
        }

        if (CurrentTechnique.InUse)
        {
            CurrentTechnique.Update();
            // ReSharper disable once CompareOfFloatsByEqualityOperator
            if (CurrentTechnique.Time == CurrentTechnique.StartTime() + CurrentTechnique.Duration())
            {
                CurrentTechnique.InUse = false;
            }
        }
    }
}