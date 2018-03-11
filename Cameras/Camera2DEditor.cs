using Microsoft.Xna.Framework;
using System;

namespace Monogame_GL
{
    public static class Camera2DEditor
    {
        private static float _offset { get { return 128; } }
        public static Vector2 Position { get; private set; }

        public static Vector2 PositionPoint
        {
            get { return new Vector2((int)Math.Round(Position.X), (int)Math.Round(Position.Y)); }
            private set { }
        }

        public static RectangleF Boundary
        {
            get { return new RectangleF(Globals.WinRenderSize, Position); }
        }

        static Camera2DEditor()
        {
            Position = new Vector2(-_offset);
        }

        public static void MoveBy(Vector2 move)
        {
            Position -= move / Globals.ScreenRatio.X;
            if (Position.X > (Editor.EditMap.FunctionTileMap.GetLength(0) * Globals.TileSize - Globals.WinRenderSize.X) + _offset)
            {
                Position = new Vector2((Editor.EditMap.FunctionTileMap.GetLength(0) * Globals.TileSize - Globals.WinRenderSize.X) + _offset, Position.Y);
            }
            if (Position.Y > (Editor.EditMap.FunctionTileMap.GetLength(1) * Globals.TileSize - Globals.WinRenderSize.Y) + _offset)
            {
                Position = new Vector2(Position.X, (Editor.EditMap.FunctionTileMap.GetLength(1) * Globals.TileSize - Globals.WinRenderSize.Y) + _offset);
            }
            if (Position.X < -_offset)
                Position = new Vector2(-_offset, Position.Y);
            if (Position.Y < -_offset)
                Position = new Vector2(Position.X, -_offset);
        }

        public static void Update()
        {
        }

        public static Matrix GetViewMatrix()
        {
            return Matrix.CreateTranslation(new Vector3((float)Math.Round(-PositionPoint.X), (float)Math.Round(-PositionPoint.Y), 0f));
        }

        public static Matrix GetViewMatrixLayer2()
        {
            return Matrix.CreateTranslation(new Vector3((float)Math.Round(-PositionPoint.X * 0.75f), (float)Math.Round(-PositionPoint.Y), 0f));
        }
    }
}