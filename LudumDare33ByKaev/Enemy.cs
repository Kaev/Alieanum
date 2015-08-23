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
    class Enemy : GameObject
    {
        Random random = new Random();
        float m_TimeBetweenShots;
        float m_Velocity;

        public Enemy()
        {
            Load(@"graphics/enemy1.png");
            m_Velocity = 200f;
            m_TimeBetweenShots = 0f;
        }

        public override void Draw(RenderTarget target)
        {
            target.Draw(Sprite);
        }

        public override void Update(float elapsedTime)
        {
            m_TimeBetweenShots += elapsedTime;
            Vector2f movement = new Vector2f(0, 0);

            if (m_TimeBetweenShots > 0.8f)
            {
                EnemyProjectile enemyProjectile = new EnemyProjectile();
                enemyProjectile.Position = new Vector2f(Sprite.Position.X + Sprite.GetGlobalBounds().Width / 2 - enemyProjectile.Width / 2,
                                                        Sprite.Position.Y + Sprite.GetGlobalBounds().Height / 2);

                string name = "enemyProjectile_" + random.Next().ToString();
                Game.ObjectManager.AddObject(name, enemyProjectile);

                m_TimeBetweenShots = 0;
            }

            movement.Y += m_Velocity;
            Sprite.Position += movement * elapsedTime;

            Player player = (Player)Game.ObjectManager.GetObject("player");
            if (player != null)
                if (!player.AlreadyHit && Game.CollisionManager.Collision(player.Sprite, Sprite, 1))
                    player.Hit();
        }
    }
}
