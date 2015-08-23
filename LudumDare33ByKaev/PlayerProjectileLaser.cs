using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.System;

namespace LudumDare33ByKaev
{
    class PlayerProjectileLaser : GameObject
    {
        Sprite m_SecondShot = new Sprite();
        float m_Velocity;


        public PlayerProjectileLaser()
        {
            Load(@"graphics/bullet0.png");
            m_SecondShot = new Sprite(Sprite);
            m_Velocity = 500f;
        }

        public override void Draw(RenderTarget target)
        {
            target.Draw(Sprite);
            target.Draw(m_SecondShot);
        }

        public override Vector2f Position
        {
            set
            {
                Sprite.Position = new Vector2f(value.X - 10, value.Y);
                m_SecondShot.Position = new Vector2f(value.X + 10, value.Y);
            }
        }

        public override void Update(float elapsedTime)
        {
            Vector2f movement = new Vector2f(0, 0);

            movement.Y -= m_Velocity;

            Sprite.Position += movement * elapsedTime;
            m_SecondShot.Position += movement * elapsedTime;

            foreach (KeyValuePair<string, GameObject> pair in Game.ObjectManager.GetAllObjects().ToList())
            {
                Enemy enemy = pair.Value as Enemy;
                if (enemy != null)
                {
                    if (Game.CollisionManager.Collision(enemy.Sprite, Sprite, 1) || Game.CollisionManager.Collision(enemy.Sprite, m_SecondShot, 1))
                    {
                        Game.ObjectManager.RemoveObject(pair.Key);
                        Game.SoundManager.PlaySound("explosion");
                        MainMenu.IncrementScore();
                        if (Game.ItemManager.CalculateItemChance())
                            Game.ItemManager.DropRandomItem(enemy.Position);
                    }
                }
                else
                {
                    EnemyRocket enemyRocket = pair.Value as EnemyRocket;
                    if (enemyRocket != null)
                    {
                        if (Game.CollisionManager.Collision(enemyRocket.Sprite, Sprite, 1) || Game.CollisionManager.Collision(enemyRocket.Sprite, m_SecondShot, 1))
                        {
                            Game.ObjectManager.RemoveObject(pair.Key);
                            Game.SoundManager.PlaySound("explosion");
                            MainMenu.IncrementScore();
                            if (Game.ItemManager.CalculateItemChance())
                                Game.ItemManager.DropRandomItem(enemyRocket.Position);
                        }
                    }
                }
            }
        }
    }
}
