using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Monogame_GL
{
    internal class Saw : NpcBase, Inpc, IUnkillable
    {
        private float rotation;
        public bool Friendly { get; }

        public Saw(Vector2 OriginPosition)
        {
            Boundary = new RectangleF(new Vector2(32), OriginPosition - new Vector2(16));
            BoundaryCircle = new CircleF(OriginPosition, 32);
            Friendly = false;
            rotation = 0;
        }

        public void Update(List<Inpc> npcs)
        {
            foreach (Inpc npc in npcs)
            {
                if (npc != this)
                {
                    if (CompareF.RectangleFVsCircleF(BoundaryCircle, npc.Boundary) == true)
                    {
                        npc.KineticDamage(25);
                        LineSegmentF temp = new LineSegmentF(BoundaryCircle.Center, npc.Boundary.Origin);
                        npc.Push(temp.NormalizedWithZeroSolution() * 6f);
                        npc.Stun();
                        PlaySaw();
                    }
                }
            }

            if (CompareF.RectangleFVsCircleF(BoundaryCircle, Game1.PlayerInstance.Boundary) == true)
            {
                Game1.PlayerInstance.TakeDamage(25);
                LineSegmentF temp = new LineSegmentF(BoundaryCircle.Center, Game1.PlayerInstance.Boundary.Origin);
                Game1.PlayerInstance.Push(temp.NormalizedWithZeroSolution() * 6f);
                PlaySaw();
            }

            rotation += Game1.Delta;
        }

        public override void Draw()
        {
            DrawEntities.DrawSaw(BoundaryCircle.Center, rotation);
        }

        public void PlaySaw()
        {
            Sound.PlaySoundPosition(Boundary.Origin, Game1.soundSaw);
        }

        public override void Push(Vector2 velocity)
        {
        }
    }
}