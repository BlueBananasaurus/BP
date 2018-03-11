using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Monogame_GL
{
    public class EndPort : ITriggers
    {
        public string Name { get; set; }
        public Vector2 Position { get; set; }
        public bool On { get; set; }
        public RectangleF Boundary { get; set; }
        public string Target { get; set; }

        public EndPort(Vector2 position)
        {
            Position = position;
            Boundary = new RectangleF(80, 128, position.X, position.Y);
        }

        public void Update()
        {
        }

        public void TriggerSwitch()
        {
            Game1.mapLive.GoOut();
        }

        public void SetOff()
        {
        }

        public void SetOn()
        {
        }

        public void Draw()
        {
            Game1.SpriteBatchGlobal.Draw(Game1.Textures["EndPort"], Boundary.Position, scale: new Vector2(2));
        }

        public void DrawNormal()
        {
        }

        public void DrawLight()
        {
        }
    }
}