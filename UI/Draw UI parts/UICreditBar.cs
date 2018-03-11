using Microsoft.Xna.Framework;

namespace Monogame_GL
{
    internal class UICreditBar
    {
        private Vector2 _position;

        public UICreditBar(Vector2 position)
        {
            _position = position;
        }

        public void Draw()
        {
            Game1.SpriteBatchGlobal.Draw(Game1.Textures["electroIcon"], _position - new Vector2(96 + 16, 40) + new Vector2(4, 6));
            DrawNumber.Draw_digits(Game1.numbersMedium, Game1.PlayerInstance.Electronics, _position - new Vector2(0, 32), Align.left, new Point(15, 18));
            Game1.SpriteBatchGlobal.Draw(Game1.Textures["tissueIcon"], _position - new Vector2(96 + 16, 8) + new Vector2(4, 6));
            DrawNumber.Draw_digits(Game1.numbersMedium, Game1.PlayerInstance.Tissue, _position - new Vector2(0, 0), Align.left, new Point(15, 18));
        }
    }
}