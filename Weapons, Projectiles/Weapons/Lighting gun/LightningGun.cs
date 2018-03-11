using Microsoft.Xna.Framework;
using System.Diagnostics;

namespace Monogame_GL
{
    internal class LightningGun : IWeapon
    {
        private IWeapon _module;
        private float OffsetKick;

        public ushort MaxAmmo { get { return _module.MaxAmmo; } set { _module.MaxAmmo = value; } }
        public ushort Ammo { get { return _module.Ammo; } set { _module.Ammo = value; } }
        public Timer GunTimer { get { return _module.GunTimer; } set { _module.GunTimer = value; } }
        public float GunBarrel { get { return _module.GunBarrel; } set { _module.GunBarrel = value; } }
        public int Reach { get { return _module.Reach; } set { _module.Reach = value; } }
        public float VelocityOfProjectile { get { return _module.VelocityOfProjectile; } set { _module.VelocityOfProjectile = value; } }
        public float Dispersion { get { return _module.Dispersion; } set { _module.Dispersion = value; } }
        public short Damage { get { return _module.Damage; } set { _module.Damage = value; } }

        public short VelocityOfProjectilePlus { get; set; }
        public short DamagePlus { get; set; }
        public float DispersionPlus { get; set; }

        public LightningGun(float velocityAdd, ushort maxAmmo, ushort ammo, object owner, int time, short damage)
        {
            _module = new BasicLighting(this, velocityAdd, maxAmmo, ammo, owner, time, damage);

            DispersionPlus = 0;
            DamagePlus = 0;
            VelocityOfProjectilePlus = 0;
        }

        public void Update()
        {
            GunTimer.Update();
            _module.Update();
            CompareF.DecreaseToZero(ref OffsetKick, 1f);
        }

        public bool Fire(LineSegmentF rayEnlonged, Vector2Object destination)
        {
            if (_module.Fire(rayEnlonged, destination) == true)
            {
                return true;
            }
            return false;
        }

        public void Draw(LineSegmentF raybarrel)
        {
            SimpleGunDrawModule.DrawLightningGun(raybarrel, (int)OffsetKick);
        }

        public void DrawNormal(LineSegmentF rayBarrel)
        {
            SimpleGunDrawModule.DrawLightningGunNormal(rayBarrel, (int)OffsetKick);
        }

        public void SetOff()
        {
            _module.SetOff();
        }

        public void SetOn()
        {
            _module.SetOn();
        }

        public void DrawLight(LineSegmentF rayBarrel)
        {

        }
    }
}