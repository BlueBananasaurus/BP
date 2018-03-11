using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Monogame_GL
{
    public class UIGraphicsMenu : ScreenBase, IWindow
    {
        private static UIGraphicsMenu instance;
        private Button BtnBack;
        private TextBox _horizontal;
        private TextBox _vertical;
        public CheckBox FullScreen { get; set; }

        private UIGraphicsMenu() : base()
        {
            BtnBack = new Button(new Vector2(Globals.ButtonXPos, Globals.BackButtonPosY), "BACK", onBack, Globals.ChangeWindowTo, "Options", type: ButtonType.back);
            FullScreen = new CheckBox("Fullscreen:", new Vector2(512 + 64, 64), CheckBoxType.classic, false, Globals.FullscreenOn, Globals.FullscreenOff);
            _horizontal = new TextBox(Globals.Winsize.X.ToString(), 128, new Vector2(512 + 64 + 512, 64 + 64 + 6), textBoxtype.number, null, 0, int.MaxValue);
            _vertical = new TextBox(Globals.Winsize.Y.ToString(), 128, new Vector2(512 + 64 + 512, 64 + 128 + 6), textBoxtype.number, null, 0, int.MaxValue);
            Reset();
        }

        public void onBack()
        {
            Game1.STP.Save();
        }

        public void Update()
        {
            BtnBack.Update();
            FullScreen.Update();
            _horizontal.Update();
            _vertical.Update();
            UpdateBase();
        }

        public void Draw()
        {
            Game1.SpriteBatchGlobal.Begin(samplerState: SamplerState.PointWrap, sortMode: SpriteSortMode.Immediate);
            BtnBack.Draw();
            FullScreen.Draw();
            _horizontal.Draw();
            _vertical.Draw();
            DrawString.DrawText("Horizontal resolution", new Vector2(512 + 64, 128 + 12), Align.left, Globals.LightBlueText, FontType.small);
            DrawString.DrawText("vertical resolution", new Vector2(512 + 64, 128 + 64 + 12), Align.left, Globals.LightBlueText, FontType.small);
            DrawBase("Graphics");
            Game1.SpriteBatchGlobal.End();
        }

        public static UIGraphicsMenu Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new UIGraphicsMenu();
                }
                return instance;
            }
        }
    }
}