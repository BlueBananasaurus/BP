using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System;

namespace Monogame_GL
{
    public interface IHolderItem
    {
        bool Selected { get; set; }
        int Index { get; set; }
        RectangleF Boundary { get; }

        void UpdateIndex(List<IHolderItem> list);

        bool DoubleClicked();

        void Update();

        void Move(float offset);

        void Draw();
    }
}