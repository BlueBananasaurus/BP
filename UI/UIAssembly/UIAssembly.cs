using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Monogame_GL
{
    public class UIAssembly
    {
        private static UIAssembly _instance;
        public Grid GridCraft { get; set; }
        public ItemsHolder Inventory { get; set; }
        private List<GridItem> _items;
        private float XposText;
        private float XposTextBase;
        private float XposTextBonus;
        private float YposText;

        private UIAssembly()
        {
            _items = new List<GridItem>();
            Inventory = new ItemsHolder(new RectangleF(new Vector2(224*3 + 64, 48 * 4 * 4), new Vector2(1024, 128)),3,224, ChangedSelection, ItemHolderTypes.purple);
            GridCraft = new Grid(new Point(6), new Vector2(1024/2 -(6*48)/2));
            XposText = new Vector2(1024 / 2 - (6 * 48) / 2).X;
            XposTextBase = XposText + 128 + 64;
            XposTextBonus = XposText + 256 + 64;
            YposText = 800;
        }

        public void SwitchSelectedItem(ItemsHolder holder)
        {
            IHolderItem thing = null;
            if (holder.Items.Any(s => (s as ListItemPartInventory).Type == MouseItem.Item.Type) == true)
                thing = holder.Items.Single(s => (s as ListItemPartInventory).Type == MouseItem.Item.Type);

            if (thing != null)
            {
                (thing as ListItemPartInventory).Amount++;
            }
            else
            {
                holder.Items.Add(new ListItemPartInventory(holder.Items.Count, holder.Boundary, MouseItem.Item.Type, 1));
            }
        }

        public void ChangedSelection()
        {
            if (Inventory.SelectedIndex != null)
            {
                if(MouseItem.Item != null)
                {
                    SwitchSelectedItem(Inventory);
                    SwitchSelectedItem(UIShop.Instance.Invetory);
                }

                MouseItem.Item = new GridItem(0, Point.Zero, (Inventory.Items[Inventory.SelectedIndex.Value] as ListItemPartInventory).Type);

                IHolderItem thing = UIShop.Instance.Invetory.Items.Single(s => (s as ListItemPartInventory).Type == MouseItem.Item.Type);
                (thing as ListItemPartInventory).Amount--;

                if ((Inventory.Items[Inventory.SelectedIndex.Value] as ListItemPartInventory).Amount == 1)
                {
                    Inventory.Items.RemoveAt(Inventory.SelectedIndex.Value);
                    foreach(IHolderItem item in Inventory.Items)
                    {
                        item.UpdateIndex(Inventory.Items);
                    }
                }
                else if ((thing as ListItemPartInventory).Amount == 0)
                {
                    UIShop.Instance.Invetory.Items.Remove(thing);
                }
                else
                {
                    (Inventory.Items[Inventory.SelectedIndex.Value] as ListItemPartInventory).Amount--;
                }

                Inventory.ResetChoosed();
            }
            else MouseItem.Item = null;
        }

        public void Update()
        {
            MouseItem.Update();

            Inventory.Update();

            if (MouseInput.MouseClickedLeft())
            {
                if (MouseItem.Item != null && GridCraft.AddItem(MouseInput.MouseRealPosMenu(), _items, MouseItem.Item.Rotation) == true)
                {
                    if (Game1.PlayerInstance.Items.Any(i => i.Type == MouseItem.Item.Type) == true)
                    {
                        Game1.PlayerInstance.Items.Single(i => i.Type == MouseItem.Item.Type).Amount--;
                    }

                    if(Game1.PlayerInstance.Items.Single(i => i.Type == MouseItem.Item.Type).Amount<=0)
                    {
                        Game1.PlayerInstance.Items.Remove(Game1.PlayerInstance.Items.Single(i => i.Type == MouseItem.Item.Type));
                    }

                    MouseItem.Item = null;

                    Inventory.ResetChoosed();
                    CalculateAll();
                }
            }

            if (MouseInput.MouseClickedRight())
            {
                GridItem item = GridCraft.RemoveItem(MouseInput.MouseRealPosMenu(), _items);

                if (item != null)
                {
                    if (Game1.PlayerInstance.Items.Any(i => i.Type == item.Type) == true)
                    {
                        Game1.PlayerInstance.Items.Single(i => i.Type == item.Type).Amount++;
                    }
                    else
                    {
                        Game1.PlayerInstance.Items.Add(new PlayersItem(item.Type, 1));
                    }
                }

                UIShop.Instance.Sync();

                CalculateAll();
            }
            UIShop.Instance.Sync();
            Inventory.Set();
        }

        public void CalculateAll()
        {
            Game1.PlayerInstance.Weapons[0].DamagePlus = (short)(Game1.PlayerInstance.Weapons[0].Damage * _items.Sum(a => a.DamagePercentage));
        }

        public void Draw()
        {

            Inventory.Draw(Game1.MenuParts);
            GridCraft.Draw(_items);
            MouseItem.Draw(GridCraft.Position);

            DrawString.DrawText("Max Ammo: ", new Vector2(XposText, YposText), Align.left, Color.White, FontType.small);
            DrawString.DrawText(Game1.PlayerInstance.Weapons[0].MaxAmmo.ToString(), new Vector2(XposTextBase, YposText), Align.left, Color.White, FontType.small);

            DrawString.DrawText("Timing: ", new Vector2(XposText, YposText + 32), Align.left, Color.White, FontType.small);
            DrawString.DrawText(Game1.PlayerInstance.Weapons[0].GunTimer.DelayMilisec.ToString(), new Vector2(XposTextBase, YposText +32), Align.left, Color.White, FontType.small);

            DrawString.DrawText("Velocity: ", new Vector2(XposText, YposText + 64), Align.left, Color.White, FontType.small);
            DrawString.DrawText(Game1.PlayerInstance.Weapons[0].VelocityOfProjectile.ToString(), new Vector2(XposTextBase, YposText +64), Align.left, Color.White, FontType.small);

            if (Game1.PlayerInstance.Weapons[0].DispersionPlus <= 0)
            {
                DrawString.DrawText(((int)(Game1.PlayerInstance.Weapons[0].VelocityOfProjectilePlus * 100)).ToString(), new Vector2(XposTextBonus, YposText +64), Align.left, Color.ForestGreen, FontType.small);
            }
            else
            {
                DrawString.DrawText(((int)(Game1.PlayerInstance.Weapons[0].VelocityOfProjectilePlus * 100)).ToString(), new Vector2(XposTextBonus, YposText +64), Align.left, Color.DarkRed, FontType.small);
            }

            DrawString.DrawText("Dispersion: ", new Vector2(XposText, YposText + 96), Align.left, Color.White, FontType.small);
            DrawString.DrawText(((int)(Game1.PlayerInstance.Weapons[0].Dispersion * 100)).ToString(), new Vector2(XposTextBase, YposText +96), Align.left, Color.White, FontType.small);

            if (Game1.PlayerInstance.Weapons[0].DispersionPlus <=0)
            {
                DrawString.DrawText(((int)(Game1.PlayerInstance.Weapons[0].DispersionPlus * 100)).ToString(), new Vector2(XposTextBonus, YposText + 96), Align.left, Color.ForestGreen, FontType.small);
            }
            else
            {
                DrawString.DrawText(((int)(Game1.PlayerInstance.Weapons[0].DispersionPlus * 100)).ToString(), new Vector2(XposTextBonus, YposText + 96), Align.left, Color.DarkRed, FontType.small);
            }

            DrawString.DrawText("Damage: ", new Vector2(XposText, YposText + 128), Align.left, Color.White, FontType.small);
            DrawString.DrawText(Game1.PlayerInstance.Weapons[0].Damage.ToString(), new Vector2(XposTextBase, YposText + 128), Align.left, Color.White, FontType.small);

            if (Game1.PlayerInstance.Weapons[0].DamagePlus >= 0)
            {
                DrawString.DrawText(((Game1.PlayerInstance.Weapons[0].DamagePlus)).ToString(), new Vector2(XposTextBonus, YposText + 128), Align.left, Color.ForestGreen, FontType.small);
            }
            else
            {
                DrawString.DrawText(((Game1.PlayerInstance.Weapons[0].DamagePlus)).ToString(), new Vector2(XposTextBonus, YposText + 128), Align.left, Color.DarkRed, FontType.small);
            }
        }

        public static UIAssembly Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new UIAssembly();
                }
                return _instance;
            }
        }
    }
}