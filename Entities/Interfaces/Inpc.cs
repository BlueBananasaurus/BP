using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Monogame_GL
{
    public interface Inpc
    {
        float Health { get; }
        CircleF BoundaryCircle { get; }
        RectangleF Boundary { get; }
        bool Friendly { get; }
        Vector2 Velocity { get; set; }

        void KineticDamage(short damage);

        void Push(Vector2 velocity);

        void Update(List<Inpc> npcs);

        void Draw();

        void DrawNormal();

        void DrawLight();

        void Stun();
    }
}