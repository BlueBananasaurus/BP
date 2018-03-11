using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Monogame_GL
{
    public class ParticleCircleFlame :  IParticle
    {
        public RectangleF Boundary { get; set; }
        public float _size;
        public Vector2 _origin;
        public Texture2D Tex;

        public ParticleCircleFlame(Vector2 origin,Texture2D texture)
        {
            Boundary = new RectangleF(new Vector2(32), origin - new Vector2(16));
            _origin = Boundary.Size / 2;
            _size = 1f;
            Tex = texture;
        }

        public void Update()
        {
            _size -= Game1.Delta / 512;

            if (_size <= 0)
            {
                Game1.mapLive.mapParticles.Remove(this);
            }
        }

        public void Draw()
        {
            Game1.SpriteBatchGlobal.Draw(Tex, position: Boundary.Origin, origin: _origin, scale: new Vector2(_size));
        }

        public void DrawLight()
        {
            Game1.SpriteBatchGlobal.Draw(Tex, position: Boundary.Origin, origin: _origin, scale: new Vector2(_size));
        }

        public void Push(Vector2 velocity)
        {

        }

        public void DrawNormal()
        {

        }
    }
}
