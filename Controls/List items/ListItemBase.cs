using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System;

namespace Monogame_GL
{
    public abstract class ListItemBase : IHolderItem
    {
        public int Index { get; set; }
        public bool Selected { get; set; }
        protected RectangleF _boundary;
        protected RectangleF _listBoundary;
        public Vector2 Size { get; set; }
        public RectangleF Boundary { get { return new RectangleF(Size, _boundary.Position + _listBoundary.Position); } }
        private Timer _doubleClickTime;
        private byte _clicks;
        private int _itemsPerLine;

        public ListItemBase(Vector2 size, int index, RectangleF listBoundary, int itemsPerLine)
        {
            _listBoundary = listBoundary;
            Selected = false;
            Index = index;
            Size = size;
            _itemsPerLine = itemsPerLine;
            _boundary = new RectangleF(Size, new Vector2(32) + new Vector2((index % itemsPerLine) * Size.X, (int)Math.Ceiling((float)index / itemsPerLine) * Size.Y));
            _doubleClickTime = new Timer(1000, false);
            _clicks = 0;
        }

        public void UpdateIndex(List<IHolderItem> list)
        {
            Index = list.IndexOf(this);
        }

        public void Move(float offset)
        {
            _boundary.Position = new Vector2(32) + new Vector2((Index % _itemsPerLine) * Size.X, (int)Math.Floor((float)Index / _itemsPerLine) * Size.Y + offset);
        }

        public bool DoubleClicked()
        {
            if (_clicks >= 2)
            {
                _clicks = 0;
                return true;
            }

            return false;
        }

        public void UpdateBase()
        {
            _doubleClickTime.Update();

            if (CompareF.RectangleVsVector2(_listBoundary, MouseInput.MouseRealPosMenu()) == true)
            {
                if (MouseInput.MouseStateNew.LeftButton == ButtonState.Released && MouseInput.MouseStateOld.LeftButton == ButtonState.Pressed)
                {
                    if (CompareF.RectangleVsVector2(Boundary, MouseInput.MouseRealPosMenu()) == true && CompareF.RectangleVsVector2(_listBoundary, MouseInput.MouseRealPosMenu()) == true)
                    {
                        _doubleClickTime.Reset();

                        if (_doubleClickTime.Ready == false)
                        {
                            _clicks++;
                        }
                        else
                        {
                            _clicks = 1;
                            _doubleClickTime.Reset();
                        }

                        Selected = true;
                    }

                    if (CompareF.RectangleVsVector2(Boundary, MouseInput.MouseRealPosMenu()) == false)
                    {
                        Selected = false;
                    }
                }
            }
        }

        public virtual void Update()
        {

        }

        public virtual void Draw()
        {

        }
    }
}