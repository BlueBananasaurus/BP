using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Monogame_GL
{
    internal class AndGate : ITriggers
    {
        public string Name { get; set; }
        public Vector2 Position { get; set; }
        public bool On { get; set; }
        public RectangleF Boundary { get; set; }
        public string Target { get; set; }
        public bool FoundOn { get; set; }

        public AndGate(string name, string target)
        {
            Position = Vector2.Zero;
            Target = target;
            On = false;
            Boundary = null;
            Name = name;
            FoundOn = true;
        }

        public void Update()
        {
            FoundOn = true;

            foreach (ITriggers trgr in Game1.mapLive.mapTriggers)
            {
                if (trgr.Target == Name && trgr.On == false)
                {
                    SetOff();
                    FoundOn = false;
                    break;
                }
            }

            if (FoundOn == true)
            {
                SetOn();
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