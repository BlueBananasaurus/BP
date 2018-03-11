using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Monogame_GL
{
    internal class UIMachBar
    {
        private Vector2 _position;

        public UIMachBar(Vector2 position)
        {
            _position = position;
        }

        public void Draw(Player player)
        {
                Game1.SpriteBatchGlobal.Draw(Game1.Textures["HealthBarMachBackground"], _position - new Vector2(1) + new Vector2(0, 5) - new Vector2(130 / 2, 128));
                Game1.SpriteBatchGlobal.Draw(Game1.Textures["HealthBarMach"], _position + new Vector2(0, 5) - new Vector2(130 / 2, 128), null, new Rectangle(0, 0, (int)Math.Ceiling((player.CurretnObjectControl.HealthMachine) * 128 / player.CurretnObjectControl.MaxHealth), 8));
        }
    }
}