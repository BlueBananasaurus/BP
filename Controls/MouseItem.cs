using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;


namespace Monogame_GL
{
    public static class MouseItem
    {
        public static GridItem Item { get; set; }

        public static void Update()
        {
            if (Item != null)
            {
                if (KeyboardInput.KeyPressed(Keys.A))
                {
                    Item.Rotation -= (float)Math.PI / 2;
                    Item.ItemBounds = Item.ItemBounds.RotateCounterClockwise();
                }
                if (KeyboardInput.KeyPressed(Keys.D))
                {
                    Item.Rotation += (float)Math.PI / 2;
                    Item.ItemBounds = Item.ItemBounds.RotateClockwise();
                }
            }
        }

        static MouseItem()
        {
            Item = null;
        }

        public static void Draw(Vector2 gridStart)
        {
            if( Item != null)Game1.SpriteBatchGlobal.Draw(Item.Texture, new Vector2((int)((MouseInput.MouseRealPosMenu().X - gridStart.X + 24) / 48) * 48 + gridStart.X, (int)((MouseInput.MouseRealPosMenu().Y - gridStart.Y + 24) / 48) * 48 + gridStart.Y), origin: new Vector2(192 / 2), rotation: Item.Rotation);
        }
    }
}
