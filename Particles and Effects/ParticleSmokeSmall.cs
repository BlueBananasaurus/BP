using Microsoft.Xna.Framework;

namespace Monogame_GL
{
    public class ParticleSmokeSmall : IParticle
    {
        public RectangleF Boundary { get; set; }
        public short frame { get; private set; }
        private Timer timeParticle;
        private float _size;

        public ParticleSmokeSmall(Vector2 position)
        {
            Boundary = new RectangleF(new Vector2(32), position - new Vector2(16));
            frame = (short)(-1);
            timeParticle = new Timer(50 + Globals.GlobalRandom.Next(0, 101), true);
            _size = 1;
        }

        public void Update()
        {
            _size += Game1.Delta / 256;

            timeParticle.Update();

            if (timeParticle.Ready == true)
            {
                frame++;
                timeParticle.Reset();
            }

            if (frame > 2)
            { Game1.mapLive.mapParticles.Remove(this); }

            Boundary.Position -= new Vector2(0, Game1.Delta / 10);
        }

        public void Push(Vector2 velocity)
        {
        }

        public void Draw()
        {
            Game1.SpriteBatchGlobal.DrawAtlas(Game1.smokeSmall, Boundary.Origin, new Point(32), new Point(frame, 0), new Vector2(16), _size);
        }

        public void DrawLight()
        {
        }
    }
}