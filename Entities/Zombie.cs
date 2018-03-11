using Microsoft.Xna.Framework;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Monogame_GL
{
    public class Zombie : NpcBase, Inpc
    {
        public ZombieStates Goes { get; set; }
        public Timer StunLeft { get; set; }
        public Timer StrikeTimer;
        public float RotationLeg { get; set; }
        public bool WalkingVisible { get; set; }
        public float RotationBody { get; set; }
        public bool Friendly { get; set; }

        [JsonIgnore]
        public float Sense { get; set; }

        [JsonIgnore]
        public Vector2 RightLeg
        {
            get { return Boundary.Origin + new Vector2(12, 58); }
            private set { }
        }

        [JsonIgnore]
        public Vector2 LeftLeg
        {
            get { return Boundary.Origin + new Vector2(-12, 58); }
            private set { }
        }

        [JsonIgnore]
        public Vector2 LeftLegJoin
        {
            get { return Boundary.Origin + new Vector2(-12, 50); }
            private set { }
        }

        [JsonIgnore]
        public Vector2 RightLegJoin
        {
            get { return Boundary.Origin + new Vector2(12, 50); }
            private set { }
        }

        public Zombie(Vector2 position) : base()
        {
            Velocity = new Vector2(0.0f, 0.0f);
            _maxSpeed = new Vector2(0.5f);
            Boundary = new RectangleF(44, 124, position.X - 22, position.Y - 62);
            _resolver = new CollisionResolver(Globals.TileSize);
            _weight = 1f;
            Goes = ZombieStates.stay;
            StunLeft = new Timer(100);
            Health = 100;
            StrikeTimer = new Timer(1000, true);
            RotationLeg = 0;
            RotationBody = 0;
            Sense = 1024;
            WalkingVisible = true;
            Friendly = false;
        }

        public void Update(List<Inpc> npcs)
        {
            if (Tint < 1f)
                Tint += Game1.Delta / 100;
            if (Tint > 1f)
                Tint = 1f;

            if (Health <= 0)
            {
                Kill();
            }

            StunLeft.Update();
            Controling();

            _resolver.move(ref _velocity, new Vector2(2f), Boundary, 0f, new Vector2(0.2f), new Vector2(0.02f), new Vector2(0.3f), Game1.mapLive.MapMovables);

            if (_resolver.VerticalPressure == true)
            {
                Kill();
            }

            WalkingDrawingData();
        }

        private void Controling()
        {
            Direction();
            Walk();
            Jump();
        }

        private void Direction()
        {
            if (Game1.PlayerInstance.Alive == true)
            {
                if (LineSegmentF.Lenght(Game1.PlayerInstance.Boundary.Origin, Boundary.Origin) < Sense)
                {
                    if (Boundary.Origin.X > Game1.PlayerInstance.Boundary.Origin.X && Game1.PlayerInstance.Boundary.Origin.X - Boundary.Origin.X < -16f)
                    {
                        Goes = ZombieStates.left;
                    }
                    if (Boundary.Origin.X < Game1.PlayerInstance.Boundary.Origin.X && Game1.PlayerInstance.Boundary.Origin.X - Boundary.Origin.X > 16f)
                    {
                        Goes = ZombieStates.right;
                    }
                }
                else
                {
                    Goes = ZombieStates.stay;
                }
            }
        }

        private void Walk()
        {
            if (StunLeft.Ready == true)
            {
                if ((_resolver.TouchTop == true || _resolver.TouchBottomMovable == true) || _resolver.InWater)
                {
                    if (Goes == ZombieStates.right)
                    {
                        Velocity = new Vector2(_maxSpeed.X, Velocity.Y);
                    }
                    if (Goes == ZombieStates.left)
                    {
                        Velocity = new Vector2(-_maxSpeed.X, Velocity.Y);
                    }
                }
            }

            StrikeTimer.Update();

            if (Game1.PlayerInstance.Alive == true)
            {
                if (StrikeTimer.Ready == true)
                {
                    if (CompareF.RectangleFVsRectangleF(Boundary, Game1.PlayerInstance.Boundary) == true)
                    {
                        if (StrikeTimer.Ready == true)
                        {
                            LineSegmentF line = new LineSegmentF(Boundary.Origin, Game1.PlayerInstance.Boundary.Origin);
                            Vector2 velocity = line.NormalizedWithZeroSolution() * 4;
                            Game1.PlayerInstance.Push(new Vector2(velocity.X, velocity.Y / 2));
                            Game1.PlayerInstance.TakeDamage(10);

                            StrikeTimer.Reset();
                        }
                    }
                }
            }
        }

        private void Jump()
        {
            if (_resolver.TouchTop == true || _resolver.TouchBottomMovable == true)
            {
                if (Globals.GlobalRandom.Next(0, 128) == 1)
                {
                    if (Goes == ZombieStates.left)
                        Push(new Vector2(-0.2f, -0.8f));
                    if (Goes == ZombieStates.right)
                        Push(new Vector2(0.2f, -0.8f));
                }
            }
            if (Game1.PlayerInstance.Alive == true)
            {
                if (_resolver.TouchTop == true || _resolver.TouchBottomMovable == true)
                {
                    if (Globals.GlobalRandom.Next(0, 128) == 1)
                    {
                        if (LineSegmentF.Lenght(Boundary.Origin, Game1.PlayerInstance.Boundary.Origin) < Sense)
                        {
                            if (Goes == ZombieStates.left)
                                Push(new Vector2(-0.2f, -0.8f));
                            if (Goes == ZombieStates.right)
                                Push(new Vector2(0.2f, -0.8f));
                        }
                    }
                }
            }
        }

        private void WalkingDrawingData()
        {
            if (_resolver.TouchTop == true || _resolver.TouchBottomMovable == true)
            {
                WalkingVisible = true;

                if (Velocity.X > 0.05)
                {
                    RotationLeg += Game1.Delta / 25 * Velocity.X;

                    RotationBody = 0.1f;
                }
                else if (Velocity.X < -0.05)
                {
                    RotationBody = -0.1f;
                    RotationLeg += Game1.Delta / 25 * Velocity.X;
                }
                else
                {
                    RotationBody = 0f;
                    WalkingVisible = false;
                }
            }
            else
            {
                WalkingVisible = false;
            }
        }

        new public void Stun()
        {
            StunLeft.Reset();
        }

        new public void Kill()
        {
            Health = 0;
            Globals.AddPick(new Coin(Boundary.Origin, new Vector2(0), Pickups.tissue));
            Game1.mapLive.MapNpcs.Remove(this);
        }

        public override void Draw()
        {
            Effects.ColorEffect(new Vector4(1f, Tint, Tint, 1f));

            DrawEntities.DrawZombie(WalkingVisible, _resolver, RotationBody, Boundary, RotationLeg, Velocity);

            Game1.EffectBaseColor.CurrentTechnique.Passes[0].Apply();
        }
    }
}