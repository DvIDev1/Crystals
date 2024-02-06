using Crystals.Content.Items.Weapons.Ranged;
using Crystals.Core.Systems;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Crystals.Content.Projectiles
{
    internal class TestarangProjectile : ModProjectile
    {
        public override string Texture => AssetDir.Projectiles + Name;
        public Vector2 SpawnLoc;
        
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
