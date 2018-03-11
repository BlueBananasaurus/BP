using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Monogame_GL
{
    public class UIContinueMenu : ScreenBase, IWindow
    {
        private static UIContinueMenu instance;
        private Button BtnContinue;
        private Button BtnLoadGame;
        private Button BtnExitToMenuGame;
        private Button BtnOptions;
        private Button BtnExit;

        private UIContinueMenu() : base()
        {
            BtnContinue = new Button(new Vector2(Globals.ButtonXPos, 256), "CONTINUE", Globals.HideWindow);
            BtnLoadGame = new Button(new Vector2(Globals.ButtonXPos, 256 + 96), "SAVE \\ LOAD", null, Globals.ChangeWindowTo, "Load");
            BtnOptions = new Button(new Vector2(Globals.ButtonXPos, 256 + 96 + 96), "OPTIONS", null, Globals.ChangeWindowTo, "Options");
            BtnExit = new Button(new Vector2(Globals.ButtonXPos, Globals.BackButtonPosY), "QUIT", Globals.Quit, type: ButtonType.back);
            BtnExitToMenuGame = new Button(new Vector2(Globals.ButtonXPos, 256 + 96 + 96 + 96), "MAIN MENU", EndCurrentGame, Globals.ChangeWindowTo, "Main");
        }

        public void EndCurrentGame()
        {
            Game1.GameInProgress = false;
        }

        public void Update()
        {
            BtnContinue.Update();
            BtnLoadGame.Update();
            BtnOptions.Update();
            BtnExit.Update();
            BtnExitToMenuGame.Update();

            UpdateBase();
        }

        public void Draw()
        {
            Game1.SpriteBatchGlobal.Begin(samplerState: SamplerState.PointWrap, sortMode: SpriteSortMode.Immediate);

            BtnContinue.Draw();
            BtnLoadGame.Draw();
            BtnOptions.Draw();
            BtnExit.Draw();
            BtnExitToMenuGame.Draw();
            DrawBase("Main menu");
            Game1.SpriteBatchGlobal.End();
        }

        public static UIContinueMenu Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new UIContinueMenu();
                }
                return instance;
            }
        }
    }
}