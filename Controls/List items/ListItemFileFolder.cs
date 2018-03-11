using Microsoft.Xna.Framework;
using System.IO;

namespace Monogame_GL
{
    public class ListItemFileFolder : ListItemBase, IHolderItem
    {
        private string _name;
        public string Name { get; private set; }
        public FileFolder _treat;

        public ListItemFileFolder(int index, RectangleF listBoundary, string name, bool treatSimple = false, FileFolder treat = FileFolder.folder) : base(new Vector2(512 - 64, 28), index, listBoundary, 1)
        {
            Name = name;
            if (treatSimple == false)
                _name = Path.GetFileName(name).Limit(23);
            else
                _name = name;
            _treat = treat;
            if (Path.GetExtension(name) == ".json" && _treat == FileFolder.file)
                _treat = FileFolder.json;
        }

        public override void Update()
        {
            UpdateBase();
        }

        public override void Draw()
        {
            if (Selected == true)
            {
                Game1.SpriteBatchGlobal.DrawAtlas(Game1.Textures["FileBackground"], _boundary.Position, new Point(512 - 64, 28), new Point(0, 1));
                DrawString.DrawText(_name, _boundary.Position + new Vector2(64, 5), Align.left, new Color(255, 255, 255), FontType.small);
            }
            else
            {
                //if (CompareF.RectangleVsVector2(Boundary, MouseInput.MouseRealPosMenu()) == true && CompareF.RectangleVsVector2(_listBoundary, MouseInput.MouseRealPosMenu()) == true)
                //{
                //    Game1.SpriteBatch.DrawAtlas(Game1.Textures["SaveBackground"], _boundary.Position + new Vector2(0, 16), new Point(736, 256), new Point(0, 1));
                //}
                //else
                //{
                //    Game1.SpriteBatch.DrawAtlas(Game1.Textures["SaveBackground"], _boundary.Position + new Vector2(0, 16), new Point(736, 256), new Point(0, 0));

                //}
                if (Index % 2 == 0)
                {
                    Game1.SpriteBatchGlobal.DrawAtlas(Game1.Textures["FileBackground"], _boundary.Position, new Point(512 - 64, 28), new Point(0, 0));
                }
                DrawString.DrawText(_name, _boundary.Position + new Vector2(64, 5), Align.left, Globals.LightBlueText, FontType.small);
            }

            if (_treat == FileFolder.folder)
                Game1.SpriteBatchGlobal.DrawAtlas(Game1.Textures["FileFolder"], _boundary.Position + new Vector2(16, 5), new Point(28, 18), new Point(0, 0));
            if (_treat == FileFolder.json)
                Game1.SpriteBatchGlobal.DrawAtlas(Game1.Textures["FileFolder"], _boundary.Position + new Vector2(16, 5), new Point(28, 18), new Point(1, 0));
            if (_treat == FileFolder.file)
                Game1.SpriteBatchGlobal.DrawAtlas(Game1.Textures["FileFolder"], _boundary.Position + new Vector2(16, 5), new Point(28, 18), new Point(2, 0));
            if (_treat == FileFolder.drive)
                Game1.SpriteBatchGlobal.DrawAtlas(Game1.Textures["FileFolder"], _boundary.Position + new Vector2(16, 5), new Point(28, 18), new Point(3, 0));
        }
    }
}