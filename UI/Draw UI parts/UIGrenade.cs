using Microsoft.Xna.Framework;

namespace Monogame_GL
{
    public class UIGrenade
    {
        private Vector2 _position;

        public UIGrenade(Vector2 position)
        {
            _position = position;
        }

        public void Update()
        {
        }

        public void Draw()
        {
            if (Game1.PlayerInstance.WeaponsAvailable == true)
            {
                Game1.SpriteBatchGlobal.Draw(Game1.Textures["grenadeIcon"], _position - new Vector2(0, 8) + new Vector2(8, 10), scale: new Vector2(1f));
                DrawNumber.Draw_digits(Game1.numbersMedium, Game1.PlayerInstance.GrenadesCount, _position + new Vector2(20, 40), Align.center, new Point(15, 18));
            }
        }
    }
}