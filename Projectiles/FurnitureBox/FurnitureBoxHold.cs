using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace enchtablethemod.Projectiles.FurnitureBox
{
	public class FurnitureBoxHold : ModProjectile
	{
		//DO NOT UNCOMMENT ANY COMMENTED CODE LEGOFACTOR
		//Holding style for the furniture box

		//Right here are some basic variable switcharoos because I am lazy ass
		public float activation
		{
			get => projectile.ai[1];
			set => projectile.ai[1] = value;

		}
		public float sanschungusingame
		{
			get => projectile.ai[0];
			set => projectile.ai[0] = value;

		}
		//Display name even though projectiles don't even have a proper display name thing
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("FBox");
			Main.projFrames[projectile.type] = 9;
		}

		//Basic Defaults
		public override void SetDefaults()
		{
			projectile.width = 13;
			projectile.height = 19;
			projectile.aiStyle = 1;
			projectile.friendly = true;
			projectile.magic = true;
			projectile.tileCollide = false;
			projectile.aiStyle = 75;
			projectile.hide = true;
			projectile.ignoreWater = true;
		}


		//Box AI
		public override void AI()
        {

			Player player = Main.player[projectile.owner];
			float rot = 0;
			Vector2 vector = player.RotatedRelativePoint(player.MountedCenter);
			if (projectile.spriteDirection == -1)
			{
				rot = (float)Math.PI;
			}
			sanschungusingame += 1f;
			int margin = 5;
			int margin2 = 0;
			activation -= 1f;
			bool check = false;
			if (activation <= 0f)
			{
				activation = margin - margin2;
				check = true;
				/*if ((int)sanschungusingame / (margin - margin2) % 7 == 0)
				{
					tempprojshoot = 0;
				}*/
			}
			//Frames
			//-TODO(done) Sync the animation and the proj shooting
			projectile.frameCounter += 1;
			if (projectile.frameCounter >= 9)
			{
				projectile.frameCounter = 0;
				projectile.frame++;

				//Main.NewText("yo mama " + tempprojshoot);
				if (projectile.frame >= Main.projFrames[projectile.type])
				{
					projectile.frame = 0;
					//Main.NewText("yo mama " + tempprojshoot);
				}
			}
			if (projectile.soundDelay <= 0)
			{
				//TODO: Give it a better sound because boxes dont shoot like guns
				projectile.soundDelay = margin - margin2;
				if (projectile.ai[0] != 1f)
				{
					Main.PlaySound(SoundID.Item36, projectile.position);
				}
			}
			if (check && Main.myPlayer == projectile.owner)
			{
				bool canShoot = player.channel && player.HasAmmo(player.inventory[player.selectedItem], canUse: true) && !player.noItems && !player.CCed;
				float speed = 14f;
				int Damage = player.GetWeaponDamage(player.inventory[player.selectedItem]);
				float KnockBack = player.inventory[player.selectedItem].knockBack;
				if (canShoot)
				{
					KnockBack = player.GetWeaponKnockback(player.inventory[player.selectedItem], KnockBack);
					float num37 = player.inventory[player.selectedItem].shootSpeed * projectile.scale;
					Vector2 value10 = vector;
					Vector2 value11 = Main.screenPosition + new Vector2(Main.mouseX, Main.mouseY) - value10;
					if (player.gravDir == -1f)
					{
						value11.Y = (float)(Main.screenHeight - Main.mouseY) + Main.screenPosition.Y - value10.Y;
					}
					Vector2 spinningpoint = Vector2.Normalize(value11);
					if (float.IsNaN(spinningpoint.X) || float.IsNaN(spinningpoint.Y))
					{
						spinningpoint = -Vector2.UnitY;
					}
					spinningpoint *= num37;
					spinningpoint = spinningpoint.RotatedBy(Main.rand.NextDouble() * 0.13089969754219055 - 0.065449848771095276);
					if (spinningpoint.X != projectile.velocity.X || spinningpoint.Y != projectile.velocity.Y)
					{
						projectile.netUpdate = true;
					}
					projectile.velocity = spinningpoint;
					if (projectile.frame == 0)
					{
						speed = 8f;
						
						Vector2 projrot = Vector2.Normalize(projectile.velocity) * speed;
						projrot = projrot.RotatedBy(Main.rand.NextDouble() * 0.39269909262657166 - 0.19634954631328583);
						if (float.IsNaN(projrot.X) || float.IsNaN(projrot.Y))
						{
							projrot = -Vector2.UnitY;
						}
						//Shoots a table
						//TODO: Make it shoot only 1 projectile at a time
						//TODO: Make it shoot more projectiles at random (ex:chair,workbench,lamp)
						Projectile.NewProjectile(value10.X, value10.Y, projrot.X, projrot.Y, ModContent.ProjectileType<TableProj>(), Damage + 20, KnockBack * 1.25f, projectile.owner);

						
					}
				}
				else
				{
					projectile.Kill();
				}
			}
			projectile.position = player.RotatedRelativePoint(player.MountedCenter) - projectile.Size / 2f;
			projectile.rotation = projectile.velocity.ToRotation() + rot;
			projectile.spriteDirection = projectile.direction;
			projectile.timeLeft = 2;
			player.ChangeDir(projectile.direction);
			player.heldProj = projectile.whoAmI;
			player.itemTime = 2;
			player.itemAnimation = 2;
			player.itemRotation = (float)Math.Atan2(projectile.velocity.Y * (float)projectile.direction, projectile.velocity.X * (float)projectile.direction);
			projectile.position.Y += player.gravDir * 2f;
		}
		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
		{
			//Draw effects which I stole
			SpriteEffects spriteEffects = SpriteEffects.None;
			if (projectile.spriteDirection == -1)
			{
				spriteEffects = SpriteEffects.FlipHorizontally;
			}
			Texture2D texture = Main.projectileTexture[projectile.type];
			int frameHeight = Main.projectileTexture[projectile.type].Height / Main.projFrames[projectile.type];
			int startY = frameHeight * projectile.frame;
			Rectangle sourceRectangle = new Rectangle(0, startY, texture.Width, frameHeight);
			Vector2 origin = sourceRectangle.Size() / 2f;
			origin.X = (float)(projectile.spriteDirection == 1 ? sourceRectangle.Width - 20 : 20);

			Color drawColor = projectile.GetAlpha(lightColor);
			Main.spriteBatch.Draw(texture,
				projectile.Center - Main.screenPosition + new Vector2(0f, projectile.gfxOffY),
				sourceRectangle, drawColor, projectile.rotation, origin, projectile.scale, spriteEffects, 0f);

			return false;
		}
	}
}
