using Microsoft.Xna.Framework;

namespace Monogame_GL
{
    public abstract class ScreenBase
    {
        private float transparency;

        protected ScreenBase()
        {
            Reset();
        }

        protected void UpdateBase()
        {
            if (transparency > 0)
            {
                transparency -= Game1.Delta / Globals.UITransparencyFade;
            }
        }

        protected void DrawBase(string title)
        {
            DrawString.DrawText(title, Globals.MenuTitlePosition, Align.center, new Color(100, 100, 100), FontType.small);
            Effects.ColorEffect(new Vector4(1, 1, 1, transparency));
            Game1.SpriteBatchGlobal.Draw(Game1.Transition, Vector2.Zero);
            Game1.EffectBaseColor.CurrentTechnique.Passes[0].Apply();
            Effects.ResetEffect3D();
        }

        public void Reset()
        {
            transparency = 1f;
        }
    }
}