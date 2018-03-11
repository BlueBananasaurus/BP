using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Monogame_GL
{
    public class ItemsHolder
    {
        private RenderTarget2D _contentTarget;
        private float _itemSizeY;
        private ChangedSelectionMethod _method;
        private float _offset;
        private int? _oldSelectedIndex;
        private ScrollBar _scrollBar;
        private ItemHolderTypes _type;

        public ItemsHolder(RectangleF boundary, byte lineHolds, int itemSizeY, ChangedSelectionMethod change = null, ItemHolderTypes type = ItemHolderTypes.basic )
        {
            Boundary = boundary;
            _contentTarget = new RenderTarget2D(Game1.GraphicsGlobal.GraphicsDevice, (int)Globals.WinRenderSize.X, (int)Globals.WinRenderSize.Y);
            Items = new List<IHolderItem>();
            _offset = 0;
            LineHolds = lineHolds;
            _scrollBar = new ScrollBar((int)boundary.Position.Y, (int)boundary.CornerRightBottom.Y, (int)boundary.CornerRightBottom.X + 2, boundary.Size.Y, GetLenghtOfContent(), Grab);
            _itemSizeY = itemSizeY;
            _method = change;
            _type = type;
        }

        public delegate void ChangedSelectionMethod();

        public RectangleF Boundary { get; set; }
        public List<IHolderItem> Items { get; private set; }
        public byte LineHolds { get; private set; }
        public int? SelectedIndex { get; private set; }

        public void Draw(RenderTarget2D target)
        {
            Game1.GraphicsGlobal.GraphicsDevice.SetRenderTarget(_contentTarget);
            Game1.GraphicsGlobal.GraphicsDevice.Clear(Color.Transparent);
            if(_type == ItemHolderTypes.basic)
            Game1.SpriteBatchGlobal.Draw(Game1.Textures["PanelBackgroundLight"], position: Vector2.Zero, sourceRectangle: new Rectangle(new Point(0, 0), Boundary.Size.ToPoint()));

            for (int i = 0; i < Items.Count; i++)
            {
                if (CompareF.RectangleFVsRectangleF(Boundary, Items[i].Boundary) == true)
                    Items[i].Draw();
            }

            Game1.GraphicsGlobal.GraphicsDevice.SetRenderTarget(target);
            Game1.SpriteBatchGlobal.Draw(_contentTarget, Boundary.Position, new Rectangle(0, 0, (int)Boundary.Size.X, (int)Boundary.Size.Y), Color.White);
            _scrollBar.Draw();

            if (_type == ItemHolderTypes.basic || _type == ItemHolderTypes.blue)
                DrawRectangleBoundary.DrawBlue(Boundary.ToRectangle());
            else if (_type == ItemHolderTypes.purple)
                DrawRectangleBoundary.DrawPurple(Boundary.ToRectangle());
        }

        public float GetLenghtOfContent()
        {
            return _itemSizeY * (float)Math.Ceiling((float)Items.Count / LineHolds) + 64;
        }

        public void UpdateAllIndexes()
        {
            foreach(IHolderItem item in Items)
            {
                item.UpdateIndex(Items);
            }
        }

        public IHolderItem GetSelected()
        {
            if (SelectedIndex != null)
            return Items[SelectedIndex.Value];
            return null;
        }

        public float Grab()
        {
            return _offset = _scrollBar.Offset();
        }

        public bool ChangedSelection()
        {
            if (_oldSelectedIndex != SelectedIndex)
                return true;
            return false;
        }

        public void Reset()
        {
            _offset = 0;
            _scrollBar.ResetBar();
        }

        public void ResetChoosed()
        {
            foreach (IHolderItem tile in Items)
            {
                tile.Selected = false;
            }

            SelectedIndex = null;
        }

        public void Set()
        {
            if (_offset < -(((_itemSizeY) * (float)Math.Ceiling((float)Items.Count / LineHolds)) - (Boundary.Size.Y)) - 64)
            {
                _offset = -(((_itemSizeY) * (float)Math.Ceiling((float)Items.Count / LineHolds)) - (Boundary.Size.Y)) - 64;
            }

            if (_offset > 0)
            {
                _offset = 0;
            }

            for (int i = 0; i < Items.Count; i++)
            {
                Items[i].Move(_offset);
            }
        }

        public bool ShowIndexIfExists(uint index)
        {
            if (index > Items.Count - 1) return false;
            else { _offset = -_itemSizeY * (float)Math.Floor((index + 1) / (float)LineHolds); ResetChoosed(); Items[(int)index].Selected = true; _scrollBar.SyncWithContentOffset(_offset); Set(); return true; }
        }

        public void Update()
        {
            _oldSelectedIndex = SelectedIndex;

            _scrollBar.Update(GetLenghtOfContent());

            if (CompareF.RectangleVsVector2(Boundary, MouseInput.MouseRealPosMenu()))
            {
                if (MouseInput.ScrolledDown())
                    _scrollBar.ScroolDown();
                if (MouseInput.ScrolledUp())
                    _scrollBar.ScroolUp();
            }

            Set();

            SelectedIndex = null;

            foreach (IHolderItem tile in Items)
            {
                tile.Update();

                if (tile.Selected == true)
                {
                    SelectedIndex = tile.Index;
                }
            }

            if(ChangedSelection() == true) { _method?.Invoke(); }
        }
    }
}