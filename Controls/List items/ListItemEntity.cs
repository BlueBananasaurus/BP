using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Monogame_GL
{
    public class ListItemEntity : ListItemBase, IHolderItem
    {
        private Texture2D _tex;

        public PropertyHolder properties;

        public ListItemEntity(int index, RectangleF listBoundary, Texture2D texture, int itemsPerLine, PropertyHolder property) : base(new Vector2(256 + 16, 256 + 16), index, listBoundary, itemsPerLine)
        {
            _tex = texture;

            properties = property;
        }

        public override void Update()
        {
            UpdateBase();
        }

        public override void Draw()
        {
            if (Selected == true)
                Game1.SpriteBatchGlobal.Draw(Game1.Textures["TileBackSelected"], _boundary.Position, sourceRectangle: new Rectangle(0, 0, 256 + 16, 256 + 16));
            else if (CompareF.RectangleVsVector2(Boundary, MouseInput.MouseRealPosMenu()) == true && CompareF.RectangleVsVector2(_listBoundary, MouseInput.MouseRealPosMenu()) == true)
            {
                Game1.SpriteBatchGlobal.Draw(Game1.Textures["TileBackHover"], _boundary.Position, sourceRectangle: new Rectangle(0, 0, 256 + 16, 256 + 16));
            }
            Game1.SpriteBatchGlobal.Draw(_tex, _boundary.Position + new Vector2(8));
        }
    }
}