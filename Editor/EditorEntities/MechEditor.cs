using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Monogame_GL
{
    public class MechEditor : BaseEditor
    {
        public RectangleF boundary { get; set; }
        private Vector2 _size;
        private mechFacingCabin _face;

        public MechEditor(Vector2 position, mechFacingCabin face) : base()
        {
            _size = MechModule.MechBoundarySize;
            boundary = new RectangleF(MechModule.MechBoundarySize, position);
            _face = face;
            _items.Add(new ItemHolder("Facing", _face));
        }

        public void Draw()
        {
            DrawEntities.DrawMechDefault(boundary.Position,_face);
        }
    }
}