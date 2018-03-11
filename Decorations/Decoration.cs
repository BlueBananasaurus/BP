using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Runtime.Serialization;

namespace Monogame_GL
{
    public class TexPos
    {
        [JsonIgnore]
        public Texture2D Texture { get; set; }
        public string TextureName { get; set; }
        public Vector2 Position { get; set; }

        public TexPos(string textureName, Vector2 pos)
        {
            Texture = Game1.Textures[textureName];
            TextureName = textureName;
            Position = pos;
        }

        [OnSerialized]
        public void Serial(StreamingContext context)
        {
            Texture = Game1.Textures[TextureName];
        }
    }

    internal class DecorationStatic : IDecoration
    {
        public Vector2 Position { get; set; }
        public TexPos Texture { get; set; }
        public TexPos TextureNormal { get; set; }
        public TexPos TextureLight { get; set; }
        public Light LightObj { get; set; }
        public bool IsFront { get; set; }
        private Rectangle? _source;

        public DecorationStatic(Vector2 position, TexPos texture = null, TexPos textureNormal = null, TexPos textureLight = null, bool isFront = false, Rectangle? source = null,Light light = null)
        {
            Position = position;
            Texture = texture;
            TextureLight = textureLight;
            IsFront = isFront;
            _source = source;
            TextureNormal = textureNormal;
            LightObj = light;
        }

        public void DrawFlare()
        {
            if(LightObj?.Flare != null && LightObj?.Position != null)
            {
                Game1.SpriteBatchGlobal.Draw(LightObj?.Flare, LightObj?.Position + LightObj?.FlarePosition);
            }
        }

        public void Draw()
        {
            if (Texture != null)
                Game1.SpriteBatchGlobal.Draw(Texture.Texture, Position + Texture.Position, sourceRectangle: _source);
        }

        public void DrawNormal()
        {
            if (TextureNormal != null)
                Game1.SpriteBatchGlobal.Draw(TextureNormal.Texture, Position + TextureNormal.Position, sourceRectangle: _source);
        }

        public void DrawLight()
        {
            if (TextureLight != null)
                Game1.SpriteBatchGlobal.Draw(TextureLight.Texture, Position + TextureLight.Position);
        }
    }
}