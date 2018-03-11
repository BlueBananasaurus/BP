using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json;

namespace Monogame_GL
{
    public class Light
    {
        public Vector2? FlarePosition { get; set; }
        public Vector2 Position { get; set; }
        public Vector4 LightColor { get; set; }
        [JsonIgnore]
        public Texture2D Flare { get; set; }
        public string FlareName { get; set; }

        public Light(Vector2 position, Vector4 color, string flare, Vector2? flarePosition = null)
        {
            Position = position;
            LightColor = color;
            FlareName = flare;
            if(FlareName != null)
            Flare = Game1.Textures[FlareName];
            FlarePosition = flarePosition;
        }

        [OnDeserialized]
        public void Serial(StreamingContext context)
        {
            Flare = Game1.Textures[FlareName];
        }
    }
}
