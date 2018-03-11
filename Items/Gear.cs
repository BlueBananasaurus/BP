using Microsoft.Xna.Framework;
using System;

namespace Monogame_GL
{
    public class GearItem : ItemBase, IPickable
    {
        public GearItem(Vector2 position):base(position)
        {

        }

        override public void Pick()
        {
            if (CompareF.RectangleFVsRectangleF(Game1.PlayerInstance.Boundary, this._boundary))
            {
                Game1.PlayerInstance.PutOnVest();
                Game1.PlayerInstance.AccesWeapons();
                Game1.mapLive.mapPickables.Remove(this);
            }
        }

        override public void Draw()
        {
            DrawEntities.DrawGear(_boundary.Origin, _size);
        }
    }
}