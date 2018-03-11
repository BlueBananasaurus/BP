using Microsoft.Xna.Framework;

namespace Monogame_GL
{
    public struct FourColorsButtons
    {
        public Color None { get; private set; }
        public Color Hover { get; private set; }
        public Color Pressed { get; private set; }
        public Color Locked { get; private set; }

        public static FourColorsButtons Default { get { return new FourColorsButtons(Color.Black, Color.Black, Color.Black, Color.Black); } }

        public FourColorsButtons(Color none, Color hover, Color pressed, Color locked)
        {
            None = none;
            Hover = hover;
            Pressed = pressed;
            Locked = locked;
        }
    }
}