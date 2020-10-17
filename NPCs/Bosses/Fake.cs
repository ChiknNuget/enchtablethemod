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

namespace enchtablethemod.NPCs.Bosses.FakeBoss
{
    [AutoloadBossHead]

    public class Fake : ModNPC
    {
        public int despawn = 0;
        int PhaseVar { get; set; } = 0;
        int FunnyCounter { get; set; } = 0;
        public Vector2 targetPos;
        public float xAccel = 0;
        private int movement = -1;
        public float timer
        {
            get => npc.ai[2];
            set => npc.ai[2] = value;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Fake");
            Main.npcFrameCount[npc.type] = 1;
        }



        public override void SetDefaults()
        {
            npc.width = 68;
            npc.height = 106;

            npc.boss = true;
            npc.aiStyle = -1;
            aiType = -1;
            npc.npcSlots = 5;

            npc.lifeMax = 1500;
            npc.damage = 20;
            npc.defense = 7;
            npc.knockBackResist = 0f;

            npc.value = Item.buyPrice(gold: 3);

            npc.lavaImmune = true;
            npc.noTileCollide = true;
            npc.noGravity = true;

            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;
            music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/DotheMario");
            musicPriority = MusicPriority.BossHigh;
        }

        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = (2000 + (500 * numPlayers));
            npc.damage = (int)(npc.damage * 1.3f);
        }

        public override void AI()
        {
            despawnTest(npc);
            executeAttacks();

        }

        private void executeAttacks()
        {
            Player player = Main.player[npc.target];
            npc.TargetClosest(true);
            Vector2 target = npc.HasPlayerTarget ? player.Center : Main.npc[npc.target].Center;
            movement = Main.rand.Next(0, 1);
            npc.netAlways = true;
            switch (PhaseVar)
            {
                case 0:
                    teleportThingButAwsome();
                    timer = 0;
                    break;
                case 1:
                    minionCrap();
                    break;
                case 2:
                    PhaseVar = 0;
                    npc.position.X -= 1000;
                    FunnyDust();
                    break;
            }
            npc.netUpdate = true;
        }

        private void teleportThing()
        {
        //DO NOT DELETE I WILL BE USING THIS FOR LATER -arnold
            if (FunnyCounter % 2 == 0)
                targetPos = Main.player[npc.target].Center + new Vector2(Main.rand.Next(300,700), Main.rand.Next(-100,100));
            else
            {
                FunnyDust();
                npc.position.X -= 1500;
                FunnyDust();
                Main.PlaySound(SoundID.Item8, npc.Center);
            }
            targetPos = Main.player[npc.target].Center + new Vector2(Main.rand.Next(300, 700), Main.rand.Next(-100, 100));
            if (npc.Center.Y > targetPos.Y)
            {
                npc.velocity.Y -= .2f;
                if (npc.velocity.Y > 0)
                    npc.velocity.Y *= .9f;
            }
            if (npc.Center.Y < targetPos.Y)
            {
                npc.velocity.Y += .2f;
                if (npc.velocity.Y < 0)
                    npc.velocity.Y *= .9f;
            }
            if (Math.Abs(npc.velocity.Y) > 7)
                npc.velocity.Y *= .98f;
            if (npc.Center.X > targetPos.X)
            {
                xAccel -= .3f;
                if (FunnyCounter % 2 == 0)
                {
                    FunnyCounter++;
                }
            }
            if (npc.Center.X < targetPos.X)
            {
                xAccel += .3f;
                if (FunnyCounter % 2 != 0)
                {
                    FunnyCounter++;
                }
            }
            if (Math.Abs(xAccel) > 7)
                xAccel *= .98f;
            npc.velocity.X = xAccel;
            if (FunnyCounter > 6)
            {
                FunnyCounter = 0;
                PhaseVar++;
            }
        }

        private void teleportThingButAwsome()
        {
        //teleport thing but flipped
            if (FunnyCounter % 2 == 0)
                targetPos = Main.player[npc.target].Center + new Vector2(Main.rand.Next(-700, -300), Main.rand.Next(-100, 100));
            else
            {
                FunnyDust();
                npc.position.X -= -1500;
                FunnyDust();
                Main.PlaySound(SoundID.Item8, npc.Center);
            }
            targetPos = Main.player[npc.target].Center + new Vector2(Main.rand.Next(-700, -300), Main.rand.Next(-100, 100));
            if (npc.Center.Y > targetPos.Y)
            {
                npc.velocity.Y -= .2f;
                if (npc.velocity.Y > 0)
                    npc.velocity.Y *= .9f;
            }
            if (npc.Center.Y < targetPos.Y)
            {
                npc.velocity.Y += .2f;
                if (npc.velocity.Y < 0)
                    npc.velocity.Y *= .9f;
            }
            if (Math.Abs(npc.velocity.Y) > 7)
                npc.velocity.Y *= .98f;
            if (npc.Center.X < targetPos.X)
            {
                xAccel -= -.3f;
                if (FunnyCounter % 2 == 0)
                {
                    FunnyCounter++;
                }
            }
            if (npc.Center.X > targetPos.X)
            {
                xAccel += -.3f;
                if (FunnyCounter % 2 != 0)
                {
                    FunnyCounter++;
                }
            }
            if (Math.Abs(xAccel) > 7)
                xAccel *= .98f;
            npc.velocity.X = xAccel;
            if (FunnyCounter > 6)
            {
                FunnyCounter = 0;
                PhaseVar++;
            }
        }

        private void minionCrap()
        {
            npc.velocity *= 0.9f;
            if (timer >= 150)
            {
                NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, NPCType<Phanto>(), npc.whoAmI);
                timer = 0;
                PhaseVar++;
            }
            timer += 1f;
        }

        

        private void FunnyDust()
        {
            for (int i = 0; i < 25; i++)
            {
                int dust = Dust.NewDust(npc.position, npc.width, npc.height, 259);
                Main.dust[dust].velocity *= 6f;
            }
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

    }
}

class FakeFire : ModProjectile
{
    public override string Texture => "Terraria/Projectile_" + ProjectileID.CursedFlameHostile;
    public override void SetDefaults()
    {
        projectile.CloneDefaults(ProjectileID.CursedFlameHostile);
    }
}
