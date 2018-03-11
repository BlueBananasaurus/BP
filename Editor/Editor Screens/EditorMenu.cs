using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Monogame_GL
{
    public static class EditorMenu
    {
        public static Dictionary<string, IWindow> _windows;
        private static string _actualWin;
        public static GroupRadios Radios { get; private set; }
        private static Radio _r1;
        private static Radio _r2;
        private static Radio _r3;
        private static Radio _r4;
        private static Radio _r5;
        private static float _offsetX;

        static EditorMenu()
        {
            _windows = new Dictionary<string, IWindow>();
            _windows.Add("settings", UISettings.Instance);
            _windows.Add("draw", UIDrawing.Instance);
            _windows.Add("save", UISave.Instance);
            _windows.Add("entities", UIEntities.Instance);
            _actualWin = "settings";
            _offsetX = (Globals.WinRenderSize.X - (256 * 5 + 64 * 4)) / 2;
            _r1 = new Radio("Map settings", new Vector2(_offsetX, 64), RadioType.big, null, null, "settings");
            _r2 = new Radio("Draw tiles", new Vector2(_offsetX + 64 + 256 * 1, 64), RadioType.big, null, null, "draw");
            _r3 = new Radio("Entities", new Vector2(_offsetX + 64 * 2 + 256 * 2, 64), RadioType.big, null, null, "entities");
            _r4 = new Radio("Insert", new Vector2(_offsetX + 64 * 3 + 256 * 3, 64), RadioType.big, null, null, "four");
            _r5 = new Radio("Save \\ Open", new Vector2(_offsetX + 64 * 4 + 256 * 4, 64), RadioType.big, null, null, "save");
            Radios = new GroupRadios(0, _r1, _r2, _r3, _r4, _r5);
        }

        public static void Update()
        {
            Radios.Update();
            _actualWin = Radios.stringSelected;
            if (_actualWin != null && _windows.ContainsKey(_actualWin) == true)
                _windows[_actualWin].Update();
        }

        public static void Draw()
        {
            Radios.Draw();
            if (_actualWin != null && _windows.ContainsKey(_actualWin) == true)
                _windows[_actualWin].Draw();
        }
    }
}