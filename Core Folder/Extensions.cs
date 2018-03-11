namespace Monogame_GL
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using System;

    public static class ExtensionMethods
    {
        public static double RadianToDegree(this double angle)
        {
            return angle * (180.0 / Math.PI);
        }

        public static double AngleDifference(double firstAngle, double secondAngle)
        {
            double deg1 = firstAngle.RadianToDegree();
            double deg2 = secondAngle.RadianToDegree();

            double difference = deg2 - deg1;
            while (difference < -180) difference += 360;
            while (difference > 180) difference -= 360;
            return difference;
        }

        public static T Clamp<T>(this T val, T min, T max) where T : IComparable<T>
        {
            if (val.CompareTo(min) < 0) return min;
            else if (val.CompareTo(max) > 0) return max;
            else return val;
        }

        public static Vector2 Rotate(this Vector2 vector, float angle)
        {
            double x = Math.Cos(angle) * vector.X - Math.Sin(angle) * vector.Y;
            double y = Math.Sin(angle) * vector.X + Math.Cos(angle) * vector.Y;

            return new Vector2((float)x, (float)y);
        }

        public static T[,] RotateClockwise<T>(this T[,] arr)
        {
            return arr.Transpose().FlipHorizontaly();
        }

        public static T[,] RotateCounterClockwise<T>(this T[,] arr)
        {
            return arr.Transpose().FlipVerticaly();
        }

        public static T[,] Transpose<T>(this T[,] arr)
        {
            int rowCount = arr.GetLength(0);
            int columnCount = arr.GetLength(1);
            T[,] transposed = new T[columnCount, rowCount];
            if (rowCount == columnCount)
            {
                transposed = (T[,])arr.Clone();
                for (int i = 1; i < rowCount; i++)
                {
                    for (int j = 0; j < i; j++)
                    {
                        T temp = transposed[i, j];
                        transposed[i, j] = transposed[j, i];
                        transposed[j, i] = temp;
                    }
                }
            }
            else
            {
                for (int column = 0; column < columnCount; column++)
                {
                    for (int row = 0; row < rowCount; row++)
                    {
                        transposed[column, row] = arr[row, column];
                    }
                }
            }
            return transposed;
        }

        public static T[,] FlipHorizontaly<T>(this T[,] arr)
        {
            T[,] flipped = new T[arr.GetLength(0), arr.GetLength(1)];

            for (int y = 0; y < arr.GetLength(1); y++)
            {
                for (int x = 0; x < arr.GetLength(0); x++)
                {
                    flipped[x, y] = arr[arr.GetLength(0) - 1 - x, y];
                }
            }

            return flipped;
        }

        public static T[,] FlipVerticaly<T>(this T[,] arr)
        {
            T[,] flipped = new T[arr.GetLength(0), arr.GetLength(1)];

            for (int y = 0; y < arr.GetLength(1); y++)
            {
                for (int x = 0; x < arr.GetLength(0); x++)
                {
                    flipped[x, y] = arr[x, arr.GetLength(1) - 1 - y];
                }
            }

            return flipped;
        }

        public static bool IsZero(this double d)
        {
            return Math.Abs(d) < double.Epsilon;
        }

        public static double Cross(this Vector2 vl, Vector2 vr)
        {
            return vl.X * vr.Y - vl.Y * vr.X;
        }

        public static string Limit(this string s, byte max)
        {
            string temp = "";

            if (max < s.Length)
            {
                for (int i = 0; i < max; i++)
                {
                    temp += s[i];
                }

                temp += "…";
                return temp;
            }
                return s;
        }

        public static Vector2 ToUnitVector2(this float angle)
        {
            return new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle));
        }

        public static Vector2 NormalizeCustom(this Vector2 vector)
        {
            return Vector2.Normalize(vector);
        }

        public static Vector2 ShiftOverDistance(this Vector2 vector, float distance, float rotation)
        {
            return vector + (rotation.ToUnitVector2() * distance);
        }

        public static float ToAngle(this Vector2 vector)
        {
            return (float)Math.Atan2(vector.Y, vector.X);
        }

        public static string Cut(this string s, uint min, uint max)
        {
            if (min >= max)
                throw new ArgumentOutOfRangeException();

            if (min > s.Length)
                throw new ArgumentOutOfRangeException();

            string temp = "";

            if (max < s.Length)
            {
                for (uint i = min; i < max; i++)
                {
                    temp += s[(int)i];
                }

                return temp;
            }
            else
            {
                for (uint i = min; i < s.Length; i++)
                {
                    temp += s[(int)i];
                }

                return temp;
            }
        }

        public static T[,] DefaultFill<T>(this T[,] array, T value)
        {
            array = new T[array.GetLength(0), array.GetLength(1)];

            for (int y = 0; y < array.GetLength(1); y++)
            {
                for (int x = 0; x < array.GetLength(0); x++)
                {
                    array[x, y] = value;
                }
            }

            return array;
        }

        public static T[,] ResizeArray<T>(this T[,] array, T defaultValue, int left = 0, int top = 0, int right = 0, int bottom = 0)
        {
            int sizeX = array.GetLength(0) + left + right;
            int sizeY = array.GetLength(1) + top + bottom;

            T[,] temp = new T[sizeX, sizeY];
            temp = temp.DefaultFill(defaultValue);

            for (int y = Math.Max(0, top); y < sizeY; y++)
            {
                for (int x = Math.Max(0, left); x < sizeX; x++)
                {
                    if (x - left >= 0 && y - top >= 0 && x - left < array.GetLength(0) && y - top < array.GetLength(1))
                        temp[x, y] = array[x - left, y - top];
                }
            }

            return temp;
        }

        public static void DrawAtlas(this SpriteBatch spriteBatch, Texture2D texture, Vector2 drawPosition, Point drawSize, Point index, float scale = 1f)
        {
            spriteBatch.Draw(texture, drawPosition.ToPoint().ToVector2(), sourceRectangle: new Rectangle(index.X * drawSize.X, index.Y * drawSize.Y, drawSize.X, drawSize.Y), scale: new Vector2(scale));
        }

        public static void DrawAtlas(this SpriteBatch spriteBatch, Texture2D texture, Vector2 drawPosition, Point drawSize, Point index, Vector2 origin, float scale = 1f)
        {
            spriteBatch.Draw(texture, drawPosition.ToPoint().ToVector2(), sourceRectangle: new Rectangle(index.X * drawSize.X, index.Y * drawSize.Y, drawSize.X, drawSize.Y), scale: new Vector2(scale), origin: origin);
        }

        public static Rectangle ToRectangle(this RectangleF rec)
        {
            return new Rectangle((int)rec.Position.X, (int)rec.Position.Y, (int)rec.Size.X, (int)rec.Size.Y);
        }
    }
}

namespace Monogame_GL
{
    using System;

    public class NegativeSizeException : Exception
    {
        public NegativeSizeException(string message) : base(message)
        {
        }
    }
}