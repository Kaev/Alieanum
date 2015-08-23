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
    class StartMenu : GameObject
    {
        Font m_Font;
        Text m_Text = new Text();

        public StartMenu()
        {
            Visible = true;
            m_Font = new Font(@"Pixeltype.ttf");
            m_Text.Font = m_Font;
            m_Text.CharacterSize = 50;
            m_Text.Color = Color.White;
            m_Text.DisplayedString = "Ludum Dare 33\nYou are the monster\n\nYou are an evil alien who's attacking the earth.\nThe humans saw your spaceship through telescopes\nand are attacking you right now.\n\nPress arrow keys to move your ship\nPress space to shoot\n\nPress enter to play\nGood Luck!";
            m_Text.Position = new Vector2f(Game.WindowWidth / 2 - m_Text.GetGlobalBounds().Width / 2, 20);
        }

        public override void Draw(RenderTarget target)
        {
            if (Visible)
                target.Draw(m_Text);
        }
    }
}
