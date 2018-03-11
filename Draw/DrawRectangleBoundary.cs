using Microsoft.Xna.Framework;
using System;

namespace Monogame_GL
{
    public static class DrawRectangleBoundary
    {
        public static void DrawRed(Rectangle rec)
        {
            Game1.SpriteBatchGlobal.Draw(Game1.Textures["BorderRec"], position: rec.Location.ToVector2(), sourceRectangle: new Rectangle(new Point(0), new Point(rec.Size.X, 4)), origin: new Vector2(0, 2));
            Game1.SpriteBatchGlobal.Draw(Game1.Textures["BorderRec"], position: rec.Location.ToVector2() + new Vector2(0, rec.Size.Y), sourceRectangle: new Rectangle(new Point(0), new Point(rec.Size.X, 4)), origin: new Vector2(0, 2));
            Game1.SpriteBatchGlobal.Draw(Game1.Textures["BorderRec"], position: rec.Location.ToVector2(), sourceRectangle: new Rectangle(new Point(0), new Point(rec.Size.Y, 4)), origin: new Vector2(0, 2), rotation: (float)Math.PI / 2);
            Game1.SpriteBatchGlobal.Draw(Game1.Textures["BorderRec"], position: rec.Location.ToVector2() + new Vector2(rec.Size.X, 0), sourceRectangle: new Rectangle(new Point(0), new Point(rec.Size.Y, 4)), origin: new Vector2(0, 2), rotation: (float)Math.PI / 2);

            Game1.SpriteBatchGlobal.Draw(Game1.Textures["CornerRec"], position: rec.Location.ToVector2(), origin: new Vector2(2, 2));
            Game1.SpriteBatchGlobal.Draw(Game1.Textures["CornerRec"], position: rec.Location.ToVector2() + new Vector2(rec.Size.X, 0), origin: new Vector2(2, 2));
            Game1.SpriteBatchGlobal.Draw(Game1.Textures["CornerRec"], position: rec.Location.ToVector2() + new Vector2(0, rec.Size.Y), origin: new Vector2(2, 2));
            Game1.SpriteBatchGlobal.Draw(Game1.Textures["CornerRec"], position: rec.Location.ToVector2() + new Vector2(rec.Size.X, rec.Size.Y), origin: new Vector2(2, 2));
        }

        public static void DrawWhite(Rectangle rec)
        {
            Game1.SpriteBatchGlobal.Draw(Game1.Textures["BorderWhiteRec"], position: rec.Location.ToVector2(), sourceRectangle: new Rectangle(new Point(0), new Point(rec.Size.X, 2)), origin: new Vector2(0, 1));
            Game1.SpriteBatchGlobal.Draw(Game1.Textures["BorderWhiteRec"], position: rec.Location.ToVector2() + new Vector2(0, rec.Size.Y), sourceRectangle: new Rectangle(new Point(0), new Point(rec.Size.X, 2)), origin: new Vector2(0, 1));
            Game1.SpriteBatchGlobal.Draw(Game1.Textures["BorderWhiteRec"], position: rec.Location.ToVector2(), sourceRectangle: new Rectangle(new Point(0), new Point(rec.Size.Y, 2)), origin: new Vector2(0, 1), rotation: (float)Math.PI / 2);
            Game1.SpriteBatchGlobal.Draw(Game1.Textures["BorderWhiteRec"], position: rec.Location.ToVector2() + new Vector2(rec.Size.X, 0), sourceRectangle: new Rectangle(new Point(0), new Point(rec.Size.Y, 2)), origin: new Vector2(0, 1), rotation: (float)Math.PI / 2);

            Game1.SpriteBatchGlobal.Draw(Game1.Textures["CornerWhiteRec"], position: rec.Location.ToVector2(), origin: new Vector2(1));
            Game1.SpriteBatchGlobal.Draw(Game1.Textures["CornerWhiteRec"], position: rec.Location.ToVector2() + new Vector2(rec.Size.X, 0), origin: new Vector2(1));
            Game1.SpriteBatchGlobal.Draw(Game1.Textures["CornerWhiteRec"], position: rec.Location.ToVector2() + new Vector2(0, rec.Size.Y), origin: new Vector2(1));
            Game1.SpriteBatchGlobal.Draw(Game1.Textures["CornerWhiteRec"], position: rec.Location.ToVector2() + new Vector2(rec.Size.X, rec.Size.Y), origin: new Vector2(1));
        }

        public static void DrawBlue(Rectangle rec)
        {
            Game1.SpriteBatchGlobal.Draw(Game1.Textures["FrameBlue"], position: rec.Location.ToVector2(), sourceRectangle: new Rectangle(new Point(0), new Point(rec.Size.X, 1)), origin: new Vector2(0, 0));
            Game1.SpriteBatchGlobal.Draw(Game1.Textures["FrameBlue"], position: rec.Location.ToVector2() + new Vector2(0, rec.Size.Y), sourceRectangle: new Rectangle(new Point(0), new Point(rec.Size.X, 1)), origin: new Vector2(0, 1));
            Game1.SpriteBatchGlobal.Draw(Game1.Textures["FrameBlue"], position: rec.Location.ToVector2(), sourceRectangle: new Rectangle(new Point(0), new Point(1, rec.Size.Y)), origin: new Vector2(0, 0));
            Game1.SpriteBatchGlobal.Draw(Game1.Textures["FrameBlue"], position: rec.Location.ToVector2() + new Vector2(rec.Size.X, 0), sourceRectangle: new Rectangle(new Point(0), new Point(1, rec.Size.Y)), origin: new Vector2(1, 0));
        }

        public static void DrawPurple(Rectangle rec)
        {
            Game1.SpriteBatchGlobal.Draw(Game1.Textures["FramePurple"], position: rec.Location.ToVector2(), sourceRectangle: new Rectangle(new Point(0), new Point(rec.Size.X, 1)), origin: new Vector2(0, 0));
            Game1.SpriteBatchGlobal.Draw(Game1.Textures["FramePurple"], position: rec.Location.ToVector2() + new Vector2(0, rec.Size.Y), sourceRectangle: new Rectangle(new Point(0), new Point(rec.Size.X, 1)), origin: new Vector2(0, 1));
            Game1.SpriteBatchGlobal.Draw(Game1.Textures["FramePurple"], position: rec.Location.ToVector2(), sourceRectangle: new Rectangle(new Point(0), new Point(1, rec.Size.Y)), origin: new Vector2(0, 0));
            Game1.SpriteBatchGlobal.Draw(Game1.Textures["FramePurple"], position: rec.Location.ToVector2() + new Vector2(rec.Size.X, 0), sourceRectangle: new Rectangle(new Point(0), new Point(1, rec.Size.Y)), origin: new Vector2(1, 0));
        }
    }
}