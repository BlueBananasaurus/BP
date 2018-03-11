using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Monogame_GL
{
    public static class SimpleGunDrawModule
    {
        public static void DrawSimpleGun(LineSegmentF rayBarrel, int GunOffset)
        {
            if (Math.Abs(rayBarrel.ToAngle() * (180 / Math.PI)) < 25 + 45)
            {
                Game1.SpriteBatchGlobal.Draw(Game1.projectilGun, rayBarrel.Start, rotation: rayBarrel.ToAngle(), origin: new Vector2(0 + GunOffset, 8.5f*2));
            }
            else if (Math.Abs(rayBarrel.ToAngle() * (180 / Math.PI)) > 155 - 45)
            {
                Game1.SpriteBatchGlobal.Draw(Game1.projectilGun, rayBarrel.Start, rotation: rayBarrel.ToAngle(), origin: new Vector2(0 + GunOffset, 6.5f*2), effects: SpriteEffects.FlipVertically);
            }
            else if (rayBarrel.ToAngle() * (180 / Math.PI) > 25 + 45 && rayBarrel.ToAngle() * (180 / Math.PI) < 155 - 45)
            {
                Game1.SpriteBatchGlobal.Draw(Game1.projectilGunDown, rayBarrel.Start, rotation: rayBarrel.ToAngle(), origin: new Vector2(0 + GunOffset, 4.5f*2));
            }
            else
            {
                Game1.SpriteBatchGlobal.Draw(Game1.projectilGunUp, rayBarrel.Start, rotation: rayBarrel.ToAngle(), origin: new Vector2(0 + GunOffset, 4.5f*2));
            }
        }

        public static void DrawSimpleGunNormal(LineSegmentF rayBarrel, int GunOffset)
        {
            if (Math.Abs(rayBarrel.ToAngle() * (180 / Math.PI)) < 25 + 45)
            {
                Effects.RotateNormalsEffect(rayBarrel.ToAngle(), new Vector2(1, 1));
                Game1.SpriteBatchGlobal.Draw(Game1.Textures["projectilGunNormal"], rayBarrel.Start, rotation: rayBarrel.ToAngle(), origin: new Vector2(0 + GunOffset, 8.5f * 2));
            }
            else if (Math.Abs(rayBarrel.ToAngle() * (180 / Math.PI)) > 155 - 45)
            {
                Effects.RotateNormalsEffect(rayBarrel.ToAngle(), new Vector2(1, -1));
                Game1.SpriteBatchGlobal.Draw(Game1.Textures["projectilGunNormal"], rayBarrel.Start, rotation: rayBarrel.ToAngle(), origin: new Vector2(0 + GunOffset, 6.5f * 2), effects: SpriteEffects.FlipVertically);
            }
            else if (rayBarrel.ToAngle() * (180 / Math.PI) > 25 + 45 && rayBarrel.ToAngle() * (180 / Math.PI) < 155 - 45)
            {
                Effects.RotateNormalsEffect(rayBarrel.ToAngle(), new Vector2(1, 1));
                Game1.SpriteBatchGlobal.Draw(Game1.Textures["projectilGunDownNormal"], rayBarrel.Start, rotation: rayBarrel.ToAngle(), origin: new Vector2(0 + GunOffset, 4.5f * 2));
            }
            else
            {
                Effects.RotateNormalsEffect(rayBarrel.ToAngle(), new Vector2(1, 1));
                Game1.SpriteBatchGlobal.Draw(Game1.Textures["projectilGunUpNormal"], rayBarrel.Start, rotation: rayBarrel.ToAngle(), origin: new Vector2(0 + GunOffset, 4.5f * 2));
            }
        }

        public static void DrawLightningGun(LineSegmentF rayBarrel, int GunOffset)
        {
            if (Math.Abs(rayBarrel.ToAngle() * (180 / Math.PI)) < 25 + 45)
            {
                Game1.SpriteBatchGlobal.Draw(Game1.lightingGun, rayBarrel.Start, rotation: rayBarrel.ToAngle(), origin: new Vector2(0, 6.5f*2));
            }
            else if (Math.Abs(rayBarrel.ToAngle() * (180 / Math.PI)) > 155 - 45)
            {
                Game1.SpriteBatchGlobal.Draw(Game1.lightingGun, rayBarrel.Start, rotation: rayBarrel.ToAngle(), origin: new Vector2(0, 6.5f*2), effects: SpriteEffects.FlipVertically);
            }
            else if (rayBarrel.ToAngle() * (180 / Math.PI) > 25 + 45 && rayBarrel.ToAngle() * (180 / Math.PI) < 155 - 45)
            {
                Game1.SpriteBatchGlobal.Draw(Game1.lightingGunUp, rayBarrel.Start, rotation: rayBarrel.ToAngle(), origin: new Vector2(0, 6.5f*2));
            }
            else
            {
                Game1.SpriteBatchGlobal.Draw(Game1.lightingGunDown, rayBarrel.Start, rotation: rayBarrel.ToAngle(), origin: new Vector2(0, 6.5f*2));
            }
        }

        public static void DrawLightningGunNormal(LineSegmentF rayBarrel, int GunOffset)
        {
            if (Math.Abs(rayBarrel.ToAngle() * (180 / Math.PI)) < 25 + 45)
            {
                Effects.RotateNormalsEffect(rayBarrel.ToAngle(), new Vector2(1, 1));
                Game1.SpriteBatchGlobal.Draw(Game1.Textures["lightingGunNormal"], rayBarrel.Start, rotation: rayBarrel.ToAngle(), origin: new Vector2(0, 6.5f * 2));
            }
            else if (Math.Abs(rayBarrel.ToAngle() * (180 / Math.PI)) > 155 - 45)
            {
                Effects.RotateNormalsEffect(rayBarrel.ToAngle(), new Vector2(1, -1));
                Game1.SpriteBatchGlobal.Draw(Game1.Textures["lightingGunNormal"], rayBarrel.Start, rotation: rayBarrel.ToAngle(), origin: new Vector2(0, 6.5f * 2), effects: SpriteEffects.FlipVertically);
            }
            else if (rayBarrel.ToAngle() * (180 / Math.PI) > 25 + 45 && rayBarrel.ToAngle() * (180 / Math.PI) < 155 - 45)
            {
                Effects.RotateNormalsEffect(rayBarrel.ToAngle(), new Vector2(1, 1));
                Game1.SpriteBatchGlobal.Draw(Game1.Textures["lightingGunUpNormal"], rayBarrel.Start, rotation: rayBarrel.ToAngle(), origin: new Vector2(0, 6.5f * 2));
            }
            else
            {
                Effects.RotateNormalsEffect(rayBarrel.ToAngle(), new Vector2(1, 1));
                Game1.SpriteBatchGlobal.Draw(Game1.Textures["lightingGunDownNormal"], rayBarrel.Start, rotation: rayBarrel.ToAngle(), origin: new Vector2(0, 6.5f * 2));
            }
        }

        public static void DrawLaserGun(LineSegmentF rayBarrel, int GunOffset)
        {
            if (Math.Abs(rayBarrel.ToAngle() * (180 / Math.PI)) < 25 + 45)
            {
                Game1.SpriteBatchGlobal.Draw(Game1.laserGun, rayBarrel.Start, rotation: rayBarrel.ToAngle(), origin: new Vector2(0, 8.5f*2));
            }
            else if (Math.Abs(rayBarrel.ToAngle() * (180 / Math.PI)) > 155 - 45)
            {
                Game1.SpriteBatchGlobal.Draw(Game1.laserGun, rayBarrel.Start, rotation: rayBarrel.ToAngle(), origin: new Vector2(0, 7.5f*2), effects: SpriteEffects.FlipVertically);
            }
            else if (rayBarrel.ToAngle() * (180 / Math.PI) > 25 + 45 && rayBarrel.ToAngle() * (180 / Math.PI) < 155 - 45)
            {
                Game1.SpriteBatchGlobal.Draw(Game1.laserGunDown, rayBarrel.Start, rotation: rayBarrel.ToAngle(), origin: new Vector2(0, 5.5f*2));
            }
            else
            {
                Game1.SpriteBatchGlobal.Draw(Game1.laserGunUp, rayBarrel.Start, rotation: rayBarrel.ToAngle(), origin: new Vector2(0, 5.5f*2));
            }
        }

        public static void DrawLaserGunNormal(LineSegmentF rayBarrel, int GunOffset)
        {
            if (Math.Abs(rayBarrel.ToAngle() * (180 / Math.PI)) < 25 + 45)
            {
                Effects.RotateNormalsEffect(rayBarrel.ToAngle(), new Vector2(1, 1));
                Game1.SpriteBatchGlobal.Draw(Game1.Textures["laserGunNormal"], rayBarrel.Start, rotation: rayBarrel.ToAngle(), origin: new Vector2(0, 8.5f*2));
            }
            else if (Math.Abs(rayBarrel.ToAngle() * (180 / Math.PI)) > 155 - 45)
            {
                Effects.RotateNormalsEffect(rayBarrel.ToAngle(), new Vector2(1, -1));
                Game1.SpriteBatchGlobal.Draw(Game1.Textures["laserGunNormal"], rayBarrel.Start, rotation: rayBarrel.ToAngle(), origin: new Vector2(0, 7.5f * 2), effects: SpriteEffects.FlipVertically);
            }
            else if (rayBarrel.ToAngle() * (180 / Math.PI) > 25 + 45 && rayBarrel.ToAngle() * (180 / Math.PI) < 155 - 45)
            {
                Effects.RotateNormalsEffect(rayBarrel.ToAngle(), new Vector2(1, 1));
                Game1.SpriteBatchGlobal.Draw(Game1.Textures["laserGunDownNormal"], rayBarrel.Start, rotation: rayBarrel.ToAngle(), origin: new Vector2(0, 5.5f*2));
            }
            else
            {
                Effects.RotateNormalsEffect(rayBarrel.ToAngle(), new Vector2(1, 1));
                Game1.SpriteBatchGlobal.Draw(Game1.Textures["laserGunUpNormal"], rayBarrel.Start, rotation: rayBarrel.ToAngle(), origin: new Vector2(0, 5.5f*2));
            }
        }

        public static void DrawFlameGun(LineSegmentF rayBarrel, int GunOffset)
        {
            if (Math.Abs(rayBarrel.ToAngle() * (180 / Math.PI)) < 25 + 45)
            {
                Game1.SpriteBatchGlobal.Draw(Game1.flameThrower, rayBarrel.Start, rotation: rayBarrel.ToAngle(), origin: new Vector2(0, 8f * 2));
            }
            else if (Math.Abs(rayBarrel.ToAngle() * (180 / Math.PI)) > 155 - 45)
            {
                Game1.SpriteBatchGlobal.Draw(Game1.flameThrower, rayBarrel.Start, rotation: rayBarrel.ToAngle(), origin: new Vector2(0, 16f * 2), effects: SpriteEffects.FlipVertically);
            }
            else if (rayBarrel.ToAngle() * (180 / Math.PI) > 25 + 45 && rayBarrel.ToAngle() * (180 / Math.PI) < 155 - 45)
            {
                Game1.SpriteBatchGlobal.Draw(Game1.flameThrowerUp, rayBarrel.Start, rotation: rayBarrel.ToAngle(), origin: new Vector2(0, 5.5f * 2));
            }
            else
            {
                Game1.SpriteBatchGlobal.Draw(Game1.flameThrowerDown, rayBarrel.Start, rotation: rayBarrel.ToAngle(), origin: new Vector2(0, 5.5f * 2));
            }
        }

        public static void DrawFlameGunNormal(LineSegmentF rayBarrel, int GunOffset)
        {
            if (Math.Abs(rayBarrel.ToAngle() * (180 / Math.PI)) < 25 + 45)
            {
                Effects.RotateNormalsEffect(rayBarrel.ToAngle(), new Vector2(1, 1));
                Game1.SpriteBatchGlobal.Draw(Game1.Textures["flameThrowerNormal"], rayBarrel.Start, rotation: rayBarrel.ToAngle(), origin: new Vector2(0, 8f * 2));
            }
            else if (Math.Abs(rayBarrel.ToAngle() * (180 / Math.PI)) > 155 - 45)
            {
                Effects.RotateNormalsEffect(rayBarrel.ToAngle(), new Vector2(1, -1));
                Game1.SpriteBatchGlobal.Draw(Game1.Textures["flameThrowerNormal"], rayBarrel.Start, rotation: rayBarrel.ToAngle(), origin: new Vector2(0, 16f * 2), effects: SpriteEffects.FlipVertically);
            }
            else if (rayBarrel.ToAngle() * (180 / Math.PI) > 25 + 45 && rayBarrel.ToAngle() * (180 / Math.PI) < 155 - 45)
            {
                Effects.RotateNormalsEffect(rayBarrel.ToAngle(), new Vector2(1, 1));
                Game1.SpriteBatchGlobal.Draw(Game1.Textures["flameThrowerUpNormal"], rayBarrel.Start, rotation: rayBarrel.ToAngle(), origin: new Vector2(0, 5.5f * 2));
            }
            else
            {
                Effects.RotateNormalsEffect(rayBarrel.ToAngle(), new Vector2(1,-1));
                Game1.SpriteBatchGlobal.Draw(Game1.Textures["flameThrowerDownNormal"], rayBarrel.Start, rotation: rayBarrel.ToAngle(), origin: new Vector2(0, 5.5f * 2));
            }

            Effects.ResetEffect3D();
        }

        public static void DrawRocketGun(LineSegmentF rayBarrel, int GunOffset)
        {
            if (Math.Abs(rayBarrel.ToAngle() * (180 / Math.PI)) < 25 + 45)
            {
                Game1.SpriteBatchGlobal.Draw(Game1.Textures["RocketLauncher"], rayBarrel.Start, rotation: rayBarrel.ToAngle(), origin: new Vector2(64 + GunOffset, 6f*2));
            }
            else if (Math.Abs(rayBarrel.ToAngle() * (180 / Math.PI)) > 155 - 45)
            {
                Game1.SpriteBatchGlobal.Draw(Game1.Textures["RocketLauncher"], rayBarrel.Start, rotation: rayBarrel.ToAngle(), origin: new Vector2(64 + GunOffset, 12f*2),  effects: SpriteEffects.FlipVertically);
            }
            else if (rayBarrel.ToAngle() * (180 / Math.PI) > 25 + 45 && rayBarrel.ToAngle() * (180 / Math.PI) < 155 - 45)
            {
                Game1.SpriteBatchGlobal.Draw(Game1.Textures["RocketLauncherUp"], rayBarrel.Start, rotation: rayBarrel.ToAngle(), origin: new Vector2(64 + GunOffset, 6.5f*2));
            }
            else
            {
                Game1.SpriteBatchGlobal.Draw(Game1.Textures["RocketLauncherDown"], rayBarrel.Start, rotation: rayBarrel.ToAngle(), origin: new Vector2(64 + GunOffset, 6.5f*2));
            }
        }

        public static void DrawRocketGunNormal(LineSegmentF rayBarrel, int GunOffset)
        {
            if (Math.Abs(rayBarrel.ToAngle() * (180 / Math.PI)) < 25 + 45)
            {
                Effects.RotateNormalsEffect(rayBarrel.ToAngle(), new Vector2(1, 1));
                Game1.SpriteBatchGlobal.Draw(Game1.Textures["rocketLauncherNormal"], rayBarrel.Start, rotation: rayBarrel.ToAngle(), origin: new Vector2(64 + GunOffset, 6f * 2));
            }
            else if (Math.Abs(rayBarrel.ToAngle() * (180 / Math.PI)) > 155 - 45)
            {
                Effects.RotateNormalsEffect(rayBarrel.ToAngle(), new Vector2(1, -1));
                Game1.SpriteBatchGlobal.Draw(Game1.Textures["rocketLauncherNormal"], rayBarrel.Start, rotation: rayBarrel.ToAngle(), origin: new Vector2(64 + GunOffset, 12f * 2), effects: SpriteEffects.FlipVertically);
            }
            else if (rayBarrel.ToAngle() * (180 / Math.PI) > 25 + 45 && rayBarrel.ToAngle() * (180 / Math.PI) < 155 - 45)
            {
                Effects.RotateNormalsEffect(rayBarrel.ToAngle(), new Vector2(1, 1));
                Game1.SpriteBatchGlobal.Draw(Game1.Textures["rocketLauncherUpNormal"], rayBarrel.Start, rotation: rayBarrel.ToAngle(), origin: new Vector2(64 + GunOffset, 6.5f * 2));
            }
            else
            {
                Effects.RotateNormalsEffect(rayBarrel.ToAngle(), new Vector2(1, 1));
                Game1.SpriteBatchGlobal.Draw(Game1.Textures["rocketLauncherDownNormal"], rayBarrel.Start, rotation: rayBarrel.ToAngle(), origin: new Vector2(64 + GunOffset, 6.5f * 2));
            }
        }

        public static void DrawEnergyGun(LineSegmentF rayBarrel, int GunOffset)
        {
            if (Math.Abs(rayBarrel.ToAngle() * (180 / Math.PI)) < 25 + 45)
            {
                Game1.SpriteBatchGlobal.Draw(Game1.energyLauncher, rayBarrel.Start, rotation: rayBarrel.ToAngle(), origin: new Vector2(64 + GunOffset, 12));
            }
            else if (Math.Abs(rayBarrel.ToAngle() * (180 / Math.PI)) > 155 - 45)
            {
                Game1.SpriteBatchGlobal.Draw(Game1.energyLauncher, rayBarrel.Start, rotation: rayBarrel.ToAngle(), origin: new Vector2(64 + GunOffset, 26), effects: SpriteEffects.FlipVertically);
            }
            else if (rayBarrel.ToAngle() * (180 / Math.PI) > 25 + 45 && rayBarrel.ToAngle() * (180 / Math.PI) < 155 - 45)
            {
                Game1.SpriteBatchGlobal.Draw(Game1.energyLauncherDown, rayBarrel.Start, rotation: rayBarrel.ToAngle(), origin: new Vector2(64 + GunOffset, 7.5f*2));
            }
            else
            {
                Game1.SpriteBatchGlobal.Draw(Game1.energyLauncherUp, rayBarrel.Start, rotation: rayBarrel.ToAngle(), origin: new Vector2(64 + GunOffset, 7.5f*2));
            }
        }

        public static void DrawEnergyGunNormal(LineSegmentF rayBarrel, int GunOffset)
        {
            if (Math.Abs(rayBarrel.ToAngle() * (180 / Math.PI)) < 25 + 45)
            {
                Effects.RotateNormalsEffect(rayBarrel.ToAngle(), new Vector2(1, 1));
                Game1.SpriteBatchGlobal.Draw(Game1.Textures["EnergyLauncherNormal"], rayBarrel.Start, rotation: rayBarrel.ToAngle(), origin: new Vector2(64 + GunOffset, 12));
            }
            else if (Math.Abs(rayBarrel.ToAngle() * (180 / Math.PI)) > 155 - 45)
            {
                Effects.RotateNormalsEffect(rayBarrel.ToAngle(), new Vector2(1, -1));
                Game1.SpriteBatchGlobal.Draw(Game1.Textures["EnergyLauncherNormal"], rayBarrel.Start, rotation: rayBarrel.ToAngle(), origin: new Vector2(64 + GunOffset, 26), effects: SpriteEffects.FlipVertically);
            }
            else if (rayBarrel.ToAngle() * (180 / Math.PI) > 25 + 45 && rayBarrel.ToAngle() * (180 / Math.PI) < 155 - 45)
            {
                Effects.RotateNormalsEffect(rayBarrel.ToAngle(), new Vector2(1, 1));
                Game1.SpriteBatchGlobal.Draw(Game1.Textures["EnergyLauncherDownNormal"], rayBarrel.Start, rotation: rayBarrel.ToAngle(), origin: new Vector2(64 + GunOffset, 7.5f * 2));
            }
            else
            {
                Effects.RotateNormalsEffect(rayBarrel.ToAngle(), new Vector2(1, 1));
                Game1.SpriteBatchGlobal.Draw(Game1.Textures["EnergyLauncherUpNormal"], rayBarrel.Start, rotation: rayBarrel.ToAngle(), origin: new Vector2(64 + GunOffset, 7.5f * 2));
            }
        }

        public static void DrawPlasmaGun(LineSegmentF rayBarrel, int GunOffset)
        {
            if (Math.Abs(rayBarrel.ToAngle() * (180 / Math.PI)) < 25 + 45)
            {
                Game1.SpriteBatchGlobal.Draw(Game1.plasmaGun, rayBarrel.Start, rotation: rayBarrel.ToAngle(), origin: new Vector2(0 + GunOffset, 7.0f*2));
            }
            else if (Math.Abs(rayBarrel.ToAngle() * (180 / Math.PI)) > 155 - 45)
            {
                Game1.SpriteBatchGlobal.Draw(Game1.plasmaGun, rayBarrel.Start, rotation: rayBarrel.ToAngle(), origin: new Vector2(0 + GunOffset, 10.0f*2), effects: SpriteEffects.FlipVertically);
            }
            else if (rayBarrel.ToAngle() * (180 / Math.PI) > 25 + 45 && rayBarrel.ToAngle() * (180 / Math.PI) < 155 - 45)
            {
                Game1.SpriteBatchGlobal.Draw(Game1.PlasmaGunDown, rayBarrel.Start, rotation: rayBarrel.ToAngle(), origin: new Vector2(0 + GunOffset, 4f*2));
            }
            else
            {
                Game1.SpriteBatchGlobal.Draw(Game1.PlasmaGunUp, rayBarrel.Start, rotation: rayBarrel.ToAngle(), origin: new Vector2(0 + GunOffset, 4f*2));
            }
        }

        public static void DrawPlasmaGunNormal(LineSegmentF rayBarrel, int GunOffset)
        {
            if (Math.Abs(rayBarrel.ToAngle() * (180 / Math.PI)) < 25 + 45)
            {
                Effects.RotateNormalsEffect(rayBarrel.ToAngle(), new Vector2(1, 1));
                Game1.SpriteBatchGlobal.Draw(Game1.Textures["PlasmaGunNormal"], rayBarrel.Start, rotation: rayBarrel.ToAngle(), origin: new Vector2(0 + GunOffset, 7.0f * 2));
            }
            else if (Math.Abs(rayBarrel.ToAngle() * (180 / Math.PI)) > 155 - 45)
            {
                Effects.RotateNormalsEffect(rayBarrel.ToAngle(), new Vector2(1, -1));
                Game1.SpriteBatchGlobal.Draw(Game1.Textures["PlasmaGunNormal"], rayBarrel.Start, rotation: rayBarrel.ToAngle(), origin: new Vector2(0 + GunOffset, 10.0f * 2), effects: SpriteEffects.FlipVertically);
            }
            else if (rayBarrel.ToAngle() * (180 / Math.PI) > 25 + 45 && rayBarrel.ToAngle() * (180 / Math.PI) < 155 - 45)
            {
                Effects.RotateNormalsEffect(rayBarrel.ToAngle(), new Vector2(1, 1));
                Game1.SpriteBatchGlobal.Draw(Game1.Textures["PlasmaGunDownNormal"], rayBarrel.Start, rotation: rayBarrel.ToAngle(), origin: new Vector2(0 + GunOffset, 4f * 2));
            }
            else
            {
                Effects.RotateNormalsEffect(rayBarrel.ToAngle(), new Vector2(1, 1));
                Game1.SpriteBatchGlobal.Draw(Game1.Textures["PlasmaGunUpNormal"], rayBarrel.Start, rotation: rayBarrel.ToAngle(), origin: new Vector2(0 + GunOffset, 4f * 2));
            }
        }
    }
}