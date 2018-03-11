using Microsoft.Xna.Framework;
using System;

namespace Monogame_GL
{
    public class Healthitem : ItemBase,IPickable
    {
        public Healthitem(Vector2 position):base(position)
        {

        }

        override public void Pick()
        {
            if (Game1.PlayerInstance.Health < Game1.PlayerInstance.MaxHealth)
                if (CompareF.RectangleFVsRectangleF(Game1.PlayerInstance.Boundary, this._boundary))
                {
                    Game1.mapLive.mapPickables.Remove(this);
                    Game1.PlayerInstance.Addhealth(25);
                }
        }

        override public void Draw()
        {
            DrawEntities.DrawHealth(_boundary.Position);
        }
    }
}