using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.IO;

namespace Monogame_GL
{
    public class UILoadMenu : ScreenBase, IWindow
    {
        private static UILoadMenu _instance;
        private Button _btnBack;
        private Button _btnLoad;
        private ItemsHolder _saves;

        private UILoadMenu() : base()
        {
            _saves = new ItemsHolder(new RectangleF(512 + 256 + 32, 512 + 256 + 192, 512 + 64, 64), 1, 256 + 32, LoadLocked);
            _btnBack = new Button(new Vector2(Globals.ButtonXPos, Globals.BackButtonPosY), "BACK", null, Globals.ChangeWindowTo, "Main", type: ButtonType.back);
            _btnLoad = new Button(new Vector2(Globals.ButtonXPos + 1024 + 256, 64), "LOAD", null, Globals.ChangeWindowTo, "Main", ButtonType.small);

            List<string> paths = CustomSearcher.GetDirectories(Directory.GetCurrentDirectory() + "/Save", "*");
            if (paths.Count > 0)
                for (int i = 0; i < paths.Count; i++)
                {
                    string[] directories = paths[i].Split(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);
                    _saves.Items.Add(new ListItemLoad(new Point(0, i), _saves.Boundary, directories[directories.Length - 2] + "/" + directories[directories.Length - 1] + "/" + directories[directories.Length - 1] + ".png", directories[directories.Length - 1]));
                }
        }

        public void LoadLocked()
        {
            _btnLoad.LockState(_saves.SelectedIndexInt == null);
        }

        public void Update()
        {
            _btnBack.Update();
            _btnLoad.Update();
            _saves.Update();
            UpdateBase();
        }

        public void Draw()
        {
            Game1.SpriteBatchGlobal.Begin(samplerState: SamplerState.PointWrap, sortMode: SpriteSortMode.Immediate);

            _btnBack.Draw();
            _btnLoad.Draw();
            _saves.Draw();
            DrawBase("Load");

            Game1.SpriteBatchGlobal.End();
        }

        public static UILoadMenu Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new UILoadMenu();
                }
                return _instance;
            }
        }
    }
}