using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Monogame_GL
{
    public abstract class ProjectileBase
    {
        protected Vector2 _velocity;
        protected Vector2 _position;
        protected Vector2 _oldPosition;
        protected LineObject _track;
        protected bool _wet;
        protected short _damage;
        protected object _from;
        protected bool _wetPreviousFrame;

        public ProjectileBase(Vector2 velocity, Vector2 position, object from, short damage)
        {
            _wet = false;
            _position = position;
            _oldPosition = position;
            _track = new LineObject(from, new LineSegmentF(_position, _position));
            _velocity = velocity;
            _from = from;
            _damage = damage;
        }

        public virtual void Draw()
        {
        }

        public virtual void DrawNormal()
        {
        }

        public virtual void DrawLight()
        {
        }

        protected virtual void Update(Map map, IProjectile projectile)
        {
            Vector2Object hitPos = null;
            UpdateLine();
            OutOfmap(projectile, out hitPos);
            RemoveOnMapCollision(map.MapTree, projectile,out hitPos);
            GetWet(map);
        }

        protected virtual void PlayerTakeDamage(ushort amount, IProjectile projectile)
        {
            List<Vector2Object> intersectionsPlayer = CompareF.LineIntersectionRectangle(Game1.PlayerInstance.Boundary, new LineObject(Game1.PlayerInstance, _track.Line));

            if (intersectionsPlayer?.Count > 0 && (_from is Inpc))
            {
                Game1.PlayerInstance.TakeDamage(amount);
                Game1.mapLive.MapProjectiles.Remove(projectile);
            }
        }

        protected virtual bool CollisionWithMovables(Map map, IProjectile projectile, out Vector2Object hit)
        {
            foreach (IRectanglePhysics rec in map.MapMovables)
            {
                List<Vector2Object> intersections = CompareF.LineIntersectionRectangle(rec, new LineObject(rec, _track.Line));

                if (intersections?.Count > 0)
                {
                    Game1.mapLive.MapProjectiles.Remove(projectile);
                    hit = CompareF.NearestVector(_track.Line.Start, intersections);
                    return true;
                }

                if (CompareF.RectangleVsVector2(rec, _track.Line.End) == true)
                {
                    Game1.mapLive.MapProjectiles.Remove(projectile);
                    hit = new Vector2Object(rec,_track.Line.End);
                    return true;
                }

            }
            hit = null;
            return false;
        }

        protected virtual void DamageToNPC(Map map,IProjectile projectile)
        {
            foreach (Inpc npc in map.MapNpcs)
            {
                if (npc.Friendly == false)
                {
                    List<Vector2Object>  intersections = CompareF.LineIntersectionRectangle(npc, new LineObject(npc, _track.Line));

                    if ((intersections?.Count > 0 || CompareF.RectangleVsVector2(npc.Boundary, _track.Line.End) == true) && (_from is Player))
                    {
                        Damage(npc, projectile);
                        break;
                    }
                }
            }
        }

        protected abstract void AfterCollision(Map map,Vector2Object hit);

        protected void GetWet(Map map)
        {
            if (_wet == false)
            {
                if (CompareF.LineVsMap(map.WaterTree, _track).Count > 0)
                {
                    _wet = true;
                }
            }
        }

        protected virtual void Damage(Inpc npc, IProjectile projectile)
        {
            npc.Stun();
            npc.KineticDamage(_damage);
            npc.Push(_velocity * 0.1f);
            Game1.mapLive.MapProjectiles.Remove(projectile);
        }

        protected void UpdateLine()
        {
            _oldPosition = _position;
            _position += _velocity * Game1.Delta;
            _track.Line.Start = _oldPosition;
            _track.Line.End = _position;
        }

        protected bool OutOfmap(IProjectile projectile, out Vector2Object hitPos)
        {
            hitPos = null;

            if (CompareF.LineIntersectionRectangle(Game1.mapLive.MapBoundary, _track).Count > 0)
            {
                hitPos = CompareF.NearestVector(_track.Line.Start, CompareF.LineIntersectionRectangle(Game1.mapLive.MapBoundary, _track));
                Game1.mapLive.MapProjectiles.Remove(projectile);
                return true;
            }
            return false;
        }

        protected bool RemoveOnMapCollision(MapTreeHolder map, IProjectile projectile, out Vector2Object hit)
        {
            hit = null;

            if (CompareF.LineVsMap(map, _track).Count > 0)
            {
                hit = CompareF.NearestVector(_track.Line.Start, CompareF.LineVsMap(map, _track));
                Game1.mapLive.MapProjectiles.Remove(projectile);
                return true;
            }
            return false;
        }
    }
}