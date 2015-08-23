using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LudumDare33ByKaev
{
    class ItemManager
    {
        Random m_Random = new Random();

        List<bool> m_PlayerHasItem = new List<bool>();
        public List<bool> PlayerHasItem
        {
            get
            {
                return m_PlayerHasItem;
            }
            set
            {
                m_PlayerHasItem = value;
            }
        }

        public ItemManager()
        {
            PlayerHasItem.Add(false);
            PlayerHasItem.Add(false);
        }

        ~ItemManager()
        {
            m_PlayerHasItem.Clear();
        }

        // 8 / 100 chance to drop an item
        public bool CalculateItemChance()
        {
            if (m_Random.Next(100) < 8)
                return true;
            return false;
        }


        // drop a random item at the given position
        public void DropRandomItem(Vector2f position)
        {
            switch(m_Random.Next(2))
            {
                case 0:
                    ItemDoubleLaser itemLaser = new ItemDoubleLaser();
                    itemLaser.Position = position;
                    Game.ObjectManager.AddObject("itemLaser_" + m_Random.Next(), itemLaser);
                    break;
                case 1:
                    ItemSide itemSide = new ItemSide();
                    itemSide.Position = position;
                    Game.ObjectManager.AddObject("itemSide_" + m_Random.Next(), itemSide);
                    break;
            }
        }

        // reset the items a player has
        public void Reset()
        {
            PlayerHasItem.Clear();
            PlayerHasItem.Add(false);
            PlayerHasItem.Add(false);
        }
    }
}
