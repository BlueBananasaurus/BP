using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Monogame_GL
{
    class BatteryTrigger : ITriggers
    {
        public string Name { get; set; }
        private float _transparency;

        public BatteryTrigger(Vector2 position, string target)
        {
            Boundary = new RectangleF(64, 96, position.X, position.Y);
            Position = position;
            Target = target;
            On = false;
            _transparency = 0f;
        }

        public RectangleF Boundary { get; set; }
        public bool On { get; set; }
        public Vector2 Position { get; set; }
        public string Target { get; set; }

        public void Draw()
        {
            Game1.SpriteBatchGlobal.Draw(Game1.Textures["TriggerBattery"], Boundary.Position);
            if(On == true)
                Game1.SpriteBatchGlobal.Draw(Game1.Textures["Battery"], Boundary.Position + new Vector2(16,14));
        }

        public void DrawLight()
        {
            if (On == true)
            {
                Effects.ColorEffect(new Vector4(1, 1, 1, _transparency));
                Game1.SpriteBatchGlobal.Draw(Game1.Textures["BatteryLight"], Boundary.Origin, origin:new Vector2(32));
                Effects.ResetEffect3D();
            }
        }

        public void TriggerSwitch()
        {

        }
        public void DrawNormal()
        {
        }
        public void Update()
        {
            if (On == false)
            {
                foreach (Inpc npc in Game1.mapLive.MapNpcs.Reverse<Inpc>())
                {
                    if ((npc as Battery)?.Locked == false && CompareF.RectangleFVsRectangleF(Boundary, npc.Boundary) == true)
                    {
                        On = true;
                        Game1.mapLive.MapNpcs.Remove(npc);
                        Sound.PlaySoundPosition(Boundary.Origin, Game1.Sounds["BatteryInserted"]);

                        foreach (IRectanglePhysics recGet in Game1.mapLive.MapMovables)
                        {
                            if (recGet.Name == Target)
                            {
                                recGet.SetOn();
                            }
                        }

                        break;
                    }
                }
            }
            else
            {
                _transparency = 0.75f + (float)(Math.Sin(Game1.Time * 2 + Globals.GlobalRandom.NextDouble()) / 4);
            }
        }
    }
}
