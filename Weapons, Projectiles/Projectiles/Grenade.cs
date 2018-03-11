using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace Monogame_GL
{
    public class Grenade : PhysicEntity, IProjectile
    {
        private Timer _timer;
        private Timer _bubbleTime;
        private Vector2 _oldVelocity;
        private short _damage;

        public Grenade(Vector2 position, Vector2 velocity, short damage)
        {
            Boundary = new RectangleF(new Vector2(12 * 2, 14 * 2), position);
            Velocity = velocity;
            _resolver = new CollisionResolver(Globals.TileSize);
            _timer = new Timer(1000, false);
            _bubbleTime = new Timer(300, true);
            _damage = damage;
            _oldVelocity = velocity;
        }

        public void Update(Map map)
        {
            _oldVelocity = Velocity;

            _resolver.move(ref _velocity, new Vector2(1f), Boundary, 0.5f, new Vector2(0.025f), new Vector2(0.0005f), new Vector2(0.3f), Game1.mapLive.MapMovables);

            if (Math.Abs(Velocity.X)>0.1f && Math.Sign(Velocity.X) != Math.Sign(_oldVelocity.X) && (_resolver.TouchLeft || _resolver.TouchRight || _resolver.TouchLeftMovable || _resolver.TouchRightMovable))
            {
                Sound.PlaySoundPosition(Boundary.Origin, Game1.soundGrenadeHit);
            }

            if (Math.Abs(Velocity.Y) > 0.1f && Math.Sign(Velocity.Y) != Math.Sign(_oldVelocity.Y) && (_resolver.TouchBottom || _resolver.TouchBottomMovable || _resolver.TouchTop || _resolver.TouchTopMovable))
            {
                Sound.PlaySoundPosition(Boundary.Origin, Game1.soundGrenadeHit);
            }

            _bubbleTime.Update();

            if (_bubbleTime.Ready == true)
            {
                Game1.mapLive.mapParticles.Add(new ParticleBubble(Boundary.Origin));
                _bubbleTime.Reset();
            }

            _timer.Update();

            if (_timer.Ready == true || _resolver.VerticalPressure == true || _resolver.HorizontalPressure == true)
            {
                Explode(map.MapTree, map.MapNpcs);
            }
        }

        private void Explode(MapTreeHolder map, List<Inpc> npcs)
        {
            Explosion.Explode(Boundary.Origin, _damage);
            Game1.mapLive.MapProjectiles.Remove(this);
        }

        public override void Draw()
        {
            Game1.SpriteBatchGlobal.Draw(Game1.grenade, Boundary.Position);
        }

        public void DrawNormal()
        {
            Game1.SpriteBatchGlobal.Draw(Game1.Textures["grenadeNormal"], Boundary.Position);
        }
    }
}