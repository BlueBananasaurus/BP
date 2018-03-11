using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.IO;

namespace Monogame_GL
{
    public class TextBox : IControl
    {
        private List<Keys> keys;
        public string Text { get; private set; }
        private string _textOld;
        private int _cursor;
        private RectangleF _boundary;
        private Timer _blink;
        private Timer _backSpace;
        private Timer _left;
        private Timer _right;
        private bool _cursorVisible;
        public bool IsActive { get; private set; }
        private textBoxType _type;
        public bool Parsed { get; private set; }
        public bool Valid { get; private set; }

        public delegate void OnChange();

        public OnChange _changeMehod;
        private int _min;
        private int _max;
        private int _offset;

        public TextBox(string text, int width, Vector2 position, textBoxType type, OnChange change = null, int min = 0, int max = 0)
        {
            Text = text;
            _cursor = 0;
            _boundary = new RectangleF(width, 32, position.X, position.Y);
            _blink = new Timer(500, false);
            _backSpace = new Timer(100, false);
            _left = new Timer(100, false);
            _right = new Timer(100, false);
            _cursorVisible = true;
            IsActive = false;
            _type = type;
            Parsed = false;
            Valid = true;
            _changeMehod = change;
            _min = min;
            _max = max;
            _offset = 0;
        }

        private void MakeVisible()
        {
            _cursorVisible = true;
            _blink.Reset();
        }

        public bool Update()
        {
            _textOld = Text;

            if (MouseInput.MouseClickedLeft() == true)
            {
                if (CompareF.RectangleVsVector2(_boundary, MouseInput.MouseRealPosMenu()) == true)
                {
                    IsActive = true;
                    _cursor = (int)Math.Round(MouseInput.MouseRealPosMenu().X - _boundary.Position.X) / 16;
                    if (_cursor > Text.Length) _cursor = Text.Length;
                    if (_cursor < 0) _cursor = 0;
                    MakeVisible();
                }
                else
                {
                    IsActive = false;
                    MakeVisible();
                }
            }

            if (IsActive == true)
            {
                keys = KeyboardInput.GetPressedKeys();
                _backSpace.Update();
                _left.Update();
                _right.Update();

                _blink.Update();
                if (_blink.Ready == true)
                {
                    _cursorVisible = !_cursorVisible;
                    _blink.Reset();
                }

                foreach (Keys key in keys)
                {
                    int number = (int)key;

                    if (_type == textBoxType.text || _type == textBoxType.fileName)
                    {
                        if (number >= 65 && number <= 90)
                        {
                            if (_cursor <= Text.Length)
                            {
                                Text = Text.Insert(_cursor + _offset, key.ToString());
                                CursorAdd();
                                MakeVisible();
                            }
                        }
                        if (key == Keys.Space)
                        {
                            if (_cursor <= Text.Length)
                            {
                                Text = Text.Insert(_cursor + _offset, " ");
                                CursorAdd();
                                MakeVisible();
                            }
                        }
                    }
                    if (number >= 96 && number <= 105)
                    {
                        if (_cursor <= Text.Length)
                        {
                            Text = Text.Insert(_cursor + _offset, (number - 96).ToString());
                            CursorAdd();
                            MakeVisible();
                        }
                    }
                    if (number >= 48 && number <= 57)
                    {
                        if (_cursor <= Text.Length)
                        {
                            Text = Text.Insert(_cursor + _offset, (number - 48).ToString());
                            CursorAdd();
                            MakeVisible();
                        }
                    }
                    if (key == Keys.OemMinus || key == Keys.Subtract)
                    {
                        if (_cursor <= Text.Length)
                        {
                            Text = Text.Insert(_cursor + _offset, "-");
                            CursorAdd();
                            MakeVisible();
                        }
                    }
                }

                if (KeyboardInput.KeyboardStateNew.IsKeyDown(Keys.Back) == true && KeyboardInput.KeyboardStateOld.IsKeyDown(Keys.Back) == true)
                {
                    if (_backSpace.Ready == true)
                    {
                        if (_cursor > 0 || _offset > 0)
                        {
                            Text = Text.Remove(_cursor - 1 + _offset, 1);
                            MakeVisible();
                            _backSpace.Reset();
                        }

                        if (_offset > 0) { _offset--; }
                        else if (_cursor > 0) { _cursor--; }
                    }
                }

                if (KeyboardInput.KeyboardStateNew.IsKeyDown(Keys.Left) == true && KeyboardInput.KeyboardStateOld.IsKeyDown(Keys.Left) == true)
                {
                    if (_left.Ready == true)
                    {
                        if (_cursor > 0) { _cursor--; }
                        else if (_offset > 0) { _offset--; }
                        MakeVisible();
                        _left.Reset();
                    }
                }
                if (KeyboardInput.KeyboardStateNew.IsKeyDown(Keys.Right) == true && KeyboardInput.KeyboardStateOld.IsKeyDown(Keys.Right) == true)
                {
                    if (_right.Ready == true)
                    {
                        if (_cursor < Text.Length)
                        {
                            if (_cursor < (int)((_boundary.Size.X - 16) / 16)) { _cursor++; }
                            else if (_offset < Text.Length - (int)((_boundary.Size.X - 16) / 16)) { _offset++; }
                        }

                        MakeVisible();
                        _right.Reset();
                    }
                }
            }

            int i = 0;
            Parsed = int.TryParse(Text, out i);

            Valid = true;
            if ((Text.IndexOfAny(Path.GetInvalidFileNameChars()) != -1 && _type == textBoxType.fileName) || Text == "")
                Valid = false;
            if (_type == textBoxType.number && Parsed == true && (i < _min || i > _max))
                Valid = false;
            if (Parsed == false && _type == textBoxType.number)
                Valid = false;

            if (_changeMehod != null)
            {
                _changeMehod?.Invoke();
                return true;
            }
            else return false;
        }

