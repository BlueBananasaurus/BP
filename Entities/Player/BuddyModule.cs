using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;

namespace Monogame_GL
{
    public class BuddyModule : IPlayer
    {
        public static Vector2 PlayerBoundarySize { get { return new Vector2(24 * 2, 62 * 2); } }
        public float RotLeg { get; set; }
        public float RotationBody { get; set; }
        public bool WalkingVisible { get; set; }
        public LineShootCalc ShootCalc{get;set;}
        public float HealthMachine { get; set; }
        public float MaxHealth { get; set; }
        public bool Walking { get; set; }

        public BuddyModule(Player player, Vector2 center)
        {
            player.Boundary.Size = PlayerBoundarySize;
            player.Boundary.MoveToCenter(center);
            RotLeg = 0;
            RotationBody = 0;
            WalkingVisible = false;
            ShootCalc = new LineShootCalc();
            MaxHealth = 100;
        }

        public void LineAim(Vector2 realPos, Player player)
        {
            ShootCalc.LineAim(realPos, player, player.WeaponOrigin, player.CurrentWeaponObject.GunBarrel, player.CurrentWeaponObject.Reach);
        }

        public void Push(Player player, Vector2 velocity)
        {
            player.Velocity += new Vector2(velocity.X, velocity.Y);
        }

        public void HowILookCalc(Player player)
        {
            if (player.Resolver.TouchTop || player.Resolver.InWater || player.Resolver.TouchTopMovable)
            {
                WalkingVisible = true;

                RotLeg += Game1.Delta / 25 * player.Velocity.X;

                if (Math.Abs(player.Velocity.X) - Math.Abs(player.Resolver.VelocityReceived.X) < 0.05f)
                {
                    WalkingVisible = false;
                }
            }
            else
            {
                WalkingVisible = false;
            }

            if (player.Velocity.X - player.Resolver.VelocityReceived.X > 0.05)
            {
                RotationBody = 0.1f;
            }
            else if (player.Velocity.X - player.Resolver.VelocityReceived.X < -0.05)
            {
                RotationBody = -0.1f;
            }
            else
            {
                RotationBody = 0f;
            }
        }

        public void PhysicsMove(Player player)
        {
            if (player.ControlsActive == true)
                player.Resolver.move(ref player.Velocity, new Vector2(2f), 
                    player.Boundary, 0f, new Vector2(0.2f, 0f), new Vector2(0.1f, 0.02f),
                    new Vector2(0.3f), Game1.mapLive.MapMovables, 0, walking: player.Walking);
        }

        public void ShootWeapons(Player player, Vector2 realPos)
        {
            ShootCalc.LineAim(realPos, player, player.WeaponOrigin, 
                player.CurrentWeaponObject.GunBarrel, player.CurrentWeaponObject.Reach);

            if (player.WeaponsAvailable == true && player.ControlsActive == true 
                && player.Carry == null && player.InVehicle == false)
            {
                if (MouseInput.MouseStateNew.LeftButton == ButtonState.Pressed)
                {
                    if (CompareF.WeaponRayObstruction(player.Boundary, 
                        ShootCalc.RayBarrel, Game1.PlayerInstance).Object == null)
                    {
                        player.CurrentWeaponObject.Fire(ShootCalc.RaySegment, 
                            ShootCalc.RayDestination);
                    }
                    else
                    {
                        player.CurrentWeaponObject.SetOff();
                    }
                }
            }
        }

        public void ThrowGrenade(Player player)
        {
            if (player.InVehicle == false)
            {
                if (player.GrenadesCount > 0)
                {
                    player.GrenadesCount--;
                    Game1.mapLive.MapProjectiles.Add(new Grenade(player.WeaponOrigin - new Vector2(12, 14),player.Velocity + ShootCalc.RayBarrel.NormalizedWithZeroSolution() * 1.0f, player.GrenadeDmg));
                }
            }
        }

