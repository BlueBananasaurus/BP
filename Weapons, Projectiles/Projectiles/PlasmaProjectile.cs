using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Monogame_GL
{
    public class PlasmaProjectile : ProjectileBase, IProjectile
    {
        private Vector2 _offset;

        public PlasmaProjectile(short damage, Vector2 velocity, Vector2 position, object from) : base(velocity, position, from, damage)
        {
            _offset = velocity.NormalizeCustom() * 24f;
        }

        public void Update(Map map)
        {
            Update(map, this);
            PlayerTakeDamage(5, this);
            DamageToNPC(map, this);

            Vector2Object hitPos = null;

            if (OutOfmap(this, out hitPos) == true) { }

            if (RemoveOnMapCollision(map.MapTree, this, out hitPos) == true) { }

            if (CollisionWithMovables(map, this, out hitPos) == true) { }


            if (_wet == true)
            {
                Game1.mapLive.MapProjectiles.Remove(this);
            }
            else
            {
                if (CompareF.LineVsMap(map.WaterTree, _track).Count > 0)
                {
                    _velocity /= 4;
                }
            }
        }

        protected override void AfterCollision(Map map, Vector2Object hit)
        {

        }

        new public void Draw()
        {
            Game1.SpriteBatchGlobal.Draw(Game1.plasma, _position, null, null, new Vector2(24));
        }

        new public void DrawLight()
        {
            Game1.SpriteBatchGlobal.Draw(Game1.plasmaLight, _position, null, null, new Vector2(24));
        }
    }
}