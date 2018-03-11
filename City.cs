using Microsoft.Xna.Framework;

namespace Monogame_GL
{
    public class City
    {
        public RectangleF Boundary { get; set; }
        public string MapLink { get; set; }
        public bool Procedural { get; set; }

        public City(Vector2 position, string mapLink, bool procedural)
        {
            Boundary = new RectangleF(new Vector2(32), position);
            MapLink = mapLink;
            Procedural = procedural;
        }

        public void GoInside()
        {
            Game1.State = GlobalState.inGame;
            if (Procedural == false)
            {
                Globals.LoadJson(ref Game1.mapLive, MapLink);
                Game1.mapLive.GenerateMapAndWater();
            }
            else
            {
                Game1.mapLive.ShowBoundary = true;
                Game1.mapLive.MapNpcs.Clear();
                Game1.mapLive.Generate();
                Game1.mapLive.GenerateMapAndWater();
                Game1.mapLive.mapCrates.Clear();
                Game1.mapLive.mapDecorations.Clear();
                Game1.mapLive.mapDecorationsLayer2.Clear();
                Game1.mapLive.MapMovables.Clear();
                Game1.mapLive.mapTriggers.Clear();
            }
        }

        public void Draw()
        {
            Game1.SpriteBatchGlobal.Draw(Game1.Textures["City"], Boundary.Position);
        }
    }
}
