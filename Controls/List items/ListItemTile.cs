using Microsoft.Xna.Framework;

namespace Monogame_GL
{
    public class ListItemTile : ListItemBase, IHolderItem
    {
        private Tile _tile;

        public ListItemTile(int index, RectangleF listBoundary, Tile tile) : base(new Vector2(40, 40), index, listBoundary,14)
        {
            _tile = tile;
        }

        public override void Update()
        {
            UpdateBase();
            if (Selected && Editor.SelectedEditTile != _tile.Code) Editor.SelectedEditTile = _tile.Code;
        }

        public override void Draw()
        {
            if (Selected == true)
                Game1.SpriteBatchGlobal.Draw(Game1.Textures["TileBackSelected"], _boundary.Position, Color.White);
            else if (CompareF.RectangleVsVector2(Boundary, MouseInput.MouseRealPosMenu()) == true && CompareF.RectangleVsVector2(_listBoundary, MouseInput.MouseRealPosMenu()) == true)
            {
                Game1.SpriteBatchGlobal.Draw(Game1.Textures["TileBackHover"], _boundary.Position, Color.White);
            }
            Game1.SpriteBatchGlobal.Draw(Game1.Textures[_tile.TextureName], _boundary.Position + new Vector2(4), sourceRectangle: new Rectangle(new Point(_tile.IndexInPicture.X * 32, _tile.IndexInPicture.Y * 32), new Point(32)), scale: new Vector2(1));
        }
    }
}