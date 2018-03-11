using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Monogame_GL
{
    public class FlameProjectile : ProjectileBase, IProjectile
    {
        private Timer _hurtTimer;
        private Timer _trailTimer;
        private Vector2 _oldInterpol;
        private int i = 0;
        private int _maximums = 8;
        private RibbonTrail _trail;

        public FlameProjectile(Vector2 velocity, Vector2 position, object from, short damage) : base(velocity, position, from, damage)
        {
            _hurtTimer = new Timer(25, true);
            _trailTimer = new Timer(150, true);
            _oldInterpol = _oldPosition;
            _trail = new RibbonTrail(position, _velocity, 16, 32, 1, Game1.Textures["RibbonFire"], null, Game1.Textures["RibbonFire"]);
        }

        public void Update(Map map)
        {
            UpdateLine();

            _hurtTimer.Update();

            List<Vector2Object> intersectionsPlayer = CompareF.LineIntersectionRectangle(Game1.PlayerInstance.Boundary, new LineObject(Game1.PlayerInstance, _track.Line));

            if (intersectionsPlayer?.Count > 0 && (_from is Inpc))
            {
                Game1.PlayerInstance.TakeDamage(5);
            }

            foreach (Inpc npc in map.MapNpcs)
            {
                if (npc.Friendly == false)
                {
                    List<Vector2Object> intersections = CompareF.LineIntersectionRectangle(npc, new LineObject(npc, _track.Line));

                    if ((intersections != null && intersections.Count > 0 && (_from is Player)) || CompareF.RectangleVsVector2(npc.Boundary, _track.Line.End) == true)
                    {
                        if (_hurtTimer.Ready == true)
                        {
                            npc.Stun();
                            npc.KineticDamage(_damage);
                            npc.Push(_velocity * 0.1f);
                            _hurtTimer.Reset();
                            i++;
                        }
                        break;
                    }

                    if (i > 4)
                    {
                        Game1.mapLive.MapProjectiles.Remove(this);
                    }
                }
            }

            Vector2Object hitPos = null;

            if (OutOfmap(this,out hitPos) == true)
            { BurstParticles(hitPos.Vector2); }

            if (RemoveOnMapCollision(map.MapTree, this, out hitPos) == true)
            { BurstParticles(hitPos.Vector2); }

            if (CollisionWithMovables(map, this, out hitPos) == true)
            { BurstParticles(hitPos.Vector2); }

            GetWet(map);
            if (_wet)
            {
                Game1.mapLive.MapProjectiles.Remove(this);
            }

            _velocity.Y += 0.03f;

            _trailTimer.Update();

            if (_trailTimer.Ready == true)
            {
                List<Vector2> vectors = LineSegmentF.PointsOnLine(_position, _oldInterpol, 64);

                Vector2 random = new Vector2(Globals.GlobalRandom.Next(-_maximums, _maximums + 1), Globals.GlobalRandom.Next(-_maximums, _maximums + 1));

                foreach (Vector2 vector in vectors)
                {
                    for(int i =0;i<3;i++)
                    Game1.mapLive.mapParticles.Add(new ParticleCircleFlame(vector + new Vector2(Globals.GlobalRandom.Next(-16,17), Globals.GlobalRandom.Next(-16, 17)), Game1.Textures["FlameCircle"]));
                }

                _trailTimer.Reset();

                _oldInterpol = _position + random;
            }

            _trail.Update(_position, _velocity,1);
        }

        public void BurstParticles(Vector2 position)
        {
            for (int i = 0; i < 3; i++)
                Game1.mapLive.mapParticles.Add(new ParticleFlameDropBig(position, Game1.Textures["FlameDropBig"]));
        }

        protected override void AfterCollision(Map map, Vector2Object hit)
        {

        }

        public override void Draw()
        {
            _trail.Draw();
        }

        public override void DrawLight()
        {
            _trail.DrawLight();
        }
    }
}