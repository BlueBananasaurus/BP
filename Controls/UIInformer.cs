using Microsoft.Xna.Framework;

namespace Monogame_GL
{
    public class UIInformer
    {
        private string _message;
        private float _SizeX;
        private float _opacity;
        public Vector2 Position { get; set; }

        public UIInformer(string message)
        {
            _message = message;
            _SizeX = DrawString.MeasureText(message, 16);
            _opacity = 1f;
        }

        public void Update()
        {
            _opacity -= Game1.Delta / 2048f;
            if (_opacity < 0)
            {
                Game1.InfoList.Remove(this);
            }
        }

        public void Draw()
        {
            Game1.EffectAlpha.Parameters["A"].SetValue(_opacity);
            Game1.EffectAlpha.CurrentTechnique.Passes[0].Apply();
            Game1.SpriteBatchGlobal.Draw(Game1.Info, new Rectangle((int)Position.X, (int)Position.Y, (int)_SizeX + 8, 26), Color.White);
            DrawString.DrawText(_message, Position + new Vector2(4), Align.left, new Color(255, 150, 100), FontType.small);
            Game1.EffectBaseColor.CurrentTechnique.Passes[0].Apply();
        }
    }
}