        public void SetText(string text)
        {
            _cursor = 0;
            IsActive = false;
            _cursorVisible = false;
            Text = text;
        }

        public void SetText(int number)
        {
            _cursor = 0;
            IsActive = false;
            _cursorVisible = false;
            Text = number.ToString();
        }

        public byte? getParsedValue()
        {
            int i = -1;
            Parsed = int.TryParse(Text, out i);
            if (i == -1) return null;
            else return (byte?)i;
        }

        private void CursorAdd()
        {
            if (_cursor >= (int)((_boundary.Size.X - 16) / 16)) { _offset++; }
            else _cursor++;
        }

        public bool ChangedText()
        {
            return Text != _textOld;
        }

        public void Draw()
        {
            Game1.SpriteBatchGlobal.Draw(Game1.Textures["TextBoxBack"], destinationRectangle: new Rectangle((int)_boundary.Position.X + 1, (int)_boundary.Position.Y + 1, (int)_boundary.Size.X - 2, (int)_boundary.Size.Y - 2));

            DrawRectangleBoundary.DrawBlue(_boundary.ToRectangle());
            if (Valid)
            {
                DrawString.DrawText(Text.Substring(_offset, Math.Min(Text.Length - _offset, (int)((_boundary.Size.X - 16) / 16))), _boundary.Position + new Vector2(8, 7), Align.left, Globals.LightBlueText, FontType.small);
            }
            else
            {
                Game1.SpriteBatchGlobal.Draw(Game1.Textures["NotParsed"], destinationRectangle: new Rectangle((int)_boundary.Position.X + 1, (int)_boundary.Position.Y + 1, (int)_boundary.Size.X - 2, 30));
                DrawString.DrawText(Text.Substring(_offset, Math.Min(Text.Length - _offset, (int)((_boundary.Size.X - 16) / 16))), _boundary.Position + new Vector2(8, 7), Align.left, Globals.LightRedish, FontType.small);
            }
            if (_cursorVisible == true && IsActive)
                Game1.SpriteBatchGlobal.Draw(Game1.Textures["EditSymbol"], _boundary.Position + new Vector2(8 + 16 * _cursor - 3, 7), Color.Black);
        }
    }
}