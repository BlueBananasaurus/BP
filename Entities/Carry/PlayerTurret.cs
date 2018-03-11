using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace Monogame_GL
{
    public class PlayerTurret : CarryBase, Inpc, Icarry, IUI
    {
        private float _angle;
        private float _rotVelocity;
        private float _rotation;
        private Vector2 _destination;
        private Timer _time;
        private Inpc _target;
        private short _ammo;
        private Timer _bubbleTime;
        private byte _kick;
        private float _muzzleAlpha;

        public Vector2 HeadPos { get { return Boundary.Origin - new Vector2(0, 24); } }

        public Vector2 Barrel
        {
            get
            {
                return HeadPos + Vector2.Normalize(_destination) * (24);
            }
        }

        public PlayerTurret(Vector2 position) : base()
        {
            Boundary = new RectangleF(new Vector2(36, 64), position);
            _resolver = new CollisionResolver(Globals.TileSize);
            _weight = 1f;
            _time = new Timer(100, true);
            _target = null;
            _rotation = (float)Math.PI / 2f;
            _ammo = 0;
            _bubbleTime = new Timer(300, true);
            _kick = 0;

            if (_target == null || LineSegmentF.Lenght(HeadPos, _target.Boundary.Origin) > 512 || CompareF.IntersectionLineWithOthers(new LineObject(Game1.PlayerInstance, new LineSegmentF(HeadPos, _target.Boundary.Origin)), Game1.mapLive.TileMapLines, Game1.mapLive) != null)
            {
                foreach (Inpc npc in Game1.mapLive.MapNpcs)
                {
                    if (npc.Friendly == false && !(npc is IUnkillable) && LineSegmentF.Lenght(HeadPos, npc.Boundary.Origin) <= 512 && CompareF.IntersectionLineWithOthers(new LineObject(Game1.PlayerInstance, new LineSegmentF(HeadPos, npc.Boundary.Origin)), Game1.mapLive.TileMapLines, Game1.mapLive) == null)
                    {
                        _target = npc;
                        break;
                    }
                }
            }
        }

        public void Update(List<Inpc> npcs)
        {
            _time.Update();
            _bubbleTime.Update();

            if (_muzzleAlpha > 0)
                _muzzleAlpha -= Game1.Delta / 50;

            if (_kick > 0)
                _kick--;

            if (_resolver.InWater == true)
            {
                if (_bubbleTime.Ready == true)
                {
                    Game1.mapLive.mapParticles.Add(new ParticleBubble(HeadPos));
                    _bubbleTime.Reset();
                }
            }

            if (_target != null && _ammo > 0)
            {
                _angle = Vector2.Dot(new LineSegmentF(HeadPos, _target.Boundary.Origin).NormalizedWithZeroSolution(), CompareF.AngleToVector(_rotation));

                _rotVelocity = 0f;

                if (_angle > 0.06f)
                    _rotVelocity = -(float)Math.PI / 720;
                if (_angle < -0.06f)
                    _rotVelocity = (float)Math.PI / 720;

                //---------

                _rotation += _rotVelocity * Game1.Delta;

                _destination = CompareF.AngleToVector((float)(_rotation + Math.PI / 2f));
            }
            else
            {
                _angle = Vector2.Dot(CompareF.AngleToVector(_rotation), CompareF.AngleToVector((float)Math.PI));

                _rotVelocity = 0f;

                if (_angle > 0.02f)
                    _rotVelocity = -(float)Math.PI / 720;
                if (_angle < -0.02f)
                    _rotVelocity = (float)Math.PI / 720;

                _rotation += _rotVelocity * Game1.Delta;

                _destination = CompareF.AngleToVector((float)(_rotation + Math.PI / 2f));
            }

            if (_time.Ready == true && _target != null && _ammo > 0 && Math.Abs(ExtensionMethods.AngleDifference(new LineSegmentF(HeadPos, _target.Boundary.Origin).ToAngle(), _rotation)) < 25f)
            {
                if (Locked == false)
                {
                    _ammo--;
                }
                Game1.mapLive.MapProjectiles.Add(new Projectile(5, _destination, Barrel, Game1.PlayerInstance));
                _kick = 4;
                _muzzleAlpha = 1f;
                Sound.PlaySoundPosition(HeadPos, Game1.sound);
                _time.Reset();
            }

            if (Game1.mapLive.MapNpcs.Contains(_target) == false)
            {
                _target = null;
            }

            Pressure(this);

            _target = null;

            foreach (Inpc npc in Game1.mapLive.MapNpcs)
            {
                if (npc.Friendly == false && !(npc is IUnkillable) && LineSegmentF.Lenght(HeadPos, npc.Boundary.Origin) <= 512 && CompareF.WeaponRayObstruction(Boundary, new LineSegmentF(HeadPos, npc.Boundary.Origin), this).Object == null)
                {
                    _target = npc;
                    break;
                }
            }

            _resolver.move(ref _velocity, new Vector2(2f), Boundary, 0f, new Vector2(0.2f), new Vector2(0.02f), new Vector2(0.3f), Game1.mapLive.MapMovables);
        }

        public override void Draw()
        {
            DrawEntities.DrawPlayerTurret(Boundary, _rotation, _kick);

            Effects.ColorEffect(new Vector4(1f, 1f, 1f, _muzzleAlpha));
            Game1.SpriteBatchGlobal.Draw(Game1.muzzleSimple, Barrel, scale: new Vector2(1), origin: new Vector2(0, 19f), rotation: _rotation);
            Game1.EffectBaseColor.CurrentTechnique.Passes[0].Apply();
        }

        public override void DrawLight()
        {
            Effects.ColorEffect(new Vector4(1f, 1f, 1f, _muzzleAlpha));
            Game1.SpriteBatchGlobal.Draw(Game1.muzzleSimpleLight, Barrel, scale: new Vector2(1), origin: new Vector2(20, 80), rotation: _rotation);
            Game1.EffectBaseColor.CurrentTechnique.Passes[0].Apply();
        }

        public override void DrawNormal()
        {
            DrawEntities.DrawPlayerTurretNormal(Boundary, _rotation, _kick);
        }

        public void DrawUI()
        {
            DrawNumber.Draw_digits(Game1.numbersMedium, _ammo, new Vector2((int)Boundary.Origin.X, (int)(Boundary.Origin.Y)) - new Vector2(0, 72), Align.center, new Point(15, 18));
        }

        override public void Carry(Vector2 center)
        {
            _ammo = 100;
            base.Carry(center);
        }
    }
}