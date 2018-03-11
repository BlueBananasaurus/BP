using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Monogame_GL
{
    public static class Effects
    {
        public static void WaterEffect(Vector2 mult, Vector2 div, Vector2 time, Vector2 pos, Texture2D tex)
        {
            Game1.EffectWater.Parameters["Mult"].SetValue(mult);
            Game1.EffectWater.Parameters["Div"].SetValue(div);
            Game1.EffectWater.Parameters["Time"].SetValue(time);
            Game1.EffectWater.Parameters["pos"].SetValue(pos);
            Game1.EffectWater.Parameters["Texture"].SetValue(tex);
            Game1.EffectWater.CurrentTechnique.Passes[0].Apply();
        }

        public static void ColorEffect(Vector4 color)
        {
            Game1.EffectColors.Parameters["R"].SetValue(color.X);
            Game1.EffectColors.Parameters["G"].SetValue(color.Y);
            Game1.EffectColors.Parameters["B"].SetValue(color.Z);
            Game1.EffectColors.Parameters["A"].SetValue(color.W);
            Game1.EffectColors.CurrentTechnique.Passes[0].Apply();
        }

        public static void GaussHorizontalEffect(Texture2D texture, Texture2D mask)
        {
            Game1.EffectBlur.Parameters["dimensions"].SetValue(Globals.WinRenderSize);
            Game1.EffectBlur.Parameters["Mask"].SetValue(mask);
            Game1.EffectBlur.Parameters["TextureIn"].SetValue(texture);
            Game1.EffectBlur.Parameters["kernelSize"].SetValue(16);
            Game1.EffectBlur.CurrentTechnique.Passes[0].Apply();
        }

        public static void GaussVerticalEffect(Texture2D texture, Texture2D mask)
        {
            Game1.EffectBlur.Parameters["dimensions"].SetValue(Globals.WinRenderSize);
            Game1.EffectBlur.Parameters["Mask"].SetValue(mask);
            Game1.EffectBlur.Parameters["TextureIn"].SetValue(texture);
            Game1.EffectBlur.Parameters["kernelSize"].SetValue(16);
            Game1.EffectBlur.CurrentTechnique.Passes[1].Apply();
        }

        public static void ResetEffect3D()
        {
            Game1.EffectBaseColor.Parameters["World"].SetValue(Game1.World);
            Game1.EffectBaseColor.Parameters["View"].SetValue(Game1.View);
            Game1.EffectBaseColor.Parameters["Projection"].SetValue(Game1.projection);
            Game1.EffectBaseColor.CurrentTechnique.Passes[0].Apply();
        }

        public static void ResetEffectSimple()
        {
            Game1.EffectBaseColorSimple.CurrentTechnique.Passes[0].Apply();
        }

        public static void RotateNormalsEffect(float rotation, Vector2 flip)
        {
            Game1.EffectRotateNormals.Parameters["flip"].SetValue(flip);
            Game1.EffectRotateNormals.Parameters["angle"].SetValue(rotation);
            Game1.EffectRotateNormals.CurrentTechnique.Passes[0].Apply();
        }
    }
}
