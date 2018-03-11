using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Monogame_GL
{
    public interface IEffect
    {
        void Update(List<IEffect> effects);

        void Draw();

        void DrawLight();
    }
}