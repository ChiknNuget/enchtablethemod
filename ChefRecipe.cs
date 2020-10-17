using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using static Terraria.ModLoader.ModContent;
using enchtablethemod.Buffs;

namespace enchtablethemod
{
    public abstract class ChefRecipe : ModItem
    {
        public bool isRecipe = true;

        public override bool CanUseItem(Player player)
        {
            return !player.HasBuff(BuffType<Full>());
        }
    }
}
