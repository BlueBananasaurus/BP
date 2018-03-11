using Microsoft.Xna.Framework;

namespace Monogame_GL
{
    internal class PlasmaGun : WeaponBase, IWeapon
    {
        private float _muzzleAlpha;

        public PlasmaGun(float velOfProjectile, ushort maxAmmo, ushort ammo, object owner, float time, short damage) : base(time, maxAmmo, ammo, owner, damage)
        {
            VelocityOfProjectile = velOfProjectile;
            _muzzleAlpha = 0f;
            GunBarrel = 64;
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

            if (GunTimer.Ready == true)
            {
                Game1.soundPlasmaRifleShoot.Play((float)((1f / 2f) + Globals.GlobalRandom.NextDouble() / 2f), (float)(Globals.GlobalRandom.NextDouble() - 0.5f) / 2f, 0f);
                GunTimer.Reset();
                Game1.mapLive.MapProjectiles.Add(new PlasmaProjectile(Damage, rayEnlonged.NormalizedWithZeroSolution() * VelocityOfProjectile, barrel, Owner));
                _muzzleAlpha = 1f;
                Kick(4);
                return true;
            }
            return false;
        }

        new public void Draw(LineSegmentF rayBarrel)
        {
            SimpleGunDrawModule.DrawPlasmaGun(rayBarrel, (int)OffsetKick);

            Effects.ColorEffect(new Vector4(1f, 1f, 1f, _muzzleAlpha));
            Game1.SpriteBatchGlobal.Draw(Game1.muzzlePlasma, rayBarrel.End, scale: new Vector2(1), origin: new Vector2(0, 19f), rotation: rayBarrel.ToAngle());
            Game1.EffectBaseColor.CurrentTechnique.Passes[0].Apply();
        }

        new public void DrawLight(LineSegmentF rayBarrel)
        {
            Effects.ColorEffect(new Vector4(1f, 1f, 1f, _muzzleAlpha));
            Game1.SpriteBatchGlobal.Draw(Game1.muzzlePlasmaLight, rayBarrel.End, scale: new Vector2(1), origin: new Vector2(40, 80f), rotation: rayBarrel.ToAngle());
            Game1.EffectBaseColor.CurrentTechnique.Passes[0].Apply();
        }

        new public void DrawNormal(LineSegmentF rayBarrel)
        {
            SimpleGunDrawModule.DrawPlasmaGunNormal(rayBarrel, (int)OffsetKick);
        }
    }
}