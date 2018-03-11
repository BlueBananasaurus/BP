using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Monogame_GL
{
    public class Grid
    {
        public GridItem[,] GridHolder { get; set; }
        public Vector2 Position { get; set; }

        public Grid(Point dimensions, Vector2 position)
        {
            GridHolder = new GridItem[dimensions.X, dimensions.Y];
            Position = position;
        }

        public GridItem RemoveItem(Vector2 position, List<GridItem> items)
        {
            GridItem toDelete = null;
            GridItem temp = null;

            if ((int)Math.Floor((position.X - Position.X) / 48) >=0 && (int)Math.Floor((position.X - Position.X) / 48) < GridHolder.GetLength(0))
                if ((int)Math.Floor((position.Y - Position.Y) / 48) >= 0 && (int)Math.Floor((position.Y - Position.Y) / 48) < GridHolder.GetLength(0))
                    toDelete = GridHolder[(int)Math.Floor((position.X - Position.X) / 48), (int)Math.Floor((position.Y - Position.Y) / 48)];

            for (int y = 0; y < GridHolder.GetLength(0); y++)
            {
                for (int x = 0; x < GridHolder.GetLength(0); x++)
                {
                    if(GridHolder[x, y]!= null && GridHolder[x,y] == toDelete)
                    {
                        if (items.Contains(GridHolder[x, y]))
                        {
                            temp = GridHolder[x, y];
                            items.Remove(GridHolder[x, y]);
                        }
                        GridHolder[x, y] = null;
                    }
                }
            }

            return temp;
        }

        public bool AddItem(Vector2 origin, List<GridItem> selfItems, float rotation)
        {
            if (MouseItem.Item != null)
            {
                int Left = 3;
                int Right = 0;
                int Top = 3;
                int Bottom = 0;

                for (int y = 0; y < 4; y++)
                {
                    for (int x = 0; x < 4; x++)
                    {
                        if (MouseItem.Item.ItemBounds[x, y] == 1 && x < Left)
                            Left = x;
                        if (MouseItem.Item.ItemBounds[x, y] == 1 && x > Right)
                            Right = x;
                        if (MouseItem.Item.ItemBounds[x, y] == 1 && y < Top)
                            Top = y;
                        if (MouseItem.Item.ItemBounds[x, y] == 1 && y > Bottom)
                            Bottom = y;
                    }
                }

                Right = 3 - Right;
                Bottom = 3 - Bottom;

                for (int y = 0 + (int)Math.Floor((origin.Y - Position.Y - 72) / 48); y < 4 + (int)Math.Floor((origin.Y - Position.Y - 72) / 48); y++)
                {
                    for (int x = 0 + (int)Math.Floor((origin.X - Position.X - 72) / 48); x < 4 + (int)Math.Floor((origin.X - Position.X - 72) / 48); x++)
                    {
                        if (0 + (int)Math.Floor((origin.Y - Position.Y - 72) / 48) < -Top || 4 + (int)Math.Floor((origin.Y - Position.Y - 72) / 48) > GridHolder.GetLength(1) + Bottom)
                            return false;
                        if (0 + (int)Math.Floor((origin.X - Position.X - 72) / 48) < -Left || 4 + (int)Math.Floor((origin.X - Position.X - 72) / 48) > GridHolder.GetLength(0) + Right)
                            return false;
                    }
                }

                GridItem item = new GridItem(MouseItem.Item.Rotation, new Point((int)(origin.X - Position.X + 24) / 48, (int)(origin.Y - Position.Y + 24) / 48), MouseItem.Item.Type);

                for (int y = 0 + (int)Math.Floor((origin.Y - Position.Y - 72) / 48); y < 4 + (int)Math.Floor((origin.Y - Position.Y - 72) / 48); y++)
                {
                    for (int x = 0 + (int)Math.Floor((origin.X - Position.X - 72) / 48); x < 4 + (int)Math.Floor((origin.X - Position.X - 72) / 48); x++)
                    {
                        if (MouseItem.Item.ItemBounds[x - (int)Math.Floor((origin.X - Position.X - 72) / 48), y - (int)Math.Floor((origin.Y - Position.Y - 72) / 48)] == 1)
                            if (GridHolder[x, y] == null)
                            { }
                            else return false;
                    }
                }

                for (int y = 0 + (int)Math.Floor((origin.Y - Position.Y - 72) / 48); y < 4 + (int)Math.Floor((origin.Y - Position.Y - 72) / 48); y++)
                {
                    for (int x = 0 + (int)Math.Floor((origin.X - Position.X - 72) / 48); x < 4 + (int)Math.Floor((origin.X - Position.X - 72) / 48); x++)
                    {
                        if (MouseItem.Item.ItemBounds[x - (int)Math.Floor((origin.X - Position.X - 72) / 48), y - (int)Math.Floor((origin.Y - Position.Y - 72) / 48)] == 1)
                        {
                            GridHolder[x, y] = item;
                            if (selfItems.Contains(item) == false)
                                selfItems.Add(item);
                        }
                    }
                }

                return true;
            }
            else return false;
        }       

        public void Draw(List<GridItem> items)
        {
            for (int y = 0; y < GridHolder.GetLength(1); y++)
            {
                for (int x = 0; x < GridHolder.GetLength(0); x++)
                {
                    if(GridHolder[x,y] == null)
                    Game1.SpriteBatchGlobal.Draw(Game1.Textures["SlotBackground"], new Vector2(x*48, y*48) + Position);
                }
            }

            foreach(GridItem item in items)
            {
                Game1.SpriteBatchGlobal.Draw(item.Texture, new Vector2(item.Index.X * 48, item.Index.Y* 48)  + Position, origin: new Vector2(192/2), rotation: item.Rotation);
            }
        }
    }
}
