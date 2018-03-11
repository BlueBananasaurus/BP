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
    public class ParticleFlameDropBig : PhysicEntity, IParticle
    {
        private float _alpha;
        private Texture2D _tex;

        public ParticleFlameDropBig(Vector2 position, Texture2D tex)
        {
            Boundary = new RectangleF(new Vector2(16), position - new Vector2(8));
            _resolver = new CollisionResolver(Globals.TileSize);
            _alpha = 1f;
            _resolver.move(ref _velocity, new Vector2(2f), Boundary, 0f, new Vector2(0f), new Vector2(0.01f), new Vector2(0.5f), Game1.mapLive.MapMovables, 0.00f);
            _velocity = new Vector2((float)Globals.GlobalRandom.NextDouble() - 0.5f, (float)Globals.GlobalRandom.NextDouble() - 0.5f);
            _tex = tex;
        }

        public void Update()
        {
            _alpha -= Game1.Delta / 512;
            _resolver.move(ref _velocity, new Vector2(2f), Boundary, 0f, new Vector2(0f), new Vector2(0.01f), new Vector2(0.5f), Game1.mapLive.MapMovables, 0.00f);
            if (_alpha <= 0 || _resolver.InWater == true)
            {
                Game1.mapLive.mapParticles.Remove(this);
            }
        }

        public override void Draw()
        {
            Effects.ColorEffect(new Vector4(1, 1, 1, _alpha));
            if (_resolver.TouchTop == false && _resolver.TouchBottomMovable == false)
                Game1.SpriteBatchGlobal.Draw(_tex, Boundary.Origin, rotation: _velocity.ToAngle(),origin: new Vector2(32), scale: new Vector2(_velocity.Length() + 0.25f, 0.25f));
            else
                Game1.SpriteBatchGlobal.Draw(_tex, Boundary.Origin, rotation: _velocity.ToAngle(), origin: new Vector2(32), scale: new Vector2(0.25f));
            Effects.ResetEffect3D();
        }

        public override void DrawLight()
        {
            Effects.ColorEffect(new Vector4(1, 1, 1, _alpha));
            if (_resolver.TouchTop == false && _resolver.TouchBottomMovable == false)
                Game1.SpriteBatchGlobal.Draw(_tex, Boundary.Origin, rotation: _velocity.ToAngle(), origin: new Vector2(32), scale: new Vector2(_velocity.Length() + 0.25f, 0.25f));
            else
                Game1.SpriteBatchGlobal.Draw(_tex, Boundary.Origin, rotation: _velocity.ToAngle(), origin: new Vector2(32), scale: new Vector2(0.25f));
            Effects.ResetEffect3D();
        }

        public override void Push(Vector2 velocity)
        {

        }
        public void DrawNormal()
        {

        }
    }
}
