using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;
using System;

namespace Monogame_GL
{
    public class EnergyBall : ProjectileBase, IProjectile
    {
        private Timer _lightTime;
        private Timer _ballTime;
        private Timer _particleTime;
        private LineSegmentF _strikeMap;
        private List<IEffect> _effects;
        private float _size;

        public EnergyBall(Vector2 velocity, Vector2 position, object from, short damage) : base(velocity, position, from, damage)
        {
            _lightTime = new Timer(400, true);
            _ballTime = new Timer(200, true);
            _particleTime = new Timer(100, true);
            _strikeMap = new LineSegmentF();
            _effects = new List<IEffect>();
            _size = 1f;
        }

        public void Update(Map map)
        {
            _size = (float)Math.Sin(Game1.Time*32)/8 + 0.5f;
            _particleTime.Update();

            if (_ballTime.Ready)
            {
                _effects.Add(new EnerergyCircle(new CircleF(_position, 16), 16, _velocity));

                _ballTime.Reset();
            }

            List<Vector2> trackVectors = LineSegmentF.PointsOnLine(_position, _oldPosition, 64);

            if (_particleTime.Ready == true)
            {
                foreach (Vector2 vector in trackVectors)
                {
                    Game1.mapLive.mapParticles.Add(new ParticleBlackDrop(vector, _velocity, Game1.Textures["BlackParticle"]));
                }
                _particleTime.Reset();
            }

            UpdateLine();
            _lightTime.Update();
            _ballTime.Update();
            GetWet(map);

            int j = 0;

            foreach (Inpc npc in map.MapNpcs.Reverse<Inpc>())
            {
                if (j < 8)
                {
                    if (npc.Friendly == false)
                    {
                        List<Vector2Object> intersections = CompareF.LineIntersectionRectangle(npc, new LineObject(npc, _track.Line));

                        if (intersections != null && intersections.Count > 0)
                        {
                            Damage(npc, this);
                            break;
                        }
                        else if (CompareF.RectangleVsVector2(npc.Boundary, _track.Line.End) == true)
                        {
                            Damage(npc, this);
                            break;
                        }

                        if (_lightTime.Ready)
                        {
                            if (LightningAttack.StrikeLightning(_position, npc, _damage) == true)
                            {
                                j++;
                            }
                            _lightTime.Reset();
                        }
                    }
                }
            }
            Vector2Object hitPos = null;

            if (OutOfmap(this,out hitPos) == true)
            {
                AfterCollision(map, hitPos);
            }

            if (RemoveOnMapCollision(map.MapTree, this, out hitPos) == true)
            {
                AfterCollision(map, hitPos);
            }

            if (CollisionWithMovables(map, this, out hitPos) == true)
            {
                AfterCollision(map,hitPos);
            }

            if(_wet == true)
            {
                Game1.mapLive.MapProjectiles.Remove(this);
            }

            foreach (IEffect effect in _effects.Reverse<IEffect>())
            {
                effect.Update(_effects);
            }
        }

        protected override void AfterCollision(Map map, Vector2Object hit)
        {
            foreach (Inpc npc in map.MapNpcs)
            {
                LightningAttack.StrikeLightning(_position, npc, _damage);
            }

            for(int i = 0; i < 8; i++)
            {
                Game1.mapLive.mapParticles.Add(new ParticleBlackDrop(hit.Vector2, _velocity, Game1.Textures["BlackParticle"]));
            }
        }

        override public void Draw()
        {
            foreach(IEffect effect in _effects)
            {
                effect.Draw();
            }

            Game1.SpriteBatchGlobal.Draw(Game1.LightTex, _position, scale: new Vector2(_size), origin: new Vector2(48));
        }

        override public void DrawLight()
        {
            foreach (IEffect effect in _effects)
            {
                effect.DrawLight();
            }
        }
    }
}