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
    public class ParticleSpark : PhysicEntity, IParticle
    {
        private float _alpha;
        private RibbonTrail _trail;
        public ParticleSpark(Vector2 position)
        {
            Boundary = new RectangleF(new Vector2(2), position - new Vector2(1));
            _resolver = new CollisionResolver(Globals.TileSize);
            _alpha = 1f;
            _resolver.move(ref _velocity, new Vector2(2f), Boundary, 0f, new Vector2(0f), new Vector2(0.01f), new Vector2(0.5f), Game1.mapLive.MapMovables, 0.00f);
            _trail = new RibbonTrail(Boundary.Origin, _velocity, 4, 2, 1, Game1.Textures["RibbonSpark"], null,Game1.Textures["RibbonSpark"]);
            _velocity = new Vector2((float)Globals.GlobalRandom.NextDouble() - 0.5f, (float)Globals.GlobalRandom.NextDouble() - 0.5f);
        }

        public void Update()
        {
               _alpha -= Game1.Delta/512;
            _resolver.move(ref _velocity, new Vector2(2f), Boundary, 0f, new Vector2(0f), new Vector2(0.01f), new Vector2(0.5f), Game1.mapLive.MapMovables, 0.00f);
            _trail.Update(Boundary.Origin, _velocity,_alpha);
            if (_alpha <= 0)
            {
                Game1.mapLive.mapParticles.Remove(this);
            }
        }

        public override void Draw()
        {
            _trail.Draw();
        }

        public override void DrawLight()
        {
            _trail.Draw();
        }

        public override void Push(Vector2 velocity)
        {

        }

        public void DrawNormal()
        {

        }
    }
}
