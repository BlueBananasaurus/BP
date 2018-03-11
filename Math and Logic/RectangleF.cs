using Microsoft.Xna.Framework;
using System;
using Newtonsoft.Json;

namespace Monogame_GL
{
    public class RectangleF
    {
        public Vector2 Size { set; get; }
        public Vector2 Position { set; get; }
        public Vector2 Origin
        {
            get { return new Vector2((int)Math.Round(Position.X + (Size.X / 2f)), (int)Math.Round(Position.Y + (Size.Y / 2f))); }
        }
        public Vector2 CornerLeftTop
        {
            get { return new Vector2(Position.X, Position.Y); }
        }
        public Vector2 CornerRightTop
        {
            get { return new Vector2(Position.X + Size.X, Position.Y); }
        }
        public Vector2 CornerLeftBottom
        {
            get { return new Vector2(Position.X, Position.Y + Size.Y); }
        }
        public Vector2 CornerRightBottom
        {
            get { return new Vector2(Position.X + Size.X, Position.Y + Size.Y); }
        }
        public float Left
        {
            get { return Position.X; }
        }
        public float Top
        {
            get { return Position.Y; }
        }
        public float Right
        {
            get { return Position.X + Size.X; }
        }
        public float Bottom
        {
            get { return Position.Y + Size.Y; }
        }

        [JsonConstructor]
        public RectangleF(float sizeX, float sizeY, float positionX, float positionY)
        {
            {
                if (sizeX < 0 || sizeY < 0)
                    throw new NegativeSizeException("All sides of rectangle must have positive value");

                Size = new Vector2(sizeX, sizeY);
                Position = new Vector2(positionX, positionY);
            }
        }

        public RectangleF(Vector2 size, Vector2 position)
        {
            if (size.X >= 0 && size.Y >= 0)
            {
                Size = size;
                Position = new Vector2(position.X, position.Y);
            }
            else
            {
                throw new NegativeSizeException("All sides of rectangle must have positive value");
            }
        }

        public Vector2 IntersectionSize(RectangleF R)
        {
            if (CompareF.RectangleFVsRectangleF(this, R) == true)
            {
                float TempX = (Size.X + R.Size.X) 
                    / 2 - Math.Abs(Origin.X - R.Origin.X);
                float TempY = (Size.Y + R.Size.Y) 
                    / 2 - Math.Abs(Origin.Y - R.Origin.Y);

                return new Vector2(TempX, TempY);
            }
            return Vector2.Zero;
        }

        public void MoveToCenter(Vector2 center)
        {
            Position = center - Size / 2;
        }

        //public void CenterThisRectangleF(RectangleF by)
        //{
        //    Position = by.Origin - Size/2;
        //}

        //public static Point RandomPointInRec(RectangleF rec, Random rnd)
        //{
        //    return new Point(rnd.Next((int)Math.Floor(rec.CornerLeftTop.X), (int)Math.Ceiling(rec.CornerRightBottom.X + 1)), rnd.Next((int)Math.Floor(rec.CornerLeftTop.Y), (int)Math.Ceiling(rec.CornerRightBottom.Y + 1)));
        //}

        //public static CollisionSides TouchSide(RectangleF left, RectangleF right)
        //{
        //    float w = 0.5f * (left.Size.X + right.Size.X);
        //    float h = 0.5f * (left.Size.Y + right.Size.Y);
        //    float dx = left.Origin.X - right.Origin.X;
        //    float dy = left.Origin.Y - right.Origin.Y;

        //    if (Math.Abs(dx) <= w && Math.Abs(dy) <= h)
        //    {
        //        /* collision! */
        //        float wy = w * dy;
        //        float hx = h * dx;

        //        if (wy >= hx)
        //        {
        //            if (wy >= -hx) { return CollisionSides.bottom; }
        //            /* collision at the top */
        //            else { return CollisionSides.left; }
        //            /* on the left */
        //        }
        //        else
        //        {
        //            if (wy > -hx) { return CollisionSides.right; }
        //            /* on the right */
        //            else { return CollisionSides.top; }
        //            /* at the bottom */
        //        }
        //    }
        //    return CollisionSides.none;
        //}

        //public bool Contains(RectangleF rec)
        //{
        //    if (CompareF.RectangleVsVector2(this, rec.CornerLeftTop) && CompareF.RectangleVsVector2(this, rec.CornerRightBottom))
        //        return true;
        //    return false;
        //}

    }
}