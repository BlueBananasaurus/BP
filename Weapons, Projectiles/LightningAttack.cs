using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Monogame_GL
{
    public static class LightningAttack
    {
        public static bool StrikeLightning(Vector2 position, Inpc npc, short damage)
        {
            if (npc.Friendly == false)
            {
                if (LineSegmentF.Lenght(npc.Boundary.Origin, position) < 256)
                {
                    LineSegmentF _ray = new LineSegmentF(npc.Boundary.Origin, position);

                    if (CompareF.LineVsMap(Game1.mapLive.MapTree, new LineObject(Game1.mapLive, _ray)).Count == 0)
                    {
                        Game1.mapLive.mapLightings.Add(new Lightning(npc.Boundary.Origin, position, Game1.debug_thin, 6));
                        Game1.mapLive.mapLightings.Add(new Lightning(npc.Boundary.Origin, position, Game1.debug_thin, 2));
                        npc.Stun();
                        npc.Push(new Vector2(0, -0.2f));
                        npc.KineticDamage(damage);
                        Camera2DGame.Shake(5, position);

                        Sound _strikeSound = new Sound(Game1.soundElectro, position);
                        Game1.Sounds3D.Add(_strikeSound);
                        Game1.Sounds3D[Game1.Sounds3D.IndexOf(_strikeSound)].Play();
                    }
                    return true;
                }
            }

            return false;
        }
    }
}
