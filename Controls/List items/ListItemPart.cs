using Microsoft.Xna.Framework;

namespace Monogame_GL
{
    public class ListItemPart : ListItemBase, IHolderItem
    {
        public Module Type { get; set; }
        public int Electronics { get; set; }
        public int Tissue { get; set; }

        public ListItemPart(int index, RectangleF listBoundary, Module type, int electronics, int tissue) : base(new Vector2(224,224+96), index, listBoundary,3)
        {
            Type = type;
            Electronics = electronics;
            Tissue = tissue;
        }

        public override void Update()
        {
            UpdateBase();
        }

        public override void Draw()
        {
            if(Selected == true)
            {
                Game1.SpriteBatchGlobal.Draw(Game1.Textures["ShopItemSelected"], _boundary.Position, scale: new Vector2(1));
            }

            Game1.SpriteBatchGlobal.Draw(Game1.Textures["ShopItemBack"], _boundary.Position, scale: new Vector2(1));

            switch (Type)
            {
                case Module.test:
                    Game1.SpriteBatchGlobal.Draw(Game1.Textures["Part"], _boundary.Position + new Vector2(15), scale: new Vector2(2));
                    break;
                case Module.tier2:
                    Game1.SpriteBatchGlobal.Draw(Game1.Textures["Tier2"], _boundary.Position + new Vector2(15));
                    break;
                case Module.tier3:
                    Game1.SpriteBatchGlobal.Draw(Game1.Textures["Tier3"], _boundary.Position + new Vector2(15));
                    break;
            }

            Game1.SpriteBatchGlobal.DrawAtlas(Game1.electronics, _boundary.Position + new Vector2(32, 128 + 96 -6), new Point(32), new Point(1, 0));
            DrawString.DrawText(Electronics.ToString(), _boundary.Position + new Vector2(32 + 32 + 16, 128 + 96 + 2), Align.left, Color.White, FontType.small);

            Game1.SpriteBatchGlobal.DrawAtlas(Game1.tissue, _boundary.Position + new Vector2(32,128+96+42), new Point(32), new Point(0, 0));
            DrawString.DrawText(Tissue.ToString(), _boundary.Position + new Vector2(32+32+16, 128 + 96+2+48), Align.left, Color.White, FontType.small);
        }
    }
}