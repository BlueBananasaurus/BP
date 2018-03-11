using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Monogame_GL
{
    internal class NegGate : ITriggers
    {
        public string Name { get; set; }
        public Vector2 Position { get; set; }
        public bool On { get; set; }
        public RectangleF Boundary { get; set; }
        public string Target { get; set; }

        public NegGate(string name, string target)
        {
            Position = Vector2.Zero;
            Target = target;
            On = false;
            Boundary = null;
            Name = name;
        }

        public void Update()
        {
            foreach (ITriggers trgr in Game1.mapLive.mapTriggers)
            {
                if (trgr.Target == Name)
                {
                    if (trgr.On == false)
                    {
                        SetOn();
                        On = true;
                        break;
                    }
                    else
                    {
                        SetOff();
                        On = false;
                        break;
                    }
                }
            }
        }

        public void TriggerSwitch()
        {

        }

        public void SetOn()
        {
            foreach (IRectanglePhysics recGet in Game1.mapLive.MapMovables)
            {
                if (recGet.Name == Target)
                {
                    recGet.SetOn();
                }
            }
        }

        public void SetOff()
        {
            foreach (IRectanglePhysics recGet in Game1.mapLive.MapMovables)
            {
                if (recGet.Name == Target)
                {
                    recGet.SetOff();
                }
            }
        }

        public void Draw()
        {
        }
        public void DrawNormal()
        {
        }
        public void DrawLight()
        {
        }
    }
}