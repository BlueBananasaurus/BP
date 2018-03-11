using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;

namespace Monogame_GL
{
    public class ListItemClassic
    {
        private string _key;
        private int _index;
        private float _offset;
        private ListItemStatesEnum _state;

        public RectangleF Boundary { get; set; }

        public ListItemClassic(string key, int index, RectangleF boundary)
        {
            _key = key;
            _index = index;
            _offset = 0;
            Boundary = boundary;
            _state = ListItemStatesEnum.none;
        }

        public void Move(float offset)
        {
            _offset = offset;
            Boundary.Position = new Vector2(Boundary.Position.X, offset + _index * 32 + 64);
        }

        public void SetNone()
        {
            _state = ListItemStatesEnum.none;
        }

        public void Update(ListPage page)
        {
            if (CompareF.RectangleVsVector2(Boundary, MouseInput.MouseRealPosMenu()) == true)
            {
                if (_state != ListItemStatesEnum.selected)
                    _state = ListItemStatesEnum.hover;
                if (MouseInput.MouseStateNew.LeftButton == ButtonState.Released && MouseInput.MouseStateOld.LeftButton == ButtonState.Pressed)
                {
                    page.CloseOthers(this);
                    _state = ListItemStatesEnum.selected;
                }
            }
            else
            {
                if (_state != ListItemStatesEnum.selected)
                    _state = ListItemStatesEnum.none;
            }

            if (_state == ListItemStatesEnum.selected)
            {
                foreach (Keys key in Enum.GetValues(typeof(Keys)))
                {
                    if (KeyboardInput.KeyboardStateNew.IsKeyDown(key))
                    {
                        Game1.STP.ControlKeys[_key] = key;
                        _state = ListItemStatesEnum.none;
                    }
                }
            }
        }

        public void Draw()
        {
            if (_state == ListItemStatesEnum.none)
                Game1.SpriteBatchGlobal.Draw(Game1.controlBack, new Vector2(32, _index * 32 + _offset));
            if (_state == ListItemStatesEnum.hover)
                Game1.SpriteBatchGlobal.Draw(Game1.controlBackHover, new Vector2(32, _index * 32 + _offset));
            DrawString.DrawText(_key + ": ", new Vector2(64, _index * 32 + _offset + 6), Align.left, new Color(50, 50, 50), FontType.small);
            DrawString.DrawText(Game1.STP.ControlKeys[_key].ToString(), new Vector2(64 + 256 + 128, _index * 32 + _offset + 6), Align.left, new Color(50, 50, 50), FontType.small);
        }
    }
}