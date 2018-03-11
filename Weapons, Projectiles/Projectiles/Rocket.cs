using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Monogame_GL
{
    public sealed class Rocket : ProjectileBase, IProjectile
    {
        private Vector2 _offset;
        private Timer _timeTrail;
        private Vector2 _oldInterpol;
        private bool _InWater;
        private Timer _bubbleTime;
        private RibbonTrail _trail;

        public Rocket(Vector2 velocity, Vector2 position, object from, short damage) : base(velocity, position, from, damage)
        {
            _oldInterpol = position;
            _offset = Vector2.Normalize(velocity) * 32;
            _timeTrail = new Timer(50, true);
            _InWater = false;
            _bubbleTime = new Timer(50, true);
            _trail = new RibbonTrail(position,_velocity, 16, 32,1, Game1.Textures["RibbonSmoke"], Game1.Textures["ribbonSmokeNormal"]/*, Game1.Textures["RibbonSmokeLight"]*/);
        }

        public void Update(Map map)
        {
            if(_wet == false)
            _trail.Update(_position,_velocity,1);
            UpdateLine();
            _bubbleTime.Update();

            _InWater = false;

            if (CompareF.LineBoundaryVsMap(map.WaterTree, _track).Count > 0)
            {
                _InWater = true;

                if (_bubbleTime.Ready == true)
                {
                    Game1.mapLive.mapParticles.Add(new ParticleBubble(_position));
                    _bubbleTime.Reset();
                }
            }

            if (_wet == true && _wet != _wetPreviousFrame)
            {
                _velocity /= 4;
                _trail.LeftParticle();
            }

            foreach (Inpc npc in map.MapNpcs)
            {
                if (npc.Friendly == false)
                {
                    List<Vector2Object> intersections = CompareF.LineIntersectionRectangle(npc, new LineObject(npc, _track.Line));

                    if ((intersections?.Count > 0 || CompareF.RectangleVsVector2(npc.Boundary, _track.Line.End) == true) && (_from is Player))
                    {
                        Damage(new Vector2Object(npc,_position),npc);
                        AfterCollision(map,new Vector2Object(npc, _position));
                        break;
                    }
                }
            }

            Vector2Object hitPos = null;

            if (OutOfmap(this, out hitPos) == true)
            {
                AfterCollision(map,new Vector2Object(Game1.mapLive, _position));
            }

            if (CollisionWithMovables(map, this, out hitPos) == true)
            {
                AfterCollision(map,hitPos);
            }

            if (RemoveOnMapCollision(map.MapTree, this, out hitPos) == true)
            {
                AfterCollision(map, hitPos);
            }

            _velocity = CompareF.RotateVector2(_velocity, (float)(Math.Sin(Game1.Time * 16) / 64f));

            _timeTrail.Update();

            if (_timeTrail.Ready == true || _wet == true)
            {
                if (_wet == false)
                {
                    List<Vector2> vectors = LineSegmentF.PointsOnLine(_position.ShiftOverDistance(32, (float)(CompareF.VectorToAngle(_velocity) + Math.PI)), _oldInterpol, 64);

                    foreach (Vector2 vector in vectors)
                    {
                        for (int i = 0; i < 3; i++)
                            Game1.mapLive.mapParticles.Add(new ParticleCircleSmoke(vector + new Vector2(Globals.GlobalRandom.Next(-8, 9), Globals.GlobalRandom.Next(-8, 9)), Game1.Textures["SmokeCircle"]));
                    }
                }

                _timeTrail.Reset();
                _oldInterpol = _position;
            }

            _wetPreviousFrame = _wet;
            GetWet(map);
        }

        protected override void AfterCollision(Map map,Vector2Object hit)
        {
            Explosion.Explode(hit.Vector2, _damage);
            if(_wet == false)
            _trail.LeftParticle();
        }

        private void Damage(Vector2Object position, Inpc npc)
        {
            npc.Stun();
            npc.KineticDamage(_damage);
            npc.Push(_velocity * 0.5f);
            Game1.mapLive.MapProjectiles.Remove(this);
        }

        new public void Draw()
        {
            Game1.SpriteBatchGlobal.Draw(Game1.projectilTex, _position, null, rotation: CompareF.VectorToAngle(_velocity), origin: new Vector2(20, 10));

            if (_InWater == false)
            {
                Effects.ColorEffect(new Vector4(1f, 1f, 1f, (float)Globals.GlobalRandom.NextDouble()));
                if (_InWater == false)
                    Game1.SpriteBatchGlobal.Draw(Game1.FlameProjectile, null, new Rectangle(_position.ShiftOverDistance(20, (float)(CompareF.VectorToAngle(_velocity) + Math.PI)).ToPoint(), new Vector2(36 + Globals.GlobalRandom.Next(-16, 17), 16).ToPoint()), null, new Vector2(0, 8), (float)(CompareF.VectorToAngle(_velocity) + Math.PI), null, Color.White, SpriteEffects.None);
                Effects.ResetEffect3D();
            }

            if(_wet == false)
            _trail.Draw();
        }

        new public void DrawNormal()
        {
            Effects.RotateNormalsEffect(CompareF.VectorToAngle(_velocity), Vector2.One);
            Game1.SpriteBatchGlobal.Draw(Game1.Textures["rocketNormal"], _position, null, rotation: CompareF.VectorToAngle(_velocity), origin: new Vector2(20, 10));
            Effects.ResetEffect3D();

            if (_wet == false)
                _trail.DrawNormal();
        }

        new public void DrawLight()
        {
            Game1.SpriteBatchGlobal.Draw(Game1.LightRocket, _position.ShiftOverDistance(16, (float)(CompareF.VectorToAngle(_velocity) + Math.PI)) - new Vector2(64));

            if (_wet == false)
                _trail.DrawLight();
        }
    }
}