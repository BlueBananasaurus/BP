using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Monogame_GL
{
    public class UIGameMenu : ScreenBase, IWindow
    {
        private static UIGameMenu instance;
        private Button BtnNewGame;
        private Button BtnLoadGame;
        private Button BtnEditor;
        private Button BtnOptions;
        private Button BtnExit;

        private UIGameMenu() : base()
        {
            BtnNewGame = new Button(new Vector2(Globals.ButtonXPos, 256), "NEW GAME", Globals.NewGame);
            BtnLoadGame = new Button(new Vector2(Globals.ButtonXPos, 256 + 96), "LOAD GAME", null, Globals.ChangeWindowTo, "Load");
            BtnEditor = new Button(new Vector2(Globals.ButtonXPos, 256 + 96 + 96), "EDITOR", Globals.EditorEnter);
            BtnOptions = new Button(new Vector2(Globals.ButtonXPos, 256 + 96 + 96 + 96), "OPTIONS", null, Globals.ChangeWindowTo, "Options");
            BtnExit = new Button(new Vector2(Globals.ButtonXPos, Globals.BackButtonPosY), "QUIT", Globals.Quit, type: ButtonType.back);
        }

        public void Update()
        {
            BtnNewGame.Update();
            BtnLoadGame.Update();
            BtnEditor.Update();
            BtnOptions.Update();
            BtnExit.Update();
            UpdateBase();
        }

        public void Draw()
        {
            Game1.SpriteBatchGlobal.Begin(samplerState: SamplerState.PointWrap, sortMode: SpriteSortMode.Immediate);
            BtnNewGame.Draw();
            BtnLoadGame.Draw();
            BtnEditor.Draw();
            BtnOptions.Draw();
            BtnExit.Draw();
            DrawBase("");
            Game1.SpriteBatchGlobal.End();
        }

        public static UIGameMenu Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new UIGameMenu();
                }
                return instance;
            }
        }
    }
}