using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Monogame_GL
{
    public interface IMap
    {
        short[,] BackgroundTileMap { get; set; }
        short[,] ForegroundTileMap { get; set; }
        short[,] FunctionTileMap { get; set; }
        MapTreeHolder MapTree { get; set; }
        MapTreeHolder WaterTree { get; set; }
        Vector3 LightLevel { get; set; }
        void DrawWaterMask();
        void DrawLayer2();
        void Draw();
        void DrawNormal();
        void DrawLights();
    }
}
