namespace Monogame_GL
{
    using Microsoft.Xna.Framework;
    using System;
    using System.Collections.Generic;

    public class LineSegmentF
    {
        public Vector2 Start { get; set; }
        public Vector2 End { get; set; }

        public static LineSegmentF Zero
        {
            get { return new LineSegmentF(new Vector2(0, 0), new Vector2(0, 0)); }
        }

        public RectangleF LineBoundingBox()
        {
            float posX = Math.Min(Start.X, End.X);
            float posY = Math.Min(Start.Y, End.Y);
            float sizeX = Math.Abs(End.X - Start.X);
            float sizeY = Math.Abs(End.Y - Start.Y);

            return new RectangleF(sizeX, sizeY, posX, posY);
        }

        public LineSegmentF()
        {
            Start = Vector2.Zero;
            End = Vector2.One;
        }

        public LineSegmentF(Vector2 start, Vector2 end)
        {
            Start = start;
            End = end;
        }

        public LineSegmentF(float startX, float startY, float endX, float endY)
        {
            Start = new Vector2(startX, startY);
            End = new Vector2(endX, endY);
        }

        public static List<Vector2> PointsOnLine(Vector2 start, Vector2 end, float offsetAdd)
        {
            List<Vector2> vectors = new List<Vector2>();

            Vector2 offsetUnit = new LineSegmentF(start, end).NormalizedWithZeroSolution() * offsetAdd;

            vectors.Add(start);

            float i = 1;
            while (Lenght(start, start + offsetUnit * i) < Lenght(start, end))
            {
                vectors.Add(start + offsetUnit * i);
                i++;
            }

            return vectors;
        }

        public float? Lenght()
        {
            return (float)Math.Sqrt(Math.Pow(End.X - Start.X, 2) + Math.Pow(End.Y - Start.Y, 2));
        }

        public static float Lenght(Vector2 start, Vector2 end)
        {
            return (float)Math.Sqrt(Math.Pow(end.X - start.X, 2) + Math.Pow(end.Y - start.Y, 2));
        }

        public Vector2 NormalizedWithZeroSolution()
        {
            LineSegmentF segment = new LineSegmentF(Start, End);
            Vector2 line_to_vector = segment.ToVector2();
            if (Math.Abs(line_to_vector.X) > 0 || Math.Abs(line_to_vector.Y) > 0)
                return Vector2.Normalize(line_to_vector);
            else return Vector2.UnitY;
        }

        public Vector2 ToVector2()
        {
            LineSegmentF segment = new LineSegmentF(Start, End);
            return new Vector2(segment.End.X - segment.Start.X, segment.End.Y - segment.Start.Y);
        }

        public float ToAngle()
        {
            LineSegmentF segment = new LineSegmentF(Start, End);
            return segment.ToVector2().ToAngle();
        }
    }
}