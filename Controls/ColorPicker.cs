using Microsoft.Xna.Framework;

namespace Monogame_GL
{
    public class ColorPicker
    {
        private Graber _grabRed;
        private Graber _grabGreen;
        private Graber _grabBlue;
        private TextBox _texRed;
        private TextBox _texGreen;
        private TextBox _texBlue;
        private Vector2 _position;

        public ColorPicker(Vector2 position)
        {
            _grabRed = new Graber(position + new Vector2(64, 40), 0, Game1.Textures["ColorBarRed"], UpdateRedText);
            _grabGreen = new Graber(position + new Vector2(64, 64) + new Vector2(0, 40), 0, Game1.Textures["ColorBarGreen"], UpdateGreenText);
            _grabBlue = new Graber(position + new Vector2(64, 64) + new Vector2(0, 40 + 64), 0, Game1.Textures["ColorBarBlue"], UpdateBlueText);
            _texRed = new TextBox(_grabRed.GetValue().ToString(), 64, position + new Vector2(256 + 96, 50), textBoxType.number, UpdateRed, 0, 255);
            _texGreen = new TextBox(_grabRed.GetValue().ToString(), 64, position + new Vector2(256 + 96, 50 + 64), textBoxType.number, UpdateGreen, 0, 255);
            _texBlue = new TextBox(_grabRed.GetValue().ToString(), 64, position + new Vector2(256 + 96, 50 + 128), textBoxType.number, UpdateBlue, 0, 255);
            _position = position;
        }

        public void Update()
        {
            _grabRed.Update();
            _grabGreen.Update();
            _grabBlue.Update();
            _texRed.Update();
            _texGreen.Update();
            _texBlue.Update();
        }

        private void UpdateRed()
        {
            if (_texRed.getParsedValue() != null && _texRed.Valid == true)
                _grabRed.SetValue(_texRed.getParsedValue().Value);
            else
                _grabRed.SetValue(0);
        }

        private void UpdateGreen()
        {
            if (_texGreen.getParsedValue() != null && _texGreen.Valid == true)
                _grabGreen.SetValue(_texGreen.getParsedValue().Value);
            else
                _grabGreen.SetValue(0);
        }

        private void UpdateBlue()
        {
            if (_texBlue.getParsedValue() != null && _texBlue.Valid == true)
                _grabBlue.SetValue(_texBlue.getParsedValue().Value);
            else
                _grabBlue.SetValue(0);
        }

        private void UpdateRedText(byte value)
        {
            _texRed.SetText(value.ToString());
        }

        private void UpdateGreenText(byte value)
        {
            _texGreen.SetText(value.ToString());
        }

        private void UpdateBlueText(byte value)
        {
            _texBlue.SetText(value.ToString());
        }

        public Vector3 GetColor()
        {
            return new Vector3(_grabRed.GetValue(), _grabGreen.GetValue(), _grabBlue.GetValue());
        }

        public void Draw()
        {
            Game1.SpriteBatchGlobal.Draw(Game1.Textures["PanelBackground"], position: _position, sourceRectangle: new Rectangle(new Point(0, 0), new Point(512 + 128, 256)));
            _grabRed.Draw();
            _grabGreen.Draw();
            _grabBlue.Draw();
            _texRed.Draw();
            _texGreen.Draw();
            _texBlue.Draw();
            Game1.SpriteBatchGlobal.Draw(Game1.Textures["ColorPreviewPanel"], _position + new Vector2(256 + 128, 34) + new Vector2(64, 32));
            Game1.SpriteBatchGlobal.Draw(Game1.Textures["ColorPanelBlank"], _position + new Vector2(256 + 128, 34) + Vector2.One + new Vector2(64, 32), new Color((byte)_grabRed.GetValue(), (byte)_grabGreen.GetValue(), (byte)_grabBlue.GetValue()));
            DrawRectangleBoundary.DrawBlue(new Rectangle(_position.ToPoint(), new Point(512 + 128, 256)));
        }
    }
}