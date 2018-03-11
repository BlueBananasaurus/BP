using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Monogame_GL
{
    public sealed class ParticleFlameTrail : IParticle
    {
        public LineSegmentF LineSegment { get; set; }

        public RectangleF Boundary { get; set; }

        private float _transparency;

        public ParticleFlameTrail(Vector2 start, Vector2 end)
        {
            LineSegment = new LineSegmentF(start, end);
            _transparency = 1f;
        }

        public ParticleFlameTrail(LineSegmentF lineSegment)
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
            Game1.SpriteBatchGlobal.Draw(Game1.Textures["Flame"], LineSegment.Start, null, new Rectangle(0, 0, distance, 32), new Vector2(0, 16), rotation, new Vector2(1), Color.White, SpriteEffects.None);
            Effects.ResetEffect3D();
        }

        public void DrawLight()
        {
            float rotation = CompareF.VectorToAngle(LineSegment.ToVector2());
            int distance = (int)Math.Ceiling(LineSegment.Lenght().Value);
            Effects.ColorEffect(new Vector4(1, 1, 1, _transparency));
            Game1.SpriteBatchGlobal.Draw(Game1.Textures["Flame"], LineSegment.Start, null, new Rectangle(0, 0, distance, 32), new Vector2(0, 16), rotation, new Vector2(1), Color.White, SpriteEffects.None);
            Effects.ResetEffect3D();
        }

        public void Push(Vector2 velocity)
        {

        }
        public void DrawNormal()
        {

        }
    }
}