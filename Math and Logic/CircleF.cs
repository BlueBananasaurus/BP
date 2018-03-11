using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Monogame_GL
{
    public class CircleF
    {
        public float Radius { get; set; }
        public Vector2 Center { get; set; }

        public List<Vector2> DidvideCircle(int by)
        {
            List<Vector2> temp = new List<Vector2>();

            double angle;

            for (int i = 0; i < by; i++)
            {
                angle = i * ((Math.PI * 2) / by);
                Vector2 vector = new Vector2();
                vector.X = (float)(Center.X + Radius * Math.Cos(angle));
                vector.Y = (float)(Center.Y + Radius * Math.Sin(angle));
                temp.Add(vector);
            }

            return temp;

        }

        public Vector2 GenerateRandomPoint()
        {
            double radius = Math.Sqrt(Globals.GlobalRandom.NextDouble()) * Radius;
            double angle = Globals.GlobalRandom.NextDouble() * Math.PI * 2;
            double x = radius * Math.Cos(angle);
            double y = radius * Math.Sin(angle);

            return new Vector2((float)x, (float)y) + Center;
        }

        public CircleF(Vector2 center, float radius)
        {
            if (radius < 0)
            {
                throw new NegativeSizeException("Radius must have positive value");
            }
            else
            {
                Radius = radius;
                Center = center;
            }
        }

        public CircleF(float centerX, float centerY, float radius)
        {
            if (radius < 0)
            {
                throw new NegativeSizeException("Radius must have positive value");
            }
            else
            {
                Center = new Vector2(centerX, centerY);
                Radius = radius;
            }
        }
    }
}