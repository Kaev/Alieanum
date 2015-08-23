using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.System;

namespace LudumDare33ByKaev
{
    class EnemyRocket: GameObject
    {
        Random random = new Random();
        float m_Velocity;
        bool m_Direction; // true = fly from right to left

        public EnemyRocket()
        {
            Load(@"graphics/enemy0.png");
            m_Velocity = 300f;
            m_Direction = false;
        }

        public EnemyRocket(bool direction)
        {
            Load(@"graphics/enemy0.png");
            m_Velocity = 300f;
            if (direction)
                m_Direction = true;
            else
                m_Direction = false;
        }

        public override void Draw(RenderTarget target)
        {
            target.Draw(Sprite);
        }

        public override void Update(float elapsedTime)
        {
            Vector2f movement = new Vector2f(0, 0);

            if (m_Direction)
                movement.X -= m_Velocity;
            else
                movement.X += m_Velocity;

            Sprite.Position += movement * elapsedTime;

            Player player = (Player)Game.ObjectManager.GetObject("player");
            if (player != null)
                if (!player.AlreadyHit && Game.CollisionManager.Collision(player.Sprite, Sprite, 1))
                    player.Hit();
        }
    }
}
