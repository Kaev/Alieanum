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
    class Background : GameObject
    {
        uint m_Id;
        float m_Velocity;

        public Background(uint id)
        {
            m_Id = id;
            // in case i want to add different backgrounds with different velocity
            switch(m_Id)
            {
                case 0:
                    Load(@"graphics/bg0.png");
                    m_Velocity = 50f;
                    break;
                case 1:
                    Load(@"graphics/bg1.png");
                    m_Velocity = 35f;
                    break;
                default:
                    break;
            }
        }

        public override void Draw(RenderTarget target)
        {
            target.Draw(Sprite);
        }

        public override void Update(float elapsedTime)
        {
            Vector2f movement = new Vector2f(0, 0);
            
            switch(m_Id)
            {
                case 0:
                    movement.Y += m_Velocity;
                    movement.X += m_Velocity;

                    if (Sprite.Position.Y > Game.WindowWidth)
                        Sprite.Position = new Vector2f(-Game.WindowWidth, -Game.WindowHeight);

                    if (Sprite.Position.X > Game.WindowWidth)
                        Sprite.Position = new Vector2f(-Game.WindowWidth, -Game.WindowHeight);

                    break;
                case 1:
                    movement.Y += m_Velocity;
                    movement.X -= m_Velocity;

                    if (Sprite.Position.Y > Game.WindowWidth)
                        Sprite.Position = new Vector2f(-Game.WindowWidth, -Game.WindowHeight);

                    if (Sprite.Position.X < 0)
                        Sprite.Position = new Vector2f(Game.WindowWidth + Sprite.GetGlobalBounds().Width - 200, 0 - Sprite.GetGlobalBounds().Height);
                    break;
            }

            Sprite.Position += movement * elapsedTime;
        }
    }
}
