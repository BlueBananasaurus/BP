using Microsoft.Xna.Framework;
using System;

namespace Monogame_GL
{
    public static class Camera2DWorld
    {
        public static Vector2 Position { get; private set; }
        public static Vector2 PositionPoint { get; private set; }
        public static Vector2 ShakeAmount { get; private set; }
        public static int ShakeAmountTime { get; private set; }
        public static int ShakeAmountVelocity { get; private set; }

        public static RectangleF Boundary
        {
            get { return new RectangleF(Globals.WinRenderSize, Position); }
        }

        static Camera2DWorld()
        {
            Position = new Vector2(0, 0);
            PositionPoint = new Vector2(0, 0);
            ShakeAmount = Vector2.Zero;
            ShakeAmountVelocity = 6;
        }

        public static void Update()
        {
            Position = new Vector2((Game1.PlayerWorldInstance.Boundary.Origin.X - (Globals.Winsize.X / 2) / Globals.ScreenRatio.X), (Game1.PlayerWorldInstance.Boundary.Origin.Y - (Globals.Winsize.Y / 2) / Globals.ScreenRatio.Y));
            PositionPoint = new Vector2((int)Math.Round(Position.X), (int)Math.Round(Position.Y));

            if (ShakeAmountTime > 0)
            {
                ShakeAmount = new Vector2(Globals.GlobalRandom.Next(-ShakeAmountVelocity, ShakeAmountVelocity + 1), Globals.GlobalRandom.Next(-ShakeAmountVelocity, ShakeAmountVelocity + 1));
                ShakeAmountTime--;
            }
            else
            {
                ShakeAmount = Vector2.Zero;
            }
        }

        public static Matrix GetViewMatrix()
        {
            return Matrix.CreateTranslation(new Vector3((float)Math.Round(-PositionPoint.X + ShakeAmount.ToPoint().X), (float)Math.Round(-PositionPoint.Y + ShakeAmount.ToPoint().Y), 0f));
        }

        public static Matrix GetViewMatrixLayer2()
        {
            return Matrix.CreateTranslation(new Vector3((float)Math.Round(-PositionPoint.X * 0.75f + ShakeAmount.ToPoint().X), (float)Math.Round(-PositionPoint.Y + ShakeAmount.ToPoint().Y), 0f));
        }

        public static void Shake(int amount, int velocity, Vector2 origin)
        {
            ShakeAmountTime = amount;
            ShakeAmountVelocity = (512 - (int)LineSegmentF.Lenght(Position + Globals.WinRenderSize / 2, origin)) / 32;
            if (ShakeAmountVelocity > 16)
                ShakeAmountVelocity = 16;
            if (ShakeAmountVelocity < 0)
                ShakeAmountVelocity = 0;
        }
    }
}