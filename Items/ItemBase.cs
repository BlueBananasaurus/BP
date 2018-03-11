using Microsoft.Xna.Framework;
using System;

namespace Monogame_GL
{
    public class ItemBase : IPickable
    {
        public Vector2 Position;
        protected RectangleF _boundary;
        protected Vector2 _size;

        public ItemBase(Vector2 position)
        {
            _boundary = new RectangleF(new Vector2(48, 48), position);
            Position = position;
            _size = new Vector2(2);
        }

        public void Update()
        {
            _boundary.Position = new Vector2(Position.X, Position.Y + (float)Math.Sin(Game1.Time * 5) * 10 - 16);
            _size.Y = 2;
            _size.X = 2 + (float)Math.Sin(Game1.Time * 5) / 4;
            Pick();
        }

        public virtual void Pick()
        {

        }

        public virtual void Draw()
        {

        }

        public void DrawLight()
        {
            Game1.SpriteBatchGlobal.Draw(Game1.Textures["LightPick"], _boundary.Origin, origin: new Vector2(64));
        }
    }
}