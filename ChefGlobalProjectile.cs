using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace enchtablethemod.Projectiles
{
	public class ChefGlobalProjectile : GlobalProjectile // This is sadly stolen from Clicker Class but I wasn't able to figure out how to do it
	{
		public override void ModifyHitNPC(Projectile projectile, NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
		{
			if (!(projectile.modProjectile is ChefProjectile chefProj && chefProj.isChefProj))
			{
				return;
			}

			Player player = Main.player[projectile.owner];

			// Vanilla crit chance calculations. Crit chance of the currently held weapon matters, regardless of the damage type of the weapon.
			int critChance = player.HeldItem.crit;
			ItemLoader.GetWeaponCrit(player.HeldItem, player, ref critChance);
			PlayerHooks.GetWeaponCrit(player, player.HeldItem, ref critChance);
			crit = critChance >= 100 || Main.rand.Next(1, 101) <= critChance;
		}
	}
}