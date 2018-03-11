using Microsoft.Xna.Framework;

namespace Monogame_GL
{
    public class ParticleSmokeBig : IParticle
    {
        public RectangleF Boundary { get; set; }
        private float _size;
        private float _transparency;

        public ParticleSmokeBig(Vector2 origin)
        {
            Boundary = new RectangleF(new Vector2(128), origin - new Vector2(64));
            _size = 1f;
            _transparency = 1f;
        }

        public void Update()
        {
            _size -= Game1.Delta / 512;

            if (_size <= 0)
            { Game1.mapLive.mapParticles.Remove(this); }
        }

        public void Push(Vector2 velocity)
        {
        }

        public void Draw()
        {
            Effects.ColorEffect(new Vector4(new Vector3(1), _transparency));
            Game1.SpriteBatchGlobal.Draw(Game1.SmokeBig, Boundary.Origin, origin: new Vector2(64), scale: new Vector2(_size));
            Effects.ResetEffect3D();
        }

        public void DrawLight()
        {
        }

        public void DrawNormal()
        {
            Effects.ColorEffect(new Vector4(new Vector3(1), _transparency));
            Game1.SpriteBatchGlobal.Draw(Game1.Textures["SmokeBigNormal"], Boundary.Origin, origin: new Vector2(64), scale: new Vector2(_size));
            Effects.ResetEffect3D();
        }
    }
}