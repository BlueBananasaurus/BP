using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Monogame_GL
{
    internal class UIHealthBar
    {
        private Vector2 _position;

        public UIHealthBar(Vector2 position)
        {
            _position = position;
        }

        public void Draw()
        {
            Game1.SpriteBatchGlobal.Draw(Game1.health, _position - new Vector2(96 + 16, 4), scale: new Vector2(1f));
            Game1.SpriteBatchGlobal.Draw(Game1.healthBarBackground, _position - new Vector2(1) + new Vector2(0, 5), null, new Rectangle(0, 0, 130, 10), null, 0f, new Vector2(1), Color.White, SpriteEffects.None);
            Game1.SpriteBatchGlobal.Draw(Game1.barBackground, _position + new Vector2(0, 5), null, new Rectangle(0, 0, (int)(Game1.PlayerInstance.PotentialHealth) * 128 / 100, 8), null, 0f, new Vector2(1), Color.White, SpriteEffects.None);
            Game1.SpriteBatchGlobal.Draw(Game1.healthBar, _position + new Vector2(0, 5), null, new Rectangle(0, 0, (int)(Game1.PlayerInstance.Health) * 128 / 100, 8), null, 0f, new Vector2(1), Color.White, SpriteEffects.None);

            if (Game1.PlayerInstance.Health >= 0)
                DrawNumber.Draw_digits(Game1.numbersMedium, (int)Math.Round(Game1.PlayerInstance.Health), _position - new Vector2(64 - 16, 0), Align.center, new Point(15, 18));
            else
                DrawNumber.Draw_digits(Game1.numbersMedium, 0, _position - new Vector2(64 - 16, 0), Align.center, new Point(15, 18));
        }
    }
}