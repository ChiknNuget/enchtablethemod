using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace enchtablethemod
{
    public abstract class ChefDamageItem : ModItem // STOLEN:tm: from daedalus class code but i'm pretty sure it's fine since half of the former PaS devs are in ench table
    {
		public virtual void SafeSetDefaults()
		{

		}
		public sealed override void SetDefaults()
		{
			SafeSetDefaults();
			item.melee = false;
			item.ranged = false;
			item.magic = false;
			item.thrown = false;
			item.summon = false;
		}
		public override void ModifyWeaponDamage(Player player, ref float add, ref float mult, ref float flat)
		{ 
			add += Chef.ModPlayer(player).chefDamageAdd;
			mult *= Chef.ModPlayer(player).chefDamageMult;
		}

		public override void GetWeaponKnockback(Player player, ref float knockback)
		{
			knockback += Chef.ModPlayer(player).chefKB;
		}

		public override void GetWeaponCrit(Player player, ref int crit)
		{
			crit += Chef.ModPlayer(player).chefCrit;
		}

		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			TooltipLine tt = tooltips.FirstOrDefault(x => x.Name == "Damage" && x.mod == "Terraria");
			if (tt != null)
			{
				string[] splitText = tt.text.Split(' ');
				string damageValue = splitText.First();
				string damageWord = splitText.Last();
				tt.text = damageValue + " chef " + damageWord;
			}
		}
	}
}
