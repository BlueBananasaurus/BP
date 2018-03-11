using Microsoft.Xna.Framework;
using System;

namespace Monogame_GL
{
    public class RocketLauncher : WeaponBase, IWeapon
    {
        private float _muzzleAlpha;
        private Vector2 _flameOrigin;

        public RocketLauncher(float velOfProjectile, ushort maxAmmo, ushort ammo, object owner, float time, short damage) : base(time, maxAmmo, ammo, owner, damage)
        {
            VelocityOfProjectile = velOfProjectile;
            _muzzleAlpha = 0;
            _flameOrigin = Vector2.Zero;
            GunBarrel = 64;
            Reach = 512;
        }

        public void Update()
        {
            GunTimer.Update();
            if (_muzzleAlpha > 0)
                _muzzleAlpha -= Game1.Delta / 100;
            CompareF.DecreaseToZero(ref OffsetKick, 1f);
        }

        public bool Fire(LineSegmentF rayEnlonged, Vector2Object destination)
        {
            Vector2 barrel = rayEnlonged.Start + rayEnlonged.NormalizedWithZeroSolution() * GunBarrel;

            if (Ammo > 0 && GunTimer.Ready == true)
            {
                Ammo--;
                GunTimer.Reset();
                Game1.mapLive.MapProjectiles.Add(new Rocket(rayEnlonged.NormalizedWithZeroSolution() * VelocityOfProjectile, barrel, Owner, 64));
                _muzzleAlpha = 1;
                Kick(8);
                return true;
            }
            return false;
        }

        new public void Draw(LineSegmentF rayBarrel)
        {
            SimpleGunDrawModule.DrawRocketGun(rayBarrel, (int)OffsetKick);

            Effects.ColorEffect(new Vector4(1f, 1f, 1f, _muzzleAlpha));
            Game1.SpriteBatchGlobal.Draw(Game1.rocketLauncherFlame, rayBarrel.Start.ShiftOverDistance(-64 - OffsetKick, rayBarrel.ToAngle()), scale: new Vector2((2 - _muzzleAlpha * 2), 1), origin: new Vector2(0, 22), rotation: (float)(rayBarrel.ToAngle() - Math.PI));
            Game1.EffectBaseColor.CurrentTechnique.Passes[0].Apply();
        }

        new public void DrawLight(LineSegmentF rayBarrel)
        {
            Effects.ColorEffect(new Vector4(1f, 1f, 1f, _muzzleAlpha));
            Game1.SpriteBatchGlobal.Draw(Game1.rocketLauncherFlame, rayBarrel.Start.ShiftOverDistance(-64 - OffsetKick, rayBarrel.ToAngle()), scale: new Vector2((2 - _muzzleAlpha * 2), 1), origin: new Vector2(0, 22), rotation: (float)(rayBarrel.ToAngle() - Math.PI));
            Game1.EffectBaseColor.CurrentTechnique.Passes[0].Apply();
        }

        new public void DrawNormal(LineSegmentF rayBarrel)
        {
            SimpleGunDrawModule.DrawRocketGunNormal(rayBarrel, (int)OffsetKick);
        }
    }
}