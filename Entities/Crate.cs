using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Monogame_GL
{
    public class Crate : NpcBase, Inpc
    {
        public int Tissue;
        public int Electro;
        public int HighTissue;
        public int HighElectro;

        public bool Friendly { get;}

        public Crate(Vector2 position, int tissue, int electro, int highTissue, int highElectro)
        {
            Boundary = new RectangleF(new Vector2(64), position);
            _resolver = new CollisionResolver(Globals.TileSize, Game1.mapLive);
            Tissue = tissue;
            Electro = electro;
            HighTissue = highTissue;
            HighElectro = highElectro;
            _velocity = new Vector2(0);
            _weight = 2;
            Friendly = false;
            Health = 25;
        }

        public void Open()
        {
            for (byte i = 0; i < Tissue; i++)
            {
                Globals.AddPick(new Coin(Boundary.Origin, new Vector2((float)(Globals.GlobalRandom.NextDouble() - 0.5f), (float)(Globals.GlobalRandom.NextDouble() - 0.5f)), Pickups.tissue));
            }
            for (byte i = 0; i < Electro; i++)
            {
                Globals.AddPick(new Coin(Boundary.Origin, new Vector2((float)(Globals.GlobalRandom.NextDouble() - 0.5f), (float)(Globals.GlobalRandom.NextDouble() - 0.5f)), Pickups.electronics));
            }
            for (byte i = 0; i < HighTissue; i++)
            {
                Globals.AddPick(new Coin(Boundary.Origin, new Vector2((float)(Globals.GlobalRandom.NextDouble() - 0.5f), (float)(Globals.GlobalRandom.NextDouble() - 0.5f)), Pickups.highTissue));
            }
            for (byte i = 0; i < HighElectro; i++)
            {
                Globals.AddPick(new Coin(Boundary.Origin, new Vector2((float)(Globals.GlobalRandom.NextDouble() - 0.5f), (float)(Globals.GlobalRandom.NextDouble() - 0.5f)), Pickups.highElectronics));
            }
        }

        public override void Draw()
        {
            Game1.SpriteBatchGlobal.Draw(Game1.Textures["Crate"], Boundary.Position, scale: new Vector2(2));
        }

        public void Update(List<Inpc> npcs)
        {
            _resolver.move(ref _velocity, new Vector2(2), Boundary, 0.1f, new Vector2(0.1f), new Vector2(0.05f), new Vector2(0.2f), Game1.mapLive.MapMovables, buoyancyInWater: 0.2f);

            if (Health <= 0)
            {
                Open();
                Game1.mapLive.MapNpcs.Remove(this);
            }
        }
    }
}