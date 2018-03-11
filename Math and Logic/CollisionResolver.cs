using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace Monogame_GL
{
    public class CollisionResolver
    {
        public bool TouchBottomMovable { get; set; }
        public bool TouchTopMovable { get; set; }
        public bool TouchLeftMovable { get; set; }
        public bool TouchRightMovable { get; set; }

        public bool TouchLeft { get; set; }
        public bool TouchRight { get; set; }
        public bool TouchTop { get; set; }
        public bool TouchBottom { get; set; }

        public bool InWater { get; set; }

        public bool TouchVertical { get; set; }
        public bool TouchHorizontal { get; set; }

        public bool VerticalPressure { get; set; }
        public bool HorizontalPressure { get; set; }

        public Vector2 ReceivedVelocity;

        public Vector2 VelocityReceived;

        public List<RectangleF> RecsToCheckMap { get; private set; }
        public List<RectangleF> RecsOfWater { get; private set; }

        private Vector2 maxVelocityClamped;

        private int tileSize;

        private IMap _map;

        public static CollisionResolver Standing { get { return new CollisionResolver(Globals.TileSize, true, true); } }

        public CollisionResolver(int TileSize, IMap map = null)
        {
            tileSize = TileSize;
            RecsToCheckMap = new List<RectangleF>();
            RecsOfWater = new List<RectangleF>();
            if (map == null)
                _map = Game1.mapLive;
            else
                _map = map;
        }

        private CollisionResolver(int TileSize, bool touchBottom, bool touchBottomMovable, IMap map = null)
        {
            tileSize = TileSize;
            RecsToCheckMap = new List<RectangleF>();
            RecsOfWater = new List<RectangleF>();
            TouchTop = touchBottom;
            TouchTopMovable = touchBottomMovable;
            if (map == null)
                _map = Game1.mapLive;
            else
                _map = map;
        }

        public void move(ref Vector2 velocity, Vector2 maxSpeed, RectangleF boundary, float bouncines, Vector2 friction, Vector2 dragAir, Vector2 dragWater, List<IRectanglePhysics> collection = null, float buoyancy = 0f, bool gravity = true, float buoyancyInWater = 0.0f, bool walking = false, IMap map = null)
        {
            if (map == null)
                _map = Game1.mapLive;
            else _map = map;

            InWater = false;

            RecsOfWater = CompareF.VectorVsWater(_map.WaterTree, boundary.Origin);

            if (RecsOfWater.Count > 0)
            {
                InWater = true;
            }

            if (gravity == true)
            {
                velocity += new Vector2(0, Globals.Gravity);
                velocity -= new Vector2(0, buoyancy);

                if (InWater == true)
                {
                    velocity.Y -= buoyancyInWater;
                }
            }

            if (Math.Abs(velocity.X) > maxSpeed.X)
            {
                if (velocity.X > 0)
                    velocity.X = maxSpeed.X;
                else
                    velocity.X = -maxSpeed.X;
            }
            if (Math.Abs(velocity.Y) > maxSpeed.Y)
            {
                if (velocity.Y > 0)
                    velocity.Y = maxSpeed.Y;
                else
                    velocity.Y = -maxSpeed.Y;
            }

            TouchRight = false;
            TouchLeft = false;
            TouchTop = false;
            TouchBottom = false;

            TouchBottomMovable = false;
            TouchTopMovable = false;
            TouchLeftMovable = false;
            TouchRightMovable = false;

            VerticalPressure = false;
            HorizontalPressure = false;

            TouchHorizontal = false;
            TouchVertical = false;

            VelocityReceived = Vector2.Zero;

            if (InWater == false)
            {
                velocity.X *= (1 - dragAir.X);
                velocity.Y *= (1 - dragAir.Y);
            }
            else
            {
                velocity.X *= (1 - dragWater.X);
                velocity.Y *= (1 - dragWater.Y);
            }

            int iterations = 1;

            for (int i = 0; i < iterations; i++)
            {
                boundary.Position += new Vector2(0, velocity.Y.Clamp(-maxSpeed.Y, maxSpeed.Y) * Game1.Delta) / iterations;

                maxVelocityClamped = new Vector2(velocity.X.Clamp(-maxSpeed.X, maxSpeed.X), velocity.Y.Clamp(-maxSpeed.Y, maxSpeed.Y));

                RecsToCheckMap = CompareF.RectangleVsMap(_map.MapTree, boundary);
                ResolveWithMapYMinus(ref boundary, ref velocity, friction, bouncines);
                ResolveWithMapYPlus(ref boundary, ref velocity, friction, bouncines, walking);

                YCollideMoving(ref velocity, boundary, bouncines, friction, collection,walking);

                RecsToCheckMap = CompareF.RectangleVsMap(_map.MapTree, boundary);
                ResolveWithMapYMinus(ref boundary, ref velocity, friction, bouncines);
                ResolveWithMapYPlus(ref boundary, ref velocity, friction, bouncines, walking);

                YCollideMoving(ref velocity, boundary, bouncines, friction, collection, walking);
            }

            #region map boundary

            if (boundary.Position.Y < 0)
            {
                TouchBottom = true;

                boundary.Position = new Vector2(boundary.Position.X, 0);
                boundary.Position = new Vector2(boundary.Position.X, (int)Math.Round(boundary.Position.Y));
                velocity = new Vector2(velocity.X, velocity.Y * -bouncines);
                velocity *= new Vector2(1 - friction.X, 1);
            }
            if (boundary.CornerRightBottom.Y > _map.FunctionTileMap.GetLength(1) * tileSize)
            {
                TouchTop = true;

                boundary.Position = new Vector2(boundary.Position.X, _map.FunctionTileMap.GetLength(1) * tileSize - boundary.Size.Y);
                boundary.Position = new Vector2(boundary.Position.X, (int)Math.Round(boundary.Position.Y));
                velocity = new Vector2(velocity.X, velocity.Y * -bouncines);
                velocity *= new Vector2(1 - friction.X, 1);
            }

            #endregion map boundary

            if (TouchBottomMovable && TouchTopMovable) VerticalPressure = true;
            if (TouchTopMovable && TouchBottom) VerticalPressure = true;
            if (TouchTop && TouchBottomMovable) VerticalPressure = true;

            if (TouchBottom || TouchTopMovable || TouchTop || TouchBottomMovable) TouchVertical = true;

            for (int i = 0; i < iterations; i++)
            {
                boundary.Position += new Vector2(velocity.X.Clamp(-maxSpeed.X, maxSpeed.X) * Game1.Delta, 0) / iterations;

                maxVelocityClamped = new Vector2(velocity.X.Clamp(-maxSpeed.X, maxSpeed.X), velocity.Y.Clamp(-maxSpeed.Y, maxSpeed.Y));

                RecsToCheckMap = CompareF.RectangleVsMap(_map.MapTree, boundary);
                ResolveWithMapXMinus(ref boundary, ref velocity, friction, bouncines);
                ResolveWithMapXPlus(ref boundary, ref velocity, friction, bouncines);

                XCollideMoving(ref velocity, boundary, bouncines, Vector2.Zero, collection);

                RecsToCheckMap = CompareF.RectangleVsMap(_map.MapTree, boundary);
                ResolveWithMapXMinus(ref boundary, ref velocity, friction, bouncines);
                ResolveWithMapXPlus(ref boundary, ref velocity, friction, bouncines);

                XCollideMoving(ref velocity, boundary, bouncines, Vector2.Zero, collection);
            }

            #region map boundary

            if (boundary.Position.X < 0)
            {
                TouchRight = true;

                boundary.Position = new Vector2(0, boundary.Position.Y);
                boundary.Position = new Vector2((int)Math.Round(boundary.Position.X), boundary.Position.Y);
                velocity = new Vector2(velocity.X* -bouncines, velocity.Y );
            }

            if (boundary.CornerRightTop.X > _map.FunctionTileMap.GetLength(0) * tileSize)
            {
                TouchLeft = true;

                boundary.Position = new Vector2(_map.FunctionTileMap.GetLength(0) * tileSize - boundary.Size.X, boundary.Position.Y);
                boundary.Position = new Vector2((int)Math.Round(boundary.Position.X), boundary.Position.Y);
                velocity = new Vector2(velocity.X * -bouncines, velocity.Y);
            }

            #endregion map boundary

            if (TouchRightMovable && TouchLeftMovable) HorizontalPressure = true;
            if (TouchRightMovable && TouchLeft) HorizontalPressure = true;
            if (TouchRight && TouchLeftMovable) HorizontalPressure = true;

            if (TouchLeft || TouchRightMovable || TouchRight || TouchLeftMovable) TouchHorizontal = true;
        }

        private void YCollideMoving(ref Vector2 velocity, RectangleF boundary, float bouncines, Vector2 friction, List<IRectanglePhysics> collection, bool walking)
        {
            if (collection != null)
            {
                foreach (IRectanglePhysics rec in collection)
                {
                    if (CompareF.RectangleFVsRectangleF(boundary, rec.Boundary) == true)
                    {
                        if (Math.Abs(maxVelocityClamped.X + rec.velocity.X) / boundary.IntersectionSize(rec.Boundary).X < Math.Abs(maxVelocityClamped.Y + rec.velocity.Y) / boundary.IntersectionSize(rec.Boundary).Y)
                        {
                            if (boundary.Origin.Y > rec.Boundary.Origin.Y && maxVelocityClamped.Y <= rec.velocity.Y)
                            {
                                TouchBottomMovable = true;
                                velocity = new Vector2(velocity.X, velocity.Y * -bouncines + rec.velocity.Y);
                                boundary.Position = new Vector2(boundary.Position.X, rec.Boundary.CornerRightBottom.Y);
                                velocity -= new Vector2(0, friction.Y * velocity.Y);
                                VelocityReceived.Y = rec.velocity.Y;
                            }
                            else if (boundary.Origin.Y < rec.Boundary.Origin.Y && maxVelocityClamped.Y >= rec.velocity.Y)
                            {
                                TouchTopMovable = true;

                                if (Math.Abs(velocity.Y * -bouncines) > 0.2f)
                                    velocity = new Vector2(velocity.X, velocity.Y * -bouncines + rec.velocity.Y);
                                else
                                    velocity = new Vector2(velocity.X, rec.velocity.Y);

                                    if (walking == false)
                                    velocity *= new Vector2(1 - friction.X, 1);

                                if (Math.Abs(rec.velocity.X) > Math.Abs(maxVelocityClamped.X))
                                {
                                    velocity = new Vector2(rec.velocity.X, velocity.Y);
                                    VelocityReceived.X = rec.velocity.X;

                                }



                                boundary.Position = new Vector2(boundary.Position.X, rec.Boundary.CornerRightTop.Y - boundary.Size.Y);
                                VelocityReceived.Y = rec.velocity.Y;
                            }
                        }
                    }
                }
            }
        }

        private void XCollideMoving(ref Vector2 velocity, RectangleF boundary, float bouncines, Vector2 friction, List<IRectanglePhysics> collection)
        {
            if (collection != null)
            {
                foreach (IRectanglePhysics rec in collection)
                {
                    if (CompareF.RectangleFVsRectangleF(boundary, rec.Boundary) == true)
                    {
                        if (boundary.Origin.X > rec.Boundary.Origin.X)
                        {
                            TouchRightMovable = true;
                            velocity = new Vector2(velocity.X * -bouncines + rec.velocity.X, velocity.Y);
                            boundary.Position = new Vector2(rec.Boundary.CornerRightBottom.X, boundary.Position.Y);
                            VelocityReceived.X = rec.velocity.X;
                        }
                        else
                        {
                            TouchLeftMovable = true;
                            velocity = new Vector2(velocity.X * -bouncines + rec.velocity.X, velocity.Y);
                            boundary.Position = new Vector2(rec.Boundary.CornerLeftTop.X - boundary.Size.X, boundary.Position.Y);
                            VelocityReceived.X = rec.velocity.X;
                        }
                    }
                }
            }
        }

        private void ResolveWithMapYPlus(ref RectangleF boundary, ref Vector2 velocity, Vector2 friction, float bouncines, bool walking)
        {
            int count = 0;

            float minY = float.MaxValue;

            foreach (RectangleF rec in RecsToCheckMap)
            {
                if (boundary.Origin.Y < rec.Origin.Y)
                {
                    if (minY > rec.CornerLeftTop.Y)
                    {
                        minY = rec.CornerLeftTop.Y;
                        count++;
                    }
                }
            }

            if (count > 0)
            {
                TouchTop = true;
                boundary.Position = new Vector2(boundary.Position.X, minY - boundary.Size.Y);
                velocity = new Vector2(velocity.X, velocity.Y * -bouncines);
                if (walking == false)
                    velocity *= new Vector2(1 - friction.X, 1);
            }
        }

        private void ResolveWithMapYMinus(ref RectangleF boundary, ref Vector2 velocity, Vector2 friction, float bouncines)
        {
            int count = 0;

            float maxY = -float.MaxValue;

            foreach (RectangleF rec in RecsToCheckMap)
            {
                if (boundary.Origin.Y > rec.Origin.Y)
                {
                    if (maxY < rec.CornerLeftBottom.Y)
                    {
                        maxY = rec.CornerLeftBottom.Y;
                        count++;
                    }
                }
            }

            if (count > 0 && velocity.Y < 0)
            {
                TouchBottom = true;
                boundary.Position = new Vector2(boundary.Position.X, maxY);
                velocity = new Vector2(velocity.X, velocity.Y * -bouncines);
                velocity *= new Vector2(1 - friction.X, 1);
            }
        }

        private void ResolveWithMapXPlus(ref RectangleF boundary, ref Vector2 velocity, Vector2 friction, float bouncines)
        {
            int count = 0;

            float minX = float.MaxValue;

            foreach (RectangleF rec in RecsToCheckMap)
            {
                if (boundary.Origin.X < rec.Origin.X)
                {
                    if (minX > rec.CornerLeftTop.X)
                    {
                        minX = rec.CornerLeftTop.X;
                        count++;
                    }
                }
            }

            if (count > 0)
            {
                TouchLeft = true;
                boundary.Position = new Vector2(minX - boundary.Size.X, boundary.Position.Y);
                velocity = new Vector2(velocity.X * -bouncines, velocity.Y);
            }
        }

        private void ResolveWithMapXMinus(ref RectangleF boundary, ref Vector2 velocity, Vector2 friction, float bouncines)
        {
            int count = 0;

            float maX = -float.MaxValue;

            foreach (RectangleF rec in RecsToCheckMap)
            {
                if (boundary.Origin.X >= rec.Origin.X)
                {
                    if (maX < rec.CornerRightBottom.X)
                    {
                        maX = rec.CornerRightBottom.X;
                        count++;
                    }
                }
            }

            if (count > 0)
            {
                TouchRight = true;
                boundary.Position = new Vector2(maX, boundary.Position.Y);
                velocity = new Vector2(velocity.X * -bouncines, velocity.Y);
            }
        }
    }
}