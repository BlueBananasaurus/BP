using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Monogame_GL
{
    public class Movable
    {
        public RectangleF Boundary { get; set; }
        public Vector2 Velocity { get; set; }

        public Vector2 Position
        {
            get
            {
                return new Vector2((float)Math.Round(Boundary.Position.X), (float)Math.Round(Boundary.Position.Y));
            }
        }

        public Vector2 Origin
        {
            set
            {
                Boundary.Position = value - Boundary.Size / 2;
            }
            get
            {
                return Boundary.Origin;
            }
        }

        public Movable(RectangleF boundary)
        {
            Boundary = boundary;
        }

        public void Update(Vector2 velocity)
        {
            Velocity = velocity;
            Boundary.Position += velocity * Game1.Delta;
        }

        public void Draw(Texture2D tex)
        {
            Game1.SpriteBatchGlobal.Draw(tex, Position);
        }

        public void DrawNormal(Texture2D tex)
        {
            Game1.SpriteBatchGlobal.Draw(tex, Position);
        }
    }
}