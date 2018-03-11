using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace Monogame_GL
{
    public class GroupRadios
    {
        private List<Radio> _radios;
        public int IndexSelected { get; private set; }
        public string stringSelected { get; private set; }

        public GroupRadios(byte indexSelected, params Radio[] radios)
        {
            IndexSelected = indexSelected;
            _radios = new List<Radio>(radios);
            stringSelected = _radios[indexSelected].Name;

            if (indexSelected > _radios.Count - 1)
            {
                throw new ArgumentOutOfRangeException();
            }

            _radios[indexSelected].Check();
        }

        public void Update()
        {
            foreach (Radio radio in _radios)
            {
                if (radio.Update() == true)
                {
                    IndexSelected = _radios.IndexOf(radio);
                    stringSelected = _radios[IndexSelected].Name;

                    foreach (Radio radioOthers in _radios)
                    {
                        if (radioOthers != radio)
                        {
                            radioOthers.Uncheck();
                        }
                    }
                }
            }
        }

        public void Draw()
        {
            foreach (Radio radio in _radios)
            {
                radio.Draw();
            }
        }

        public byte? GetIndex()
        {
            foreach (Radio radio in _radios)
            {
                if (radio.GetState() == true) return (byte)_radios.IndexOf(radio);
            }
            return null;
        }
    }

    public class Radio
    {
        public delegate void CallMethodOnChange();

        private Button _btn;
        private bool _isChecked;
        private RectangleF _boudary;
        private Vector2 _position;
        private CallMethodOnChange _methodChecked;
        private CallMethodOnChange _methodUnchecked;
        private string _text;
        private RadioType _type;
        public string Name { get; set; }

        public Radio(string text, Vector2 position, RadioType type, CallMethodOnChange methodChecked = null, CallMethodOnChange methodUnchecked = null, string name = null)
        {
            if (type == RadioType.classic)
            {
                if (text != null)
                {
                    _boudary = new RectangleF(24, 24, position.X + 512, position.Y);
                }
                else
                {
                    _boudary = new RectangleF(24, 24, position.X, position.Y);
                }
                _btn = new Button(_boudary.Position, "", ChangeState, null, null, ButtonType.check);
            }
            else if (type == RadioType.big)
            {
                _boudary = new RectangleF(128, 64, position.X, position.Y);
                _btn = new Button(_boudary.Position, "", ChangeState, null, null, ButtonType.big);
            }

            _methodChecked = methodChecked;
            _methodUnchecked = methodUnchecked;
            _text = text;
            _position = position;
            _type = type;
            Name = name;
        }

        public bool Update()
        {
            _btn.Update();
            if (_isChecked == true)
                return true;
            return false;
        }

        public void ChangeState()
        {
            _isChecked = true;

            if (_isChecked == true)
            {
                _methodChecked?.Invoke();
            }
            else
            {
                _methodUnchecked?.Invoke();
            }
        }

        public void Check()
        {
            _isChecked = true;
        }

        public void Uncheck()
        {
            _isChecked = false;
        }

        public bool GetState()
        {
            return _isChecked;
        }

        public void Draw()
        {
            if (_type == RadioType.classic)
            {
                if (_text != null)
                    DrawString.DrawText(_text, new Vector2(_position.X, _position.Y + 8), Align.left, Globals.LightBlueText, FontType.small);

                if (_btn.StateOfButton == ButtonStates.none)
                {
                    if (_isChecked == true)
                        Game1.SpriteBatchGlobal.DrawAtlas(Game1.Textures["RadioClassic"], _boudary.Position, new Point(24, 24), new Point(0, 0));
                    else
                        Game1.SpriteBatchGlobal.DrawAtlas(Game1.Textures["RadioClassic"], _boudary.Position, new Point(24, 24), new Point(3, 0));
                }
                else if (_btn.StateOfButton == ButtonStates.hover)
                {
                    if (_isChecked == true)
                        Game1.SpriteBatchGlobal.DrawAtlas(Game1.Textures["RadioClassic"], _boudary.Position, new Point(24, 24), new Point(1, 0));
                    else
                        Game1.SpriteBatchGlobal.DrawAtlas(Game1.Textures["RadioClassic"], _boudary.Position, new Point(24, 24), new Point(4, 0));
                }
                else
                {
                    Game1.SpriteBatchGlobal.DrawAtlas(Game1.Textures["RadioClassic"], _boudary.Position, new Point(24, 24), new Point(2, 0));
                }
            }
            else if (_type == RadioType.big)
            {
                if (_btn.StateOfButton == ButtonStates.none)
                {
                    if (_isChecked == true)
                        Game1.SpriteBatchGlobal.DrawAtlas(Game1.Textures["RadioBig"], _boudary.Position, new Point(256, 64), new Point(0, 0));
                    else
                        Game1.SpriteBatchGlobal.DrawAtlas(Game1.Textures["RadioBig"], _boudary.Position, new Point(256, 64), new Point(0, 3));
                }
                else if (_btn.StateOfButton == ButtonStates.hover)
                {
                    if (_isChecked == true)
                        Game1.SpriteBatchGlobal.DrawAtlas(Game1.Textures["RadioBig"], _boudary.Position, new Point(256, 64), new Point(0, 1));
                    else
                        Game1.SpriteBatchGlobal.DrawAtlas(Game1.Textures["RadioBig"], _boudary.Position, new Point(256, 64), new Point(0, 4));
                }
                else
                {
                    Game1.SpriteBatchGlobal.DrawAtlas(Game1.Textures["RadioBig"], _boudary.Position, new Point(256, 64), new Point(0, 2));
                }

                if (_text != null)
                {
                    if (_isChecked == true)
                        DrawString.DrawText(_text, new Vector2(_position.X + 128, _position.Y + 22), Align.center, Globals.LightGrayText, FontType.small);
                    if (_isChecked == false || _btn.StateOfButton == ButtonStates.pressed)
                        DrawString.DrawText(_text, new Vector2(_position.X + 128, _position.Y + 22), Align.center, Globals.LightBlueText, FontType.small);
                }
            }
        }
    }
}