using Crystals.Core.Systems;
using Microsoft.Xna.Framework;
using System.Security.Cryptography.X509Certificates;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Crystals.Content
{
    internal class TestarangProjectile : ModProjectile
    {
        public override string Texture => AssetDir.Projectiles + Name;
        public Vector2 SpawnLoc;
        public float Charge
        {
            get => Projectile.localAI[0];
            set => Projectile.localAI[0] = value;
        }
        public const float MaxCharge = 120;
        public bool IsPerfect;
        public override void SetDefaults()
        {
        
            Projectile.damage = 10;
            Projectile.width = 32;
            Projectile.height = 32;
            Projectile.penetrate = 1;
            Projectile.friendly = true;

        }
        public override void OnSpawn(IEntitySource source)
        {
            SpawnLoc = Projectile.position;
        }
        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            //Projectile.velocity.X -= 0.2f;
            Projectile.rotation += 0.2f;
            player.heldProj = Projectile.whoAmI;

            WindUp(player);
            if (!player.channel && Charge == 0) return;
            Shoot(player);
        }
        private void WindUp(Player player)
        {
            if(player.channel == true && Charge < MaxCharge)
            {
                Charge++;
            }

            else
            {
                Charge = 0;    
            }

            if(Charge < 50 || Charge > 100)
            {
                Main.NewText("Bad");
                IsPerfect = false;
            }
            else
            {
                Main.NewText("Perfect");
                IsPerfect = true;
            }
        }

        private void Shoot(Player player)
        {
            if(!IsPerfect)
            {

            }
            else
            {

            }
        }
        public override void OnKill(int timeLeft)
        {
            if (Projectile.owner == Main.myPlayer && !Projectile.noDropItem)
            {
                int dropItemType = ModContent.ItemType<Testarang>();
                int newItem = Item.NewItem(Projectile.GetSource_DropAsItem(), Projectile.Hitbox, dropItemType);
                Main.item[newItem].noGrabDelay = 0;

                if (Main.netMode == NetmodeID.MultiplayerClient && newItem >= 0)
                {
                    NetMessage.SendData(MessageID.SyncItem, -1, -1, null, newItem, 1f);
                }
            }
        }

    }
}