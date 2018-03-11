using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;

namespace Monogame_GL
{
    public class AircraftModule : IPlayer
    {
        public static Vector2 PlayerBoundarySize { get { return new Vector2(96 * 2, 62 * 2); } }
        public LineShootCalc ShootCalc{get;set;}
        public float HealthMachine { get; set; }
        private float _rotation;
        public float MaxHealth { get; set; }

        public AircraftModule(Player player, Vector2 center, float health)
        {
            player.Boundary.Size = PlayerBoundarySize;
            player.Boundary.MoveToCenter(center);
            ShootCalc = new LineShootCalc();
            HealthMachine = 100;
            _rotation = 0;
            MaxHealth = 100;
            HealthMachine = health;
        }

        public void LineAim(Vector2 realPos, Player player)
        {
            ShootCalc.LineAim(realPos, player, player.WeaponOrigin, player.CurrentWeaponObject.GunBarrel, player.CurrentWeaponObject.Reach);
        }

        public void Push(Player player, Vector2 velocity)
        {
            player.Velocity += new Vector2(velocity.X, velocity.Y) / 10;
        }

        public void DrawNormal(Player player)
        {
        }

        public void HowILookCalc(Player player)
        {
            if (_rotation > player.Velocity.X - player.Resolver.VelocityReceived.X)
            {
                _rotation -= Game1.Delta / 3000;
            }

            if (_rotation < player.Velocity.X - player.Resolver.VelocityReceived.X)
            {
                _rotation += Game1.Delta / 3000;
            }
        }

        public void PhysicsMove(Player player)
        {
            Vector2 tempVelocity = player.Velocity;
            if (player.ControlsActive == true) player.Resolver.move(ref player.Velocity, new Vector2(2f), player.Boundary, 0.5f, new Vector2(0.1f, 0.1f), new Vector2(0.01f, 0.01f), new Vector2(0.1f), Game1.mapLive.MapMovables, 0, player.Walking);
            ApplyGDamage(player, tempVelocity);
        }

        private void ApplyGDamage(Player player, Vector2 tempVelocity)
        {
            if (player.Resolver.TouchVertical == true)
            {
                if (Math.Abs(tempVelocity.Y) > 0.5f)
                {
                    HealthMachine -= Math.Abs(tempVelocity.Y) * 8f;
                    Camera2DGame.Shake((int)(Math.Abs(tempVelocity.Y*4f)), Camera2DGame.Boundary.Origin);
                }
            }
            if (player.Resolver.TouchHorizontal == true)
            {
                if (Math.Abs(tempVelocity.X) > 0.5f)
                {
                    HealthMachine -= Math.Abs(tempVelocity.X) * 8f;
                    Camera2DGame.Shake((int)(Math.Abs(tempVelocity.X*4f)), Camera2DGame.Boundary.Origin);
                }
            }

            if (HealthMachine <= 0 || player.Resolver.VerticalPressure == true || player.Resolver.HorizontalPressure == true)
            {
                player.DestroyVehicle();
            }
        }

        public void ShootWeapons(Player player, Vector2 realPos)
        {

        }

        public void ThrowGrenade(Player player)
        {

        }

        public void Draw(Player player)
        {
            DrawEntities.DrawAircraft(player.Boundary, _rotation);
        }

        public void TakeDamage(ushort amount, Player player)
        {
            if (player.Shield > 0)
            {
                if (player.Health > 0)
                {
                    player.Health -= amount / 2f;
                    player.PotentialHealth -= amount / 2f;
                    player.Shield -= amount;
                    player.PotentialShield -= amount;
                    if (player.Health < 0)
                        player.Health = 0;
                }
            }
            else
            {
                if (player.Health > 0)
                {
                    player.Health -= amount;
                    player.PotentialHealth -= amount;
                    player.Shield = 0;
                    if (player.Health < 0)
                        player.Health = 0;
                }
            }
            if (player.Shield < 0)
                player.Shield = 0;

            if (player.Health <= 0)
            {
                player.Kill();
            }
        }

        public void ControlPlayer(Player player)
        {
            player.Walking = false;

            if (KeyboardInput.KeyboardStateNew.IsKeyDown(Game1.STP.ControlKeys["Jump"]))
            {
                if(player.Resolver.TouchBottom)
                {
                    player.Velocity = new Vector2(player.Velocity.X, 0f);
                }
                if (player.Resolver.TouchTopMovable)
                {
                    player.Velocity = new Vector2(player.Velocity.X, player.Resolver.VelocityReceived.Y);
                }

                player.Velocity += new Vector2(0, -0.05f);
            }

            if (KeyboardInput.KeyboardStateNew.IsKeyDown(Game1.STP.ControlKeys["Walk left"]) && KeyboardInput.KeyboardStateNew.IsKeyUp(Game1.STP.ControlKeys["Walk right"]))
            {
                player.Velocity -= new Vector2(player.Speed.X/16, 0);
                if (player.Velocity.X < -player.MaxSpeed.X)
                {
                    player.Velocity = new Vector2(-player.MaxSpeed.X, player.Velocity.Y);
                }

                player.Walking = true;
            }

            if (KeyboardInput.KeyboardStateNew.IsKeyDown(Game1.STP.ControlKeys["Walk right"]) && KeyboardInput.KeyboardStateNew.IsKeyUp(Game1.STP.ControlKeys["Walk left"]))
            {
                player.Velocity += new Vector2(player.Speed.X / 16, 0);

                if (player.Velocity.X > player.MaxSpeed.X)
                {
                    player.Velocity = new Vector2(player.MaxSpeed.X, player.Velocity.Y);
                }

                player.Walking = true;
            }
        }

        public void DrawLight(Player player)
        {

        }

        public void LeftVehicle(Player player)
        {
            Game1.mapLive.MapNpcs.Add(new PlayerAircraft(player.Boundary.Position, player.Velocity, player.CurretnObjectControl.HealthMachine));
        }
    }
}