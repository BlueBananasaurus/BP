using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Monogame_GL
{
    public class Graber
    {
        public delegate void CallOnChange(byte value);

        private RectangleF _boudary;
        private Vector2 _position;
        private GraberState _grabed;
        private Texture2D _bar;
        private CallOnChange _onChange;
        private GrabShow _show;
        private string _text;

        public Graber(Vector2 position, byte defaultValue, Texture2D bar = null, CallOnChange onChange = null, GrabShow show = GrabShow.none, string text = null)
        {
            _boudary = new RectangleF(new Vector2(20, 32), position + new Vector2(-10, 1) + new Vector2(defaultValue, 0));
            _position = position;
            _grabed = GraberState.none;
            _bar = bar;
            _onChange = onChange;
            _show = show;
            _text = text;
        }

        public void Update()
        {
            if (MouseInput.MouseStateNew.LeftButton == ButtonState.Pressed && MouseInput.MouseStateOld.LeftButton == ButtonState.Released)
            {
                if (CompareF.RectangleVsVector2(_boudary, MouseInput.MouseRealPosMenu()) == true)
                {
                    _grabed = GraberState.grabed;
                }
            }
            else if (MouseInput.MouseStateNew.LeftButton == ButtonState.Released)
            {
                if (CompareF.RectangleVsVector2(_boudary, MouseInput.MouseRealPosMenu()) == true)
                {
                    _grabed = GraberState.hover;
                }
                else
                {
                    _grabed = GraberState.none;
                }
            }

            if (_grabed == GraberState.grabed)
            {
                _boudary.Position = new Vector2(MouseInput.MouseRealPosMenu().X, _boudary.Position.Y) - new Vector2(_boudary.Size.X / 2, 0);
            }
            if (_boudary.Position.X < _position.X - _boudary.Size.X / 2)
            {
                _boudary.Position = new Vector2(_position.X - _boudary.Size.X / 2, _boudary.Position.Y);
            }

            if (_boudary.Position.X > _position.X + 255 - _boudary.Size.X / 2)
            {
                _boudary.Position = new Vector2(_position.X + 255 - _boudary.Size.X / 2, _boudary.Position.Y);
            }

            if (_onChange != null)
            {
                if (_grabed == GraberState.grabed)
                {
                    _onChange(GetValue());
                }
            }
        }

        public byte GetValue()
        {
            return (byte)(_boudary.Position.X - _position.X + _boudary.Size.X / 2);
        }

        public void SetValue(byte value)
        {
            _boudary.Position = new Vector2(_position.X + value - 10, _boudary.Position.Y);
        }

        public void Draw()
        {
            if (_text != null)
                DrawString.DrawText(_text, new Vector2(_position.X, _position.Y) - new Vector2(256 + 128, 0), Align.left, Globals.LightBlueText, FontType.small);

            if (_bar == null)
            {
                Game1.SpriteBatchGlobal.Draw(Game1.Textures["GrabLine"], position: _position + new Vector2(0, 16));
            }
            else
            {
                Game1.SpriteBatchGlobal.Draw(_bar, position: _position + new Vector2(0, 13));
            }

            switch (_grabed)
            {
                case GraberState.none:
                    Game1.SpriteBatchGlobal.DrawAtlas(Game1.Textures["Grab"], _boudary.Position, new Point(20, 32), new Point(0, 0));
                    break;

                case GraberState.hover:
                    Game1.SpriteBatchGlobal.DrawAtlas(Game1.Textures["Grab"], _boudary.Position, new Point(20, 32), new Point(1, 0));
                    break;

                case GraberState.grabed:
                    Game1.SpriteBatchGlobal.DrawAtlas(Game1.Textures["Grab"], _boudary.Position, new Point(20, 32), new Point(2, 0));
                    break;
            }

            if (_show == GrabShow.percentage)
            {
                DrawString.DrawText(GetValue().ToString(), new Vector2(_position.X + 320, _position.Y), Align.center, Globals.LightBlueText, FontType.small);
            }
            else if (_show == GrabShow.byteValue)
            {
                DrawString.DrawText(Math.Round(((float)GetValue() / byte.MaxValue) * 100).ToString() + " %", new Vector2(_position.X + 320, _position.Y), Align.center, Globals.LightBlueText, FontType.small);
            }
        }
    }
}