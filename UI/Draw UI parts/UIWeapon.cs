using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Monogame_GL
{
    public class UIWeapon
    {
        private Vector2 _positon;
        private bool _choosed;
        private byte _index;
        private float _width;

        public UIWeapon(Vector2 position, byte index, float width)
        {
            _positon = position;
            _index = index;
            _width = width;
        }

        public void Update(bool choosed)
        {
            _choosed = choosed;
        }

        public void Draw(Texture2D tex)
        {
            if (_choosed == true)
            {
                Game1.SpriteBatchGlobal.Draw(Game1.Textures["SelectedGun"], _positon, color: Color.White);
            }

            Game1.SpriteBatchGlobal.Draw(tex, _positon + new Vector2((float)(64 - (_width)), 10), scale: new Vector2(1.0f), color: Color.White);
            if (_index != 0)
                DrawNumber.Draw_digits(Game1.numbersMedium, Game1.PlayerInstance.Weapons[_index].Ammo, _positon + new Vector2(64, 48), Align.center, new Point(15, 18));
        }
    }
}