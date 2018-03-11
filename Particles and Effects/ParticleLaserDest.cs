using Microsoft.Xna.Framework;

namespace Monogame_GL
{
    public class ParticleLaserDest : IParticle
    {
        public RectangleF Boundary { get; set; }
        private float _transparency;
        private Timer _life;

        public ParticleLaserDest(Vector2 position)
        {
            Boundary = new RectangleF(new Vector2(8, 8), position);
            _transparency = 1f;
            _life = new Timer(1000, false);
        }

        public void Update()
        {
            _life.Update();

            if (_life.Ready == true)
            {
                _transparency -= Game1.Delta / 256;
            }

            if (_transparency <= 0)
                Game1.mapLive.mapParticles.Remove(this);
        }

        public void Push(Vector2 velocity)
        {
        }

        public void Draw()
        {
            Game1.EffectColors.Parameters["R"].SetValue(1f);
            Game1.EffectColors.Parameters["G"].SetValue(1f);
            Game1.EffectColors.Parameters["B"].SetValue(1f);
            Game1.EffectColors.Parameters["A"].SetValue(0.5f + _transparency);

            Game1.EffectColors.CurrentTechnique.Passes[0].Apply();

            Game1.SpriteBatchGlobal.Draw(Game1.hot, Boundary.Position, origin: new Vector2(16));

            Game1.EffectBaseColor.CurrentTechnique.Passes[0].Apply();
        }

        public void DrawLight()
        {
            Game1.EffectColors.Parameters["R"].SetValue(1f);
            Game1.EffectColors.Parameters["G"].SetValue(1f);
            Game1.EffectColors.Parameters["B"].SetValue(1f);
            Game1.EffectColors.Parameters["A"].SetValue(0.5f + _transparency);

            Game1.EffectColors.CurrentTechnique.Passes[0].Apply();

            Game1.SpriteBatchGlobal.Draw(Game1.hotLight, Boundary.Position, origin: new Vector2(32));

            Game1.EffectBaseColor.CurrentTechnique.Passes[0].Apply();
        }

        public void DrawNormal()
        {

        }
    }
}