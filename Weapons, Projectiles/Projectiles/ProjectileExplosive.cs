using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Monogame_GL
{
    public class ProjectileExplosive : ProjectileBase, IProjectile
    {
        public ProjectileExplosive(short damage, Vector2 velocity, Vector2 position, object from) : base(velocity, position, from, damage){}

        public void Update(Map map)
        {
            base.Update(map, this);

            foreach (Inpc npc in map.MapNpcs)
            {
                if (npc.Friendly == false)
                {
                    List<Vector2Object> intersections = CompareF.LineIntersectionRectangle(npc, new LineObject(npc, _track.Line));

                    if ((intersections?.Count > 0 || CompareF.RectangleVsVector2(npc.Boundary, _track.Line.End) == true) && (_from is Player))
                    {
                        Damage(npc);
                        AfterCollision(map, new Vector2Object(npc, _position));
                        map.MapProjectiles.Remove(this);
                        break;
                    }
                }
            }

            Vector2Object hitPos = null;

            if (OutOfmap(this, out hitPos) == true) { }

            if (RemoveOnMapCollision(map.MapTree, this, out hitPos) == true)
                AfterCollision(map, hitPos);

            if (CollisionWithMovables(map, this, out hitPos) == true)
                AfterCollision(map, hitPos);

            if (_wet == true) _velocity /= 4;
        }

        protected override void AfterCollision(Map map, Vector2Object hit)
        {
            Explosion.ExplodeSmall(_position, _damage);
        }

        private void Damage(Inpc npc)
        {
            npc.Stun();
            npc.KineticDamage(_damage);
            npc.Push(_velocity * 0.05f);
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