using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Monogame_GL
{
    public class BasicLighting : WeaponBase, IWeapon
    {
        public float MuzzleAlpha { get; set; }
        public IWeapon OwnerGun { get; set; }
        private LineSegmentF _line;
        private float _velocityAdd;

        public BasicLighting(IWeapon ownerGun, float velOfProjectile, ushort maxAmmo, ushort ammo, object owner, float time, short damage) : base(time, maxAmmo, ammo, owner, damage)
        {
            VelocityOfProjectile = velOfProjectile;
            MuzzleAlpha = 0f;
            GunBarrel = 64;
            Reach = 512;
            Dispersion = (float)Math.PI / 6;
            OwnerGun = ownerGun;
            _velocityAdd = 2f;
            GunTimer = new Timer(200);
        }

        public void Update()
        {
            GunTimer.Update();
        }

        public bool Fire(LineSegmentF rayEnlonged, Vector2Object destination)
        {
            if ((Ammo > 0 && GunTimer.Ready == true) || MaxAmmo == 0)
            {
                Vector2 barrel = rayEnlonged.Start + rayEnlonged.NormalizedWithZeroSolution() * GunBarrel;
                Game1.soundElectro.Play(1f, (float)(Globals.GlobalRandom.NextDouble()) / 2f, 0f);
                Ammo--;

                fireRay(barrel, destination);

                if (Owner is Player)
                {
                    for (int i = 0; i < 3; i++)
                        Game1.mapLive.mapLightings.Add(new Lightning(barrel, destination.Vector2, Game1.Textures["Lightning"], 16, -1));
                    for (int i = 0; i < 1; i++)
                        Game1.mapLive.mapLightings.Add(new Lightning(barrel, destination.Vector2, Game1.Textures["LightningBig"], 16, -1));
                    for (int i = 0; i < 1; i++)
                        Game1.mapLive.mapLightings.Add(new Lightning(barrel, destination.Vector2, Game1.Textures["lightningChain"], 16, -3, 3f));
                }

                GunTimer.Reset();

                return true;
            }

            return false;
        }

        private void fireRay(Vector2 rayStart, Vector2Object RayDestination)
        {
            if (RayDestination != null && RayDestination.Vector2 != null)
            {
                if (Owner is Player)
                    _line = new LineSegmentF(rayStart, RayDestination.Vector2);
                if (Owner is Inpc)
                    _line = new LineSegmentF((Owner as Inpc).Boundary.Origin, RayDestination.Vector2);

                if (RayDestination.Object != null && RayDestination.Object is Inpc && (RayDestination.Object as Inpc).Friendly == false)
                {
                    (RayDestination.Object as Inpc).Push(_line.NormalizedWithZeroSolution() * _velocityAdd * 0.5f);
                    (RayDestination.Object as Inpc).Stun();
                    (RayDestination.Object as Inpc).KineticDamage(Damage);
                }
                if (RayDestination.Object != null)
                {
                    if (RayDestination.Object is Map || RayDestination.Object is Player)
                    {
                        Game1.mapLive.mapParticles.Add(new ParticleLaserDest(RayDestination.Vector2));
                    }
                    if (RayDestination.Object is IRectanglePhysics)
                    {
                    }
                }
            }
        }
    }
}