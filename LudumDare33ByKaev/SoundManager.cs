using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Audio;

namespace LudumDare33ByKaev
{
    class SoundManager
    {
        Dictionary<string, SoundBuffer> m_SoundBufferList = new Dictionary<string, SoundBuffer>();

        public SoundManager()
        {
            SoundBuffer sb = new SoundBuffer(@"sounds/explosion.wav");
            m_SoundBufferList.Add("explosion", sb);
            sb = new SoundBuffer(@"sounds/laser.wav");
            m_SoundBufferList.Add("laser", sb);
            sb = new SoundBuffer(@"sounds/hit.wav");
            m_SoundBufferList.Add("hit", sb);
            sb = new SoundBuffer(@"sounds/powerup.wav");
            m_SoundBufferList.Add("powerup", sb);
        }

        ~SoundManager()
        {
            m_SoundBufferList.Clear();
        }

        public void PlaySound(string name)
        {
            Sound sound = new Sound();
            if (m_SoundBufferList.ContainsKey(name))
            {
                sound.SoundBuffer = m_SoundBufferList[name];
                sound.Play();
            }
        }
    }
}
