using Microsoft.Xna.Framework;

namespace Monogame_GL
{
    public class ListItemPartInventory : ListItemBase, IHolderItem
    {
        public Module Type { get; set; }
        public byte Amount { get; set; }

        public ListItemPartInventory(int index, RectangleF listBoundary, Module type, byte amount) : base(new Vector2(224,224), index, listBoundary,3)
        {
            Type = type;
            Amount = amount;
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

            if (Amount > 1)
            DrawString.DrawText(Amount.ToString()+"X", _boundary.Position + new Vector2(15 + 192 - 32), Align.center, Color.White, FontType.small);
        }
    }
}