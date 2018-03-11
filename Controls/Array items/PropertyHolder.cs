using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Monogame_GL
{
    public class PropertyHolder
    {
        public Property[] Controls { get; private set; }
        private int Count;

        public PropertyHolder()
        {
            Controls = new Property[16];
            Count = 0;
        }

        public void Clear()
        {
            Controls = new Property[16];
            Count = 0;
        }

        public void AddPropertyDrop(string description, Type type)
        {
            Controls[Count] = new Property(new Vector2(32, 64 + 8 + 128 + Count * 48), description, new DropDown(type, 256, new Vector2(512, 64 + 128 + Count * 48)));
            Count++;
        }

        public void AddPropertyCheck(string description, bool checkedVal)
        {
            Controls[Count] = new Property(new Vector2(32, 64 + 8 + 128 + Count * 48), description, new CheckBox(null, new Vector2(512, 64 + 128 + Count * 48), CheckBoxType.classic, checkedVal));
            Count++;
        }

        public void AddPropertyText(string description, string defaultVal)
        {
            Controls[Count] = new Property(new Vector2(32, 64 + 8 + 128 + Count * 48), description, new TextBox(defaultVal, 256 + 32, new Vector2(512, 64 + 128 + Count * 48), textBoxType.text));
            Count++;
        }

        public void Update()
        {
            for (int i = Controls.GetLength(0) - 1; i >= 0; i--)
            {
                if (Controls[i] != null)
                    Controls[i].Update();
            }
        }

        public void Draw()
        {
            for (int i = Controls.GetLength(0) - 1; i >= 0; i--)
            {
                if (Controls[i] != null)
                    Controls[i].Draw();
            }
        }
    }
}
