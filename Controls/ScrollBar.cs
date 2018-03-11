using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;

namespace Monogame_GL
{
    public class ScrollBar
    {
        public delegate float CallMethodOnGrab();

        private RectangleF _bar;
        private Button _up;
        private Button _down;
        public Vector2 Start { get; private set; }
        private Vector2 _end;
        public float ContentSize { get; private set; }
        public float ContentContainerSize { get; private set; }
        private CallMethodOnGrab _scrollByBar;
        private bool _grabed;
        private float _grabOffset;

        public ScrollBar(int start, int end, int x, float contentContainerSize, float contentSize, CallMethodOnGrab scrollByBar)
        {
            _up = new Button(new Vector2(x, start), "", ScroolUp, null, null, ButtonType.scrooll);
            _down = new Button(new Vector2(x, end - 16), "", ScroolDown, null, null, ButtonType.scrooll);
            Start = new Vector2(x, start + 16);
            _end = new Vector2(x, end - 16);
            ContentSize = contentSize;
            ContentContainerSize = contentContainerSize;
            _bar = new RectangleF(16, (int)BarSize(), (int)Start.X, (int)(Start.Y));
            _scrollByBar = scrollByBar;
            _grabOffset = 0;
        }

        private float GetLenght()
        {
            return _end.Y - Start.Y;
        }

        public void PosYBar(float pos)
        {
            _bar.Position = new Vector2(_bar.Position.X, pos);
        }

        public int BarSize()
        {
            float temp = GetLenght() * (ContentContainerSize / ContentSize);
            if (temp > 16) return (int)Math.Floor(temp);
            return 16;
        }

        public float RealBarSize()
        {
            return GetLenght() * (ContentContainerSize / ContentSize);
        }

        public void ScroolUp()
        {
            _bar.Position -= new Vector2(0, RealBarSize() / 2f);
            _scrollByBar?.Invoke();
            Limit();
        }

        public void ScroolDown()
        {
            _bar.Position += new Vector2(0, RealBarSize() / 2f);
            _scrollByBar?.Invoke();
            Limit();
        }

        public void ResetBar()
        {
            PosYBar(Start.Y);
        }

        public void SyncWithContentOffset(float offset)
        {
            PosYBar(Start.Y + (1 / scrollRatio()) * -offset);
            Limit();
        }

        public void Limit()
        {
            if (_bar.Position.Y < Start.Y)
            {
                PosYBar(Start.Y);
            }
            if (_bar.Position.Y + BarSize() > _end.Y)
            {
                PosYBar(_end.Y - BarSize());
            }
        }

        public float Offset()
        {
            return -(_bar.Position.Y - Start.Y) * scrollRatio();
        }

        private float manipulationSize()
        {
            return GetLenght() - BarSize();
        }

        private float scrollRatio()
        {
            return (ContentSize - ContentContainerSize) / manipulationSize();
        }

        public void Set()
        {
            if (_grabed == true)
            {
                _bar = new RectangleF(16, BarSize(), Start.X, MouseInput.MouseRealPosMenu().Y - _grabOffset);
                _scrollByBar?.Invoke();
            }

            _bar.Size = new Vector2(_bar.Size.X, BarSize());

            Limit();
        }

        public void Update(float contentSize)
        {
            ContentSize = contentSize;

            if (ContentSize > ContentContainerSize)
            {
                if (CompareF.RectangleVsVector2(_bar, MouseInput.MouseRealPosMenu()) == true && MouseInput.MouseStateNew.LeftButton == ButtonState.Pressed && MouseInput.MouseStateOld.LeftButton == ButtonState.Released)
                {
                    _grabed = true;
                    _grabOffset = MouseInput.MouseRealPosMenu().Y - _bar.Position.Y;
                }
                if (MouseInput.MouseStateNew.LeftButton == ButtonState.Released)
                {
                    _grabed = false;
                    _grabOffset = 0;
                }

                _up.Update();
                _down.Update();

                Set();
            }
        }

        public void Draw()
        {
            if (ContentSize > ContentContainerSize)
            {
                _up.Draw();
                _down.Draw();
                if (_grabed == false)
                    Game1.SpriteBatchGlobal.Draw(Game1.Textures["ScrollBar"], destinationRectangle: new Rectangle((int)_bar.Position.X+4, (int)(Math.Floor(_bar.Position.Y + 2)), 8, BarSize() - 4));
                else
                    Game1.SpriteBatchGlobal.Draw(Game1.Textures["ScrollBarGrabed"], destinationRectangle: new Rectangle((int)_bar.Position.X+4, (int)(Math.Floor(_bar.Position.Y + 2)), 8, BarSize() - 4));
            }
        }
    }
}