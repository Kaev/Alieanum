using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.System;
using SFML.Graphics;

namespace LudumDare33ByKaev
{
    class CollisionManager
    {
        Dictionary<Texture, uint[]> Bitmasks = new Dictionary<Texture, uint[]>();

        ~CollisionManager()
        {
            Bitmasks.Clear();
        }

        public bool Collision(Sprite sprite1, Sprite sprite2, uint alphaLimit)
        {
            FloatRect intersection;
            if (sprite1.GetGlobalBounds().Intersects(sprite2.GetGlobalBounds(), out intersection))
            {
                IntRect subRect1 = sprite1.TextureRect;
                IntRect subRest2 = sprite2.TextureRect;
                uint[] mask1 = Bitmasks[sprite1.Texture];
                uint[] mask2 = Bitmasks[sprite2.Texture];

                for (int i = (int)intersection.Left; i < intersection.Left + intersection.Width; i++)
                    for (int j = (int)intersection.Top; j < intersection.Top + intersection.Height; j++)
                    {
                        Vector2f vector1 = sprite1.InverseTransform.TransformPoint(i, j);
                        Vector2f vector2 = sprite2.InverseTransform.TransformPoint(i, j);

                        if (vector1.X > 0 && vector1.Y > 0 && vector2.X > 0 && vector2.Y > 0 &&
                           vector1.X < subRect1.Width && vector1.Y < subRect1.Height &&
                           vector2.X < subRest2.Width && vector2.Y < subRest2.Height)
                            if (GetPixel(mask1, sprite1.Texture, (uint)(vector1.X) + (uint)subRect1.Left, (uint)(vector1.Y) + (uint)subRect1.Top) > alphaLimit &&
                                GetPixel(mask2, sprite2.Texture, (uint)vector2.X + (uint)subRest2.Left, (uint)vector2.Y + (uint)subRest2.Top) > alphaLimit)
                                return true;
                    }

            }

            return false;
        }

        public uint[] CreateBitmask(Texture texture, Image image)
        {
            uint[] mask = new uint[texture.Size.X * texture.Size.Y];

            for (uint y = 0; y < texture.Size.Y; y++)
                for (uint x = 0; x < texture.Size.X; x++)
                    mask[x + y * texture.Size.X] = image.GetPixel(x, y).A;

            Bitmasks.Add(texture, mask);

            return mask;
        }

        uint[] GetBitmask(Texture texture)
        {
            uint[] mask;

            if (!Bitmasks.ContainsKey(texture))
            {
                Image image = texture.CopyToImage();
                mask = CreateBitmask(texture, image);
            }
            else
                return Bitmasks[texture];

            return mask;
        }

        uint GetPixel(uint[] mask, Texture texture, uint x, uint y)
        {
            if (x > texture.Size.X || y > texture.Size.Y)
                return 0;

            return mask[x + y * texture.Size.X];
        }
    }
}
