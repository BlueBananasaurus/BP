using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.IO;

namespace Monogame_GL
{
    public class ListItemLoad : ListItemBase, IHolderItem
    {
        private Texture2D _preview;
        private string _saveName;

        public ListItemLoad(int index, RectangleF listBoundary, string screenShot, string saveName) : base(new Vector2(736, 256 + 32), index, listBoundary,1)
        {
            using (FileStream stream = new FileStream(screenShot, FileMode.Open))
            {
                _preview = Texture2D.FromStream(Game1.GraphicsGlobal.GraphicsDevice, stream);
            }

            _saveName = saveName;
        }

        public override void Update()
        {
            UpdateBase();
        }

        public override void Draw()
        {
            if (Selected == true)
            {
                Game1.SpriteBatchGlobal.DrawAtlas(Game1.Textures["SaveBackground"], _boundary.Position + new Vector2(0, 16), new Point(736, 256), new Point(0, 2));
            }
            else
            {
                if (CompareF.RectangleVsVector2(Boundary, MouseInput.MouseRealPosMenu()) == true && CompareF.RectangleVsVector2(_listBoundary, MouseInput.MouseRealPosMenu()) == true)
                {
                    Game1.SpriteBatchGlobal.DrawAtlas(Game1.Textures["SaveBackground"], _boundary.Position + new Vector2(0, 16), new Point(736, 256), new Point(0, 1));
                }
                else
                {
                    Game1.SpriteBatchGlobal.DrawAtlas(Game1.Textures["SaveBackground"], _boundary.Position + new Vector2(0, 16), new Point(736, 256), new Point(0, 0));
                }
            }

            Game1.SpriteBatchGlobal.Draw(_preview, _boundary.Position + new Vector2(16) + new Vector2(0, 16), scale: new Vector2((256f - 32f) / Globals.WinRenderSize.Y));

            DrawString.DrawText(_saveName, _boundary.Position + new Vector2(576, 16) + new Vector2(0, 16), Align.center, new Color(255, 255, 255), FontType.small);
        }
    }
}