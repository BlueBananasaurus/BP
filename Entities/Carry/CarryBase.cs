using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace Monogame_GL
{
    public class CarryBase : NpcBase
    {
        public bool Friendly { get; protected set; }
        public bool Locked { get; protected set; }

        public CarryBase()
        {
            Friendly = true;
            Locked = false;
        }

        protected void Pressure(Inpc inpc)
        {
            if (_resolver.VerticalPressure == true || _resolver.HorizontalPressure == true)
            {
                Game1.mapLive.MapNpcs.Remove(inpc);
                Explosion.Explode(Boundary.Origin, 32);
            }
        }

        public void LetGo(Vector2 velocity)
        {
            Locked = false;
            _resolver.TouchTop = false;
            _resolver.TouchTopMovable = false;
            Velocity = velocity;
        }

        virtual public void Carry(Vector2 center)
        {
            Locked = true;
            Boundary.Position = center - (Boundary.Size / 2f);
        }
    }
}
