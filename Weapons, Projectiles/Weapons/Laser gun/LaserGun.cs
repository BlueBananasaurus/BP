using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Monogame_GL
{
    internal class LaserGun : WeaponBase, IWeapon
    {
        private float _velocityAdd;
        private LineSegmentF _line;
        public bool _IsOn { get; private set; }
        private float _rotation;
        private float _timeMultiplier;

        public LaserGun(float velocityAdd, ushort maxAmmo, ushort ammo, object owner, int time, short damage) : base(time, maxAmmo, ammo, owner, damage)
        {
            _velocityAdd = velocityAdd;
            _IsOn = false;
            _timeMultiplier = 24;
            GunBarrel = 56;
            Reach = 1024;
            VelocityOfProjectile = 0;
        }

        public new void SetOff()
        {
            _IsOn = false;
        }

        public new void SetOn()
        {
            _IsOn = true;
        }

        public void Update()
        {
            GunTimer.Update();
            CompareF.DecreaseToZero(ref OffsetKick, 1f);
        }

        public bool Fire(LineSegmentF rayEnlonged, Vector2Object destination)
        {
            Vector2 barrel = rayEnlonged.Start + rayEnlonged.NormalizedWithZeroSolution() * GunBarrel;

            if (destination != null && destination != null)
            {
                if (Owner is Player)
                    _line = new LineSegmentF(rayEnlonged.Start, destination.Vector2);
                if (Owner is Inpc)
                    _line = new LineSegmentF((Owner as Inpc).Boundary.Origin, destination.Vector2);
            }

            _rotation = rayEnlonged.ToAngle();

            if ((Ammo > 0 && GunTimer.Ready == true) || MaxAmmo == 0)
            {
                Ammo--;
                SetOn();
                fireRay(barrel, destination);

                GunTimer.Reset();

                if (destination != null && destination != null)
                {
                    if (Owner is Player)
                    {
                    }
                    if (Owner is Inpc)
                    {
                        //Game1.PlayerIns.Push(LineSegmentF.SegmentToNormalizedVector((Owner as Enemy).Ray) * 0.02f);
                    }
                }

                return true;
            }

            return false;
        }

        private bool fireRay(Vector2 rayStart, Vector2Object RayDestination)
        {
            if (RayDestination != null && RayDestination.Vector2 != null)
            {
                if (RayDestination.Object != null && RayDestination.Object is Inpc && (RayDestination.Object as Inpc).Friendly == false)
                {
                    (RayDestination.Object as Inpc).Push(_line.NormalizedWithZeroSolution() * _velocityAdd * 0.1f);
                    (RayDestination.Object as Inpc).Stun();
                    (RayDestination.Object as Inpc).KineticDamage(Damage);
                }
                if (RayDestination.Object != null && (RayDestination.Object is Map || RayDestination.Object is Player))
                {
                    Game1.mapLive.mapParticles.Add(new ParticleLaserDest(RayDestination.Vector2));
                }
                return true;
            }
            return false;
        }

        new public void Draw(LineSegmentF rayBarrel)
        {
            if (Ammo > 0)
            {
                if (_IsOn)
                {
                    if (_line != null)
                    {
                        Game1.SpriteBatchGlobal.Draw(Game1.debug_longTex, rayBarrel.End, null,
                            new Rectangle(0, 0, (int)(_line.Lenght().Value - GunBarrel), 8),
                            new Vector2(0, 4),
                            _rotation, new Vector2(1, (float)(Math.Sin(Game1.Time * _timeMultiplier) * 0.5f) + 1), Color.Red, SpriteEffects.None);

                        Game1.SpriteBatchGlobal.Draw(Game1.laserDest, _line.End, origin: new Vector2(64), scale: new Vector2((float)(Math.Sin(Game1.Time * _timeMultiplier) * 0.2f) + 0.5f), color: Color.White);
                    }
                }
            }

            SimpleGunDrawModule.DrawLaserGun(rayBarrel, (int)OffsetKick);
        }

        new public void DrawNormal(LineSegmentF rayBarrel)
        {
           SimpleGunDrawModule.DrawLaserGunNormal(rayBarrel, (int)OffsetKick);
        }

        new public void DrawLight(LineSegmentF rayBarrel)
        {
            if (Ammo > 0)
            {
                if (_IsOn)
                {
                    if (_line != null)
                    {
                        Game1.SpriteBatchGlobal.Draw(Game1.LaserLight, rayBarrel.End, null,
                            new Rectangle(0, 0, (int)(_line.Lenght().Value - GunBarrel), 48),
                            new Vector2(0, 24),
                            _rotation, new Vector2(1, (float)(Math.Sin(Game1.Time * _timeMultiplier) * 0.5f) + 1), Color.White, SpriteEffects.None);

                        Game1.SpriteBatchGlobal.Draw(Game1.laserDestHalo, _line.End, origin: new Vector2(32), scale: new Vector2((float)(Math.Sin(Game1.Time * _timeMultiplier) * 1.2f) + 2f), color: Color.White);
                    }
                }
            }
        }
    }
}