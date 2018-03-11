using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Monogame_GL
{
    public class CheckBox : IControl
    {
        public delegate void CallMethodOnChange();

        private Button _btn;
        private bool _isChecked;
        private RectangleF _boudary;
        private Vector2 _position;
        private CallMethodOnChange _methodChecked;
        private CallMethodOnChange _methodUnchecked;
        private string _text;
        private Texture2D _tex;
        private CheckBoxType _type;

        public bool State { get { return _isChecked; } }

        public CheckBox(string text, Vector2 position, CheckBoxType type, bool isChecked, CallMethodOnChange methodChecked = null, CallMethodOnChange methodUnchecked = null)
        {
            _isChecked = isChecked;
            if (text != null)
            {
                _boudary = new RectangleF(24, 24, position.X + 512, position.Y);
            }
            else
            {
                _boudary = new RectangleF(24, 24, position.X, position.Y);
            }

            _btn = new Button(_boudary.Position, "", ChangeState, null, null, ButtonType.check);
            _methodChecked = methodChecked;
            _methodUnchecked = methodUnchecked;
            _text = text;
            _position = position;
            _type = type;

            switch (_type)
            {
                case CheckBoxType.visibility:
                    _tex = Game1.Textures["CheckBoxVisibility"];
                    break;

                case CheckBoxType.classic:
                    _tex = Game1.Textures["CheckBoxClassic"];
                    break;

                case CheckBoxType.sound:
                    _tex = Game1.Textures["CheckBoxSound"];
                    break;
            }
        }

        public bool Update()
        {
            _btn.Update();

            if (_isChecked) return true;
            return false;
        }

        public void SetState(bool isChecked)
        {
            _isChecked = isChecked;
        }

        public void ChangeState()
        {
            _isChecked = !_isChecked;

            if (_isChecked == true)
            {
                _methodChecked?.Invoke();
            }
            else
            {
                _methodUnchecked?.Invoke();
            }
        }

        private void drawBox(Point index)
        {
            Game1.SpriteBatchGlobal.DrawAtlas(_tex, _boudary.Position, new Point(24, 24), index);
        }

        public void Draw()
        {
            if (_text != null)
                DrawString.DrawText(_text, new Vector2(_position.X, _position.Y + 8), Align.left, Globals.LightBlueText, FontType.small);

            if (_btn.StateOfButton == ButtonStates.none)
            {
                if (_isChecked == true)
                {
                    drawBox(new Point(0, 0));
                }
                else
                {
                    drawBox(new Point(3, 0));
                }
            }
            else if (_btn.StateOfButton == ButtonStates.hover)
            {
                if (_isChecked == true)
                {
                    drawBox(new Point(1, 0));
                }
                else
                {
                    drawBox(new Point(4, 0));
                }
            }
            else
            {
                drawBox(new Point(2, 0));
            }
        }
    }
}