using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.ModLoader;

namespace Crystals.Content.Foresta.Menus
{
    public class ForestaMystica : ModMenu
    {
        public override int Music => MusicLoader.GetMusicSlot(Mod, "Sounds/Music/Opening");
        
        public override string DisplayName => "Foresta Mystica";
        
        public override Asset<Texture2D> MoonTexture => ModContent.Request<Texture2D>("Crystals/Assets/Foresta/Menus/OvergrownMoon");
        
       //public override ModSurfaceBackgroundStyle MenuBackgroundStyle

       class ForestBG : ModSurfaceBackgroundStyle
       {
           public override void ModifyFarFades(float[] fades, float transitionSpeed)
           {
               throw new System.NotImplementedException();
           }

           public override int ChooseFarTexture()
           {
               return base.ChooseFarTexture();
           }

           public override int ChooseMiddleTexture()
           {
               return base.ChooseMiddleTexture();
           }

           public override int ChooseCloseTexture(ref float scale, ref double parallax, ref float a, ref float b)
           {
               return base.ChooseCloseTexture(ref scale, ref parallax, ref a, ref b);
           }
       }
        
    }
}