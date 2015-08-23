using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;

namespace LudumDare33ByKaev
{
    class ItemSide : GameObject
    {
        public ItemSide()
        {
            Load(@"graphics/item1.png");
        }

        public override void Draw(RenderTarget target)
        {
            if (Visible)
                target.Draw(Sprite);
        }

        public override void Update(float elapsedTime)
        {
            Player player = (Player)Game.ObjectManager.GetObject("player");
            if (player != null)
            {
                if (Visible && Game.CollisionManager.Collision(player.Sprite, Sprite, 1))
                {
                    Game.SoundManager.PlaySound("powerup");
                    Game.ItemManager.PlayerHasItem[1] = true;
                    Visible = false;
                    MainMenu.IncrementScore(2);
                }
            }
        }
    }
}
