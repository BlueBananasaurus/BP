using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Monogame_GL
{
    public static class DrawNumber
    {
        public static void Draw_digits(Texture2D tex, int number, Vector2 position, Align align, Point sizeOfDigit)
        {
            string numberString = Convert.ToString(number);

            if (align == Align.center)
            {
                for (int i = 0; i < numberString.Length; i++)
                {
                    Draw_single_digit(tex, numberString[i], i, new Vector2(position.X - (numberString.Length * sizeOfDigit.X) / 2f, position.Y), sizeOfDigit);
                }
            }
            else if (align == Align.left)
            {
                for (int i = 0; i < numberString.Length; i++)
                {
                    Draw_single_digit(tex, numberString[i], i, position, sizeOfDigit);
                }
            }
        }

        private static void Draw_single_digit(Texture2D tex, char digit, int index, Vector2 position, Point sizeOfDigit)
        {
            int temp = Convert.ToByte(digit.ToString());
            Game1.SpriteBatchGlobal.Draw
                (
                tex,
                sourceRectangle: new Rectangle(temp * sizeOfDigit.X, 0, sizeOfDigit.X, sizeOfDigit.Y),
                destinationRectangle: new Rectangle((int)Math.Floor(position.X + index * sizeOfDigit.X), (int)Math.Floor(position.Y), sizeOfDigit.X, sizeOfDigit.Y)
                );
        }
    }
}