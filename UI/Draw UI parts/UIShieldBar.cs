using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Monogame_GL
{
    internal class UIShieldBar
    {
        private Vector2 _position;

        public UIShieldBar(Vector2 position)
        {
            _position = position;
        }

        public void Draw()
        {
            Game1.SpriteBatchGlobal.Draw(Game1.shield, _position - new Vector2(96 + 16, 8) + new Vector2(0, 5), scale: new Vector2(1f));
            Game1.SpriteBatchGlobal.Draw(Game1.shieldBarBackground, _position - new Vector2(1) + new Vector2(0, 5), null, new Rectangle(0, 0, 130, 10), null, 0f, new Vector2(1), Color.White, SpriteEffects.None);
            Game1.SpriteBatchGlobal.Draw(Game1.barBackground, _position + new Vector2(0, 5), null, new Rectangle(0, 0, (int)(Game1.PlayerInstance.PotentialShield) * 128 / 100, 8), null, 0f, new Vector2(1), Color.White, SpriteEffects.None);
            Game1.SpriteBatchGlobal.Draw(Game1.shieldBar, _position + new Vector2(0, 5), null, new Rectangle(0, 0, (int)(Game1.PlayerInstance.Shield) * 128 / 100, 8), null, 0f, new Vector2(1), Color.White, SpriteEffects.None);

            if (Game1.PlayerInstance.Shield >= 0)
                DrawNumber.Draw_digits(Game1.numbersMedium, (int)Math.Round(Game1.PlayerInstance.Shield), _position - new Vector2(64 - 16, 0), Align.center, new Point(15, 18));
            else
                DrawNumber.Draw_digits(Game1.numbersMedium, 0, _position - new Vector2(64 - 16, 0), Align.center, new Point(15, 18));
        }
    }
}