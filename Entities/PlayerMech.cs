using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Monogame_GL
{
    public class PlayerMech : NpcBase, Inpc, Ivehicle
    {
        private Timer _time;
        private Timer _bubbleTime;
        public bool WalkingVisible { get; private set; }
        public mechFacingCabin MechDirection { get; private set; }
        public mechFacingLegs MechDirectionLegs { get; private set; }
        public bool Friendly { get; private set; }

        private float _rotLeg;
        private float _walkSin;

        public PlayerMech(Vector2 position, mechFacingCabin face, Vector2 velocity, float health = 200f)
        {
            Boundary = new RectangleF(MechModule.MechBoundarySize, position);
            _resolver = new CollisionResolver(Globals.TileSize);
            _weight = 1f;
            _time = new Timer(100, true);
            _bubbleTime = new Timer(300, true);
            Friendly = true;
            Velocity = velocity;
            WalkingVisible = false;
            MechDirection = mechFacingCabin.right;
            MechDirectionLegs = mechFacingLegs.left;
            _rotLeg = 0;
            _walkSin = (float)Math.Sin(0);
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

            if (_resolver.TouchTop || _resolver.InWater || _resolver.TouchBottomMovable)
            {
                WalkingVisible = true;

                _rotLeg += Game1.Delta / 25 * Velocity.X;

                if (Math.Abs(Velocity.X - _resolver.VelocityReceived.X) < 0.05f)
                {
                    WalkingVisible = false;
                }
            }
            else
            {
                WalkingVisible = false;
            }

            if (WalkingVisible == true)
            {
                if (Velocity.X < 0)
                {
                    _walkSin -= Game1.Delta / 16 * Velocity.X;
                }
                if (Velocity.X > 0)
                {
                    _walkSin += Game1.Delta / 16 * Velocity.X;
                }
            }
        }

        public override void Draw()
        {
            if (Velocity.X < 0)
            {
                MechDirectionLegs = mechFacingLegs.left;
                MechDirection = mechFacingCabin.left;
            }
            if (Velocity.X > 0)
            {
                MechDirectionLegs = mechFacingLegs.right;
                MechDirection = mechFacingCabin.right;
            }

            DrawEntities.DrawMech(Boundary, _rotLeg, WalkingVisible, Velocity, _resolver, _walkSin, MechDirectionLegs, MechDirection);
        }
    }
}