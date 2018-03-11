using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Monogame_GL
{
    internal class OrGate : ITriggers
    {
        public string Name { get; set; }
        public Vector2 Position { get; set; }
        public bool On { get; set; }
        public RectangleF Boundary { get; set; }
        public string Target { get; set; }
        public bool FoundOn { get; set; }

        public OrGate(string name, string target)
        {
            Position = Vector2.Zero;
            Target = target;
            On = false;
            Boundary = null;
            Name = name;
            FoundOn = false;
        }

        public void Update()
        {
            FoundOn = false;

            foreach (ITriggers trgr in Game1.mapLive.mapTriggers)
            {
                if (trgr.Target == Name && trgr.On == true)
                {
                    SeOn();
                    FoundOn = true;
                    break;
                }
            }
            
            if (FoundOn == false)
            {
                SetOff();
            }
        }

        public void TriggerSwitch()
        {

        }

        public void SeOn()
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