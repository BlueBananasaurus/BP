using Microsoft.Xna.Framework;

namespace Monogame_GL
{
    public static class UIPlayer
    {
        private static UIGrenade _UIGreandes;
        private static UIWeaponPanel _UIPanelGuns;
        private static UIStats _stats;
        private static int _offset;
        private static float _offsetPanel;

        static UIPlayer()
        {
            _stats = new UIStats(new Vector2(0, 0));
            _offset = 128+32;
            _offsetPanel = (Globals.WinRenderSize.X / 2 - (_offset * Game1.PlayerInstance.Weapons.Count) / 2);
            _UIPanelGuns = new UIWeaponPanel(Vector2.Zero);
            _UIPanelGuns._weapons.Add(new UIWeapon(new Vector2(_offsetPanel + 0, 0), (byte)_UIPanelGuns._weapons.Count, 32));
            _UIPanelGuns._weapons.Add(new UIWeapon(new Vector2(_offsetPanel + _offset * (byte)_UIPanelGuns._weapons.Count, 0), (byte)_UIPanelGuns._weapons.Count, 32));
            _UIPanelGuns._weapons.Add(new UIWeapon(new Vector2(_offsetPanel + _offset * (byte)_UIPanelGuns._weapons.Count, 0), (byte)_UIPanelGuns._weapons.Count, 32));
            _UIPanelGuns._weapons.Add(new UIWeapon(new Vector2(_offsetPanel + _offset * (byte)_UIPanelGuns._weapons.Count, 0), (byte)_UIPanelGuns._weapons.Count, 44));
            _UIPanelGuns._weapons.Add(new UIWeapon(new Vector2(_offsetPanel + _offset * (byte)_UIPanelGuns._weapons.Count, 0), (byte)_UIPanelGuns._weapons.Count, 64));
            _UIPanelGuns._weapons.Add(new UIWeapon(new Vector2(_offsetPanel + _offset * (byte)_UIPanelGuns._weapons.Count, 0), (byte)_UIPanelGuns._weapons.Count, 64));
            _UIPanelGuns._weapons.Add(new UIWeapon(new Vector2(_offsetPanel + _offset * (byte)_UIPanelGuns._weapons.Count, 0), (byte)_UIPanelGuns._weapons.Count, 32));
            _UIGreandes = new UIGrenade(new Vector2(Globals.WinRenderSize.X - 128 - 96, 64 - 24 - 32));
        }

        public static void Update()
        {
            _UIPanelGuns.Update((byte)Game1.PlayerInstance.CurrentWeapon);
            _stats.Update();
            _UIGreandes.Update();
        }

        public static void Draw()
        {
            _UIPanelGuns.Draw();
            _stats.Draw(Game1.PlayerInstance);
            _UIGreandes.Draw();
        }
    }
}