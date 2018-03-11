using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace Monogame_GL
{
    public class Vector2Object
    {
        public object Object { get; set; }
        public Vector2 Vector2 { get; set; }

        public Vector2Object(object ObjectReference, Vector2 vector)
        {
            Object = ObjectReference;
            Vector2 = vector;
        }
    }

    public class LineObject
    {
        public object Object { get; set; }
        public LineSegmentF Line { get; set; }

        public LineObject(object objectAll, LineSegmentF line)
        {
            Object = objectAll;
            Line = line;
        }
    }

    public static class CompareF
    {
        public static void DecreaseToZero(ref float number, float decreaseBy)
        {
            if (number > 0)
                number -= decreaseBy;
            else number = 0;
        }

        public static Vector2Object WeaponRayObstruction(RectangleF boundary, LineSegmentF ray, object whoShoots)
        {
            Vector2Object withBoundary = NearestVector(boundary.Origin, LineIntersectionRectangle(Game1.mapLive.MapBoundary, new LineObject(whoShoots, ray)));
            Vector2Object withMap = NearestVector(boundary.Origin, LineVsMap(Game1.mapLive.MapTree, new LineObject(Game1.mapLive, ray)));
            Vector2Object withMovables = NearestFromRectangles(boundary.Origin, new LineObject(whoShoots, ray), Game1.mapLive.MapMovables);

            List<Vector2Object> vectorsToCompare = new List<Vector2Object>();

            if (withBoundary != null)
                vectorsToCompare.Add(withBoundary);

            if (withMap != null)
                vectorsToCompare.Add(withMap);

            if (withMovables != null)
                vectorsToCompare.Add(withMovables);

            if (NearestVector(boundary.Origin, vectorsToCompare) != null)
            {
                return NearestVector(boundary.Origin, vectorsToCompare);
            }
            return new Vector2Object(null, ray.End);
        }

        public static Vector2Object RaySegmentCalc(Vector2 origin, LineSegmentF ray, object whoShoots)
        {
            Vector2Object withBoundary = NearestVector(origin, LineIntersectionRectangle(Game1.mapLive.MapBoundary, new LineObject(whoShoots, ray)));
            Vector2Object withNpcs = NearestFromRectangles(origin, new LineObject(whoShoots, ray), Game1.mapLive.MapNpcs);
            Vector2Object withMap = NearestVector(origin, LineVsMap(Game1.mapLive.MapTree, new LineObject(Game1.mapLive, ray)));
            Vector2Object withMovables = NearestFromRectangles(origin, new LineObject(whoShoots, ray), Game1.mapLive.MapMovables);
            Vector2Object withPlayer = NearestVector(origin, LineIntersectionRectangle(Game1.PlayerInstance.Boundary, new LineObject(Game1.PlayerInstance, ray)));

            List<Vector2Object> vectorsToCompare = new List<Vector2Object>();

            if (withBoundary != null) vectorsToCompare.Add(withBoundary);

            if (withMap != null) vectorsToCompare.Add(withMap);

            if (withMovables != null) vectorsToCompare.Add(withMovables);

            if (whoShoots is Player)
            {
                if (withNpcs != null) vectorsToCompare.Add(withNpcs);
            }

            if (whoShoots is Inpc)
            {
                if (withPlayer != null) vectorsToCompare.Add(withPlayer);
            }

            if (NearestVector(origin, vectorsToCompare) != null)
                return NearestVector(origin, vectorsToCompare);
            else return new Vector2Object(null, ray.End);
        }

        public static bool RectangleFInRectangleF(RectangleF inner, RectangleF boundary)
        {
            if (inner.CornerLeftTop.X >= boundary.CornerLeftTop.X && inner.CornerRightTop.X <= boundary.CornerRightTop.X && inner.CornerLeftTop.Y >= boundary.CornerLeftTop.Y && inner.CornerRightBottom.Y <= boundary.CornerRightBottom.Y)
            {
                return true;
            }
            return false;
        }

        public static bool LinesIntersection(LineSegmentF line_l, LineSegmentF line_r, out Vector2 intersection)
        {
            intersection = Vector2.Zero;

            //https://www.codeproject.com/Tips/862988/Find-the-Intersection-Point-of-Two-Line-Segments odkaz na originální kód, který jsem si dovolil použít

            /// <param name="p">Vector to the start point of p.</param>
            /// <param name="p2">Vector to the end point of p.</param>
            /// <param name="q">Vector to the start point of q.</param>
            /// <param name="q2">Vector to the end point of q.</param>
            /// <param name="intersection">The point of intersection, if any.</param>
            /// <param name="considerOverlapAsIntersect">Do we consider overlapping lines as intersecting?</param>
            /// <returns>True if an intersection point was found.</returns>

            var r = line_l.End - line_l.Start;
            var s = line_r.End - line_r.Start;
            var rxs = r.Cross(s);
            var qpxr = (line_r.Start - line_l.Start).Cross(r);

            // 3. If r x s = 0 and (q - p) x r != 0, then the two lines are parallel and non-intersecting.
            if (rxs.IsZero() && !qpxr.IsZero())
                return false;

            // t = (q - p) x s / (r x s)
            var t = (float)(line_r.Start - line_l.Start).Cross(s) / rxs;

            // u = (q - p) x r / (r x s)

            var u = (float)(line_r.Start - line_l.Start).Cross(r) / rxs;

            // 4. If r x s != 0 and 0 <= t <= 1 and 0 <= u <= 1
            // the two line segments meet at the point p + t r = q + u s.
            if (!rxs.IsZero() && (0 <= t && t <= 1) && (0 <= u && u <= 1))
            {
                // We can calculate the intersection point using either t or u.
                intersection = line_l.Start + new Vector2((float)t * r.X, (float)t * r.Y);
                // An intersection was found.
                return true;
            }

            // 5. Otherwise, the two line segments are not parallel but do not intersect.
            return false;
        }

        public static Vector2 RotateVector2(Vector2 vector, float angle)
        {
            Vector2 Temp = new Vector2();

            Temp.X = (float)(Math.Cos(angle) * vector.X - Math.Sin(angle) * vector.Y);
            Temp.Y = (float)(Math.Sin(angle) * vector.X + Math.Cos(angle) * vector.Y);

            return Temp;
        }

        public static Vector2 RotateAroutOrigin(Vector2 point, Vector2 origin, float rotation)
        {
            return Vector2.Transform(point - origin, Matrix.CreateRotationZ(rotation)) + origin;
        }

        public static Vector2 OriginRotateOffset(Vector2 point, Vector2 origin, float rotation, Vector2 offset)
        {
            return RotateAroutOrigin(point, origin, rotation) + offset;
        }

        public static Vector2 AngleToVector(float angle)
        {
            return new Vector2((float)Math.Sin(angle), -(float)Math.Cos(angle));
        }

        public static float VectorToAngle(Vector2 vector)
        {
            return (float)Math.Atan2(vector.Y, vector.X);
        }

        public static bool RectangleVsVector2(Vector2 point, RectangleF rectangle)
        {
            if (rectangle != null)
            {
                if (point.X <= rectangle.Position.X || point.X >= rectangle.Position.X + rectangle.Size.X)
                    return false;

                if (point.Y <= rectangle.Position.Y || point.Y >= rectangle.Position.Y + rectangle.Size.Y)
                    return false;
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool RectangleVsVector2NotEq(Vector2 point, RectangleF rectangle)
        {
            if (rectangle != null)
            {
                if (point.X < rectangle.Position.X || point.X > rectangle.Position.X + rectangle.Size.X)
                    return false;

                if (point.Y < rectangle.Position.Y || point.Y > rectangle.Position.Y + rectangle.Size.Y)
                    return false;
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool RectangleVsVector2NotEq(RectangleF rectangle, Vector2 point)
        {
            return RectangleVsVector2NotEq(point, rectangle);
        }

        public static bool RectangleVsVector2(RectangleF rectangle, Vector2 point)
        {
            return RectangleVsVector2(point, rectangle);
        }

        public static bool RectangleVsVector2(IRectanglePhysics rectangle, Vector2 point)
        {
            if (rectangle != null)
            {
                if (point.X <= rectangle.Boundary.Position.X || point.X >= rectangle.Boundary.Position.X + rectangle.Boundary.Size.X)
                    return false;

                if (point.Y <= rectangle.Boundary.Position.Y || point.Y >= rectangle.Boundary.Position.Y + rectangle.Boundary.Size.Y)
                    return false;
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool RectangleFVsRectangleF(RectangleF rectangleL, RectangleF rectangleR)
        {
            if (rectangleL != null && rectangleR != null)
            {
                if (rectangleL.CornerRightTop.X <= rectangleR.CornerLeftTop.X || rectangleL.CornerLeftTop.X >= rectangleR.CornerRightTop.X)
                    return false;

                if (rectangleL.CornerRightBottom.Y <= rectangleR.CornerRightTop.Y || rectangleL.CornerRightTop.Y >= rectangleR.CornerRightBottom.Y)
                    return false;
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool IsVector2InCircleF(Vector2 vector, CircleF circle)
        {
            if (circle != null)
            {
                if (
                    (
                    ((circle.Center.X - vector.X) * (circle.Center.X - vector.X)) +
                    ((circle.Center.Y - vector.Y) * (circle.Center.Y - vector.Y)))
                    <=
                    (circle.Radius * circle.Radius)
                    )
                    return true;
                return false;
            }
            else
            {
                return false;
            }
        }

        public static bool IsVector2InCircleF(CircleF circle, Vector2 vector)
        {
            return IsVector2InCircleF(vector, circle);
        }

        public static bool RectangleFVsCircleF(RectangleF rectangle, CircleF circle)
        {
            RectangleF Horizontal = new RectangleF(rectangle.Size.X + (circle.Radius * 2), rectangle.Size.Y, rectangle.Position.X - circle.Radius, rectangle.Position.Y);
            RectangleF Vertical = new RectangleF(rectangle.Size.X, rectangle.Size.Y + (circle.Radius * 2), rectangle.Position.X, rectangle.Position.Y - circle.Radius);
            CircleF LeftTop = new CircleF(rectangle.CornerLeftTop, circle.Radius);
            CircleF RightTop = new CircleF(rectangle.CornerRightTop, circle.Radius);
            CircleF LeftBottom = new CircleF(rectangle.CornerLeftBottom, circle.Radius);
            CircleF RightBottom = new CircleF(rectangle.CornerRightBottom, circle.Radius);

            if (rectangle != null && circle != null)
            {
                if (RectangleVsVector2(circle.Center, Horizontal) == true)
                    return true;
                if (RectangleVsVector2(circle.Center, Vertical) == true)
                    return true;
                if (IsVector2InCircleF(circle.Center, LeftTop) == true)
                    return true;
                if (IsVector2InCircleF(circle.Center, RightTop) == true)
                    return true;
                if (IsVector2InCircleF(circle.Center, LeftBottom) == true)
                    return true;
                if (IsVector2InCircleF(circle.Center, RightBottom) == true)
                    return true;
                return false;
            }
            else
            {
                return false;
            }
        }

        public static bool RectangleFVsCircleF(CircleF circle, RectangleF rectangle)
        {
            return RectangleFVsCircleF(rectangle, circle);
        }

        public static Vector2Object NearestVector(Vector2 origin, List<Vector2Object> vectors)
        {
            if (vectors != null)
            {
                Vector2Object nearest = null;
                float shortestDistance = float.PositiveInfinity;

                foreach (Vector2Object vector in vectors)
                {
                    if (LineSegmentF.Lenght(origin, vector.Vector2) < shortestDistance)
                    {
                        shortestDistance = LineSegmentF.Lenght(origin, vector.Vector2);
                        nearest = vector;
                    }
                }
                return nearest;
            }
            else
            {
                return null;
            }
        }

        public static LineSegmentF ShortestLineSegment(List<LineSegmentF> segments)
        {
            if (segments != null)
            {
                LineSegmentF shortest = null;
                LineSegmentF shortestSegment = new LineSegmentF(0, 0, float.PositiveInfinity, float.PositiveInfinity);

                foreach (LineSegmentF segment in segments)
                {
                    if (segment.Lenght() < shortestSegment.Lenght())
                    {
                        shortestSegment = segment;
                        shortest = segment;
                    }
                }
                return shortest;
            }
            else
            {
                return null;
            }
        }

        public static Vector2Object IntersectionLineWithOthers(LineObject L, List<LineSegmentF> segments, object target)
        {
            List<Vector2Object> intersections = new List<Vector2Object>();
            Vector2 intersection = new Vector2();

            foreach (LineSegmentF line in segments)
            {
                if (LinesIntersection(L.Line, line, out intersection) == true)
                {
                    intersections.Add(new Vector2Object(target, intersection));
                }
            }

            return NearestVector(L.Line.Start, intersections);
        }

        public static Vector2? IntersectionLineWithOthers(LineSegmentF L, List<LineSegmentF> segments, object target)
        {
            List<Vector2Object> intersections = new List<Vector2Object>();
            Vector2 intersection = new Vector2();

            foreach (LineSegmentF line in segments)
            {
                if (LinesIntersection(L, line, out intersection) == true)
                {
                    intersections.Add(new Vector2Object(target, intersection));
                }
            }

            if (NearestVector(L.Start, intersections) != null && NearestVector(L.Start, intersections).Vector2 != null)
                return NearestVector(L.Start, intersections).Vector2;
            else return null;
        }

        public static List<RectangleF> RectangleVsMap(MapTreeHolder map, RectangleF rectangle)
        {
            List<RectangleF> rectangles = new List<RectangleF>();

            if (map != null)
            {
                for (int Y = 0; Y < map.Trees.GetLength(1); Y++)
                {
                    for (int X = 0; X < map.Trees.GetLength(0); X++)
                    {
                        if (RectangleFVsRectangleF(map.Trees[X, Y].Boundary, rectangle) == true)
                        {
                            rectangles.AddRange(map.Trees[X, Y].FindIntersectRec(rectangle));
                        }
                    }
                }
            }

            return rectangles;
        }

        public static List<RectangleF> VectorVsWater(MapTreeHolder water, Vector2 vector)
        {
            List<RectangleF> rectangles = new List<RectangleF>();

            if (water != null)
            {
                for (int Y = 0; Y < water.Trees.GetLength(1); Y++)
                {
                    for (int X = 0; X < water.Trees.GetLength(0); X++)
                    {
                        if (RectangleVsVector2NotEq(water.Trees[X, Y].Boundary, vector) == true)
                        {
                            rectangles.AddRange(water.Trees[X, Y].FindRecWithVector(vector));
                        }
                    }
                }
            }

            return rectangles;
        }

        public static List<RectangleF> LineBoundaryVsMap(MapTreeHolder map, LineObject segment)
        {
            List<RectangleF> rectangles = new List<RectangleF>();

            for (int Y = (int)(Math.Max(0, (segment.Line.LineBoundingBox().Position.Y / 1024f) - 1)); Y < (int)Math.Ceiling(Math.Min((segment.Line.LineBoundingBox().CornerRightTop.Y / 1024f) + 1, map.Trees.GetLength(1))); Y++)
            {
                for (int X = (int)(Math.Max(0, (segment.Line.LineBoundingBox().Position.X / 1024f) - 1)); X < (int)Math.Ceiling(Math.Min((segment.Line.LineBoundingBox().CornerRightTop.X / 1024f) + 1, map.Trees.GetLength(0))); X++)
                {
                    if (RectangleFVsRectangleF(map.Trees[X, Y].Boundary, segment.Line.LineBoundingBox()) == true)
                    {
                        rectangles.AddRange(map.Trees[X, Y].FindIntersectRec(segment.Line.LineBoundingBox()));
                    }
                }
            }

            return rectangles;
        }

        //Test
        public static List<Vector2Object> LineVsMapRecReturn(MapTreeHolder map, LineObject segment)
        {
            List<Vector2Object> vectors = new List<Vector2Object>();

            for (int Y = (int)(Math.Max(0, (segment.Line.LineBoundingBox().Position.Y / 1024f) - 1)); Y < (int)Math.Ceiling(Math.Min((segment.Line.LineBoundingBox().CornerRightTop.Y / 1024f) + 1, map.Trees.GetLength(1))); Y++)
            {
                for (int X = (int)(Math.Max(0, (segment.Line.LineBoundingBox().Position.X / 1024f) - 1)); X < (int)Math.Ceiling(Math.Min((segment.Line.LineBoundingBox().CornerRightTop.X / 1024f) + 1, map.Trees.GetLength(0))); X++)
                {
                    if (LineIntersectionRectangle(map.Trees[X, Y].Boundary, segment).Count > 0 || RectangleVsVector2(map.Trees[X, Y].Boundary, segment.Line.End))
                    {
                        vectors.AddRange(map.Trees[X, Y].FindIntersectLine(segment.Line));
                    }
                }
            }

            return vectors;
        }

        public static List<Vector2Object> linevsRecs(List<RectangleF> rectangles, LineObject segment)
        {
            List<Vector2Object> Points = new List<Vector2Object>();

            foreach (RectangleF rec in rectangles)
            {
                if (LineIntersectionRectangle(rec, segment).Count > 0)
                {
                    Points.AddRange(LineIntersectionRectangle(rec, segment));
                }
            }

            return Points;
        }

        public static List<Vector2Object> LineVsMap(MapTreeHolder map, LineObject segment)
        {
            List<Vector2Object> temp = new List<Vector2Object>();

            temp.AddRange(linevsRecs(LineBoundaryVsMap(map, segment), segment));

            return temp;
        }

        public static List<Vector2Object> LineIntersectionRectangle(RectangleF rec, LineObject line)
        {
            List<Vector2Object> vectors = new List<Vector2Object>();

            Vector2 intersetionLeft;
            if (LinesIntersection(line.Line, new LineSegmentF(rec.CornerLeftTop, rec.CornerLeftBottom), out intersetionLeft))
                vectors.Add(new Vector2Object(line.Object, intersetionLeft));

            Vector2 intersetionRight;
            if (LinesIntersection(line.Line, new LineSegmentF(rec.CornerRightTop, rec.CornerRightBottom), out intersetionRight))
                vectors.Add(new Vector2Object(line.Object, intersetionRight));

            Vector2 intersetionTop;
            if (LinesIntersection(line.Line, new LineSegmentF(rec.CornerLeftTop, rec.CornerRightTop), out intersetionTop))
                vectors.Add(new Vector2Object(line.Object, intersetionTop));

            Vector2 intersetionBottom;
            if (LinesIntersection(line.Line, new LineSegmentF(rec.CornerRightBottom, rec.CornerLeftBottom), out intersetionBottom))
                vectors.Add(new Vector2Object(line.Object, intersetionBottom));

            return vectors;
        }

        public static List<Vector2Object> LineIntersectionRectangle<T>(T rec, LineObject line)
        {
            List<Vector2Object> vectors = new List<Vector2Object>();

            if (rec is Inpc)
            {
                Vector2 intersetionLeft;
                if (LinesIntersection(line.Line, new LineSegmentF((rec as Inpc).Boundary.CornerLeftTop, (rec as Inpc).Boundary.CornerLeftBottom), out intersetionLeft))
                    vectors.Add(new Vector2Object(line.Object, intersetionLeft));

                Vector2 intersetionRight;
                if (LinesIntersection(line.Line, new LineSegmentF((rec as Inpc).Boundary.CornerRightTop, (rec as Inpc).Boundary.CornerRightBottom), out intersetionRight))
                    vectors.Add(new Vector2Object(line.Object, intersetionRight));

                Vector2 intersetionTop;
                if (LinesIntersection(line.Line, new LineSegmentF((rec as Inpc).Boundary.CornerLeftTop, (rec as Inpc).Boundary.CornerRightTop), out intersetionTop))
                    vectors.Add(new Vector2Object(line.Object, intersetionTop));

                Vector2 intersetionBottom;
                if (LinesIntersection(line.Line, new LineSegmentF((rec as Inpc).Boundary.CornerRightBottom, (rec as Inpc).Boundary.CornerLeftBottom), out intersetionBottom))
                    vectors.Add(new Vector2Object(line.Object, intersetionBottom));
            }

            if (rec is IRectanglePhysics)
            {
                Vector2 intersetionLeft;
                if (LinesIntersection(line.Line, new LineSegmentF((rec as IRectanglePhysics).Boundary.CornerLeftTop, (rec as IRectanglePhysics).Boundary.CornerLeftBottom), out intersetionLeft))
                    vectors.Add(new Vector2Object(line.Object, intersetionLeft));

                Vector2 intersetionRight;
                if (LinesIntersection(line.Line, new LineSegmentF((rec as IRectanglePhysics).Boundary.CornerRightTop, (rec as IRectanglePhysics).Boundary.CornerRightBottom), out intersetionRight))
                    vectors.Add(new Vector2Object(line.Object, intersetionRight));

                Vector2 intersetionTop;
                if (LinesIntersection(line.Line, new LineSegmentF((rec as IRectanglePhysics).Boundary.CornerLeftTop, (rec as IRectanglePhysics).Boundary.CornerRightTop), out intersetionTop))
                    vectors.Add(new Vector2Object(line.Object, intersetionTop));

                Vector2 intersetionBottom;
                if (LinesIntersection(line.Line, new LineSegmentF((rec as IRectanglePhysics).Boundary.CornerRightBottom, (rec as IRectanglePhysics).Boundary.CornerLeftBottom), out intersetionBottom))
                    vectors.Add(new Vector2Object(line.Object, intersetionBottom));
            }

            return vectors;
        }

        public static Vector2Object NearestFromRectangles<T>(Vector2 origin, LineObject ray, List<T> recs)
        {
            List<Vector2Object> candidates = new List<Vector2Object>();

            if (typeof(T) == typeof(Inpc))
            {
                foreach (Inpc rec in recs)
                {
                    if (rec.Friendly == false)
                    {
                        List<Vector2Object> vectors = LineIntersectionRectangle(rec, ray);

                        foreach (Vector2Object vec in vectors)
                        {
                            candidates.Add(new Vector2Object(rec, vec.Vector2));
                        }
                    }
                }
            }

            if (typeof(T) == typeof(IRectanglePhysics))
            {
                foreach (IRectanglePhysics rec in recs)
                {
                    List<Vector2Object> vectors = LineIntersectionRectangle(rec, ray);

                    foreach (Vector2Object vec in vectors)
                    {
                        candidates.Add(new Vector2Object(rec, vec.Vector2));
                    }
                }
            }

            return NearestVector(origin, candidates);
        }
    }
}