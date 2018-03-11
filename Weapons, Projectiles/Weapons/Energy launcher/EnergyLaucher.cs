using Microsoft.Xna.Framework;

namespace Monogame_GL
{
    public class EnergyLaucher : WeaponBase, IWeapon
    {
        public EnergyLaucher(float velOfProjectile, ushort maxAmmo, ushort ammo, object owner, int time, short damage) : base(time, maxAmmo, ammo, owner, damage)
        {
            VelocityOfProjectile = velOfProjectile;
            MaxAmmo = maxAmmo;
            Ammo = ammo;
            Owner = owner;
            GunTimer = new Timer(1000);
            Damage = damage;
            GunBarrel = 64;
            Reach = 512;
        }

        public void Update()
        {
            GunTimer.Update();
            CompareF.DecreaseToZero(ref OffsetKick, 1f);
        }

        public bool Fire(LineSegmentF rayEnlonged, Vector2Object destination)
        {
            Vector2 barrel = rayEnlonged.Start + rayEnlonged.NormalizedWithZeroSolution() * GunBarrel;

            if (Ammo > 0 && GunTimer.Ready == true)
            {
                Sound.PlaySoundSimple(Game1.soundVortex,(float)((1f / 2f) + Globals.GlobalRandom.NextDouble() / 2f), (float)(Globals.GlobalRandom.NextDouble() - 0.5f) / 2f, 0f);
                Ammo--;
                GunTimer.Reset();
                Game1.mapLive.MapProjectiles.Add(new EnergyBall(rayEnlonged.NormalizedWithZeroSolution() * VelocityOfProjectile, barrel, Owner, Damage));
                Kick(4);
                return true;
            }
            return false;
        }

        new public void Draw(LineSegmentF rayBarrel)
        {
            SimpleGunDrawModule.DrawEnergyGun(rayBarrel, (int)OffsetKick);
        }

        new public void DrawNormal(LineSegmentF rayBarrel)
        {
            SimpleGunDrawModule.DrawEnergyGunNormal(rayBarrel, (int)OffsetKick);
        }
    }
}