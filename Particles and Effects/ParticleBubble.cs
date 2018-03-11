using System;
using Microsoft.Xna.Framework;

namespace Monogame_GL
{
    public class ParticleBubble : PhysicEntity, IParticle
    {
        public ParticleBubble(Vector2 origin)
        {
            Boundary = new RectangleF(new Vector2(8, 8), origin - new Vector2(4));
            Velocity = new Vector2((float)(Globals.GlobalRandom.NextDouble() - 0.5f) / 4, Velocity.Y);
            _resolver = new CollisionResolver(Globals.TileSize);
        }

        public void Update()
        {
            Velocity = new Vector2((float)(Globals.GlobalRandom.NextDouble() - 0.5f) / 4, Velocity.Y);

            _resolver.move(ref _velocity, new Vector2(2f), Boundary, 0f, new Vector2(0f), new Vector2(0.1f), new Vector2(0.5f), Game1.mapLive.MapMovables, buoyancyInWater: 0.1f);

            if (_resolver.InWater == false)
            {
                Game1.mapLive.mapParticles.Remove(this);
            }
        }

        public override void Draw()
        {
            Game1.SpriteBatchGlobal.Draw(Game1.bubble, Boundary.Position);
        }

        public override void Push(Vector2 velocity)
        {
        }

        public void DrawNormal()
        {
        }
    }
}