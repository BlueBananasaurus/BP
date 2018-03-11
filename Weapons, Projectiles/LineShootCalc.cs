using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Monogame_GL
{
    public class LineShootCalc
    {
        public LineSegmentF RaySegment { get; set; }
        public LineSegmentF RayBarrel { get; set; }
        public Vector2Object RayDestination { get; set; }
        public LineSegmentF RayEnlonged { get; set; }

        public LineShootCalc()
        {
            RaySegment = new LineSegmentF();
            RayBarrel = new LineSegmentF();
            RayEnlonged = new LineSegmentF();
            RayDestination = null;
        }

        public void LineAim(Vector2 realPos, object whoShoots, Vector2 origin, float barrelLenght, float reach)
        {
            RaySegment.Start = origin;
            RaySegment.End = realPos;

            RayEnlonged = new LineSegmentF(RaySegment.Start, RaySegment.Start + RaySegment.NormalizedWithZeroSolution() * reach);

            RayBarrel.Start = origin;
            RayBarrel.End = RaySegment.Start + RaySegment.NormalizedWithZeroSolution() * barrelLenght;

            RayDestination = CompareF.RaySegmentCalc(origin, RayEnlonged, whoShoots);
        }
    }
}
