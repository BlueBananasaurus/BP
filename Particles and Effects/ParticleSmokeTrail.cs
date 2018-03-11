using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Monogame_GL
{
    public sealed class ParticleSmokeTrail: IParticle
    {
        public LineSegmentF LineSegment { get; set; }

        public RectangleF Boundary { get; set; }

        private float _transparency;

        public ParticleSmokeTrail(Vector2 start, Vector2 end)
        {
            LineSegment = new LineSegmentF(start, end);
            _transparency = 1f;
        }

        public ParticleSmokeTrail(LineSegmentF lineSegment)
        {
            LineSegment = lineSegment;
            _transparency = 1f;
        }

        public void Update()
        {
            _transparency -= Game1.Delta / 700;

            if (_transparency <= 0)
                Game1.mapLive.mapParticles.Remove(this);
        }

        public void Draw()
        {
            float rotation = CompareF.VectorToAngle(LineSegment.ToVector2());
            int distance = (int)Math.Ceiling(LineSegment.Lenght().Value);

            Effects.ColorEffect(new Vector4(1, 1, 1, _transparency));
            Game1.SpriteBatchGlobal.Draw(Game1.smoke, LineSegment.Start, null, new Rectangle(0, 0, distance, 16), new Vector2(0, 8), rotation, new Vector2(1f), Color.White, SpriteEffects.None);
            Effects.ResetEffect3D();
        }

        public void DrawLight()
        {

        }

        public void Push(Vector2 velocity)
        {

        }

        public void DrawNormal()
        {

        }
    }
}