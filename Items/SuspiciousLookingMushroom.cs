using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using enchtablethemod.NPCs.Bosses;

namespace enchtablethemod.Items
{
    public class SuspiciousLookingMushroom : ModItem
    {
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Summons the plumber's fabrication");
			ItemID.Sets.SortingPriorityBossSpawns[item.type] = 13; // This helps sort inventory know this is a boss summoning item.
		}

		public override void SetDefaults()
		{
			item.width = 20;
			item.height = 20;
			item.maxStack = 20;
			item.rare = ItemRarityID.Blue;
			item.useAnimation = 45;
			item.useTime = 45;
			item.useStyle = ItemUseStyleID.HoldingUp;
			item.UseSound = SoundID.Item44;
			item.consumable = true;
		}

        public override bool CanUseItem(Player player)
        {
			return player.ZoneOverworldHeight && !NPC.AnyNPCs(NPCType<NPCs.Bosses.Fake>());

		}

		public override bool UseItem(Player player)
		{
			NPC.SpawnOnPlayer(player.whoAmI, NPCType<Fake>());
			Main.PlaySound(SoundID.Roar, player.position, 0);
			return true;
		}

		public override void AddRecipes()
        {
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.Mushroom, 5);
			recipe.AddIngredient(ItemID.VileMushroom, 5);
			recipe.AddTile(TileID.DemonAltar);
			recipe.SetResult(this);
			recipe.AddRecipe();
			ModRecipe recipe2 = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.Mushroom, 5);
			recipe.AddIngredient(ItemID.ViciousMushroom, 5);
			recipe.AddTile(TileID.DemonAltar);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
    }
}
