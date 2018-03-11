using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Monogame_GL
{
    public class Projectile : ProjectileBase, IProjectile
    {
        public Projectile(short damage, Vector2 velocity, Vector2 position, object from) : base(velocity, position, from, damage){}

        public void Update(Map map)
        {
            Update(map, this);

            PlayerTakeDamage(5,this);
            DamageToNPC(map, this);

            if (_wet == true && _wet != _wetPreviousFrame) _velocity /= 4;
        }

        protected override void Update(Map map, IProjectile projectile)
        {
            Vector2Object hitPos = null;
            UpdateLine();

            if (OutOfmap(projectile, out hitPos) == true || RemoveOnMapCollision(map.MapTree, projectile, out hitPos) == true || CollisionWithMovables(map, this, out hitPos) == true)
                AfterCollision(map, hitPos);

            _wetPreviousFrame = _wet;
            GetWet(map);
        }

        protected override void AfterCollision(Map map, Vector2Object hit)
        {
            for (int i = 0; i < 3; i++)
                Game1.mapLive.mapParticles.Add(new ParticleSpark(hit.Vector2));
        }

        new public void Draw()
        {
            Game1.SpriteBatchGlobal.Draw(Game1.haloProjectil, _position, null, null, new Vector2(0, 3), CompareF.VectorToAngle(_velocity), null, Color.White, SpriteEffects.None);
        }

        new public void DrawLight()
        {
            Game1.SpriteBatchGlobal.Draw(Game1.projectileLight, _position, null, null, new Vector2(16, 24), CompareF.VectorToAngle(_velocity), null, Color.White, SpriteEffects.None);
        }
    }
}