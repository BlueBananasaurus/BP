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
        private TextBox _horizontalWindow;
        private TextBox _verticalWindow;
        public CheckBox FullScreen { get; set; }
        public DropDown FilteringDrop;

        private UIGraphicsMenu() : base()
        {
            BtnBack = new Button(new Vector2(Globals.ButtonXPos, Globals.BackButtonPosY), "BACK", onBack, Globals.ChangeWindowTo, "Options", type: ButtonType.back);
            FullScreen = new CheckBox("Fullscreen:", new Vector2(512 + 64, 64), CheckBoxType.classic, false, Globals.FullscreenOn, Globals.FullscreenOff);
            _horizontal = new TextBox(Globals.Winsize.X.ToString(), 128, new Vector2(512 + 64 + 512, 64 + 64), textBoxType.number, null, 0, int.MaxValue);
            _vertical = new TextBox(Globals.Winsize.Y.ToString(), 128, new Vector2(512 + 64 + 512, 64 + 128), textBoxType.number, null, 0, int.MaxValue);
            _horizontalWindow = new TextBox(Globals.Winsize.X.ToString(), 128, new Vector2(512 + 64 + 512, 256), textBoxType.number, null, 0, int.MaxValue);
            _verticalWindow = new TextBox(Globals.Winsize.Y.ToString(), 128, new Vector2(512 + 64 + 512, 64 + 256), textBoxType.number, null, 0, int.MaxValue);
            FilteringDrop = new DropDown(typeof(Filtering), 256, new Vector2(512 + 64 + 512, 256 + 128),ChangeFilter);
            Reset();
        }

        public void onBack()
        {
            Game1.STP.Save();
        }

        public void ChangeFilter()
        {
            Globals.filter = (Filtering)FilteringDrop.SelectedIndex;
            GameDraw.UpdateFiltering();
        }

        public void Update()
        {
            BtnBack.Update();
            FullScreen.Update();
            _horizontal.Update();
            _vertical.Update();
            _horizontalWindow.Update();
            _verticalWindow.Update();
            FilteringDrop.Update();
            UpdateBase();
        }

        public void Draw()
        {
            Game1.SpriteBatchGlobal.Begin(samplerState: SamplerState.PointWrap, sortMode: SpriteSortMode.Immediate);
            BtnBack.Draw();
            FullScreen.Draw();
            _horizontal.Draw();
            _vertical.Draw();
            _horizontalWindow.Draw();
            _verticalWindow.Draw();
            DrawString.DrawText("Horizontal full resolution", new Vector2(512 + 64, 128 + 6), Align.left, Globals.LightBlueText, FontType.small);
            DrawString.DrawText("Vertical full resolution", new Vector2(512 + 64, 128 + 64 + 6), Align.left, Globals.LightBlueText, FontType.small);
            DrawString.DrawText("Horizontal window resolution", new Vector2(512 + 64, 256 + 6), Align.left, Globals.LightBlueText, FontType.small);
            DrawString.DrawText("Vertical widnow resolution", new Vector2(512 + 64, 256 + 64 + 6), Align.left, Globals.LightBlueText, FontType.small);
            DrawString.DrawText("Filtering", new Vector2(512 + 64, 256 + 128 + 12), Align.left, Globals.LightBlueText, FontType.small);
            FilteringDrop.Draw();
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