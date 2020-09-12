using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace enchtablethemod.NPCs.Bosses
{
    public class Phanto : ModNPC
    {
		int despawn = 0;
        public override void SetDefaults()
        {
			npc.width = 40;
			npc.height = 24;
			npc.aiStyle = 86;
			npc.damage = 10;
			npc.defense = 0;
			npc.lifeMax = 22;
			npc.HitSound = SoundID.NPCHit52;
			npc.DeathSound = SoundID.NPCDeath55;
			npc.knockBackResist = 0f;
			npc.value = 0f;
			npc.npcSlots = 0.1f;
		}

		private void despawnTest(NPC npc) // semi-stolen but kind of changed
		{
			if (Main.player[npc.target].dead == true || Vector2.Distance(Main.player[npc.target].Center, npc.Center) > 3500)
			{
				npc.TargetClosest(true);
				if (Main.player[npc.target].dead == true || Vector2.Distance(Main.player[npc.target].Center, npc.Center) > 3500)
				{
					if (despawn == 0) despawn++;
				}
			}
			if (despawn > 0)
			{
				despawn++;
				npc.velocity.Y = 10f;
				npc.noTileCollide = true;
				if (despawn >= 150) npc.active = false;
			}
		}

		public override void AI()
        {
			despawnTest(npc);
			for (int num368 = 0; num368 < 200; num368++)
			{
				if (num368 == npc.whoAmI || !Main.npc[num368].active || Main.npc[num368].type != npc.type)
				{
					continue;
				}
				Vector2 vector120 = Main.npc[num368].Center - npc.Center;
				if (!(vector120.Length() < 50f))
				{
					continue;
				}
				vector120.Normalize();
				if (vector120.X == 0f && vector120.Y == 0f)
				{
					if (num368 > npc.whoAmI)
					{
						vector120.X = 0.4f;
					}
					else
					{
						vector120.X = -0.4f;
					}
				}
				vector120 *= 0.4f;
				npc.velocity -= vector120;
				NPC nPC8 = Main.npc[num368];
				NPC nPC20 = nPC8;
				nPC20.velocity += vector120;
			}
				float num370 = 120f;
				if (npc.localAI[0] < num370)
				{
					if (npc.localAI[0] == 0f)
					{
						Main.PlaySound(SoundID.Item8, npc.Center);
						npc.TargetClosest();
						if (npc.direction > 0)
						{
							npc.velocity.X += 1.1f;
						}
						else
						{
							npc.velocity.X -= 1.1f;
						}
						for (int num371 = 0; num371 < 20; num371++)
						{
							Vector2 center27 = npc.Center;
							center27.Y -= 18f;
							Vector2 vector121 = new Vector2(Main.rand.Next(-100, 101), Main.rand.Next(-100, 101));
							vector121.Normalize();
							vector121 *= (float)Main.rand.Next(0, 100) * 0.1f;
							center27 += vector121;
							vector121.Normalize();;
						}
					}
					npc.localAI[0] += 1f;
					float num373 = 1f - npc.localAI[0] / num370;
					float num374 = num373 * 20f;
					for (int num375 = 0; (float)num375 < num374; num375++)
					{
						if (Main.rand.Next(5) == 0)
						{
							int num376 = Dust.NewDust(npc.position, npc.width, npc.height, 106);
							Main.dust[num376].alpha = 100;
							Dust dust32 = Main.dust[num376];
							Dust dust81 = dust32;
							dust81.velocity *= 0.3f;
							dust32 = Main.dust[num376];
							dust81 = dust32;
							dust81.velocity += npc.velocity * 0.75f;
							Main.dust[num376].noGravity = true;
						}
					}
				}
			}
		}


    }
