using Microsoft.Xna.Framework;

namespace Monogame_GL
{
    public class CrateStatic : ICrate
    {
        public RectangleF Boundary { get; set; }
        public byte Tissue;
        public byte Electro;
        public byte HighTissue;
        public byte HighElectro;

        public CrateStatic(Vector2 position, byte tissue, byte electro, byte highTissue, byte highElectro)
        {
            Boundary = new RectangleF(new Vector2(64), position);
            Tissue = tissue;
            Electro = electro;
            HighTissue = highTissue;
            HighElectro = highElectro;
        }

        public void Update()
        {
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

            //Game1.mapLive.mapDecorations.Add(new DecorationStatic(Boundary.Position, Game1.LootCrateStain, null,new Rectangle(64, 0, 64, 64)));
            Game1.mapLive.mapCrates.Remove(this);
        }

        public void DrawNormal()
        {
            DrawEntities.DrawCrateNormal(Boundary.Position);
        }

        public void Draw()
        {
            DrawEntities.DrawCrate(Boundary.Position);
        }
    }
}