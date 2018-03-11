using System;
using Microsoft.Xna.Framework;

namespace Monogame_GL
{
    public class ParticleFlake : IParticle
    {
        public RectangleF Boundary { get; set; }
        private float _transparency;
        private Vector2 _velocity;

        public ParticleFlake(Vector2 position)
        {
            Boundary = new RectangleF(new Vector2(38, 38), position);
            _transparency = 1f;
            _velocity = new Vector2((float)(Globals.GlobalRandom.NextDouble() - 0.5f) / 4, (float)(Globals.GlobalRandom.NextDouble() - 0.5f) / 4);
        }

        public void Update()
        {
            _transparency -= Game1.Delta / 512;

            if (_transparency <= 0)
                Game1.mapLive.mapParticles.Remove(this);

            Boundary.Position += _velocity * Game1.Delta;
        }

        public void Push(Vector2 velocity)
        {
        }

        public void Draw()
        {
            Game1.EffectColors.Parameters["R"].SetValue(1f);
            Game1.EffectColors.Parameters["G"].SetValue(1f);
            Game1.EffectColors.Parameters["B"].SetValue(1f);
            Game1.EffectColors.Parameters["A"].SetValue(_transparency);

            Game1.EffectColors.CurrentTechnique.Passes[0].Apply();

            Game1.SpriteBatchGlobal.Draw(Game1.Flake, Boundary.Position, scale: new Vector2(2));

            Game1.EffectBaseColor.CurrentTechnique.Passes[0].Apply();
        }

        public void DrawLight()
        {
        }

        public void DrawNormal()
        {

        }
    }
}