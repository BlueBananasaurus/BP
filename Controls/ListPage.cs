using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Monogame_GL
{
    public class ListPage
    {
        public RectangleF Boundary { get; set; }
        private RenderTarget2D _contentTarget;
        public List<ListItemClassic> _items;
        private float _offset;

        public ListPage(RectangleF boundary)
        {
            Boundary = boundary;
            _contentTarget = new RenderTarget2D(Game1.GraphicsGlobal.GraphicsDevice, (int)Globals.WinRenderSize.X, (int)Globals.WinRenderSize.Y);
            _items = new List<ListItemClassic>();
            _offset = 0;
        }

        public void ResetOffset()
        {
            _offset = 0;
        }

        public void CloseOthers(ListItemClassic Preserve)
        {
            foreach (ListItemClassic item in _items)
            {
                if (item != Preserve)
                {
                    item.SetNone();
                }
            }
        }

        public void Update()
        {
            if (CompareF.RectangleVsVector2(Boundary, MouseInput.MouseRealPosMenu()))
            {
                if (MouseInput.ScrolledDown())
                    _offset -= 64f;
                if (MouseInput.ScrolledUp())
                    _offset += 64f;
            }

            if (_offset < -((32 * _items.Count) - Boundary.Size.Y + 32))
            {
                _offset = -((32 * _items.Count) - Boundary.Size.Y + 32);
            }

            if (_offset > 0)
            {
                _offset = 0;
            }

            for (int i = 0; i < _items.Count; i++)
            {
                _items[i].Move(_offset);
                _items[i].Update(this);
            }
        }

        public void Draw()
        {
            Game1.GraphicsGlobal.GraphicsDevice.SetRenderTarget(_contentTarget);
            Game1.GraphicsGlobal.GraphicsDevice.Clear(Color.Transparent);

            for (int i = 0; i < _items.Count; i++)
            {
                _items[i].Draw();
            }

            Game1.GraphicsGlobal.GraphicsDevice.SetRenderTarget(Game1.Win);
            Game1.SpriteBatchGlobal.Draw(_contentTarget, Boundary.Position, new Rectangle(0, 0, (int)Boundary.Size.X, (int)Boundary.Size.Y), Color.White);
        }
    }
}