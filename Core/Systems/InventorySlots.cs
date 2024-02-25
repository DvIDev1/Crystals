using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using Crystals.Content.Items.Crystals;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.GameInput;
using Terraria.ModLoader;
using Terraria.UI;

namespace Crystals.Core.Systems
{
    internal class CrystalSlot : UIElement
    {
        internal Item Item;
        private readonly int contex;
        private readonly float scal;
        internal Func<Item, bool> ValidItemFunc;
        private Color Color;
        int scale1 = 50;
        private UIElement area;


        public CrystalSlot(int context = ItemSlot.Context.BankItem, float scale = 1f)
        {
            area = new UIElement();
            area.Left.Set(353f, 0f);
            area.Top.Set(258f, 0f);
            area.Width.Set(60f, 0f);
            area.Height.Set(60f, 0f);
            contex = context;
            
            scal = scale;
            Item = new Item();
            Item.SetDefaults();
            Width.Set(scale1 * scale, 0f);
            Height.Set(scale1 * scale, 0f);
            Append(area);

        }

        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            float oldScale = Main.inventoryScale;
            Main.inventoryScale = scal;

            Rectangle rectangle = GetDimensions().ToRectangle();

            if (ContainsPoint(Main.MouseScreen) && !PlayerInput.IgnoreMouseInterface)
            {
                Main.LocalPlayer.mouseInterface = true;
                if (ValidItemFunc == null || ValidItemFunc(Main.mouseItem))
                {
                    ItemSlot.Handle(ref Item, contex);
                }
            }

