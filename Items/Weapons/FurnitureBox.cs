using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace enchtablethemod.Items.Weapons
{

    public class FurnitureBox : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Furniture Box");
        }

        public override void SetDefaults()
        {
            item.width = 15;
            item.height = 21;
            item.maxStack = 1;
            item.value = 100;
            item.rare = 1;
            item.useStyle = 5;
            item.autoReuse = true;
            item.holdStyle = 1;
            item.autoReuse = true;
            item.reuseDelay = 6;
            item.rare = ItemRarityID.LightRed;
            item.noMelee = true;
            item.magic = true;
            item.mana = 5;

            // Set other item.X values here
        }

        public override void AddRecipes()
        {
            // Recipes here. See Basic Recipe Guide
        }
    }
}
