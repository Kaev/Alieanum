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
    class Player : GameObject
    {
        float m_Velocity = 300.0f;

        bool m_AlreadyHit;
        public bool AlreadyHit
        {
            get
            {
                return m_AlreadyHit;
            }
            set
            {
                m_AlreadyHit = value;
            }
        }

        uint m_Lives;
        public uint Lives
        {
            get
            {
                return m_Lives;
            }
            set
            {
                m_Lives = value;
            }
        }

        public Player()
        {
            Load(@"graphics/player.png");
            Lives = 3;
            AlreadyHit = false;
            ResetPosition();
        }

        public override void Draw(RenderTarget target)
        {
            target.Draw(Sprite);
        }

        // player gets hit
        public void Hit()
        {
            AlreadyHit = true;
            Game.SoundManager.PlaySound("hit");
            Game.ItemManager.Reset();
        }

        // movement and border collision
        public override void Update(float elapsedTime)
        {
            Vector2f movement = new Vector2f(0, 0);
            if (Keyboard.IsKeyPressed(Keyboard.Key.Up))
            {
                movement.Y -= m_Velocity;
            }
            if (Keyboard.IsKeyPressed(Keyboard.Key.Down))
            {
                movement.Y += m_Velocity;
            }
            if (Keyboard.IsKeyPressed(Keyboard.Key.Left))
            {
                movement.X -= m_Velocity;
            }
            if (Keyboard.IsKeyPressed(Keyboard.Key.Right))
            {
                movement.X += m_Velocity;
            }

            if (Sprite.Position.X <= 0 && movement.X < 0)
            {
                movement.X = 0;
            }

            if (Sprite.Position.X >= Game.WindowWidth - Sprite.GetGlobalBounds().Width && movement.X > 0)
            {
                movement.X = 0;
            }

            if (Sprite.Position.Y <= 0 && movement.Y < 0)
            {
                movement.Y = 0;
            }

            if (Sprite.Position.Y >= Game.WindowHeight - Sprite.GetGlobalBounds().Height && movement.Y > 0)
            {
                movement.Y = 0;
            }

            Sprite.Position += movement * elapsedTime;
        }

        public void ResetPlayerState()
        {
            AlreadyHit = false;
        }

        public void ResetPosition()
        {
            Vector2f pos = new Vector2f();

            pos.X = Game.WindowWidth / 2 - Sprite.GetGlobalBounds().Width / 2;
            pos.Y = Game.WindowHeight - 200f;

            Sprite.Position = pos;
        }
    }
}
