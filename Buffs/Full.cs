using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace enchtablethemod.Buffs
{
    public class Full : ModBuff
    {
		public override void SetDefaults()
		{
		//bababooey
			DisplayName.SetDefault("Full");
			Description.SetDefault("You've eaten enough and don't want to cook anything else yet");
			Main.debuff[Type] = true;
			Main.buffNoSave[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			player.GetModPlayer<Chef>().recipeCooldown = true;
		}
	}
}
