using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Monogame_GL
{
    public static class DrawString
    {
        public static void DrawText(string text, Vector2 position, Align align, Color color, FontType type)
        {
            Texture2D tex;
            Point size;

            switch (type)
            {
                case FontType.small:
                    tex = Game1.Textures["LettersSmall"];
                    size = new Point(16, 18);
                    break;

                case FontType.big:
                    tex = Game1.Textures["LettersBig"];
                    size = new Point(24, 30);
                    break;

                default:
                    tex = Game1.Textures["LettersSmall"];
                    size = new Point(16, 18);
                    break;
            }

            if (align == Align.center)
            {
                for (int i = 0; i < text.Length; i++)
                {
                    //Draw_single_digit(tex, text.ToUpper()[i], i, new Vector2(position.X - (text.Length * size.X) / 2f, position.Y)+new Vector2(1), size, new Color(0,0,0,20));
                    Draw_single_digit(tex, text.ToUpper()[i], i, new Vector2(position.X - (text.Length * size.X) / 2f, position.Y), size, color);
                }
            }
            else if (align == Align.left)
            {
                for (int i = 0; i < text.Length; i++)
                {
                    //Draw_single_digit(tex, text.ToUpper()[i], i, position + new Vector2(1), size, new Color(0, 0, 0, 20));
                    Draw_single_digit(tex, text.ToUpper()[i], i, position, size, color);
                }
            }
        }

        public static float MeasureText(string text, int sizeOfDigitX)
        {
            return text.Length * sizeOfDigitX;
        }
        private static void Draw_single_digit(Texture2D tex, char digit, int index, Vector2 position, Point sizeOfDigit, Color color)
        {
            if (Convert.ToInt16(digit) >= 65 && Convert.ToInt16(digit) <= 90)
                DrawDigit(tex, sizeOfDigit, position, index, color, new Point(Convert.ToInt16(digit) - 65, 0));
            else if (Convert.ToInt16(digit) >= 48 && Convert.ToInt16(digit) <= 57)
                DrawDigit(tex, sizeOfDigit, position, index, color, new Point(Convert.ToInt16(digit) - 48, 1));
            else if (Convert.ToInt16(digit) == 95)
                DrawDigit(tex, sizeOfDigit, position, index, color, new Point(10, 1));
            else if (Convert.ToInt16(digit) == 37)
                DrawDigit(tex, sizeOfDigit, position, index, color, new Point(11, 1));
            else if (Convert.ToInt16(digit) == 40)
                DrawDigit(tex, sizeOfDigit, position, index, color, new Point(12, 1));
            else if (Convert.ToInt16(digit) == 41)
                DrawDigit(tex, sizeOfDigit, position, index, color, new Point(13, 1));
            else if (Convert.ToInt16(digit) == 46)
                DrawDigit(tex, sizeOfDigit, position, index, color, new Point(14, 1));
            else if (Convert.ToInt16(digit) == 8230)
                DrawDigit(tex, sizeOfDigit, position, index, color, new Point(15, 1));
            else if (Convert.ToInt16(digit) == 92)
                DrawDigit(tex, sizeOfDigit, position, index, color, new Point(16, 1));
            else if (Convert.ToInt16(digit) == 58)
                DrawDigit(tex, sizeOfDigit, position, index, color, new Point(17, 1));
            else if (Convert.ToInt16(digit) == 45)
                DrawDigit(tex, sizeOfDigit, position, index, color, new Point(19, 1));
            else if (Convert.ToInt16(digit) == 32)
            {
            }
            else
            {
                DrawDigit(tex, sizeOfDigit, position, index, color, new Point(18, 1));
            }
        }

        private static void DrawDigit(Texture2D tex, Point sizeOfDigit, Vector2 position, int indexDrawed,Color color, Point index)
        {
            Game1.SpriteBatchGlobal.Draw(tex, sourceRectangle: new Rectangle(index.X * sizeOfDigit.X, index.Y * sizeOfDigit.Y, sizeOfDigit.X, sizeOfDigit.Y), destinationRectangle: new Rectangle((int)(position.X + indexDrawed * sizeOfDigit.X), (int)(position.Y), sizeOfDigit.X, sizeOfDigit.Y), color: color);
        }
    }
}