using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Monogame_GL
{
    public class PlayerEditor : BaseEditor
    {
        public RectangleF Boundary { get; set; }
        private Vector2 _size;

        public PlayerEditor(Vector2 position) : base()
        {
            _size = BuddyModule.PlayerBoundarySize;
            Boundary = new RectangleF(BuddyModule.PlayerBoundarySize, position);
            _items.Add(new ItemHolder("Vest on", true));
        }

        public void Draw()
        {
            DrawEntities.DrawBuddyDefault(Boundary.Position);
        }
    }
}