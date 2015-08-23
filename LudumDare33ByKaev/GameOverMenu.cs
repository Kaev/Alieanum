using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.System;
using SFML.Graphics;
using SFML.Window;

namespace LudumDare33ByKaev
{
    class GameOverMenu : GameObject
    {
        Font m_Font;
        Text m_Text = new Text();

        public GameOverMenu()
        {
            Visible = false;
            m_Font = new Font(@"Pixeltype.ttf");
            m_Text.Font = m_Font;
            m_Text.CharacterSize = 75;
            m_Text.Color = Color.White;
            m_Text.DisplayedString = "Press Enter to play again";
            m_Text.Position = new Vector2f(Game.WindowWidth / 2 - m_Text.GetGlobalBounds().Width / 2, Game.WindowHeight / 2 - m_Text.GetGlobalBounds().Height / 2);
        }

        public override void Draw(RenderTarget target)
        {
            if (Visible)
                target.Draw(m_Text);
        }
    }
}
