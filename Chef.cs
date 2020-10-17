using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace enchtablethemod
{
    public class Chef : ModPlayer
    {
        public static Chef ModPlayer(Player player)
        {
            return player.GetModPlayer<Chef>();
        }

        public float chefDamageAdd;
        public float chefDamageMult = 1f;
        public float chefKB;
        public int chefCrit;

        public bool recipeCooldown;

        public override void ResetEffects()
        {
            ResetVariables();
        }

        public override void UpdateDead()
        {
            ResetVariables();
        }

        private void ResetVariables()
        {
            chefDamageAdd = 0f;
            chefDamageMult = 1f;
            chefKB = 0f;
            chefCrit = 0;

            recipeCooldown = false;
        }
    }
}
