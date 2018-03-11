using Microsoft.Xna.Framework;

namespace Monogame_GL
{
    public class UISettings : IWindow
    {
        private static UISettings instance;
        private Button _buttonResize;
        private Button _buttonClear;
        private TextBox _topBox;
        private TextBox _bottomBox;
        private TextBox _leftBox;
        private TextBox _rightBox;
        private TextBox _horizontalBox;
        private TextBox _verticalBox;
        private Button _buttonNew;
        private ColorPicker _picker;

        private UISettings()
        {
            _topBox = new TextBox("0", 128, new Vector2((Globals.WinRenderSize.X - 128) / 2 + 256, 256 + 12), textBoxType.number, null, -128, 128);
            _bottomBox = new TextBox("0", 128, new Vector2((Globals.WinRenderSize.X - 128) / 2 + 256, 256 + 12 + 512), textBoxType.number, null, -128, 128);
            _leftBox = new TextBox("0", 128, new Vector2((Globals.WinRenderSize.X - 128) / 2 - 256 + 256, 256 + 12 + 256), textBoxType.number, null, -128, 128);
            _buttonResize = new Button(new Vector2((Globals.WinRenderSize.X - 128) / 2 + 256, (Globals.WinRenderSize.Y - 32) / 2), "Resize", Resize, null, null, ButtonType.small);
            _buttonClear = new Button(new Vector2((Globals.WinRenderSize.X - 128) / 2 + 256, (Globals.WinRenderSize.Y - 32) / 2 + 64), "Clear", ResetBoxes, null, null, ButtonType.small);
            _rightBox = new TextBox("0", 128, new Vector2((Globals.WinRenderSize.X - 128) / 2 + 256 + 256, 256 + 12 + 256), textBoxType.number, null, -128, 128);

            _horizontalBox = new TextBox(Editor.EditMap.FunctionTileMap.GetLength(0).ToString(), 128, new Vector2(512 + 32, 128 + 64 - 6), textBoxType.number, null, 1, int.MaxValue);
            _verticalBox = new TextBox(Editor.EditMap.FunctionTileMap.GetLength(1).ToString(), 128, new Vector2(512 + 32, 256 - 6), textBoxType.number, null, 1, int.MaxValue);

            _buttonNew = new Button(new Vector2(512 + 32, 256 - 6 + 64), "New", null, null, null, ButtonType.small);
            _picker = new ColorPicker(new Vector2(32, 256 + 128 - 6 + 64));
        }

        private void Resize()
        {
            int left = 0;
            if (int.TryParse(_leftBox.Text, out left) == true)
            {
            }

            int right = 0;
            if (int.TryParse(_rightBox.Text, out right) == true)
            {
            }

            int top = 0;
            if (int.TryParse(_topBox.Text, out top) == true)
            {
            }

            int bottom = 0;
            if (int.TryParse(_bottomBox.Text, out bottom) == true)
            {
            }

            Editor.EditMap.BackgroundTileMap = Editor.EditMap.BackgroundTileMap.ResizeArray("_blank_", left, top, right, bottom);
            Editor.EditMap.ForegroundTileMap = Editor.EditMap.ForegroundTileMap.ResizeArray("_blank_", left, top, right, bottom);
            Editor.EditMap.FunctionTileMap = Editor.EditMap.FunctionTileMap.ResizeArray("_blank_", left, top, right, bottom);
            Editor.SetMapSize(new Point(Editor.EditMap.FunctionTileMap.GetLength(0), Editor.EditMap.FunctionTileMap.GetLength(1)));

            _horizontalBox.SetText(Editor.EditMap.FunctionTileMap.GetLength(0).ToString());
            _verticalBox.SetText(Editor.EditMap.FunctionTileMap.GetLength(1).ToString());
        }

        private void ResetBoxes()
        {
            _leftBox.SetText(0);
            _rightBox.SetText(0);
            _topBox.SetText(0);
            _bottomBox.SetText(0);
        }

        public void Reset()
        {
        }

        public void Update()
        {
            _topBox.Update();
            _bottomBox.Update();
            _leftBox.Update();
            _rightBox.Update();
            _buttonResize.Update();
            _buttonClear.Update();
            _horizontalBox.Update();
            _verticalBox.Update();
            _buttonNew.Update();
            _picker.Update();
            _buttonNew.LockState(!_horizontalBox.Valid || !_verticalBox.Valid);
        }

        public void Draw()
        {
            Game1.SpriteBatchGlobal.Draw(Game1.Textures["Paper"], new Vector2((Globals.WinRenderSize.X - 512) / 2 + 256, (Globals.WinRenderSize.Y - 512) / 2));
            _topBox.Draw();
            _bottomBox.Draw();
            _leftBox.Draw();
            _rightBox.Draw();
            _buttonResize.Draw();
            _buttonClear.Draw();
            _horizontalBox.Draw();
            _verticalBox.Draw();
            _buttonNew.Draw();
            _picker.Draw();

            DrawString.DrawText("Horizontal size:", new Vector2(32, 128 + 64), Align.left, Globals.LightBlueText, FontType.small);
            DrawString.DrawText("Vertical size:", new Vector2(32, 256), Align.left, Globals.LightBlueText, FontType.small);
            DrawString.DrawText("Global light color:", new Vector2(32, 256 + 128), Align.left, Globals.LightBlueText, FontType.small);
        }

        public static UISettings Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new UISettings();
                }
                return instance;
            }
        }
    }
}