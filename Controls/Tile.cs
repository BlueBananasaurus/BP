using Microsoft.Xna.Framework;

namespace Monogame_GL
{
    public class Tile
    {
        public string TextureName { get; set; }
        public Point IndexInPicture { get; set; }
        public string Code { get; set; }

        public Tile(string name, Point index, string code)
        {
            TextureName = name;
            IndexInPicture = index;
            Code = code;
        }
    }
}