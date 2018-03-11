namespace Monogame_GL
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Input;

    public static class MouseInput

    {
        public static MouseState MouseStateNew { get; private set; }
        public static MouseState MouseStateOld { get; private set; }
        public static int OldScroll { get; private set; }
        public static int NewScroll { get; private set; }

        public static Point MousePositionGridIndex()
        {
            return new Point(
                (int)((MouseRealPosMenu().X + Camera2DEditor.Position.X) / Globals.TileSize),
                (int)((MouseRealPosMenu().Y + Camera2DEditor.Position.Y) / Globals.TileSize));
        }

        public static Vector2 MousePositionGrid()
        {
            return new Vector2
                (
                (int)((MouseStateNew.Position.X + Camera2DEditor.Position.X * Globals.ScreenRatio.X) / (Globals.TileSize * Globals.ScreenRatio.X)) * (Globals.TileSize * Globals.ScreenRatio.X) - Camera2DEditor.Position.X * Globals.ScreenRatio.X,
                (int)((MouseStateNew.Position.Y + Camera2DEditor.Position.Y * Globals.ScreenRatio.X) / (Globals.TileSize * Globals.ScreenRatio.X)) * (Globals.TileSize * Globals.ScreenRatio.X) - Camera2DEditor.Position.Y * Globals.ScreenRatio.X
                );
        }

        public static Vector2 MousePositionGridBetter()
        {
            return new Vector2(
                (float)((int)(MouseRealPos(Globals.WinOffset.ToVector2() - new Vector2(Camera2DEditor.Position.X, Camera2DEditor.Position.Y) * Globals.ScreenRatio.X, Globals.ScreenRatio.X).X / 32) * 32),
                (float)((int)(MouseRealPos(Globals.WinOffset.ToVector2() - new Vector2(Camera2DEditor.Position.X, Camera2DEditor.Position.Y) * Globals.ScreenRatio.X, Globals.ScreenRatio.X).Y / 32) * 32)
                );
        }

        public static Vector2 MousePositionGridhalfBetter()
        {
            return new Vector2(
                (float)((int)(MouseRealPos(Globals.WinOffset.ToVector2() - new Vector2(Camera2DEditor.Position.X, Camera2DEditor.Position.Y) * Globals.ScreenRatio.X, Globals.ScreenRatio.X).X/16)*16),
                (float)((int)(MouseRealPos(Globals.WinOffset.ToVector2() - new Vector2(Camera2DEditor.Position.X, Camera2DEditor.Position.Y) * Globals.ScreenRatio.X, Globals.ScreenRatio.X).Y/16)*16)
                );
        }

        public static Point MousePositionRealGrid()
        {
            return new Point(
                (int)(((MouseRealPosMenu().X + Globals.TileSize / 4) + Camera2DEditor.Position.X) / (Globals.TileSize / 2)) * (Globals.TileSize / 2),
                (int)(((MouseRealPosMenu().Y + Globals.TileSize / 4) + Camera2DEditor.Position.Y) / (Globals.TileSize / 2)) * (Globals.TileSize / 2));
        }

        public static void NewUpdate()
        {
            MouseStateNew = Mouse.GetState();
            NewScroll = MouseStateNew.ScrollWheelValue;
        }

        public static Vector2 MouseRealPosMenu()
        {
            return (MouseStateNew.Position.ToVector2() - Globals.WinOffset.ToVector2()) / Globals.ScreenRatio.X;
        }

        private static Vector2 MouseRealPos(Vector2 offset, float zoom)
        {
            return (MouseStateNew.Position.ToVector2() - offset) / zoom;
        }

        public static Vector2 MouseRealPosGame()
        {
            return new Vector2(
                (float)(MouseRealPos(Globals.WinOffset.ToVector2() - new Vector2(Camera2DGame.Position.X, Camera2DGame.Position.Y) * Globals.ScreenRatio.X, Globals.ScreenRatio.X).X),
                (float)(MouseRealPos(Globals.WinOffset.ToVector2() - new Vector2(Camera2DGame.Position.X, Camera2DGame.Position.Y) * Globals.ScreenRatio.X, Globals.ScreenRatio.X).Y)
                );
        }

        public static void OldUpdate()
        {
            MouseStateOld = MouseStateNew;
            OldScroll = NewScroll;
        }

        public static bool ScrolledDown()
        {
            if (OldScroll > NewScroll)
                return true;
            return false;
        }

        public static bool ScrolledUp()
        {
            if (OldScroll < NewScroll)
                return true;
            return false;
        }

        public static bool MouseClickedLeft()
        {
            if (MouseStateNew.LeftButton == ButtonState.Released && MouseStateOld.LeftButton == ButtonState.Pressed)
            {
                return true;
            }

            return false;
        }

        public static bool MouseClickedRight()
        {
            if (MouseStateNew.RightButton == ButtonState.Released && MouseStateOld.RightButton == ButtonState.Pressed)
            {
                return true;
            }

            return false;
        }

        public static void DrawCursorMenu()
        {
            if (MouseStateNew.LeftButton == ButtonState.Pressed)
                Game1.SpriteBatchGlobal.Draw(Game1.Textures["CursorMouse"], MouseStateNew.Position.ToVector2(), origin: new Vector2(0), scale: new Vector2(1) * Globals.ScreenRatio.X, sourceRectangle: new Rectangle(24, 0, 24, 24));
            else
                Game1.SpriteBatchGlobal.Draw(Game1.Textures["CursorMouse"], MouseStateNew.Position.ToVector2(), origin: new Vector2(0), scale: new Vector2(1) * Globals.ScreenRatio.X, sourceRectangle: new Rectangle(0, 0, 24, 24));
        }
    }
}