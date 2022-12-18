using System;
using DiscordRPC;
using DiscordRPC.Logging;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace Crystals
{
	public class Crystals : Mod
	{

		public static Effect npcEffect;
		
		
		
		public override void Load()
		{
			//Loaded Shaders 
			if (Main.netMode != NetmodeID.Server)
			{
				((EffectManager<Filter>)(object)Filters.Scene)["edge"] = new Filter(new ScreenShaderData(new Ref<Effect>(ModContent.Request<Effect>("Crystals/Effects/Edge", (AssetRequestMode)1).Value), "Edge"), (EffectPriority)4);
				((GameEffect)((EffectManager<Filter>)(object)Filters.Scene)["edge"]).Load();
				npcEffect = Filters.Scene["edge"].GetShader().Shader;
			}
		}
	}
}