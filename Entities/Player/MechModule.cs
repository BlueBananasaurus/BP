using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Monogame_GL
{
    public class MechModule : IPlayer
    {
        public static Vector2 MechBoundarySize { get { return new Vector2(64 * 2, 96 * 2); } }
        public float RotLeg { get; set; }
        public bool WalkingVisible { get; set; }
        public float WalkSin { get; set; }
        public mechFacingLegs FacingLegs { get; set; }
        public mechFacingCabin FacingCab { get; set; }
        public Vector2 Speed { get; set; }
        public Vector2 MaxSpeed { get; set; }
        public float HealthMachine { get; set; }
        public float MaxHealth { get; set; }

        public MechModule(Player player, Vector2 center, float health)
        {
            player.Boundary.Size = MechBoundarySize;
            player.Boundary.MoveToCenter(center);
            RotLeg = 0;
            WalkingVisible = false;
            WalkSin = 0;
            FacingLegs = mechFacingLegs.right;
            FacingCab = mechFacingCabin.right;
            Speed = new Vector2(0.2f, 1.0f);
            MaxSpeed = new Vector2(0.4f);
            HealthMachine = health;
            MaxHealth = 200;
        }

        public void LineAim(Vector2 realPos, Player player)
        {
            //ray.End = realPos;
            //ray.Start = player.WeaponOrigin;

            //player.RayEnlonged = new LineSegmentF(ray.Start, ray.Start + ray.ToNormalizedVector() * player.CurrentWeaponObject.Reach);

            //player.RayBarrel.Start = player.WeaponOrigin;
            //player.RayBarrel.End = ray.Start + ray.ToNormalizedVector() * player.CurrentWeaponObject.GunBarrel;

            //player.RayDestination = CompareF.RaySegmentCalc(player.Boundary, player.RayEnlonged, this);
        }

        public void LeftVehicle(Player player)
        {
            Game1.mapLive.MapNpcs.Add(new PlayerMech(player.Boundary.Position, mechFacingCabin.left, player.Velocity, player.CurretnObjectControl.HealthMachine));
        }

        public void ShootWeapons(Player player, Vector2 realPos)
        {

        }

        public void ThrowGrenade(Player player)
        {

        }

        public void DrawNormal(Player player)
        {
        }

        public void Draw(Player player)
        {
            DrawEntities.DrawMech(player.Boundary, RotLeg, WalkingVisible, player.Velocity, player.Resolver, WalkSin, FacingLegs, FacingCab);
        }

        public void TakeDamage(ushort amount, Player player)
        {
            HealthMachine -= amount;
        }

        public void ControlPlayer(Player player)
        {
            player.Walking = false;

            if (KeyboardInput.KeyboardStateNew.IsKeyDown(Game1.STP.ControlKeys["Jump"]))
            {
                if (player.Resolver.InWater == true)
                {
                    player.Velocity = new Vector2(player.Velocity.X, -0.3f);
                }
            }

            if (KeyboardInput.KeyboardStateNew.IsKeyDown(Game1.STP.ControlKeys["Walk left"]) && KeyboardInput.KeyboardStateNew.IsKeyUp(Game1.STP.ControlKeys["Walk right"]))
            {
                player.Velocity -= new Vector2(Speed.X, 0);
                if (player.Velocity.X < -MaxSpeed.X)
                {
                    player.Velocity = new Vector2(-MaxSpeed.X, player.Velocity.Y);
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
                player.Velocity += new Vector2(Speed.X, 0);
                if (player.Velocity.X > MaxSpeed.X)
                {
                    player.Velocity = new Vector2(MaxSpeed.X, player.Velocity.Y);
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

            if (KeyboardInput.KeyboardStateOld.IsKeyUp(Game1.STP.ControlKeys["Jump"]) && KeyboardInput.KeyboardStateNew.IsKeyDown(Game1.STP.ControlKeys["Jump"]))
            {
                if (player.Resolver.InWater == false)
                {
                    if (player.Resolver.TouchBottom == true || player.Resolver.TouchTopMovable == true)
                    {
                        if (player.InVehicle == false)
                        {
                            Game1.soundjump.Play();
                            player.Velocity = new Vector2(player.Velocity.X, -player.MaxSpeed.Y * 2);
                        }
                        else
                        {
                            Game1.soundjump.Play();
                            player.Velocity = new Vector2(player.Velocity.X, -player.MaxSpeed.Y * 2);
                        }
                    }
                }
            }

            if (player.WeaponsAvailable == true)
            {
                if (MouseInput.OldScroll < MouseInput.NewScroll)
                {
                    player.NextWeapon();
                }

                if (MouseInput.OldScroll > MouseInput.NewScroll)
                {
                    player.PreviousWeapon();
                }
            }

            if (KeyboardInput.KeyboardStateOld.IsKeyUp(Game1.STP.ControlKeys["Throw grenade"]) && KeyboardInput.KeyboardStateNew.IsKeyDown(Game1.STP.ControlKeys["Throw grenade"]))
            {
                if (player.WeaponsAvailable == true)
                {
                    ThrowGrenade(player);
                }
            }
        }

        public void Push(Player player,Vector2 velocity)
        {
            player.Velocity += new Vector2(velocity.X, velocity.Y)/10;
        }

        public void PhysicsMove(Player player)
        {
            if (player.ControlsActive == true) player.Resolver.move(ref player.Velocity, new Vector2(2f), player.Boundary, 0f, new Vector2(0.2f, 0f), new Vector2(0.1f, 0.02f), new Vector2(0.3f), Game1.mapLive.MapMovables, 0, player.Walking);

            if (HealthMachine <= 0 || player.Resolver.VerticalPressure == true || player.Resolver.HorizontalPressure == true)
            {
                player.DestroyVehicle();
            }
        }

        public void HowILookCalc(Player player)
        {
            if (MouseInput.MouseRealPosGame().X > player.Boundary.Origin.X)
                FacingCab = mechFacingCabin.right;
            if (MouseInput.MouseRealPosGame().X < player.Boundary.Origin.X)
                FacingCab = mechFacingCabin.left;

            if (player.Resolver.TouchBottom || player.Resolver.InWater || player.Resolver.TouchTopMovable)
            {
                WalkingVisible = true;

                RotLeg += Game1.Delta / 25 * player.Velocity.X;
                WalkSin += Game1.Delta / 25 * player.Velocity.X;

                if (Math.Abs(player.Velocity.X - player.Resolver.VelocityReceived.X) < 0.05f)
                {
                    WalkingVisible = false;
                }
            }
            else
            {
                WalkingVisible = false;
            }

            if (player.Velocity.X > 0)
            {
                FacingLegs = mechFacingLegs.right;
            }
            if (player.Velocity.X < 0)
            {
                FacingLegs = mechFacingLegs.left;
            }
        }

        public void DrawLight(Player player)
        {

        }
    }
}