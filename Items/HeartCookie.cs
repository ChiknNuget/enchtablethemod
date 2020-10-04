using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using ChefClass.Buffs;

namespace ChefClass.Items
{
    public class HeartCookie : ChefRecipe
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Recipe: Heart Cookie");
            Tooltip.SetDefault("Cooks a heart cookie, granting Rapid Healing for 10 seconds\nUsing this prevents you from using recipes for 25 seconds");
        }

		public override void SetDefaults()
		{
			item.width = 20;
			item.height = 20;
			item.useTime = 17;
			item.useAnimation = 17;
			item.useStyle = 2;
			item.useTurn = true;
			item.value = Item.sellPrice(0, 0, 75, 0);
			item.rare = ItemRarityID.LightRed;
			item.UseSound = SoundID.Item2;
		}

        public override bool UseItem(Player player)
        {
            player.AddBuff(BuffID.RapidHealing, 600);
            player.AddBuff(BuffType<Full>(), (60 * 35));
            return true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.PalladiumBar, 8);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

    }
}
