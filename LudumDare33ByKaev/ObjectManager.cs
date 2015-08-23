using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using SFML.Graphics;
using SFML.System;

namespace LudumDare33ByKaev
{
    class ObjectManager
    {

        Clock m_Clock = new Clock();
        Dictionary<string, GameObject> m_GameObjects = new Dictionary<string, GameObject>();

        ~ObjectManager()
        {
            m_GameObjects.Clear();
        }

        public void AddObject(string name, GameObject gameObject)
        {
            if (!m_GameObjects.ContainsKey(name))
                m_GameObjects.Add(name, gameObject);
        }

        public void DrawObjects(RenderTarget target)
        {
            foreach(KeyValuePair<string, GameObject> pair in m_GameObjects)
                pair.Value.Draw(target);
        }

        public Dictionary<string, GameObject> GetAllObjects()
        {
            return m_GameObjects;
            
        }

        public int GetObjectCount()
        {
            return m_GameObjects.Count();
        }

        public GameObject GetObject(string name)
        {
            if (m_GameObjects.ContainsKey(name))
            {
                return m_GameObjects[name];
            }
            return null;
        }

        // get elapsed time
        public float GetTimeDelta()
        {
            return m_Clock.ElapsedTime.AsSeconds();
        }

        public void RemoveObject(string name)
        {
            if (m_GameObjects.ContainsKey(name))
                m_GameObjects.Remove(name);
        }

        // remove all objects from m_GameObjects and reset stuff
        public void Reset()
        {
            foreach(KeyValuePair<string, GameObject> pair in m_GameObjects.ToList())
            {
                switch(pair.Key)
                {
                    case "bg0":
                        Background background0 = (Background)pair.Value;
                        background0.Position = new Vector2f(-Game.WindowWidth, -Game.WindowHeight);
                        break;
                    case "bg1":
                        Background background1 = (Background)pair.Value;
                        background1.Position = new Vector2f(Game.WindowWidth + background1.Sprite.GetGlobalBounds().Width, Game.WindowHeight / 2);
                        break;
                    case "gameover":
                        GameOverMenu gameOverMenu = (GameOverMenu)pair.Value;
                        gameOverMenu.Visible = false;
                        break;
                    case "menu":
                        MainMenu.ResetScore();
                        break;
                    case "player":
                        ((Player)pair.Value).ResetPosition();
                        break;
                    default:
                        m_GameObjects.Remove(pair.Key);
                        break;
                }
            }
        }

        public void ResetClock()
        {
            m_Clock.Restart();
        }

        // update all objects
        public void UpdateObjects()
        {
            float timeDelta = m_Clock.Restart().AsSeconds();
            foreach (KeyValuePair<string, GameObject> pair in m_GameObjects.ToList())
                pair.Value.Update(timeDelta);
        }


    }
}
