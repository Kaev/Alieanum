using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
namespace LudumDare33ByKaev
{
    class EnemyProjectile : GameObject
    {
        float m_Velocity;
        int numberOfTicks;

        public EnemyProjectile()
        {
            Load(@"graphics/bullet1.png");
            m_Velocity = 400f;
            numberOfTicks = 0;
        }

        public override void Draw(RenderTarget target)
        {
            target.Draw(Sprite);
        }

        public override void Update(float elapsedTime)
        {
            Vector2f movement = new Vector2f(0, 0);

            numberOfTicks++;

            movement.Y += m_Velocity;
            movement.X += 100 * (float)Math.Sin(numberOfTicks * 0.1 * Math.PI); // bullets move in a sinus curve

            Sprite.Position += movement * elapsedTime;

            Player player = (Player)Game.ObjectManager.GetObject("player");
            if (player != null)
            {
                if (!player.AlreadyHit && Game.CollisionManager.Collision(player.Sprite, Sprite, 1))
                {
                    player.Hit();
                }
            }
        }
    }
}
