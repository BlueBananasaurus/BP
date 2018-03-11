using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace Monogame_GL
{
    public enum QuadTreeSizes { Size_1024, Size_512, Size_256, Size_128, Size_64, Size_32 };

    public class MapTreeHolder
    {
        public QuadTree[,] Trees { get; private set; }

        public MapTreeHolder(int x, int y)
        {
            Trees = new QuadTree[(int)(Math.Ceiling(x / 1024f)),
                (int)(Math.Ceiling(y / 1024f))];

            for (int Y = 0; Y < Trees.GetLength(1); Y++)
            {
                for (int X = 0; X < Trees.GetLength(0); X++)
                {
                    Trees[X, Y] = new QuadTree(X * 1024, Y * 1024,
                        QuadTreeSizes.Size_1024);
                }
            }
        }

        public void AddRectangle32(Vector2 position)
        {
            Trees[(int)Math.Floor(position.X / 1024f), (int)Math.Floor(position.Y / 1024f)].InsertRec(new RectangleF(32, 32, position.X, position.Y));
        }

        public void Draw()
        {
            for (int Y = 0; Y < Trees.GetLength(1); Y++)
            {
                for (int X = 0; X < Trees.GetLength(0); X++)
                {
                    Trees[X, Y].Draw();
                }
            }
        }
    }

    public class QuadTree
    {
        public QuadTree LeftTop { get; private set; }
        public QuadTree RightTop { get; private set; }
        public QuadTree LeftBottom { get; private set; }
        public QuadTree RightBottom { get; private set; }

        public bool HasChilds { get; private set; }
        public QuadTreeSizes LevelSize { get; private set; }
        public int LevelSizeInt { get; private set; }

        public RectangleF Boundary { get; private set; }
        public RectangleF Holder { get; private set; }

        public QuadTree(float x, float y, QuadTreeSizes size)
        {
            HasChilds = false;
            LevelSize = size;
            Holder = null;

            if (size == QuadTreeSizes.Size_1024)
            {
                Boundary = new RectangleF(1024, 1024, x, y);
                LevelSizeInt = 1024;
            }
            if (size == QuadTreeSizes.Size_512)
            {
                Boundary = new RectangleF(512, 512, x, y);
                LevelSizeInt = 512;
            }
            if (size == QuadTreeSizes.Size_256)
            {
                Boundary = new RectangleF(256, 256, x, y);
                LevelSizeInt = 256;
            }
            if (size == QuadTreeSizes.Size_128)
            {
                Boundary = new RectangleF(128, 128, x, y);
                LevelSizeInt = 128;
            }
            if (size == QuadTreeSizes.Size_64)
            {
                Boundary = new RectangleF(64, 64, x, y);
                LevelSizeInt = 64;
            }
            if (size == QuadTreeSizes.Size_32)
            {
                Boundary = new RectangleF(32, 32, x, y);
                LevelSizeInt = 32;
            }
        }

        public List<RectangleF> FindIntersectRec(RectangleF rec)
        {
            List<RectangleF> rectangles = new List<RectangleF>();

            if (HasChilds == true)
            {
                if (CompareF.RectangleFVsRectangleF(LeftBottom.Boundary, rec) == true)
                    rectangles.AddRange(LeftBottom.FindIntersectRec(rec));

                if (CompareF.RectangleFVsRectangleF(LeftTop.Boundary, rec) == true)
                    rectangles.AddRange(LeftTop.FindIntersectRec(rec));

                if (CompareF.RectangleFVsRectangleF(RightBottom.Boundary, rec) == true)
                    rectangles.AddRange(RightBottom.FindIntersectRec(rec));

                if (CompareF.RectangleFVsRectangleF(RightTop.Boundary, rec) == true)
                    rectangles.AddRange(RightTop.FindIntersectRec(rec));

                return rectangles;
            }
            else
            {
                if (LevelSize == QuadTreeSizes.Size_32 && Holder != null && CompareF.RectangleFVsRectangleF(rec, Boundary) == true)
                {
                    rectangles.Add(Holder);
                    return rectangles;
                }

                return rectangles;
            }
        }

        //Test
        public List<Vector2Object> FindIntersectLine(LineSegmentF line)
        {
            List<Vector2Object> vectors = new List<Vector2Object>();

            if (HasChilds == true)
            {
                if (CompareF.LineIntersectionRectangle(LeftBottom.Boundary, new LineObject(null, line)).Count > 0 || CompareF.RectangleVsVector2(LeftBottom.Boundary, line.End))
                    vectors.AddRange(LeftBottom.FindIntersectLine(line));

                if (CompareF.LineIntersectionRectangle(LeftTop.Boundary, new LineObject(null, line)).Count > 0 || CompareF.RectangleVsVector2(LeftTop.Boundary, line.End))
                    vectors.AddRange(LeftTop.FindIntersectLine(line));

                if (CompareF.LineIntersectionRectangle(RightBottom.Boundary, new LineObject(null, line)).Count > 0 || CompareF.RectangleVsVector2(RightBottom.Boundary, line.End))
                    vectors.AddRange(RightBottom.FindIntersectLine(line));

                if (CompareF.LineIntersectionRectangle(RightTop.Boundary, new LineObject(null, line)).Count > 0 || CompareF.RectangleVsVector2(RightTop.Boundary, line.End))
                    vectors.AddRange(RightTop.FindIntersectLine(line));

                return vectors;
            }
            else
            {
                if (LevelSize == QuadTreeSizes.Size_32 && Holder != null && (CompareF.LineIntersectionRectangle(Boundary, new LineObject(null, line)).Count > 0 || CompareF.RectangleVsVector2(Boundary, line.End)))
                {
                    vectors = CompareF.LineIntersectionRectangle(Boundary, new LineObject(null, line));
                    return vectors;
                }

                return vectors;
            }
        }

        public List<RectangleF> FindRecWithVector(Vector2 vec)
        {
            List<RectangleF> rectangles = new List<RectangleF>();

            if (HasChilds == true)
            {
                if (CompareF.RectangleVsVector2NotEq(LeftBottom.Boundary, vec) == true)
                    rectangles.AddRange(LeftBottom.FindRecWithVector(vec));
                if (CompareF.RectangleVsVector2NotEq(LeftTop.Boundary, vec) == true)
                    rectangles.AddRange(LeftTop.FindRecWithVector(vec));
                if (CompareF.RectangleVsVector2NotEq(RightBottom.Boundary, vec) == true)
                    rectangles.AddRange(RightBottom.FindRecWithVector(vec));
                if (CompareF.RectangleVsVector2NotEq(RightTop.Boundary, vec) == true)
                    rectangles.AddRange(RightTop.FindRecWithVector(vec));

                return rectangles;
            }
            else
            {
                if (LevelSize == QuadTreeSizes.Size_32 && Holder != null && CompareF.RectangleVsVector2NotEq(vec, Boundary) == true)
                {
                    rectangles.Add(Holder);
                    return rectangles;
                }

                return rectangles;
            }
        }

        public void InsertRec(RectangleF rec)
        {
            if (CompareF.RectangleFVsRectangleF(Boundary, rec) == true)
            {
                if (LevelSize < QuadTreeSizes.Size_32)
                {
                    if (HasChilds == false)
                    {
                        HasChilds = true;
                        LeftTop = new QuadTree(Boundary.Position.X, 
                            Boundary.Position.Y, LevelSize + 1);
                        RightTop = new QuadTree(Boundary.Position.X 
                            + (LevelSizeInt / 2), Boundary.Position.Y, 
                            LevelSize + 1);
                        LeftBottom = new QuadTree(Boundary.Position.X, 
                            Boundary.Position.Y + (LevelSizeInt / 2), 
                            LevelSize + 1);
                        RightBottom = new QuadTree(Boundary.Position.X 
                            + (LevelSizeInt / 2), Boundary.Position.Y 
                            + (LevelSizeInt / 2), 
                            LevelSize + 1);
                    }

                    LeftTop.InsertRec(rec);
                    RightTop.InsertRec(rec);
                    LeftBottom.InsertRec(rec);
                    RightBottom.InsertRec(rec);
                }
                else
                {
                    Holder = rec;
                }
            }
        }

        public void Draw()
        {
            if (HasChilds == false)
            {
                if (LevelSize == QuadTreeSizes.Size_1024)
                {
                    Game1.SpriteBatchGlobal.Draw(Game1.TestRec, position: Boundary.Position, scale: new Vector2(32), color: Color.Red);
                }
                if (LevelSize == QuadTreeSizes.Size_512)
                {
                    Game1.SpriteBatchGlobal.Draw(Game1.TestRec, position: Boundary.Position, scale: new Vector2(16), color: Color.Orange);
                }
                if (LevelSize == QuadTreeSizes.Size_256)
                {
                    Game1.SpriteBatchGlobal.Draw(Game1.TestRec, position: Boundary.Position, scale: new Vector2(8), color: Color.Yellow);
                }
                if (LevelSize == QuadTreeSizes.Size_128)
                {
                    Game1.SpriteBatchGlobal.Draw(Game1.TestRec, position: Boundary.Position, scale: new Vector2(4), color: Color.Green);
                }
                if (LevelSize == QuadTreeSizes.Size_64)
                {
                    Game1.SpriteBatchGlobal.Draw(Game1.TestRec, position: Boundary.Position, scale: new Vector2(2), color: Color.Blue);
                }
                if (LevelSize == QuadTreeSizes.Size_32)
                {
                    Game1.SpriteBatchGlobal.Draw(Game1.TestRec, position: Boundary.Position, scale: new Vector2(1), color: Color.Black);

                    if (Holder != null)
                    {
                        Game1.SpriteBatchGlobal.Draw(Game1.tileMask, position: Boundary.Position, scale: new Vector2(1), color: Color.White);
                    }
                }
            }
            else
            {
                LeftTop.Draw();
                RightTop.Draw();
                LeftBottom.Draw();
                RightBottom.Draw();
            }
        }
    }
}