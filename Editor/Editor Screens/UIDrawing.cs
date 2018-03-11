using Microsoft.Xna.Framework;
using System;

namespace Monogame_GL
{
    public class UIDrawing : IWindow
    {
        private static UIDrawing instance;
        public ItemsHolder Tiles { get; private set; }
        public ItemsHolder TilesPhysics { get; private set; }
        private Graber _transaprencyForeground;
        private Graber _transaprencyBackground;
        private Graber _transaprencyPhysics;
        private Radio _radio1;
        private Radio _radio2;
        private Radio _radio3;
        public CheckBox ForegroundVisibility { get; set; }
        public CheckBox BackgroundVisibility { get; set; }
        public CheckBox PhysicsVisibility { get; set; }
        private GroupRadios _radios;

        private UIDrawing()
        {
            Tiles = new ItemsHolder(new RectangleF(80 * 7 + 64, 640 + 121, 1024 + 128 - 16, 128 + 64), 14, 40);
            _transaprencyForeground = new Graber(new Vector2(512 - 96, 128 + 128), 255, null, TransparencyForeground, GrabShow.percentage, "Foreground transparency");
            _transaprencyBackground = new Graber(new Vector2(512 - 96, 128 + 64), 255, null, TransparencyBackground, GrabShow.percentage, "Background transparency");
            _transaprencyPhysics = new Graber(new Vector2(512 - 96, 128 + 64 + 128), 255, null, null, GrabShow.percentage, "Physics transparency");
            _radio1 = new Radio(null, new Vector2(1024 - 64 - 96, 128 + 64), RadioType.classic);
            _radio2 = new Radio(null, new Vector2(1024 - 64 - 96, 256), RadioType.classic);
            _radio3 = new Radio(null, new Vector2(1024 - 64 - 96, 256 + 64), RadioType.classic);
            _radios = new GroupRadios(0, _radio1, _radio2, _radio3);
            ForegroundVisibility = new CheckBox(null, new Vector2(1024 - 128 - 96, 128 + 128), CheckBoxType.visibility, true, null, null);
            BackgroundVisibility = new CheckBox(null, new Vector2(1024 - 128 - 96, 128 + 64), CheckBoxType.visibility, false, null, null);
            PhysicsVisibility = new CheckBox(null, new Vector2(1024 - 128 - 96, 128 + 64 + 128), CheckBoxType.visibility, false, null, null);
            TilesPhysics = new ItemsHolder(new RectangleF(80 * 7 + 64, 640 + 121, 1024 + 128 - 16, 128 + 64), 14, 40);

            for (int x = 0; x < Game1.Tiles.Count; x++)
            {
                int y = (int)Math.Floor(x / 14f);
                Tiles.Items.Add(new ListItemTile(Tiles.Items.Count, Tiles.Boundary, Game1.Tiles[x]));
            }

            for (int x = 0; x < Game1.TilesPhysics.Count; x++)
            {
                int y = (int)Math.Floor(x / 14f);
                TilesPhysics.Items.Add(new ListItemTile(TilesPhysics.Items.Count, Tiles.Boundary, Game1.TilesPhysics[x]));
            }

            Tiles.Update();
            TilesPhysics.Update();
        }

        public void Reset()
        {
        }

        public void Update()
        {
            if (_radios.IndexSelected == 0 || _radios.IndexSelected == 1)
                Tiles.Update();
            else
                TilesPhysics.Update();
            _transaprencyForeground.Update();
            _transaprencyBackground.Update();
            _transaprencyPhysics.Update();
            _radios.Update();
            ForegroundVisibility.Update();
            BackgroundVisibility.Update();
            PhysicsVisibility.Update();

            if (_radios.GetIndex().HasValue)
                Editor.layerEdit = (LayerEditing)_radios.GetIndex().Value;
            else Editor.layerEdit = LayerEditing.noChoosed;
        }

        public void Draw()
        {
            if (_radios.IndexSelected == 0 || _radios.IndexSelected == 1)
                Tiles.Draw(Game1.Win);
            else
                TilesPhysics.Draw(Game1.Win);
            _transaprencyForeground.Draw();
            _transaprencyBackground.Draw();
            _transaprencyPhysics.Draw();
            ForegroundVisibility.Draw();
            BackgroundVisibility.Draw();
            PhysicsVisibility.Draw();
            _radios.Draw();
        }

        public void TransparencyForeground(byte value)
        {
            Editor.EditMap.TransparencyForeground = (float)value / byte.MaxValue;
        }

        public void TransparencyBackground(byte value)
        {
            Editor.EditMap.TransparencyBackground = (float)value / byte.MaxValue;
        }

        public static UIDrawing Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new UIDrawing();
                }
                return instance;
            }
        }
    }
}