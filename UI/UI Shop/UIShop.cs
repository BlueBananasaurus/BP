using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
namespace Monogame_GL
{
    public class UIShop
    {
        private static UIShop _instance;
        private ItemsHolder _holder;
        public ItemsHolder Invetory;
        private Button _buy;

        private UIShop()
        {
            Invetory = new ItemsHolder(new RectangleF(new Vector2(224 * 3 + 64, 48 * 4 * 4), new Vector2(160, 128)), 3, 224, ChangedSelection, ItemHolderTypes.blue);
            _holder = new ItemsHolder(new RectangleF(new Vector2(224 * 3 + 64, 48 * 4 * 4), new Vector2(1024, 128)), 3, 224+96, ChangedSelection, ItemHolderTypes.blue);
            _holder.Items.Add(new ListItemPart(_holder.Items.Count, _holder.Boundary, Module.tier2, 2000, 2000));
            _holder.Items.Add(new ListItemPart(_holder.Items.Count, _holder.Boundary, Module.tier3, 2000, 2000));
            _holder.Items.Add(new ListItemPart(_holder.Items.Count, _holder.Boundary, Module.test, 100, 20));
            _buy = new Button(new Vector2(1024, 128 + 48 * 4 * 4+16), "Buy", Buy, null, null, ButtonType.small, true);
            _holder.Update();
            Invetory.Update();
            _buy.Update();
        }

        public void Update()
        {
            _buy.Update();
            _holder.Update();
            Invetory.Update();

            if(_holder.SelectedIndex != null && ((_holder.Items[_holder.SelectedIndex.Value] as ListItemPart).Tissue <= Game1.PlayerInstance.Tissue && (_holder.Items[_holder.SelectedIndex.Value] as ListItemPart).Electronics <= Game1.PlayerInstance.Electronics))
            {
                _buy.LockState(false);
            }
            else
            {
                _buy.LockState(true);
            }
        }

        public void Buy()
        {
            Module type = (_holder.Items[_holder.SelectedIndex.Value] as ListItemPart).Type;

            Game1.PlayerInstance.Tissue -= (_holder.Items[_holder.SelectedIndex.Value] as ListItemPart).Tissue;
            Game1.PlayerInstance.Electronics -= (_holder.Items[_holder.SelectedIndex.Value] as ListItemPart).Electronics;

            if (Game1.PlayerInstance.Items.Any(i => i.Type == type) == true)
            {
                Game1.PlayerInstance.Items.Single(i => i.Type == type).Amount++;
            }
            else
            {
                Game1.PlayerInstance.Items.Add(new PlayersItem(type, 1));
            }

            Sync();
        }

        public void Sync()
        {
            Invetory.Items.Clear();
            UIAssembly.Instance.Inventory.Items.Clear();

            foreach (PlayersItem item in Game1.PlayerInstance.Items)
            {
                Invetory.Items.Add(new ListItemPartInventory(Invetory.Items.Count, Invetory.Boundary, item.Type, item.Amount));
                UIAssembly.Instance.Inventory.Items.Add(new ListItemPartInventory(UIAssembly.Instance.Inventory.Items.Count, UIAssembly.Instance.Inventory.Boundary, item.Type, item.Amount));
            }
        }

        public void ChangedSelection()
        {

        }

        public void Draw()
        {
            Game1.SpriteBatchGlobal.DrawAtlas(Game1.tissue, new Vector2(160, 80-8), new Point(32), new Point(0, 0));
            DrawString.DrawText(Game1.PlayerInstance.Tissue.ToString(), new Vector2(160+48, 80), Align.left, Color.White, FontType.small);

            Game1.SpriteBatchGlobal.DrawAtlas(Game1.electronics, new Vector2(160, 24), new Point(32), new Point(1, 0));
            DrawString.DrawText(Game1.PlayerInstance.Electronics.ToString(), new Vector2(160+48, 32), Align.left, Color.White, FontType.small);

            DrawString.DrawText("Inventory", new Vector2(736/2+160,56), Align.center, Color.White, FontType.small);
            DrawString.DrawText("Shop", new Vector2(736 / 2+1024, 56), Align.center, Color.White, FontType.small);
            _holder.Draw(Game1.MenuParts);
            Invetory.Draw(Game1.MenuParts);
            _buy.Draw();
        }

        public static UIShop Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new UIShop();
                }
                return _instance;
            }
        }
    }
}