        public void Draw(Player player)
        {
            DrawEntities.DrawBuddy(player.Boundary, RotationBody, RotLeg, WalkingVisible, player.VestOn, player.Velocity, player.Resolver);

            if (player.ControlsActive == true && player.VestOn == true && player.Carry == null)
                player.CurrentWeaponObject.Draw(new LineSegmentF(player.WeaponOrigin, ShootCalc.RayBarrel.Start + ShootCalc.RayBarrel.NormalizedWithZeroSolution() * player.CurrentWeaponObject.GunBarrel));
        }

        public void DrawNormal(Player player)
        {
            DrawEntities.DrawBuddyNormal(player.Boundary, RotationBody, RotLeg, WalkingVisible, player.VestOn, player.Velocity, player.Resolver);

            if (player.ControlsActive == true && player.VestOn == true && player.Carry == null)
                player.CurrentWeaponObject.DrawNormal(new LineSegmentF(player.WeaponOrigin, ShootCalc.RayBarrel.Start + ShootCalc.RayBarrel.NormalizedWithZeroSolution() * player.CurrentWeaponObject.GunBarrel));

            //if (player.ControlsActive == true && player.VestOn == true && player.Carry == null)
            //    player.CurrentWeaponObject.Draw(new LineSegmentF(player.WeaponOrigin, ShootCalc.RayBarrel.Start + ShootCalc.RayBarrel.ToNormalizedVector() * player.CurrentWeaponObject.GunBarrel));
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
                if (player.Resolver.InWater == true)
                {
                    player.Velocity = new Vector2(player.Velocity.X, -0.2f);
                }
            }

            if (KeyboardInput.KeyboardStateNew.IsKeyDown(Game1.STP.ControlKeys["Walk left"]) && KeyboardInput.KeyboardStateNew.IsKeyUp(Game1.STP.ControlKeys["Walk right"]))
            {
                player.Velocity -= new Vector2(player.Speed.X, 0);
                if (player.Velocity.X < -player.MaxSpeed.X)
                {
                    player.Velocity = new Vector2(-player.MaxSpeed.X, player.Velocity.Y);
                }

                if (player.Resolver.InWater == true)
                {
                    if (player.Resolver.TouchLeft == true)
                    {
                        player.Velocity = new Vector2(player.Velocity.X, -player.MaxSpeed.Y * 2);
                    }
                }

             player.Walking = true;

            }

            if (KeyboardInput.KeyboardStateNew.IsKeyDown(Game1.STP.ControlKeys["Walk right"]) && KeyboardInput.KeyboardStateNew.IsKeyUp(Game1.STP.ControlKeys["Walk left"]))
            {
                player.Velocity += new Vector2(player.Speed.X, 0);

                if (player.Velocity.X > player.MaxSpeed.X)
                {
                    player.Velocity = new Vector2(player.MaxSpeed.X, player.Velocity.Y);
                }

                if (player.Resolver.InWater == true)
                {
                    if (player.Resolver.TouchRight == true)
                    {
                        player.Velocity = new Vector2(player.Velocity.X, -player.MaxSpeed.Y * 2);
                    }
                }

                player.Walking = true;
            }

            if (KeyboardInput.KeyPressed(Game1.STP.ControlKeys["Jump"]))
            {
                if (player.Resolver.InWater == false)
                {
                    if (player.Resolver.TouchTop == true || player.Resolver.TouchTopMovable == true)
                    {
                        Game1.soundjump.Play();
                        player.Velocity = new Vector2(player.Velocity.X, -player.MaxSpeed.Y * 2);
                    }
                }
            }

            if (player.WeaponsAvailable == true)
            {
                if (MouseInput.ScrolledDown())
                {
                    player.PreviousWeapon();
                }

                if (MouseInput.ScrolledUp())
                {
                    player.NextWeapon();
                }
            }

            if (KeyboardInput.KeyPressed(Game1.STP.ControlKeys["Throw grenade"]))
            {
                if (player.WeaponsAvailable == true)
                {
                    ThrowGrenade(player);
                }
            }
        }

        public void DrawLight(Player player)
        {
            if (player.ControlsActive == true && player.VestOn == true && player.Carry == null)
                player.CurrentWeaponObject.DrawLight(new LineSegmentF(player.WeaponOrigin, ShootCalc.RayBarrel.End));
        }

        public void LeftVehicle(Player player)
        {

        }
    }
}