using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Monogame_GL
{
    public class DrawEntities
    {
        public static void DrawZombie(bool walking, CollisionResolver resolver, float bodyRotation, RectangleF boundary, float rotLeg, Vector2 velocity)
        {
            Game1.SpriteBatchGlobal.Draw(Game1.zombie, boundary.Position + new Vector2(24, 62), rotation: bodyRotation, origin: new Vector2(11, 31));

            Vector2 rightLegJoin = boundary.Origin + new Vector2(+12, +50);
            Vector2 leftLegJoin = boundary.Origin + new Vector2(-12, +50);

            if (walking == true)
            {
                if (velocity.X < 0)
                {
                    Game1.SpriteBatchGlobal.Draw(Game1.footWalk, new Vector2((float)Math.Round(CompareF.OriginRotateOffset(new Vector2(0, 8), new Vector2(0), rotLeg, leftLegJoin).X), (float)Math.Round(CompareF.OriginRotateOffset(new Vector2(0, 8), new Vector2(0), rotLeg, leftLegJoin).Y)), origin: new Vector2(10f, 5), effects: SpriteEffects.FlipHorizontally);
                    Game1.SpriteBatchGlobal.Draw(Game1.footWalk, new Vector2((float)Math.Round(CompareF.OriginRotateOffset(new Vector2(0, 8), new Vector2(0), rotLeg + (float)Math.PI, rightLegJoin).X), (float)Math.Round(CompareF.OriginRotateOffset(new Vector2(0, 8), new Vector2(0), rotLeg + (float)Math.PI, rightLegJoin).Y)), origin: new Vector2(10f, 5), effects: SpriteEffects.FlipHorizontally);
                }
                if (velocity.X > 0)
                {
                    Game1.SpriteBatchGlobal.Draw(Game1.footWalk, new Vector2((float)Math.Round(CompareF.OriginRotateOffset(new Vector2(0, 8), new Vector2(0), rotLeg, leftLegJoin).X), (float)Math.Round(CompareF.OriginRotateOffset(new Vector2(0, 8), new Vector2(0), rotLeg, leftLegJoin).Y)), origin: new Vector2(2f, 5));
                    Game1.SpriteBatchGlobal.Draw(Game1.footWalk, new Vector2((float)Math.Round(CompareF.OriginRotateOffset(new Vector2(0, 8), new Vector2(0), rotLeg + (float)Math.PI, rightLegJoin).X), (float)Math.Round(CompareF.OriginRotateOffset(new Vector2(0, 8), new Vector2(0), rotLeg + (float)Math.PI, rightLegJoin).Y)), origin: new Vector2(2f, 5));
                }
            }
            else
            {
                if (resolver.TouchBottom == false && resolver.TouchTopMovable == false)
                {
                    Game1.SpriteBatchGlobal.Draw(Game1.footAir, new Vector2((float)Math.Round(leftLegJoin.X), (float)Math.Round(leftLegJoin.Y)), origin: new Vector2(4.5f, 5));
                    Game1.SpriteBatchGlobal.Draw(Game1.footAir, new Vector2((float)Math.Round(rightLegJoin.X), (float)Math.Round(rightLegJoin.Y)), origin: new Vector2(4.5f, 5));
                }
                else
                {
                    Game1.SpriteBatchGlobal.Draw(Game1.foot, new Vector2((float)Math.Round(leftLegJoin.X), (float)Math.Round(leftLegJoin.Y + 8)), origin: new Vector2(4.5f, 5));
                    Game1.SpriteBatchGlobal.Draw(Game1.foot, new Vector2((float)Math.Round(rightLegJoin.X), (float)Math.Round(rightLegJoin.Y + 8)), origin: new Vector2(4.5f, 5));
                }
            }
        }

        public static void DrawSaw(Vector2 origin, float rotation)
        {
            Game1.SpriteBatchGlobal.Draw(Game1.saw, origin, origin: new Vector2(32f), rotation: rotation);
            Game1.SpriteBatchGlobal.Draw(Game1.sawNut, origin - new Vector2(16));
        }

        public static void DrawSawDefault(Vector2 origin)
        {
            DrawSaw(origin, 0f);
        }

        public static void DrawBuddy(RectangleF boundary, float bodyRotation, float rotLeg, bool walking, bool vest, Vector2 velocity, CollisionResolver resolver)
        {
            Game1.SpriteBatchGlobal.Draw(Game1.playerTex, new Vector2((float)Math.Round(boundary.Position.X), (float)Math.Round(boundary.Position.Y)) + new Vector2(24, 62),rotation: bodyRotation, origin: new Vector2(12*2, 31*2));

            if (vest == true)
            {
                Game1.SpriteBatchGlobal.Draw(Game1.Textures["PlayerVest"], new Vector2((float)Math.Round(boundary.Position.X), (float)Math.Round(boundary.Position.Y)) + new Vector2(24, 62),rotation: bodyRotation, origin: new Vector2(12*2, 31*2));
            }

            Vector2 rightLegJoin = boundary.Origin + new Vector2(+12, +50);
            Vector2 leftLegJoin = boundary.Origin + new Vector2(-12, +50);

            if (walking == true)
            {
                if (velocity.X - resolver.VelocityReceived.X < 0)
                {
                    Game1.SpriteBatchGlobal.Draw(Game1.footWalk, new Vector2((float)Math.Round(CompareF.OriginRotateOffset(new Vector2(0, 8), new Vector2(0), rotLeg, leftLegJoin).X), (float)Math.Round(CompareF.OriginRotateOffset(new Vector2(0, 8), new Vector2(0), rotLeg, leftLegJoin).Y)), origin: new Vector2(10f*2, 5*2), effects: SpriteEffects.FlipHorizontally);
                    Game1.SpriteBatchGlobal.Draw(Game1.footWalk, new Vector2((float)Math.Round(CompareF.OriginRotateOffset(new Vector2(0, 8), new Vector2(0), rotLeg + (float)Math.PI, rightLegJoin).X), (float)Math.Round(CompareF.OriginRotateOffset(new Vector2(0, 8), new Vector2(0), rotLeg + (float)Math.PI, rightLegJoin).Y)), origin: new Vector2(10f*2, 5*2), effects: SpriteEffects.FlipHorizontally);
                }
                if (velocity.X - resolver.VelocityReceived.X > 0)
                {
                    Game1.SpriteBatchGlobal.Draw(Game1.footWalk, new Vector2((float)Math.Round(CompareF.OriginRotateOffset(new Vector2(0, 8), new Vector2(0), rotLeg, leftLegJoin).X), (float)Math.Round(CompareF.OriginRotateOffset(new Vector2(0, 8), new Vector2(0), rotLeg, leftLegJoin).Y)), origin: new Vector2(2*2f, 5*2));
                    Game1.SpriteBatchGlobal.Draw(Game1.footWalk, new Vector2((float)Math.Round(CompareF.OriginRotateOffset(new Vector2(0, 8), new Vector2(0), rotLeg + (float)Math.PI, rightLegJoin).X), (float)Math.Round(CompareF.OriginRotateOffset(new Vector2(0, 8), new Vector2(0), rotLeg + (float)Math.PI, rightLegJoin).Y)), origin: new Vector2(2f*2, 5*2));
                }
            }
            else
            {
                if (resolver.TouchTop == false && resolver.TouchTopMovable == false)
                {
                    Game1.SpriteBatchGlobal.Draw(Game1.footAir, new Vector2((float)Math.Round(leftLegJoin.X), (float)Math.Round(leftLegJoin.Y)), origin: new Vector2(4.5f*2, 5*2));
                    Game1.SpriteBatchGlobal.Draw(Game1.footAir, new Vector2((float)Math.Round(rightLegJoin.X), (float)Math.Round(rightLegJoin.Y)), origin: new Vector2(4.5f*2, 5*2));
                }
                else
                {
                    Game1.SpriteBatchGlobal.Draw(Game1.foot, new Vector2((float)Math.Round(leftLegJoin.X), (float)Math.Round(leftLegJoin.Y + 8)), origin: new Vector2(4.5f*2, 5*2));
                    Game1.SpriteBatchGlobal.Draw(Game1.foot, new Vector2((float)Math.Round(rightLegJoin.X), (float)Math.Round(rightLegJoin.Y + 8)), origin: new Vector2(4.5f*2, 5*2));
                }
            }
        }

        public static void DrawBuddyNormal(RectangleF boundary, float bodyRotation, float rotLeg, bool walking, bool vest, Vector2 velocity, CollisionResolver resolver)
        {
            Game1.SpriteBatchGlobal.Draw(Game1.Textures["playerNormal"], new Vector2((float)Math.Round(boundary.Position.X), (float)Math.Round(boundary.Position.Y)) + new Vector2(24, 62), rotation: bodyRotation, origin: new Vector2(12 * 2, 31 * 2));

            if (vest == true)
            {
                Game1.EffectRotateNormals.Parameters["flip"].SetValue(new Vector2(1,1));
                Game1.EffectRotateNormals.Parameters["angle"].SetValue(bodyRotation);
                Game1.EffectRotateNormals.CurrentTechnique.Passes[0].Apply();
                Game1.SpriteBatchGlobal.Draw(Game1.Textures["PlayerVestNormal"], new Vector2((float)Math.Round(boundary.Position.X), (float)Math.Round(boundary.Position.Y)) + new Vector2(24, 62), rotation: bodyRotation, origin: new Vector2(12 * 2, 31 * 2));
                Effects.ResetEffect3D();
            }
        }

        public static void DrawBuddyDefault(Vector2 position)
        {
            DrawBuddy(new RectangleF(24 * 2, 62 * 2, position.X, position.Y), 0, 0, false, true, Vector2.Zero, CollisionResolver.Standing);
        }

        public static void DrawAircraft(RectangleF boundary,float rotation)
        {
            Vector2 thrusterLeft = new Vector2((int)Math.Round(boundary.Position.X), (int)Math.Round(boundary.Position.Y)) + new Vector2(32,48);
            Vector2 thrusterRight = new Vector2((int)Math.Round(boundary.CornerRightTop.X), (int)Math.Round(boundary.Position.Y)) + new Vector2(-32, 48);

            Game1.SpriteBatchGlobal.Draw(Game1.Textures["Aircraft"], new Point((int)Math.Round(boundary.Position.X), (int)Math.Round(boundary.Position.Y)).ToVector2());
            Game1.SpriteBatchGlobal.Draw(Game1.Textures["AircraftThruster"], thrusterLeft,origin: new Vector2(12,18.5f),rotation: rotation);
            Game1.SpriteBatchGlobal.Draw(Game1.Textures["AircraftThruster"], thrusterRight, origin: new Vector2(12, 18.5f), rotation: rotation);

        }

        public static void DrawMech(RectangleF boundary, float rotLeg, bool walking, Vector2 velocity, CollisionResolver resolver, float walkSin, mechFacingLegs facingLegs, mechFacingCabin facingCabin)
        {
            Vector2 rightLegJoin = boundary.Origin + new Vector2(+12 + 16, +42);
            Vector2 leftLegJoin = boundary.Origin + new Vector2(-12-16, +42);

            float jumpJoints = 4;

            if (walking == true)
            {
                if (velocity.X < 0)
                {
                    Game1.SpriteBatchGlobal.Draw(Game1.Textures["MechLeftLeg"], new Vector2((float)Math.Round(CompareF.OriginRotateOffset(new Vector2(0, 7), new Vector2(0), rotLeg, leftLegJoin).X), (float)Math.Round(CompareF.OriginRotateOffset(new Vector2(0, 8), new Vector2(0), rotLeg, leftLegJoin).Y)), origin: new Vector2(18.5f * 2, 12 * 2), effects: SpriteEffects.FlipHorizontally);
                }
                if (velocity.X > 0)
                {
                    Game1.SpriteBatchGlobal.Draw(Game1.Textures["MechLeftLeg"], new Vector2((float)Math.Round(CompareF.OriginRotateOffset(new Vector2(0, 7), new Vector2(0), rotLeg + (float)Math.PI, rightLegJoin).X), (float)Math.Round(CompareF.OriginRotateOffset(new Vector2(0, 8), new Vector2(0), rotLeg + (float)Math.PI, rightLegJoin).Y)), origin: new Vector2(18.5f * 2, 12 * 2));
                }
            }
            else
            {
                if (resolver.TouchBottom == true || resolver.TouchTopMovable == true)
                {
                    if (facingLegs == mechFacingLegs.right)
                    {
                        Game1.SpriteBatchGlobal.Draw(Game1.Textures["MechLeftLeg"], rightLegJoin + new Vector2(0, 7), origin: new Vector2(18.5f * 2, 12 * 2));
                    }
                    if (facingLegs == mechFacingLegs.left)
                    {
                        Game1.SpriteBatchGlobal.Draw(Game1.Textures["MechLeftLeg"], leftLegJoin + new Vector2(0, 7), origin: new Vector2(18.5f * 2, 12 * 2), effects: SpriteEffects.FlipHorizontally);
                    }
                }
                else
                {
                    if (facingLegs == mechFacingLegs.right)
                    {
                        Game1.SpriteBatchGlobal.Draw(Game1.Textures["MechLeftLeg"], rightLegJoin - new Vector2(0, jumpJoints), origin: new Vector2(18.5f * 2, 12 * 2));
                    }
                    if (facingLegs == mechFacingLegs.left)
                    {
                        Game1.SpriteBatchGlobal.Draw(Game1.Textures["MechLeftLeg"], leftLegJoin - new Vector2(0, jumpJoints), origin: new Vector2(18.5f * 2, 12 * 2), effects: SpriteEffects.FlipHorizontally);
                    }
                }
            }

            if (facingCabin ==  mechFacingCabin.right)
            {
                Game1.SpriteBatchGlobal.Draw(texture: Game1.Textures["MechBody"], position: boundary.Position + new Vector2(0, (int)Math.Round((float)(Math.Sin(walkSin)))));
            }
            if (facingCabin == mechFacingCabin.left)
            {
                Game1.SpriteBatchGlobal.Draw(texture: Game1.Textures["MechBody"], position: boundary.Position + new Vector2(0, (int)Math.Round((float)Math.Sin(walkSin))), effects: SpriteEffects.FlipHorizontally);
            }

            if (walking == true)
            {
                if (velocity.X < 0)
                {
                    Game1.SpriteBatchGlobal.Draw(Game1.Textures["MechRightLeg"], new Vector2((float)Math.Round(CompareF.OriginRotateOffset(new Vector2(0, 7), new Vector2(0), rotLeg + (float)Math.PI, rightLegJoin).X), (float)Math.Round(CompareF.OriginRotateOffset(new Vector2(0, 8), new Vector2(0), rotLeg + (float)Math.PI, rightLegJoin).Y)), origin: new Vector2(18.5f * 2, 12 * 2), effects: SpriteEffects.FlipHorizontally);
                }
                if (velocity.X > 0)
                {
                    Game1.SpriteBatchGlobal.Draw(Game1.Textures["MechRightLeg"], new Vector2((float)Math.Round(CompareF.OriginRotateOffset(new Vector2(0, 7), new Vector2(0), rotLeg, leftLegJoin).X), (float)Math.Round(CompareF.OriginRotateOffset(new Vector2(0, 8), new Vector2(0), rotLeg, leftLegJoin).Y)), origin: new Vector2(18.5f * 2, 12 * 2));
                }
            }
            else
            {
                if (resolver.TouchBottom == true || resolver.TouchTopMovable == true)
                {
                    if (facingLegs == mechFacingLegs.right)
                    {
                        Game1.SpriteBatchGlobal.Draw(Game1.Textures["MechRightLeg"], leftLegJoin + new Vector2(0, 7), origin: new Vector2(18.5f * 2, 12 * 2));
                    }
                    if (facingLegs == mechFacingLegs.left)
                    {
                        Game1.SpriteBatchGlobal.Draw(Game1.Textures["MechRightLeg"], rightLegJoin + new Vector2(0, 7), origin: new Vector2(18.5f * 2, 12 * 2), effects: SpriteEffects.FlipHorizontally);
                    }
                }
                else
                {
                    if (facingLegs == mechFacingLegs.right)
                    {
                        Game1.SpriteBatchGlobal.Draw(Game1.Textures["MechRightLeg"], leftLegJoin - new Vector2(0, jumpJoints), origin: new Vector2(18.5f*2, 12*2));
                    }
                    if (facingLegs == mechFacingLegs.left)
                    {
                        Game1.SpriteBatchGlobal.Draw(Game1.Textures["MechRightLeg"], rightLegJoin - new Vector2(0, jumpJoints), origin: new Vector2(18.5f*2, 12*2), effects: SpriteEffects.FlipHorizontally);
                    }
                }
            }
        }

        public static void DrawMechDefault(Vector2 position)
        {
            DrawMech(new RectangleF(64 * 2, 96 * 2, position.X, position.Y), 0, false, Vector2.Zero, CollisionResolver.Standing, 0, mechFacingLegs.right, mechFacingCabin.right);
        }

        public static void DrawMechDefault(Vector2 position, mechFacingCabin face)
        {
            DrawMech(new RectangleF(64 * 2, 96 * 2, position.X, position.Y), 0, false, Vector2.Zero, CollisionResolver.Standing,0, mechFacingLegs.right, face);
        }

        public static void MechGunsFront(Vector2 mechLeftArmJoinFaceRight, Vector2 mechRightArmJoinFaceLeft)
        {
        //    if ((Game1.PlayerInstance.RayBarrel.ToAngle() * (180 / Math.PI)) + 180 > 90 && ((Game1.PlayerInstance.RayBarrel.ToAngle() * (180 / Math.PI)) + 180 < 270))
        //    {
        //        Game1.SpriteBatchGlobal.Draw(Game1.Textures["MechGun"], mechLeftArmJoinFaceRight, origin: new Vector2(-16 + Game1.PlayerInstance.LeftKickMech, 7), rotation: Game1.PlayerInstance.RayMechLeft.ToAngle());
        //    }
        //    else
        //    {
        //        Game1.SpriteBatchGlobal.Draw(Game1.Textures["MechGun"], mechRightArmJoinFaceLeft, origin: new Vector2(-16 + Game1.PlayerInstance.RightKickMech, 12), rotation: Game1.PlayerInstance.RayMechRight.ToAngle(), effects: SpriteEffects.FlipVertically);
        //    }
        //}

        //public static void MechGunsBack(Vector2 mechRightArmJoinFaceRight, Vector2 mechLeftArmJoinFaceLeft)
        //{
        //    if ((Game1.PlayerInstance.RayBarrel.ToAngle() * (180 / Math.PI)) + 180 > 90 && (Game1.PlayerInstance.RayBarrel.ToAngle() * (180 / Math.PI)) + 180 < 270)
        //    {
        //        Game1.SpriteBatchGlobal.Draw(Game1.Textures["MechGun"], mechRightArmJoinFaceRight, origin: new Vector2(-16 + Game1.PlayerInstance.RightKickMech, 7), rotation: Game1.PlayerInstance.RayMechRight.ToAngle());
        //    }
        //    else
        //    {
        //        Game1.SpriteBatchGlobal.Draw(Game1.Textures["MechGun"], mechLeftArmJoinFaceLeft, origin: new Vector2(-16 + Game1.PlayerInstance.LeftKickMech, 12), rotation: Game1.PlayerInstance.RayMechLeft.ToAngle(), effects: SpriteEffects.FlipVertically);
        //    }
        }

        public static void DrawPlayerTurret(RectangleF boundary, float rotation, float kick)
        {
            Vector2 headPos = boundary.Origin - new Vector2(0, 24);
            Game1.SpriteBatchGlobal.Draw(texture: Game1.playerTurretBase, position: boundary.Position);
            Game1.SpriteBatchGlobal.Draw(texture: Game1.playerTurretHead, position: headPos, origin: new Vector2(20 + kick, 13f), rotation: rotation);
        }

        public static void DrawPlayerTurretDefault(Vector2 position)
        {
            Vector2 headPos = position - new Vector2(0, 24) ;
            Game1.SpriteBatchGlobal.Draw(texture: Game1.playerTurretBase, position: position- new Vector2(36/2,32));
            Game1.SpriteBatchGlobal.Draw(texture: Game1.playerTurretHead, position: headPos, origin: new Vector2(20 + 0, 13f), rotation: 0);
        }

        public static void DrawPlayerTurretNormal(RectangleF boundary, float rotation, float kick)
        {
            Vector2 headPos = boundary.Origin - new Vector2(0, 24);
            Game1.SpriteBatchGlobal.Draw(texture: Game1.Textures["playerTurretBaseNormal"], position: boundary.Position);
            Game1.EffectRotateNormals.Parameters["flip"].SetValue(new Vector2(1,1));
            Game1.EffectRotateNormals.Parameters["angle"].SetValue(rotation);
            Game1.EffectRotateNormals.CurrentTechnique.Passes[0].Apply();
            Game1.SpriteBatchGlobal.Draw(texture: Game1.Textures["playerTurretHeadNormal"], position: headPos, origin: new Vector2(20 + kick, 13f), rotation: rotation);
            Effects.ResetEffect3D();
        }

        public static void DrawCrate(Vector2 position)
        {
            Game1.SpriteBatchGlobal.Draw(Game1.LootCrateStain, position, sourceRectangle: new Rectangle(0, 0,64, 64));
        }

        public static void DrawCrateNormal(Vector2 position)
        {
            Game1.SpriteBatchGlobal.Draw(Game1.Textures["LootCrateStainNormal"], position, sourceRectangle: new Rectangle(0, 0, 64, 64));
        }

        public static void DrawShield(Vector2 position)
        {
            Game1.SpriteBatchGlobal.Draw(Game1.Textures["shieldPick"], position, origin: new Vector2(24));
        }

        public static void DrawHealth(Vector2 position)
        {
            Game1.SpriteBatchGlobal.Draw(Game1.Textures["healthPick"], position, origin: new Vector2(24));
        }

        public static void DrawGear(Vector2 position, Vector2 size)
        {
            Game1.SpriteBatchGlobal.Draw(Game1.Textures["PickableGear"], position, scale: size, origin: new Vector2(12));
        }

        public static void DrawTrigger(Vector2 position, bool on, TriggerTypes type)
        {
            switch (type)
            {
                case TriggerTypes.triggerOld:
                    if (on)
                        Game1.SpriteBatchGlobal.Draw(Game1.triggerOnTex, position);
                    else
                        Game1.SpriteBatchGlobal.Draw(Game1.triggerOffTex, position);
                    break;

                case TriggerTypes.triggerBase:
                    if (on)
                        Game1.SpriteBatchGlobal.Draw(Game1.Textures["TriggerBase"], position, sourceRectangle: new Rectangle(0, 0, 24, 48));
                    else
                        Game1.SpriteBatchGlobal.Draw(Game1.Textures["TriggerBase"], position, sourceRectangle: new Rectangle(24, 0, 24, 48));
                    break;
            }
        }

        public static void DrawTriggerDefault(Vector2 position)
        {
            DrawTrigger(position, true, TriggerTypes.triggerOld);
        }
    }
}