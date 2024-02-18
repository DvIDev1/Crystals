using Terraria.ModLoader;

namespace Crystals.Core.Systems.Combat;

public class Stamina : ModPlayer
{

    public double StatStaminaMax = 100D;

    private double StatStamina;

    public int LastStaminaUse;

    public void ReduceStamina(double amount)
    {
        StatStamina -= amount;
        
    }

        public override void PostUpdate()
    {
        //TODO No instant regen
        if (ModContent.GetInstance<PlayerState>().CurrentState == States.Idle)
        {
            
        }
    }
    
    public override void OnEnterWorld()
    {
        StatStamina = StatStaminaMax;
    }

    public override void OnRespawn()
    {
        StatStamina = StatStaminaMax;
    }

    public override void PlayerConnect()
    {
        StatStamina = StatStaminaMax;
    }
    
}