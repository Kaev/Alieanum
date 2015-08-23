using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.System;

namespace LudumDare33ByKaev
{
    class PlayerProjectile : GameObject
    {
        float m_Velocity;

        public PlayerProjectile()
        {
            Load(@"graphics/bullet0.png");
            m_Velocity = 500f;
        }

        public override void Draw(RenderTarget target)
        {
            target.Draw(Sprite);
        }

        public override void Update(float elapsedTime)
        {
            Vector2f movement = new Vector2f(0, 0);

            movement.Y -= m_Velocity;

            Sprite.Position += movement * elapsedTime;

            foreach(KeyValuePair<string, GameObject> pair in Game.ObjectManager.GetAllObjects().ToList())
            {
                Enemy enemy = pair.Value as Enemy;
                if (enemy != null)
                {
                    if (Game.CollisionManager.Collision(enemy.Sprite, Sprite, 1))
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
                        if (Game.CollisionManager.Collision(enemyRocket.Sprite, Sprite, 1))
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
