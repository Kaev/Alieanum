using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.Window;
using SFML.System;

namespace LudumDare33ByKaev
{
    class GameObject
    {
        string m_ImageKey;
        Dictionary<string, Texture> m_Images = new Dictionary<string, Texture>();

        public virtual Vector2f Position
        {
            get
            {
                if (IsLoaded)
                    return Sprite.Position;
                return new Vector2f(0, 0);
            }
            set
            {
                if (IsLoaded)
                    Sprite.Position = value;
            }
        }

        public virtual float Width
        {
            get
            {
                return Sprite.GetLocalBounds().Width;
            }
        }

        public virtual float Height
        { 
            get
            {
                return Sprite.GetLocalBounds().Height;
            }
         }

        public virtual FloatRect BoundingRect
        {
            get
            {
                return Sprite.GetGlobalBounds();
            }
        }

        bool m_IsLoaded = false;
        public virtual bool IsLoaded
        {
            get
            {
                return m_IsLoaded;
            }
            set
            {
                m_IsLoaded = value;
            }
        }

        public virtual void Draw(RenderTarget target)
        {
            if (Visible)
                target.Draw(Sprite);
        }


        public virtual void Load(string filename)
        {
            if (!m_Images.ContainsKey(filename))
            {
                Texture texture = new Texture(filename);
                m_Images.Add(filename, texture);
                Sprite.Texture = texture;
                Image image = new Image(filename);
                Game.CollisionManager.CreateBitmask(texture, image);
                IsLoaded = true;
            }
            else
            {
                m_Sprite.Texture = m_Images[filename];
                IsLoaded = true;
            }

            m_ImageKey = filename;
        }

        Sprite m_Sprite = new Sprite();
        public virtual Sprite Sprite
        {
            get
            {
                return m_Sprite;
            }
        }

        bool m_Visible = true;
        public virtual bool Visible
        {
            get
            {
                return m_Visible;
            }
            set
            {
                m_Visible = value;
            }
        }

        public virtual void Update(float elapsedTime)
        {
        }
    }
}
