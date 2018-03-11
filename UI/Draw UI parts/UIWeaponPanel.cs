using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Monogame_GL
{
    public class UIWeaponPanel
    {
        private Vector2 _positon;
        public List<UIWeapon> _weapons { get; set; }

        public UIWeaponPanel(Vector2 position)
        {
            _positon = position;
            _weapons = new List<UIWeapon>();
        }

        public void Update(byte choosedIndex)
        {
            if (choosedIndex <= _weapons.Count)
            {
                foreach (UIWeapon wpn in _weapons)
                {
                    if (_weapons.IndexOf(wpn) == choosedIndex)
                        wpn.Update(true);
                    else
                        wpn.Update(false);
                }
            }
        }

        public void Draw()
        {
            if (Game1.PlayerInstance.WeaponsAvailable == true)
            {
                foreach (UIWeapon wpn in _weapons)
                {
                    if (_weapons.IndexOf(wpn) == 0)
                        wpn.Draw(Game1.Textures["projectilGunIcon"]);
                    if (_weapons.IndexOf(wpn) == 1)
                        wpn.Draw(Game1.Textures["lightingGunIcon"]);
                    if (_weapons.IndexOf(wpn) == 2)
                        wpn.Draw(Game1.Textures["laserGunIcon"]);
                    if (_weapons.IndexOf(wpn) == 3)
                        wpn.Draw(Game1.Textures["flameThrowerIcon"]);
                    if (_weapons.IndexOf(wpn) == 4)
                        wpn.Draw(Game1.Textures["RocketLauncherIcon"]);
                    if (_weapons.IndexOf(wpn) == 5)
                        wpn.Draw(Game1.Textures["EnergyLauncherIcon"]);
                    if (_weapons.IndexOf(wpn) == 6)
                        wpn.Draw(Game1.Textures["PlasmaGunIcon"]);
                }
            }
        }
    }
}