using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Monogame_GL
{
    public static class Explosion
    {
        public static void ExplodeSmall(Vector2 pos, short damage)
        {
            CircleF circle = new CircleF(pos, 64);

            foreach (IParticle prtcl in Game1.mapLive.mapParticles)
            {
                if (prtcl.Boundary != null)
                {
                    LineSegmentF Temp = new LineSegmentF(pos, prtcl.Boundary.Origin);

                    Vector2? _thru = CompareF.IntersectionLineWithOthers(Temp, Game1.mapLive.TileMapLines, null);
                    if (_thru == null)
                        prtcl.Push(Vector2.Normalize(Temp.NormalizedWithZeroSolution() * (float)(Globals.GlobalRandom.NextDouble()) * 2));
                }
            }

            int j = 0;

            foreach (Inpc npc in Game1.mapLive.MapNpcs)
            {
                if (j < 8)
                {
                    LineSegmentF Temp = new LineSegmentF(pos, npc.Boundary.Origin);

                    Vector2? _thru = CompareF.IntersectionLineWithOthers(Temp, Game1.mapLive.TileMapLines, null);

                    if (_thru == null && LineSegmentF.Lenght(pos, npc.Boundary.Origin) < 256)
                    {
                        if (npc.Friendly == false)
                            npc.KineticDamage(damage);          
                        npc.Push(Vector2.Normalize(Temp.NormalizedWithZeroSolution() * (float)(Globals.GlobalRandom.NextDouble()) * 2));
                        j++;
                    }
                }
            }

            int x = 2;

            for (int i = 0; i < 3; i++)
                Game1.mapLive.mapParticles.Add(new ParticleFlameDropBig(circle.GenerateRandomPoint(), Game1.Textures["FlameDropBig"]));
            for (int i = 0; i < x; i++)
                Game1.mapLive.mapParticles.Add(new ParticleFireSmall(circle.GenerateRandomPoint()));

            Camera2DGame.Shake(10, pos);

            Sound.PlaySoundPosition(pos, Game1.soundExplosion, CustomMath.RandomAroundZero(Globals.GlobalRandom)/2f);
        }

        public static void Explode(Vector2 pos, short damage)
        {
            CircleF circle = new CircleF(pos, 128);

            foreach (IParticle prtcl in Game1.mapLive.mapParticles)
            {
                if (prtcl.Boundary != null)
                {
                    LineSegmentF Temp = new LineSegmentF(pos, prtcl.Boundary.Origin);

                    Vector2? _thru = CompareF.IntersectionLineWithOthers(Temp, Game1.mapLive.TileMapLines, null);
                    if (_thru == null)
                        prtcl.Push(Vector2.Normalize(Temp.NormalizedWithZeroSolution() * (float)(Globals.GlobalRandom.NextDouble()) * 2));
                }
            }

            int j = 0;

            foreach (Inpc npc in Game1.mapLive.MapNpcs)
            {
                if (j < 8)
                {
                    LineSegmentF Temp = new LineSegmentF(pos, npc.Boundary.Origin);

                    Vector2? _thru = CompareF.IntersectionLineWithOthers(Temp, Game1.mapLive.TileMapLines, null);

                    if (_thru == null && LineSegmentF.Lenght(pos, npc.Boundary.Origin) < 256)
                    {
                        if (npc.Friendly == false)
                            npc.KineticDamage(damage);
                        npc.Push(Vector2.Normalize(Temp.NormalizedWithZeroSolution() * (float)(Globals.GlobalRandom.NextDouble()) * 2));
                        j++;
                    }
                }
            }

            int x = 4;

            for (int i = 0; i < x; i++)
                Game1.mapLive.mapParticles.Add(new ParticleSmokeBig(circle.GenerateRandomPoint()));
            for (int i = 0; i < x; i++)
                Game1.mapLive.mapParticles.Add(new ParticleFireBig(circle.GenerateRandomPoint()));
            for (int i = 0; i < x; i++)
                Game1.mapLive.mapParticles.Add(new ParticleFireSmall(circle.GenerateRandomPoint()));
            Camera2DGame.Shake(10, pos);

            Sound.PlaySoundPosition(pos, Game1.soundExplosion, CustomMath.RandomAroundZero(Globals.GlobalRandom) / 2f);
        }
    }
}
