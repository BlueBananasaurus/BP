using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Monogame_GL
{
    public class Property
    {
        public string Text { get; set; }
        public IControl ControlThing { get; set; }
        public Vector2 Position { get; set; }

        public Property(Vector2 position, string text, IControl thing)
        {
            Text = text;
            ControlThing = thing;
            Position = position;
        }

        public void Update()
        {
            ControlThing.Update();
        }

        public void Draw()
        {
            DrawString.DrawText(Text, Position, Align.left, Globals.LightBlueText, FontType.small);
            ControlThing.Draw();
        }
    }
}
