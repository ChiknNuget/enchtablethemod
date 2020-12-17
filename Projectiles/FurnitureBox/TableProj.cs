using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace enchtablethemod.Projectiles.FurnitureBox
{
	public class TableProj : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("(ench)table");
		}

		public override void SetDefaults()
		{
			projectile.arrow = true;
			projectile.width = 15;
			projectile.height = 11;
			projectile.aiStyle = 1;
			projectile.friendly = true;
			projectile.ranged = true;
			aiType = ProjectileID.WoodenArrowFriendly;
		}

		// Additional hooks/methods here.
	}
}