            ItemSlot.Draw(spriteBatch, ref Item, 14, rectangle.TopLeft());
            Main.inventoryScale = oldScale;
        }
    }

    internal class TechniquesSlot : UIElement
    {
        internal Item Item;
        private readonly int contex;
        private readonly float scal;
        internal Func<Item, bool> ValidItemFunc;
        private Color Color;
        int scale1 = 50;
        private UIElement area;

        public override void OnInitialize()
        {
            Color = new Color(255, 255, 255);

            base.OnInitialize();
        }
        public TechniquesSlot(int context = ItemSlot.Context.BankItem, float scale = 1f)
        {
            area = new UIElement();
            area.Left.Set(400f, 0f);
            area.Top.Set(258f, 0f);
            area.Width.Set(60f, 0f);
            area.Height.Set(60f, 0f);
            contex = context;
            scal = scale;
            Item = new Item();
            Item.SetDefaults();
            Width.Set(scale1 * scale, 0f);
            Height.Set(scale1 * scale, 0f);
            Append(area);

        }

        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            float oldScale = Main.inventoryScale;
            Main.inventoryScale = scal;

            Rectangle rectangle = GetDimensions().ToRectangle();

            if (ContainsPoint(Main.MouseScreen) && !PlayerInput.IgnoreMouseInterface)
            {
                Main.LocalPlayer.mouseInterface = true;
                if (ValidItemFunc == null || ValidItemFunc(Main.mouseItem))
                {
                    ItemSlot.Handle(ref Item, contex);
                }
            }

            ItemSlot.Draw(spriteBatch, ref Item, 14, rectangle.TopLeft());
            Main.inventoryScale = oldScale;
        }
    }
    internal class SlotPlacement : UIState
    {
        public static CrystalSlot CRS;
        public static TechniquesSlot TCS;
        public UIPanel CrystalPanel;
        public UIPanel TechniquePanel;
        public static Color CrystalColor;
        public override void OnInitialize()
        {
            CrystalPanel = new UIPanel();
            TechniquePanel = new UIPanel();

            CrystalPanel.Width.Set(50f* 0.85f, 0);
            CrystalPanel.Height.Set(50f* 0.85f, 0);
            CrystalPanel.Left.Set(353f,0);
            CrystalPanel.Top.Set(258,0);


            TechniquePanel.Width.Set(50f* 0.85f, 0);
            TechniquePanel.Height.Set(50f* 0.85f, 0);
            TechniquePanel.Left.Set(400f,0);
            TechniquePanel.Top.Set(258,0);
            TechniquePanel.BackgroundColor = new Color(63, 65, 151);


            CRS = new CrystalSlot(ItemSlot.Context.BankItem, 0.85f)
            {
                Top = { Pixels = 258f },
                Left = { Pixels = 353f },
                ValidItemFunc = item => item.IsAir || !item.IsAir && ValidSlotItems.ValidCrystals.Contains(item.type)


            };
            TCS = new TechniquesSlot(ItemSlot.Context.BankItem, 0.85f)
            {
                
                Top = { Pixels = 258f },
                Left = { Pixels = 400f },
                ValidItemFunc = item => item.IsAir || !item.IsAir && ValidSlotItems.ValidTechniques.Contains(item.type)

            };


            Append(TechniquePanel);
            Append(CrystalPanel);
            Append(CRS);
            Append(TCS);
        }

        public override void Update(GameTime gameTime)
        {
           //This determines the color of the slot,i couldnt find a better way to do it unfortunately since there isnt a reliable way to find what is inside the slot
            if (!CRS.Item.IsAir)
            {
                if(CRS.Item.type == ModContent.ItemType<GreenCrystal>())
                {
                CrystalPanel.BackgroundColor = Color.Green;
                }

                if(CRS.Item.type == ModContent.ItemType<RedCrystal>())
                {
                CrystalPanel.BackgroundColor = Color.Red;
                }
                
            }
            else { 
                
                CrystalPanel.BackgroundColor = Color.Gray;
            }
        }

    }
    internal class CrystalSlotImage : UIState
    {
        public UIImage CrystalImage;
        public override void OnInitialize()
        {
        CrystalImage = new UIImage(ModContent.Request<Texture2D>(AssetDir.UI + "UI_Crystal"));
            CrystalImage.Width.Set(30f * 0.85f, 0);
            CrystalImage.Height.Set(30f * 0.85f, 0);
            CrystalImage.Left.Set(348f, 0);
            CrystalImage.Top.Set(254f, 0);
            CrystalImage.ImageScale = 0.58f;
            
            Append(CrystalImage);

        }
    }
    internal class TechniqueSlotImage : UIState
    {
        public UIImage TechniqueImage;
        public override void OnInitialize()
        {
            TechniqueImage = new UIImage(ModContent.Request<Texture2D>(AssetDir.Techniques + "Technique"));
            TechniqueImage.Width.Set(30f * 0.85f, 0);
            TechniqueImage.Height.Set(30f * 0.85f, 0);
            TechniqueImage.Left.Set(395f, 0);
            TechniqueImage.Top.Set(254f, 0);
            TechniqueImage.ImageScale = 0.58f;
            
            Append(TechniqueImage);

        }
    }

    internal class InventorySlots : ModSystem
    {
        private UserInterface SlotUI;
        internal SlotPlacement Slots;
        private UserInterface CrystalImage;
        internal CrystalSlotImage CRImage;
        private UserInterface TechniqueImage;
        internal TechniqueSlotImage TCImage;

        public override void Load()
        {
            Slots = new();
            SlotUI = new();
            SlotUI.SetState(Slots);
            
            CRImage = new();
            CrystalImage = new();
            CrystalImage.SetState(CRImage);
            
            TCImage = new();
            TechniqueImage = new();
            TechniqueImage.SetState(TCImage);
        }

        public override void UpdateUI(GameTime gameTime)
        {
            SlotUI.Update(gameTime);
            CrystalImage.Update(gameTime);
            TechniqueImage.Update(gameTime);
        }

        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            int resourceBarIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Inventory"));
            if (resourceBarIndex != -1)
            {
                layers.Insert(resourceBarIndex, new LegacyGameInterfaceLayer(
                "meltingsoulsmod : Frame",
                delegate
                {
                    var player = Main.LocalPlayer;
                    if (Main.playerInventory == true)
                    {
                        SlotUI.Draw(Main.spriteBatch, new GameTime());
                        if (SlotPlacement.CRS.Item.IsAir)
                        {
                            CrystalImage.Draw(Main.spriteBatch, new GameTime());
                        }
                        if (SlotPlacement.TCS.Item.IsAir)
                        {
                            TechniqueImage.Draw(Main.spriteBatch, new GameTime());
                        }
                    }
                    return true;
                },
                InterfaceScaleType.UI)
            );


            }

        }
    }
}

