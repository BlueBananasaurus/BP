namespace Monogame_GL
{
    public interface IWeapon
    {
        ushort MaxAmmo { get; set; }
        ushort Ammo { get; set; }
        Timer GunTimer { get; set; }
        float GunBarrel { get; set; }
        int Reach { get; set; }
        float VelocityOfProjectile { get; set; }
        float Dispersion { get; set; }
        short Damage { get; set; }
        short DamagePlus { get; set; }
        float DispersionPlus { get; set; }
        short VelocityOfProjectilePlus { get; set; }

        bool Fire(LineSegmentF segment, Vector2Object vector);

        void Update();

        void SetOff();

        void SetOn();

        void Draw(LineSegmentF rayBarrel);

        void DrawNormal(LineSegmentF rayBarrel);

        void DrawLight(LineSegmentF rayBarrel);
    }
}