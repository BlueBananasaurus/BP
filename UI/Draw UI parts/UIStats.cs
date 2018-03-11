using Microsoft.Xna.Framework;

namespace Monogame_GL
{
    public class UIStats
    {
        private Vector2 _position;
        private UIHealthBar _barHealth;
        private UIShieldBar _shiedlBar;
        private UICreditBar _creditBar;
        private UIMachBar _bar;

        public UIStats(Vector2 position)
        {
            _position = position;
            _bar = new UIMachBar(new Vector2(Globals.WinRenderSize.X / 2, Globals.WinRenderSize.Y / 2));
            _creditBar = new UICreditBar(new Vector2(128, Globals.WinRenderSize.Y - 32 - 8));
            _barHealth = new UIHealthBar(new Vector2(Globals.WinRenderSize.X - 128 - 32, Globals.WinRenderSize.Y - 64 - 8));
            _shiedlBar = new UIShieldBar(new Vector2(Globals.WinRenderSize.X - 128 - 32, Globals.WinRenderSize.Y - 32 - 8));
        }

        public void Update()
        {
        }

        public void Draw(Player player)
        {
            _creditBar.Draw();
            _shiedlBar.Draw();
            _barHealth.Draw();

            if (!(player.CurretnObjectControl is BuddyModule) == true)
            {
                _bar.Draw(player);
            }
        }
    }
}