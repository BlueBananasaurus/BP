using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Monogame_GL
{
    public class UIOptionsMenu : ScreenBase, IWindow
    {
        private static UIOptionsMenu instance;
        private Button BtnBack;
        private Button BtnAudio;
        private Button BtnGraphics;
        private Button BtnControls;

        private UIOptionsMenu() : base()
        {
            BtnAudio = new Button(new Vector2(Globals.ButtonXPos, 256), "AUDIO", null, Globals.ChangeWindowTo, "Audio");
            BtnGraphics = new Button(new Vector2(Globals.ButtonXPos, 256 + 96), "GRAPHICS", null, Globals.ChangeWindowTo, "Graphics");
            BtnControls = new Button(new Vector2(Globals.ButtonXPos, 256 + 96 * 2), "CONTROLS", null, Globals.ChangeWindowTo, "Controls");
            BtnBack = new Button(new Vector2(Globals.ButtonXPos, Globals.BackButtonPosY), "BACK", null, Globals.ChangeWindowTo, "Main", type: ButtonType.back);
        }

        public void Update()
        {
            BtnBack.Update();
            BtnGraphics.Update();
            BtnAudio.Update();
            BtnControls.Update();
            UpdateBase();
        }

        public void Draw()
        {
            Game1.SpriteBatchGlobal.Begin(samplerState: SamplerState.PointWrap, sortMode: SpriteSortMode.Immediate);
            BtnBack.Draw();
            BtnAudio.Draw();
            BtnGraphics.Draw();
            BtnControls.Draw();
            DrawBase("Options");
            Game1.SpriteBatchGlobal.End();
        }

        public static UIOptionsMenu Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new UIOptionsMenu();
                }
                return instance;
            }
        }
    }
}