using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace Monogame_GL
{
    internal class Turret : NpcBase, Inpc
    {
        private float _rotation;
        private float _velocityOfRotation;
        private float _angle;
        private Vector2 _destination;
        private Timer _timeShoot;
        private Vector2 _barrel;
        public bool Friendly { get; }
        private short _ammo;

        public Turret(Vector2 position, float health)
        {
            Boundary = new RectangleF(new Vector2(96), position);
            _rotation = 0;
            _velocityOfRotation = 0;
            _timeShoot = new Timer(200, true);
            Health = health;
            Friendly = false;
        }

        private void PlayDamageSound()
        {
            Sound.PlaySoundPosition(Boundary.Origin, Game1.soundDamage);
        }

        public override void Push(Vector2 velocity)
        {
        }

        public void Update(List<Inpc> npcs)
        {
            if (Tint < 1f)
                Tint += Game1.Delta / 100;
            if (Tint > 1f)
                Tint = 1f;

            _timeShoot.Update();

            if (Health <= 0)
            {
                Kill();
            }

            if (LineSegmentF.Lenght(Boundary.Origin, Game1.PlayerInstance.Boundary.Origin) < 800)
            {
                //-----------
                _angle = Vector2.Dot(new LineSegmentF(Boundary.Origin, Game1.PlayerInstance.Boundary.Origin).NormalizedWithZeroSolution(), CompareF.AngleToVector(_rotation));

                _velocityOfRotation = 0f;
                if (_angle > 0.01f)
                    _velocityOfRotation = -0.001f;
                if (_angle < -0.01f)
                    _velocityOfRotation = 0.001f;

                //---------

                _rotation += _velocityOfRotation * Game1.Delta;

                _destination = CompareF.AngleToVector((float)(_rotation + Math.PI / 2f));
                _barrel = Boundary.Origin + Vector2.Normalize(_destination) * (24 + 6);

                _destination = CompareF.RotateVector2(_destination, (float)(Globals.GlobalRandom.NextDouble() - 0.5f) / 4f);

                if (_timeShoot.Ready == true)
                {
                    _ammo--;
                    Game1.mapLive.MapProjectiles.Add(new Projectile(5, _destination, _barrel, this));
                    Sound.PlaySoundPosition(Boundary.Origin, Game1.sound);
                    _timeShoot.Reset();
                }
            }
        }

        new public void Kill()
        {
            Globals.AddPick(new Coin(Boundary.Origin, new Vector2(0), Pickups.electronics));
            Game1.mapLive.MapNpcs.Remove(this);
        }

        public override void Draw()
        {
            Effects.ColorEffect(new Vector4(1f, Tint, Tint, 1f));
            Game1.SpriteBatchGlobal.Draw(texture: Game1.turretBase, position: Boundary.Position);
            Game1.SpriteBatchGlobal.Draw(texture: Game1.turretHead, position: Boundary.Position + new Vector2(48), origin: new Vector2(48, 32), rotation: _rotation);
            Effects.ResetEffect3D();
        }

        public override void DrawNormal()
        {
            Game1.SpriteBatchGlobal.Draw(texture: Game1.Textures["turretBaseNormal"], position: Boundary.Position);

            Game1.EffectRotateNormals.Parameters["flip"].SetValue(new Vector2(1, 1));
            Game1.EffectRotateNormals.Parameters["angle"].SetValue(_rotation);
            Game1.EffectRotateNormals.CurrentTechnique.Passes[0].Apply();
            Game1.SpriteBatchGlobal.Draw(texture: Game1.Textures["turretHeadNormal"], position: Boundary.Position + new Vector2(48), origin: new Vector2(24*2, 16*2), rotation: _rotation);
            Effects.ResetEffect3D();
        }
    }
}