using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Monogame_GL
{
    public class UIAudio : ScreenBase, IWindow
    {
        private static UIAudio _instance;
        private Button _BtnBack;
        private CheckBox _mute;

        private UIAudio() : base()
        {
            _BtnBack = new Button(new Vector2(Globals.ButtonXPos, Globals.BackButtonPosY), "BACK", onBack, Globals.ChangeWindowTo, "Options", type: ButtonType.back);
            _mute = new CheckBox("All sounds:", new Vector2(512 + 64, 64), CheckBoxType.sound, false, Globals.Unmute, Globals.Mute);
        }

        public void onBack()
        {
            Game1.STP.Save();
        }

        public void Update()
        {
            _BtnBack.Update();
            _mute.Update();
            UpdateBase();
        }

        public void Draw()
        {
            Game1.SpriteBatchGlobal.Begin(samplerState: SamplerState.PointWrap, sortMode: SpriteSortMode.Immediate);
            _BtnBack.Draw();
            _mute.Draw();

            DrawBase("Audio");
            Game1.SpriteBatchGlobal.End();
        }

        public static UIAudio Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new UIAudio();
                }
                return _instance;
            }
        }
    }
}