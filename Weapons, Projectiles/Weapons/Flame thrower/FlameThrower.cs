using Microsoft.Xna.Framework;

namespace Monogame_GL
{
    public class FlameThrower : WeaponBase, IWeapon
    {
        private float _muzzleAlpha;

        public FlameThrower(float velOfProjectile, ushort maxAmmo, ushort ammo, object owner, float time, short damage) : base(time, maxAmmo, ammo, owner, damage)
        {
            VelocityOfProjectile = velOfProjectile;
            _muzzleAlpha = 0f;
            GunBarrel = 88;
            Reach = 512;
        }

        public void Update()
        {
            GunTimer.Update();

            if (_muzzleAlpha > 0)
                _muzzleAlpha -= Game1.Delta / 50;

            CompareF.DecreaseToZero(ref OffsetKick, 1f);
        }

        public bool Fire(LineSegmentF rayEnlonged, Vector2Object destination)
        {
            Vector2 barrel = rayEnlonged.Start + rayEnlonged.NormalizedWithZeroSolution() * GunBarrel;

            if (Ammo > 0 && GunTimer.Ready == true)
            {
                Game1.soundFlamb.Play((float)((1f / 2f) + Globals.GlobalRandom.NextDouble() / 2f), (float)(Globals.GlobalRandom.NextDouble() - 0.5f) / 2f, 0f);
                GunTimer.Reset();
                Game1.mapLive.MapProjectiles.Add(new FlameProjectile(rayEnlonged.NormalizedWithZeroSolution() * VelocityOfProjectile, barrel, Owner, 16));
                _muzzleAlpha = 1f;
                Ammo--;
                return true;
            }
            return false;
        }

        new public void Draw(LineSegmentF rayBarrel)
        {
            SimpleGunDrawModule.DrawFlameGun(rayBarrel, (int)OffsetKick);
        }

        new public void DrawNormal(LineSegmentF rayBarrel)
        {
            SimpleGunDrawModule.DrawFlameGunNormal(rayBarrel, (int)OffsetKick);
        }
    }
}