using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Crystals.Content.Foresta.Items.Armors.Magic.Litnum
{
    public class Litnum_ArmorSet
    {

        [AutoloadEquip(EquipType.Head)]
        class Litnum_Helmet : ModItem
        {
            public override void SetStaticDefaults()
            {
                DisplayName.SetDefault("Litnum Helmet");
                Tooltip.SetDefault("Increased Damage Reduction by 2%");

                CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
            }

            public override void SetDefaults()
            {
                Item.width = 20;
                Item.height = 20;
                Item.defense = 3;
                Item.rare = ItemRarityID.Green;
            }

            public override void UpdateEquip(Player player)
            {
                player.endurance += 0.02f;
            }
            
            public override void UpdateArmorSet(Player player)
            {
                if (Main.rand.NextFloat() < 0.16f)
                {
                    Dust dust;
                    Vector2 position = player.position;
                    dust = Main.dust[Terraria.Dust.NewDust(position, player.width, player.height, 3, -player.velocity.X, -player.velocity.Y, 0, new Color(255,255,255), 1f)];
                    dust.noGravity = true;
                }

                player.maxMinions += 1;
                player.setBonus = "Increases your defense by 3 times the amount of Minions you have deployed " +
                                  "\nIncreases your max number of minions by 1";
                player.statDefense += 3 * player.numMinions;
            }

            public override bool IsArmorSet(Item head, Item body, Item legs)
            {
                return body.type == ModContent.ItemType<Litnum_Breastplate>() && legs.type == ModContent.ItemType<Litnum_Leggings>();
            }
            
        }
        
        [AutoloadEquip(EquipType.Body)]
        class Litnum_Breastplate : ModItem
        {
            public override void SetStaticDefaults()
            {
                DisplayName.SetDefault("Litnum Breastplate");
                Tooltip.SetDefault("Increased Damage Reduction by 3%");

                CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
            }

            public override void SetDefaults()
            {
                Item.width = 44;
                Item.height = 44;
                Item.defense = 4;
                Item.rare = ItemRarityID.Green;
            }

            public override void UpdateEquip(Player player)
            {
                player.endurance += 0.03f;
            }

            public override bool IsArmorSet(Item head, Item body, Item legs)
            {
                return head.type == ModContent.ItemType<Litnum_Helmet>() && legs.type == ModContent.ItemType<Litnum_Leggings>();
            }
        }
        
        [AutoloadEquip(EquipType.Legs)]
        class Litnum_Leggings : ModItem
        {
            public override void SetStaticDefaults()
            {
                DisplayName.SetDefault("Litnum Leggings");
                Tooltip.SetDefault("Increased Damage Reduction by 2%");

                CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
            }

            public override void SetDefaults()
            {
                Item.width = 28;
                Item.height = 28;
                Item.defense = 2;
                Item.rare = ItemRarityID.Green;
            }

            public override void UpdateEquip(Player player)
            {
                player.endurance += 0.02f;
            }

            public override bool IsArmorSet(Item head, Item body, Item legs)
            {
                return body.type == ModContent.ItemType<Litnum_Breastplate>() && head.type == ModContent.ItemType<Litnum_Helmet>();
            }
            
        }
        
    }
}