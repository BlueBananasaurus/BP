using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Monogame_GL
{
    public class Button
    {
        public Vector2 Position { get; set; }
        public ButtonStates StateOfButton { get; private set; }

        public delegate void CallMethodOnClick();

        public delegate void CallMethodOnClickChangeWindow(string Key);

        public delegate void CallMethodOnClickIndex(byte index);

        private string _text;
        private CallMethodOnClick _method;
        private CallMethodOnClickChangeWindow _methodWindow;
        private CallMethodOnClickIndex _methodIndex;

        private RectangleF _border;
        private Vector2 _textOffset;
        private bool _clickedIn;
        private bool _clickedOut;
        private string _changeWin;
        private ButtonType _type;
        private bool _locked;
        private FourColorsButtons _colors;
        private Texture2D _tex;
        private Point _size;
        private Align Align;
        private byte _index;
        private Point? _drawSize;

        private void ConstructButton(FourColorsButtons color, RectangleF border, Vector2 textOffset)
        {
            _colors = color;
            _border = border;
            _textOffset = textOffset;
        }

        public Button(Vector2 position,byte index, string text = "", CallMethodOnClickIndex method = null)
        {
            Position = position;
            StateOfButton = ButtonStates.none;
            _clickedIn = false;
            _clickedOut = false;
            _text = text;
            _index = index;
            _methodIndex += method;
            _size = new Point(224, 32);
            _tex = Game1.Textures["ButtonInDrop"];
            ConstructButton(Globals.ColorBlueGray, new RectangleF(_size.ToVector2(), position), new Vector2(7, 7));
            Align = Align.left;
        }

        public Button(Vector2 position, string text = "", CallMethodOnClick method = null, CallMethodOnClickChangeWindow methodWindow = null, string changeWin = null, ButtonType type = ButtonType.big, bool locked = false, Align al = Align.center, int? width = null)
        {
            Position = position;
            StateOfButton = ButtonStates.none;
            _clickedIn = false;
            _clickedOut = false;
            _method += method;
            _methodWindow += methodWindow;
            _text = text;
            _changeWin = changeWin;
            _type = type;
            _locked = locked;
            Align = al;

            switch (_type)
            {
                case ButtonType.back:
                    _size = new Point(256, 64);
                    _tex = Game1.Textures["ButtonBigback"];
                    ConstructButton(Globals.ColorRedish, new RectangleF(_size.ToVector2(), position), new Vector2(128, 22));
                    break;

                case ButtonType.big:
                    _size = new Point(256, 64);
                    _tex = Game1.Textures["ButtonBig"];
                    ConstructButton(Globals.ColorGrayBlue, new RectangleF(_size.ToVector2(), position), new Vector2(128, 22));
                    break;

                case ButtonType.small:
                    _size = new Point(128, 32);
                    _tex = Game1.Textures["ButtonSmall"];
                    ConstructButton(Globals.ColorGrayBlue, new RectangleF(_size.ToVector2(), position), new Vector2(64, 7));
                    break;
                case ButtonType.scrooll:
                    _size = new Point(16);
                    _tex = Game1.Textures["ScrollButton"];
                    ConstructButton(Globals.ColorGrayBlue, new RectangleF(_size.ToVector2(), position), new Vector2(128, 22));
                    break;
                case ButtonType.drop:
                    _size = new Point(width.Value, 32);
                    _drawSize = new Point(32);
                    _tex = Game1.Textures["ButtonDrop"];
                    ConstructButton(Globals.ColorGrayBlue, new RectangleF(_size.ToVector2(), position), new Vector2(128, 22));
                    break;
                case ButtonType.inDrop:
                    _size = new Point(224,32);
                    _tex = Game1.Textures["ButtonInDrop"];
                    ConstructButton(Globals.ColorBlueGray, new RectangleF(_size.ToVector2(), position), new Vector2(7, 7));
                    Align = Align.left;
                    break;
                case ButtonType.check:
                    _size = new Point(24, 24);
                    _tex = null;
                    ConstructButton(FourColorsButtons.Default, new RectangleF(_size.ToVector2(), position), Vector2.Zero);
                    break;
            }
        }

        public void AddMethodCallWindow(CallMethodOnClickChangeWindow method)
        {
            _methodWindow += method;
        }

        public void RemoveMethodCallWindow(CallMethodOnClickChangeWindow method)
        {
            _methodWindow -= method;
        }

        public void AddMethodCall(CallMethodOnClick method)
        {
            _method += method;
        }

        public void RemoveMethodCall(CallMethodOnClick method)
        {
            _method -= method;
        }

        public void SetDefault()
        {
            StateOfButton = ButtonStates.none;
        }

        public void LockState(bool locked)
        {
            _locked = locked;
        }

        public bool Update()
        {
            bool returnClicked = false;

            _border.Position = Position;

            if (_locked == false)
            {
                if (_clickedIn == false)
                {
                    if (CompareF.RectangleVsVector2(_border, MouseInput.MouseRealPosMenu()) == true)
                    {
                        StateOfButton = ButtonStates.hover;
                    }
                    else
                    {
                        StateOfButton = ButtonStates.none;
                    }
                }

                if (_clickedIn == true)
                {
                    if (CompareF.RectangleVsVector2(_border, MouseInput.MouseRealPosMenu()) == true && MouseInput.MouseStateNew.LeftButton == ButtonState.Released)
                    {
                        _method?.Invoke();
                        _methodWindow?.Invoke(_changeWin);
                        _methodIndex?.Invoke(_index);
                        Game1.soundSelect.Play();
                        returnClicked = true;
                    }

                    if (CompareF.RectangleVsVector2(_border, MouseInput.MouseRealPosMenu()) == false && MouseInput.MouseStateNew.LeftButton == ButtonState.Pressed)
                    {
                        StateOfButton = ButtonStates.none;
                    }
                    if (CompareF.RectangleVsVector2(_border, MouseInput.MouseRealPosMenu()) == true && MouseInput.MouseStateNew.LeftButton == ButtonState.Pressed)
                    {
                        StateOfButton = ButtonStates.pressed;
                    }
                }

                if (_clickedIn == false && _clickedOut == false)
                {
                    if (CompareF.RectangleVsVector2(_border, MouseInput.MouseRealPosMenu()) == false && MouseInput.MouseStateNew.LeftButton == ButtonState.Pressed)
                    {
                        _clickedOut = true;
                    }

                    if (CompareF.RectangleVsVector2(_border, MouseInput.MouseRealPosMenu()) == true && MouseInput.MouseStateNew.LeftButton == ButtonState.Pressed && MouseInput.MouseStateOld.LeftButton == ButtonState.Released)
                    {
                        _clickedIn = true;
                        StateOfButton = ButtonStates.pressed;
                    }
                }

                if (MouseInput.MouseStateNew.LeftButton == ButtonState.Released)
                {
                    _clickedIn = false;
                    _clickedOut = false;

                    if (CompareF.RectangleVsVector2(_border, MouseInput.MouseRealPosMenu()) == true)
                    {
                        StateOfButton = ButtonStates.hover;
                    }
                    else
                    {
                        StateOfButton = ButtonStates.none;
                    }
                }
            }
            else
            {
                StateOfButton = ButtonStates.locked;
            }

            return returnClicked;
        }

        private void DrawButton()
        {
            if(_drawSize == null)
            switch (StateOfButton)
            {
                case ButtonStates.none:
                    Game1.SpriteBatchGlobal.DrawAtlas(_tex, Position, _size, new Point(0, 0));
                    DrawString.DrawText(_text, Position + _textOffset, Align, _colors.None, FontType.small);
                    break;

                case ButtonStates.hover:
                    Game1.SpriteBatchGlobal.DrawAtlas(_tex, Position, _size, new Point(0, 1));
                    DrawString.DrawText(_text, Position + _textOffset, Align, _colors.Hover, FontType.small);
                    break;

                case ButtonStates.pressed:
                    Game1.SpriteBatchGlobal.DrawAtlas(_tex, Position, _size, new Point(0, 2));
                    DrawString.DrawText(_text, Position + _textOffset, Align, _colors.Pressed, FontType.small);
                    break;

                case ButtonStates.locked:
                    Game1.SpriteBatchGlobal.DrawAtlas(_tex, Position, _size, new Point(0, 3));
                    DrawString.DrawText(_text, Position + _textOffset, Align, _colors.Pressed, FontType.small);
                    break;
            }
            else
            switch (StateOfButton)
            {
                case ButtonStates.none:
                    Game1.SpriteBatchGlobal.DrawAtlas(_tex, Position, _drawSize.Value, new Point(0, 0));
                    DrawString.DrawText(_text, Position + _textOffset, Align, _colors.None, FontType.small);
                    break;

                case ButtonStates.hover:
                    Game1.SpriteBatchGlobal.DrawAtlas(_tex, Position, _drawSize.Value, new Point(0, 1));
                    DrawString.DrawText(_text, Position + _textOffset, Align, _colors.Hover, FontType.small);
                    break;

                case ButtonStates.pressed:
                    Game1.SpriteBatchGlobal.DrawAtlas(_tex, Position, _drawSize.Value, new Point(0, 2));
                    DrawString.DrawText(_text, Position + _textOffset, Align, _colors.Pressed, FontType.small);
                    break;

                case ButtonStates.locked:
                    Game1.SpriteBatchGlobal.DrawAtlas(_tex, Position, _drawSize.Value, new Point(0, 3));
                    DrawString.DrawText(_text, Position + _textOffset, Align, _colors.Pressed, FontType.small);
                    break;
            }
        }

        public void Draw()
        {
            DrawButton();
        }
    }
}