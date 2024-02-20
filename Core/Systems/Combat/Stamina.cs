using Terraria;
using Terraria.ModLoader;

namespace Crystals.Core.Systems.Combat;

public class Stamina : ModPlayer
{

    public float StatStaminaMax = 100;

    public float StatStamina;

    public int LastStaminaUse;

    public void ReduceStamina(float amount)
    {
        StatStamina = StatStamina - amount >= 0 ? StatStamina - amount : 0;
        LastStaminaUse = 0;

    }

        public override void PostUpdate()
    {
        //TODO No instant regen
        LastStaminaUse++;
       
        if (ModContent.GetInstance<PlayerState>().CurrentState == States.Idle && LastStaminaUse >= 60*10)
        {
            StatStamina = StatStaminaMax;
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