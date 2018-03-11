using Microsoft.Xna.Framework;

namespace Monogame_GL
{
    public class Shotgun : WeaponBase, IWeapon
    {
        public float MuzzleAlpha { get; set; }

        public Shotgun(float velOfProjectile, ushort maxAmmo, ushort ammo, object owner, float time, short damage) : base(time*4, maxAmmo, ammo, owner, (short)(damage/2))
        {
            VelocityOfProjectile = velOfProjectile;
            MuzzleAlpha = 0f;
            GunBarrel = 64;
            Reach = 512;
        }

        public void Update()
        {
            GunTimer.Update();
        }

        public bool Fire(LineSegmentF rayEnlonged, Vector2Object destination)
        {
            Vector2 barrel = rayEnlonged.Start + rayEnlonged.NormalizedWithZeroSolution() * GunBarrel;

            if (GunTimer.Ready == true)
            {
                Game1.sound.Play(1f, (float)(Globals.GlobalRandom.NextDouble() - 0.5f) / 2f, 0f);
                GunTimer.Reset();
                for (int i = 0; i < 8; i++)
                {
                    Game1.mapLive.MapProjectiles.Add(new Projectile(Damage, CompareF.RotateVector2(rayEnlonged.NormalizedWithZeroSolution(), (float)(Globals.GlobalRandom.NextDouble() - 0.5f) / 4f) * VelocityOfProjectile  * (float)((Globals.GlobalRandom.NextDouble() / 4f)+0.75f), barrel, Owner));
                }
                return true;
            }
            return false;
        }
    }
}