using Microsoft.Xna.Framework;
using System.Runtime.Serialization;

namespace Monogame_GL
{
    public abstract class WeaponBase
    {
        public ushort MaxAmmo { get; set; }
        public ushort Ammo { get; set; }
        public object Owner { get; set; }
        public float Time { get; set; }
        public Timer GunTimer { get; set; }
        public float GunBarrel { get; set; }
        protected float OffsetKick;
        public short Damage { get; set; }
        public short DamagePlus { get; set; }
        public int Reach { get; set; }
        public float Dispersion { get; set; }
        public float VelocityOfProjectile { get; set; }
        public float DispersionPlus { get; set; }
        public short VelocityOfProjectilePlus { get; set; }

        public WeaponBase(float time, ushort maxAmmo, ushort ammo, object owner, short damage)
        {
            OffsetKick = 0;
            GunTimer = new Timer(time, true);
            MaxAmmo = maxAmmo;
            Ammo = ammo;
            Owner = owner;
            Time = time;
            Damage = damage;
        }

        [OnDeserialized]
        private void OnDeserializedMethod(StreamingContext context)
        {
            GunTimer = new Timer(Time, true);
        }

        public Vector2 BarrelVector2(LineSegmentF ray, int barrelLenght)
        {
            return ray.Start + ray.NormalizedWithZeroSolution() * barrelLenght;
        }

        public void AddAmmo(ushort amount)
        {
            Ammo += amount;

            if (Ammo > MaxAmmo)
            {
                Ammo = MaxAmmo;
            }
        }

        public void Kick(byte amount)
        {
            OffsetKick = amount;
        }

        public void SetOff()
        {
        }

        public void SetOn()
        {
        }

        public void Draw(LineSegmentF rayBarrel)
        {
        }

        public void DrawNormal(LineSegmentF rayBarrel)
        {
        }

        public void DrawLight(LineSegmentF rayBarrel)
        {
        }
    }
}