using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Monogame_GL
{
    class Assembly : ITriggers
    {
        public RectangleF Boundary { get; set; }
        public string Name { get; set; }
        public bool On { get; set; }
        public Vector2 Position { get; set; }
        public string Target { get; set; }

        public Assembly(Vector2 position)
        {
            Boundary = new RectangleF(54 * 2, 64 * 2, position.X, position.Y);
            Position = position;
        }

        public void Draw()
        {
            Game1.SpriteBatchGlobal.Draw(Game1.Textures["Automat"], Boundary.Position);
        }

        public void DrawLight()
        {
            Game1.SpriteBatchGlobal.Draw(Game1.Textures["AutomatLight"], origin: new Vector2(108/2,76/2), position: Boundary.Position + new Vector2(53f,33f));
        }

        public void TriggerSwitch()
        {
            Globals.OpenAssembly();
        }
        public void DrawNormal()
        {
        }
        public void Update()
        {

        }
    }
}
