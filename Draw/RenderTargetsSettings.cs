using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Monogame_GL
{
    public class RenderTargetsSettings
    {
        public static void SetAndClear(RenderTarget2D target, Color clearTo)
        {
            if(target != null)
                Game1.GraphicsGlobal.GraphicsDevice.SetRenderTargets(target);
            else
                Game1.GraphicsGlobal.GraphicsDevice.SetRenderTargets(null);
            Game1.GraphicsGlobal.GraphicsDevice.Clear(clearTo);
        }
    }
}
