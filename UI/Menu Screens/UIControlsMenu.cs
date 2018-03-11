using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Monogame_GL
{
    public class UIControlsMenu : ScreenBase, IWindow
    {
        private static UIControlsMenu instance;
        private Button BtnBack;
        private ListPage list;

        private UIControlsMenu() : base()
        {
            BtnBack = new Button(new Vector2(Globals.ButtonXPos, Globals.BackButtonPosY), "BACK", onBack, Globals.ChangeWindowTo, "Options", type: ButtonType.back);
            list = new ListPage(new RectangleF(512 + 256 + 32, 512 + 256 + 192, 512 + 64, 64));

            int index = 0;
            foreach (KeyValuePair<string, Keys> key in Game1.STP.ControlKeys)
            {
                list._items.Add(new ListItemClassic(key.Key, index, new RectangleF(736, 30, 32 + list.Boundary.Position.X, (index * 32) + list.Boundary.Position.Y)));
                index++;
            }
        }

        public void onBack()
        {
            list.CloseOthers(null);
            Game1.STP.Save();
        }

        public void Update()
        {
            BtnBack.Update();
            list.Update();
            UpdateBase();
        }

        public void Draw()
        {
            Game1.SpriteBatchGlobal.Begin(samplerState: SamplerState.PointWrap, sortMode: SpriteSortMode.Immediate);
            BtnBack.Draw();
            list.Draw();

            DrawBase("Options");
            Game1.SpriteBatchGlobal.End();
        }

        public static UIControlsMenu Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new UIControlsMenu();
                }
                return instance;
            }
        }
    }
}