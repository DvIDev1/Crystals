using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Crystals.Content.Foresta.Items.Armors.Magic.Gaia
{
    public class Gaia_ArmorSet : ModPlayer
    {
        public override void OnHitNPC(Item item, NPC target, int damage, float knockback, bool crit)
        {
            if (hasGaiaArmorSet)
            {
                if (!Player.HasBuff<LeafCooldown>())
                {
                    Player.AddBuff(ModContent.BuffType<LeafActive>() , 60*activeDuration);
                    Player.AddBuff(ModContent.BuffType<LeafCooldown>() , 60*(cooldownDuration + activeDuration));
                }
            }
        }

        public override void OnHitNPCWithProj(Projectile proj, NPC target, int damage, float knockback, bool crit)
        {
            if (hasGaiaArmorSet)
            {
                if (!Player.HasBuff<LeafCooldown>())
                {
                    Player.AddBuff(ModContent.BuffType<LeafActive>() , 60*activeDuration);
                    Player.AddBuff(ModContent.BuffType<LeafCooldown>() , 60*(cooldownDuration + activeDuration));
                }
            }
        }

        
        public override void ModifyHitNPC(Item item, NPC target, ref int damage, ref float knockback, ref bool crit)
        {
            if (Player.HasBuff<LeafActive>())
            {
                int ExtraDamage = Player.statLife / 100;
                if (ExtraDamage <= 1)
                {
                    ExtraDamage = 1;
                }
                
                damage += ExtraDamage;
                Player.Heal(ExtraDamage);

            }
        }
        
        

        public override void ModifyHitNPCWithProj(Projectile proj, NPC target, ref int damage, ref float knockback, ref bool crit,
            ref int hitDirection)
        {
            if (Player.HasBuff<LeafActive>())
            {
                int ExtraDamage = Player.statLife / 100;
                if (ExtraDamage <= 1)
                {
                    ExtraDamage = 1;
                }

                damage += ExtraDamage;
                Player.Heal(ExtraDamage);
            }
        }
        
        

        
        public static int activeDuration = 10;
        public static int cooldownDuration = 45;

        public static bool hasGaiaArmorSet = false;
        
        [AutoloadEquip(EquipType.Head)]
        class Gaia_Hat : ModItem
        {
            public override void SetStaticDefaults()
            {
                DisplayName.SetDefault("Gaia Hat");
                Tooltip.SetDefault("Increased Armor Penetration by 3% \nIncreased maximum mana by 10 \nIncreased Magic damage by 2%");

                CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
            }

            public float armorPen = 0.03f; //Armor Penetration the Player gets when this Armor Piece gets Equipped
            public int manaIncrease = 10; //Increased Mana when Armor is Equipped
            public float MagicDamage = 0.02f; //Increased Magic damage by Percent

            public override void SetDefaults()
            {
                Item.width = 40;
                Item.height = 40;
                Item.defense = 5;
                Item.rare = ItemRarityID.Green;
            }

            public override void UpdateEquip(Player player)
            {
                player.statManaMax2 += manaIncrease;
                player.GetArmorPenetration(DamageClass.Generic) += armorPen;
                player.GetDamage(DamageClass.Magic) += MagicDamage;
            }

            public override bool MagicPrefix()
            {
                return true;
            }

            public override void UpdateArmorSet(Player player)
            {
                player.setBonus = "When you hit an Enemy you deal an Extra 1% Current Health Damage and get Healed for the Bonus Damage for " + activeDuration +
                                  " Seconds and then goes on Cooldown for " + cooldownDuration + " Seconds";
            }

            public override bool IsArmorSet(Item head, Item body, Item legs)
            {
                hasGaiaArmorSet = body.type == ModContent.ItemType<Gaia_Robe>() &&
                                  legs.type == ModContent.ItemType<Gaia_Skirt>();
                return body.type == ModContent.ItemType<Gaia_Robe>() && legs.type == ModContent.ItemType<Gaia_Skirt>();
            }

            /*public override void AddRecipes()
            {
                Recipe mod = CreateRecipe(1);
                mod.AddIngredient<Leaf>(15);
                mod.AddIngredient(ItemID.JungleHat, 1);
                mod.AddIngredient(ItemID.Wood, 20);
                mod.AddTile(TileID.LivingLoom);
                mod.Register();
            }*/
        }
        
        [AutoloadEquip(EquipType.Body)]
        class Gaia_Robe : ModItem
        {
            public override void SetStaticDefaults()
            {
                DisplayName.SetDefault("Gaia Robe");
                Tooltip.SetDefault("Increased Armor Penetration by 5% \nIncreased maximum mana by 15 \nIncreased Magic damage by 5%");

                CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
            }

            public float armorPen = 0.05f; //Armor Penetration the Player gets when this Armor Piece gets Equipped
            public int manaIncrease = 15; //Increased Mana when Armor is Equipped
            public float MagicDamage = 0.05f; //Increased Magic damage by Percent


            public override void SetDefaults()
            {
                Item.width = 40;
                Item.height = 40;
                Item.defense = 7;
                Item.rare = ItemRarityID.Green;
            }
            
            public override bool MagicPrefix()
            {
                return true;
            }

            public override void UpdateEquip(Player player)
            {
                player.statManaMax2 += manaIncrease;
                player.GetArmorPenetration(DamageClass.Generic) += armorPen;
                player.GetDamage(DamageClass.Magic) += MagicDamage;
            }


            public override void UpdateArmorSet(Player player)
            {
                player.setBonus = "When you hit an Enemy you deal an Extra 1% Current Health Damage and get Healed for the Bonus Damage for " + activeDuration +
                                  " Seconds and then goes on Cooldown for " + cooldownDuration + " Seconds";
            }

            public override bool IsArmorSet(Item head, Item body, Item legs)
            {
                hasGaiaArmorSet = head.type == ModContent.ItemType<Gaia_Hat>() && legs.type == ModContent.ItemType<Gaia_Skirt>();
                return head.type == ModContent.ItemType<Gaia_Hat>() && legs.type == ModContent.ItemType<Gaia_Skirt>();
            }

            /*public override void AddRecipes()
            {
                Recipe mod = CreateRecipe(1);
                mod.AddIngredient<Leaf>(15);
                mod.AddIngredient(ItemID.JungleHat, 1);
                mod.AddIngredient(ItemID.Wood, 20);
                mod.AddTile(TileID.LivingLoom);
                mod.Register();
            }*/
        }
        
        [AutoloadEquip(EquipType.Legs)]
        class Gaia_Skirt : ModItem
        {
            public override void SetStaticDefaults()
            {
                DisplayName.SetDefault("Gaia Skirt");
                Tooltip.SetDefault("Increased Armor Penetration by 1% \nIncreased maximum mana by 20 \nIncreased Magic damage by 3%");

                CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
            }

            public float armorPen = 0.05f; //Armor Penetration the Player gets when this Armor Piece gets Equipped
            public int manaIncrease = 15; //Increased Mana when Armor is Equipped
            public float MagicDamage = 0.03f; //Increased Magic damage by Percent


            public override void SetDefaults()
            {
                Item.width = 32;
                Item.height = 20;
                Item.defense = 6;
                Item.rare = ItemRarityID.Green;
            }

            public override void UpdateEquip(Player player)
            {
                player.statManaMax2 += manaIncrease;
                player.GetArmorPenetration(DamageClass.Generic) += armorPen;
                player.GetDamage(DamageClass.Magic) += MagicDamage;
            }
            
            public override bool MagicPrefix()
            {
                return true;
            }
        
            public override void UpdateArmorSet(Player player)
            {
                player.setBonus = "When you hit an Enemy you deal an Extra 1% Current Health Damage and get Healed for the Bonus Damage for " + activeDuration +
                                  " Seconds and then goes on Cooldown for " + cooldownDuration + " Seconds";
            }
            

            public override bool IsArmorSet(Item head, Item body, Item legs)
            {
                hasGaiaArmorSet = head.type == ModContent.ItemType<Gaia_Hat>() && body.type == ModContent.ItemType<Gaia_Robe>();
                return head.type == ModContent.ItemType<Gaia_Hat>() && body.type == ModContent.ItemType<Gaia_Robe>();
            }

            /*public override void AddRecipes()
            {
                Recipe mod = CreateRecipe(1);
                mod.AddIngredient<Leaf>(15);
                mod.AddIngredient(ItemID.JungleHat, 1);
                mod.AddIngredient(ItemID.Wood, 20);
                mod.AddTile(TileID.LivingLoom);
                mod.Register();
            }*/
        }

        class LeafCooldown : ModBuff
        {
            public override void SetStaticDefaults()
            {
                DisplayName.SetDefault("Gaia Passive Cooldown");
                Description.SetDefault("Its a Cooldown");
                Main.debuff[Type] = true;
                BuffID.Sets.NurseCannotRemoveDebuff[Type] = true;
            }
        }

        class LeafActive : ModBuff
        {
            public override void SetStaticDefaults()
            {
                DisplayName.SetDefault("Gaia Passive");
                Description.SetDefault("Increases Damage by 1% of your current Health");
                BuffID.Sets.NurseCannotRemoveDebuff[Type] = true;
            }
        }

    }
}