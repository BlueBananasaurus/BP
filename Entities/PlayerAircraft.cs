using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Monogame_GL
{
    public class PlayerAircraft : NpcBase, Inpc, Ivehicle
    {
        private Timer _time;
        private Timer _bubbleTime;
        public bool Friendly { get; private set; }

        public PlayerAircraft(Vector2 position, Vector2 velocity, float health = 100f)
        {
            Boundary = new RectangleF(AircraftModule.PlayerBoundarySize, position);
            _resolver = new CollisionResolver(Globals.TileSize);
            _weight = 1f;
            _time = new Timer(100, true);
            _bubbleTime = new Timer(300, true);
            Friendly = true;
            Velocity = velocity;
            Health = health;
        }

        public void Update(List<Inpc> npcs)
        {
            _time.Update();
            _bubbleTime.Update();

            if (_resolver.InWater == true)
            {
                if (_bubbleTime.Ready == true)
                {
                    _bubbleTime.Reset();
                }
            }

            _resolver.move(ref _velocity, new Vector2(2f), Boundary, 0f, new Vector2(0.05f), new Vector2(0.005f), new Vector2(0.3f), Game1.mapLive.MapMovables);

            if (_resolver.VerticalPressure == true || _resolver.HorizontalPressure == true)
            {
                Explosion.Explode(Boundary.Origin, 128);
                Game1.mapLive.MapNpcs.Remove(this);
            }
        }

        public override void Draw()
        {
            DrawEntities.DrawAircraft(Boundary, 0f);
        }
    }
}