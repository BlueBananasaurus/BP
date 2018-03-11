using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json;
using System;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Monogame_GL
{
    public class ParticleBlackDrop : PhysicEntity, IParticle
    {
        private Texture2D _tex;
        private float _rotation;
        private float _scale;

        public ParticleBlackDrop(Vector2 origin,Vector2 velocity, Texture2D tex)
        {
            Boundary = new RectangleF(new Vector2(0), origin - new Vector2(0));
            _resolver = new CollisionResolver(Globals.TileSize);
            _resolver.move(ref _velocity, new Vector2(2f), Boundary, 0f, new Vector2(0f), new Vector2(0.01f), new Vector2(0.5f), Game1.mapLive.MapMovables, 0.00f);
            _tex = tex;
            _velocity = velocity + new Vector2((float)Globals.GlobalRandom.NextDouble() - 0.5f, (float)Globals.GlobalRandom.NextDouble() - 0.5f);
            _rotation = (float)(Globals.GlobalRandom.NextDouble() - 0.5f)/8;
            _scale = 0.5f;
        }

        public void Update()
        {
            _scale -= Game1.Delta / 2048;
            _velocity = _velocity.Rotate(_rotation)*0.9f;
            _resolver.move(ref _velocity, new Vector2(2f), Boundary, 0f, new Vector2(0f), new Vector2(0.01f), new Vector2(0.5f), Game1.mapLive.MapMovables, 0.00f,false);

            if (_scale<=0)
            {
                Game1.mapLive.mapParticles.Remove(this);
            }
        }

        public override void Draw()
        {
            if (_resolver.TouchBottom == false && _resolver.TouchTopMovable == false)
                Game1.SpriteBatchGlobal.Draw(_tex, Boundary.Origin, rotation: _velocity.ToAngle(), origin: new Vector2(32), scale: new Vector2(_velocity.Length() * 1 + _scale, _scale));
            else
                Game1.SpriteBatchGlobal.Draw(_tex, Boundary.Origin, rotation: _velocity.ToAngle(), origin: new Vector2(32), scale: new Vector2(_scale));
        }

        public override void DrawLight()
        {

        }

        public override void Push(Vector2 velocity)
        {

        }

        public void DrawNormal()
        {

        }
    }
}
