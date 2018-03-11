using Microsoft.Xna.Framework;

namespace Monogame_GL
{
    public class Coin : PhysicEntity, IPickable
    {
        private Pickups _value;
        private byte _variation;

        public Coin(Vector2 origin, Vector2 velocity, Pickups value)
        {
            Velocity = velocity;
            Boundary = new RectangleF(new Vector2(32), origin - new Vector2(16));
            _resolver = new CollisionResolver(Globals.TileSize);
            _value = value;
            _variation = (byte)Globals.GlobalRandom.Next(3);
        }

        public void Update()
        {
            _resolver.move(ref _velocity, new Vector2(2f), Boundary, 0.5f, new Vector2(0.1f), new Vector2(0.005f), new Vector2(0.3f), Game1.mapLive.MapMovables);
            Pick();
        }

        public override void Draw()
        {
            if (_value == Pickups.tissue)
            {
                Game1.SpriteBatchGlobal.DrawAtlas(Game1.tissue, Boundary.Position, new Point(32), new Point(_variation, 0));
            }
            else if (_value == Pickups.electronics)
            {
                Game1.SpriteBatchGlobal.DrawAtlas(Game1.electronics, Boundary.Position, new Point(32), new Point(_variation, 0));
            }
            else if (_value == Pickups.highTissue)
            {
                Game1.SpriteBatchGlobal.DrawAtlas(Game1.Textures["TissueHigh"], Boundary.Position, new Point(32), new Point(_variation, 0));
            }
            else if (_value == Pickups.highElectronics)
            {
                Game1.SpriteBatchGlobal.DrawAtlas(Game1.Textures["ElectroHigh"], Boundary.Position, new Point(32), new Point(_variation, 0));
            }
        }

        public void Pick()
        {
            if (CompareF.RectangleFVsRectangleF(Game1.PlayerInstance.Boundary, Boundary) == true)
            {
                if (_value == Pickups.tissue)
                {
                    Game1.PlayerInstance.AddTissue(10);
                }
                else if (_value == Pickups.electronics)
                {
                    Game1.PlayerInstance.AddElectronics(10);
                }
                else if (_value == Pickups.highTissue)
                {
                    Game1.PlayerInstance.AddTissue(50);
                }
                else if (_value == Pickups.highElectronics)
                {
                    Game1.PlayerInstance.AddElectronics(50);
                }

                Sound.PlaySound(Game1.soundCoin,CustomMath.RandomAroundZero(Globals.GlobalRandom)/8f);
                Game1.mapLive.mapPickables.Remove(this);
            }
        }

        public override void DrawLight()
        {
            Game1.SpriteBatchGlobal.Draw(Game1.Textures["LightPick"], Boundary.Origin, origin: new Vector2(64));
        }
    }
}