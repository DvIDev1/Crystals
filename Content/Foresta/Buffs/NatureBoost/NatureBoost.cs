using System.Drawing;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Color = Microsoft.Xna.Framework.Color;
using Rectangle = Microsoft.Xna.Framework.Rectangle;

namespace Crystals.Content.Foresta.Buffs.NatureBoost
{
    public class NatureBoost : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Crystal of the Forest");
            Description.SetDefault("Keeps you alive for as Long as Possible");
            BuffID.Sets.TimeLeftDoesNotDecrease[Type] = true;
            Main.debuff[Type] = true;
            BuffID.Sets.NurseCannotRemoveDebuff[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            if (player.statLife <= 0)
            {
                player.KillMe(PlayerDeathReason.ByCustomReason(player.name +  " Decayed"), 1 , 0);
            }
            else
            {
                player.statLife--;
                if (player.statManaMax != player.statMana)
                {
                    player.statMana += 10;  
                }
            }
        }
    }
}