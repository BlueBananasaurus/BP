using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace Monogame_GL
{
    public class TextBlock
    {
        public RectangleF Boundary { get; private set; }
        private string _text;
        private List<string> _lines;
        private ushort _symbolsInLine;

        public TextBlock(RectangleF boundary, string text = "")
        {
            Boundary = boundary;
            _symbolsInLine = (ushort)Math.Floor((boundary.Size.X - 64) / 16);
            _lines = new List<string>();
            ChangeText(text);
        }

        public void ChangeText(string text)
        {
            _lines.Clear();
            _text = text;
            for (int i = 0; i < (text.Length / _symbolsInLine) + 1; i++)
            {
                _lines.Add(_text.Cut((uint)(_symbolsInLine * i), (uint)(_symbolsInLine * (i + 1))));
            }
        }

        public void Draw()
        {
            Game1.SpriteBatchGlobal.Draw(Game1.Textures["PanelBackgroundLight"], position: Boundary.Position, sourceRectangle: new Rectangle(new Point(0, 0), Boundary.Size.ToPoint()));

            for (int i = 0; i < _lines.Count; i++)
            {
                DrawString.DrawText(_lines[i], Boundary.Position + new Vector2(32, i * 26 + 32), Align.left, Globals.LightBlueText, FontType.small);
            }
        }
    }
}