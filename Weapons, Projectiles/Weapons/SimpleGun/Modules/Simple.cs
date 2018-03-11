using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Monogame_GL
{
    public class Simple : WeaponBase, IWeapon
    {
        public float MuzzleAlpha { get; set; }
        public IWeapon OwnerGun { get; set; }

        public Simple(IWeapon ownerGun,float velOfProjectile, ushort maxAmmo, ushort ammo, object owner, float time, short damage) : base(time, maxAmmo, ammo, owner, damage)
        {
            VelocityOfProjectile = velOfProjectile;
            MuzzleAlpha = 0f;
            GunBarrel = 64;
            Reach = 512;
            Dispersion = (float)Math.PI / 6;
            OwnerGun = ownerGun;
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
                Sound.PlaySoundSimple(Game1.sound,1f, (float)(Globals.GlobalRandom.NextDouble() - 0.5f) / 2f, 0f);
                GunTimer.Reset();
                Game1.mapLive.MapProjectiles.Add(new Projectile((short)(Damage + OwnerGun.DamagePlus), CompareF.RotateVector2(rayEnlonged.NormalizedWithZeroSolution(), (float)((Globals.GlobalRandom.NextDouble() - 0.5f) * (Dispersion + OwnerGun.DispersionPlus))) * (VelocityOfProjectile+OwnerGun.VelocityOfProjectile), barrel, Owner));
                return true;
            }
            return false;
        }
    }
}