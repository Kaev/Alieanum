using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.System;
using SFML.Window;
using SFML.Graphics;

namespace LudumDare33ByKaev
{
    class MainMenu : GameObject
    {
        Font m_Font;
        Text m_Text = new Text();

        static uint m_Score;
        public static uint Score
        {
            get
            {
                return m_Score;
            }
            set
            {
                m_Score = value;
            }
        }


        public MainMenu()
        {
            Visible = false;
            m_Font = new Font(@"Pixeltype.ttf");
            Score = 0;
            m_Text.Font = m_Font;
            m_Text.CharacterSize = 50;
            m_Text.Color = Color.White;
            m_Text.Position = new Vector2f(10f, 10f);
        }

        public override void Draw(RenderTarget target)
        {
            if (Visible)
                target.Draw(m_Text);
        }

        public static void IncrementScore(uint score = 1)
        {
            Score += score;
        }

        public static void ResetScore()
        {
            Score = 0;
        }

        public override void Update(float elapsedTime)
        {
            m_Text.DisplayedString = "Score: " + m_Score.ToString();
            
        }

    }
}
