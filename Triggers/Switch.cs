using Microsoft.Xna.Framework;

namespace Monogame_GL
{
    public class Switch : ITriggers
    {
        public string Name { get; set; }
        public Vector2 Position { get; set; }
        public bool On { get; set; }
        public RectangleF Boundary { get; set; }
        public string Target { get; set; }

        public Switch(Vector2 position, string target)
        {
            Position = position;
            Target = target;
            On = false;
            Boundary = new RectangleF(32, 32, position.X, position.Y);
        }

        public void Update()
        {
        }

        public void TriggerSwitch()
        {
            SwitchState();
        }

        public void SwitchState()
        {
            if (Game1.mapLive.MapMovables != null)
            {
                foreach (IRectanglePhysics recGet in Game1.mapLive.MapMovables)
                {
                    if (recGet.Name == Target)
                    {
                        if (recGet.On == true)
                        {
                            recGet.SetOff();
                            On = false;
                        }
                        else
                        {
                            recGet.SetOn();
                            On = true;
                        }
                    }
                }
            }
        }

        public void Draw()
        {
            //if (On)
            //    Game1.SpriteBatch.Draw(texOn, Position);
            //else
            //    Game1.SpriteBatch.Draw(texOff, Position);
        }
        public void DrawNormal()
        {
        }
        public void DrawLight()
        {
        }
    }
}