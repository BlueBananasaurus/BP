using Microsoft.Xna.Framework;
using System;

namespace Monogame_GL
{
    public class ShieldItem :ItemBase, IPickable
    {
        public ShieldItem(Vector2 position): base(position)
        {
        }

        override public void Pick()
        {
            if (Game1.PlayerInstance.Health < Game1.PlayerInstance.MaxHealth)
                if (CompareF.RectangleFVsRectangleF(Game1.PlayerInstance.Boundary, this._boundary))
                {
                    Game1.mapLive.mapPickables.Remove(this);
                    Game1.PlayerInstance.AddShield(25);
                }
        }

        override public void Draw()
        {
            DrawEntities.DrawShield(_boundary.Position);
        }
    }
}