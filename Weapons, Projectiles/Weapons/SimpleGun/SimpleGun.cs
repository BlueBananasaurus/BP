using Microsoft.Xna.Framework;

namespace Monogame_GL
{
    public class SimpleGun : IWeapon
    {
        private IWeapon _module;

        public ushort MaxAmmo { get { return _module.MaxAmmo; }  set { _module.MaxAmmo = value; } }
        public ushort Ammo { get { return _module.Ammo; }  set { _module.Ammo = value; } }
        public Timer GunTimer { get { return _module.GunTimer; }  set { _module.GunTimer = value; } }
        public float GunBarrel { get { return _module.GunBarrel; }  set { _module.GunBarrel = value; } }
        public int Reach { get { return _module.Reach; }  set { _module.Reach = value; } }
        public float VelocityOfProjectile { get { return _module.VelocityOfProjectile; } set { _module.VelocityOfProjectile = value; } }
        public float Dispersion { get { return _module.Dispersion; } set { _module.Dispersion = value; } }
        public short Damage { get { return _module.Damage; } set { _module.Damage = value; } }

        public short VelocityOfProjectilePlus { get; set; }
        public short DamagePlus { get; set; }
        public float DispersionPlus { get; set; }
        public float Kick;
        public float Light;

        public SimpleGun(float velOfProjectile, ushort maxAmmo, ushort ammo, object owner, float time, short damage)
        {
            _module = new Simple(this,velOfProjectile, maxAmmo, ammo, owner, time, damage);
            DispersionPlus = 0;
            DamagePlus = 0;
            VelocityOfProjectilePlus = 0;
        }

        public void Update()
        {
            _module.Update();
            CompareF.DecreaseToZero(ref Kick, Game1.Delta/16);
            CompareF.DecreaseToZero(ref Light, Game1.Delta / 128);
        }

        public bool Fire(LineSegmentF rayEnlonged, Vector2Object destination)
        {
            if(_module.Fire(rayEnlonged, destination) == true)
            {
                Kick = 8f;
                Light = 1f;
                return true;
            }
            return false;
        }

        public void Draw(LineSegmentF rayBarrel)
        {
            SimpleGunDrawModule.DrawSimpleGun(rayBarrel, (int)Kick);

            Effects.ColorEffect(new Vector4(1f, 1f, 1f, Light));
            Game1.SpriteBatchGlobal.Draw(Game1.muzzleSimple, rayBarrel.End, origin: new Vector2(0, 19f), rotation: rayBarrel.ToAngle());
            Effects.ResetEffect3D();
        }

        public void DrawNormal(LineSegmentF rayBarrel)
        {
            SimpleGunDrawModule.DrawSimpleGunNormal(rayBarrel, (int)Kick);
            Effects.ResetEffect3D();
        }

        public void DrawLight(LineSegmentF rayBarrel)
        {
            Effects.ColorEffect(new Vector4(1f, 1f, 1f, Light));
            Game1.SpriteBatchGlobal.Draw(Game1.muzzleSimpleLight, rayBarrel.End, origin: new Vector2(40, 80f), rotation: rayBarrel.ToAngle());
            Effects.ResetEffect3D();
        }

        public void SetOff()
        {
            _module.SetOff();
        }

        public void SetOn()
        {
            _module.SetOn();
        }
    }
}