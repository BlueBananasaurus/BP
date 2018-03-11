using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Linq;
using System;

namespace Monogame_GL
{
    public static class GameDraw
    {
        static SamplerState state = SamplerState.PointWrap;

        public static void UpdateFiltering()
        {
            switch (Globals.filter)
            {
                case Filtering.point:
                    state = SamplerState.PointWrap;
                    break;
                case Filtering.linear:
                    state = SamplerState.LinearWrap;
                    break;
                case Filtering.anisotropic:
                    state = SamplerState.AnisotropicWrap;
                    break;
            }
        }

        public static void DrawEntity(AllEntitiesEditor type, Vector2 size, Vector2 position)
        {
            DrawRectangleBoundary.DrawWhite(new Rectangle((int)position.X, (int)position.Y, (int)size.X, (int)size.Y));

            if (type == AllEntitiesEditor.player)
                DrawEntities.DrawBuddyDefault(new Vector2((int)position.X, (int)position.Y));
            if (type == AllEntitiesEditor.mech)
                DrawEntities.DrawMechDefault(new Vector2((int)position.X, (int)position.Y));
        }

        public static void DrawEditor()
        {
            Game1.GraphicsGlobal.GraphicsDevice.SetRenderTargets(Game1.EditorBackground);
            Game1.SpriteBatchGlobal.Begin(samplerState: SamplerState.PointWrap, blendState: BlendState.AlphaBlend, sortMode: SpriteSortMode.Immediate, transformMatrix: Camera2DEditor.GetViewMatrix());
            Game1.SpriteBatchGlobal.Draw(Game1.Textures["BackgroundEditor"], Vector2.Zero, sourceRectangle: new Rectangle(0, 0, (int)Editor.EditMap.MapBoundary.Size.X, (int)Editor.EditMap.MapBoundary.Size.Y));

            Editor.EditMap.Draw();

            Editor.DrawEntityCursor();

            Game1.SpriteBatchGlobal.End();

            Game1.SpriteBatchGlobal.Begin();
            foreach (UIInformer info in Game1.InfoList.Reverse<UIInformer>())
            {
                info.Position = new Vector2(8, Game1.InfoList.IndexOf(info) * 32 + 8);
                info.Draw();
            }
            Game1.SpriteBatchGlobal.End();
        }

        public static void DrawWin()
        {
            RenderTargetsSettings.SetAndClear(Game1.Win, Color.Transparent);
            Game1.SpriteBatchGlobal.Begin(samplerState: SamplerState.PointWrap, blendState: BlendState.AlphaBlend, sortMode: SpriteSortMode.Immediate);
            Game1.SpriteBatchGlobal.Draw(Game1.backgroundWin, Vector2.Zero, Color.White);
            foreach (UIInformer info in Game1.InfoList.Reverse<UIInformer>())
            {
                info.Position = new Vector2(8, Game1.InfoList.IndexOf(info) * 32 + 8);
                info.Draw();
            }
            Game1.SpriteBatchGlobal.End();
            Game1.Windows[Game1.ActualWindow].Draw();
        }

        public static void DrawEditorWin()
        {
            RenderTargetsSettings.SetAndClear(Game1.Win, Color.Transparent);
            Game1.SpriteBatchGlobal.Begin(samplerState: SamplerState.PointWrap, blendState: BlendState.AlphaBlend, sortMode: SpriteSortMode.Immediate);
            Game1.SpriteBatchGlobal.Draw(Game1.Textures["BackgroundEditorMenu"], Vector2.Zero, Color.White);
            Editor.Draw();
            foreach (UIInformer info in Game1.InfoList.Reverse<UIInformer>())
            {
                info.Position = new Vector2(8, Game1.InfoList.IndexOf(info) * 32 + 8);
                info.Draw();
            }
            Game1.SpriteBatchGlobal.End();
        }

        private static void DrawFinal(IMap map)
        {
            Game1.GraphicsGlobal.GraphicsDevice.SetRenderTargets(Game1.FinalScreen);
            Game1.SpriteBatchGlobal.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointWrap);
            Game1.SpriteBatchGlobal.Draw(Game1.WaterWithoutLights, Vector2.Zero);
            Game1.SpriteBatchGlobal.End();
            if (map is Map)
            {
                Game1.SpriteBatchGlobal.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointWrap, transformMatrix: Camera2DGame.GetViewMatrix());

                foreach (Inpc ui in (map as Map).MapNpcs)
                {
                    if (ui as IUI != null)
                        (ui as IUI).DrawUI();
                }

                Game1.SpriteBatchGlobal.End();
            }
            Game1.SpriteBatchGlobal.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointWrap);
            if (Game1.State == GlobalState.inGame)
            {
                UIPlayer.Draw();
            }
            foreach (UIInformer info in Game1.InfoList.Reverse<UIInformer>())
            {
                info.Position = new Vector2(8, Game1.InfoList.IndexOf(info) * 32 + 8);
                info.Draw();
            }
            Game1.SpriteBatchGlobal.End();
        }

        private static void DrawFinalWorld(IMap map)
        {
            Game1.GraphicsGlobal.GraphicsDevice.SetRenderTargets(Game1.FinalScreen);
            Game1.SpriteBatchGlobal.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointWrap);
            Game1.SpriteBatchGlobal.Draw(Game1.Map, Vector2.Zero);
            Game1.SpriteBatchGlobal.End();
            if (map is Map)
            {
                Game1.SpriteBatchGlobal.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointWrap, transformMatrix: Camera2DGame.GetViewMatrix());

                foreach (Inpc ui in (map as Map).MapNpcs)
                {
                    if (ui as IUI != null)
                        (ui as IUI).DrawUI();
                }

                Game1.SpriteBatchGlobal.End();
            }
            Game1.SpriteBatchGlobal.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointWrap);
            if (Game1.State == GlobalState.inGame)
            {
                UIPlayer.Draw();
            }
            foreach (UIInformer info in Game1.InfoList.Reverse<UIInformer>())
            {
                info.Position = new Vector2(8, Game1.InfoList.IndexOf(info) * 32 + 8);
                info.Draw();
            }
            Game1.SpriteBatchGlobal.End();
        }

        private static void DrawApplyLights()
        {
            Game1.GraphicsGlobal.GraphicsDevice.SetRenderTargets(Game1.Map);
            Game1.SpriteBatchGlobal.Begin(SpriteSortMode.Immediate, null, SamplerState.PointWrap);
            Game1.EffectLights.Parameters["lightMask"].SetValue(Game1.Lights);
            Game1.EffectLights.CurrentTechnique.Passes[0].Apply();
            Game1.SpriteBatchGlobal.Draw(Game1.TARGET_ALL, Vector2.Zero, Color.White);
            Effects.ResetEffect3D();

            foreach(DecorationStatic decoration in Game1.mapLive.mapDecorations)
            {
                decoration.DrawFlare();
            }

            Game1.SpriteBatchGlobal.End();
            Game1.mapLive.DrawWaterTop();
        }

        private static void DrawApplyLightsWorld()
        {
            Game1.GraphicsGlobal.GraphicsDevice.SetRenderTargets(Game1.Map);
            Game1.SpriteBatchGlobal.Begin(SpriteSortMode.Immediate, null, SamplerState.PointWrap);
            Game1.EffectLights.Parameters["lightMask"].SetValue(Game1.Lights);
            Game1.EffectLights.CurrentTechnique.Passes[0].Apply();
            Game1.SpriteBatchGlobal.Draw(Game1.TARGET_ALL, Vector2.Zero, Color.White);
            Effects.ResetEffect3D();

            Game1.SpriteBatchGlobal.End();
            Game1.mapLive.DrawWaterTop();
        }

        private static void DrawWater(IMap map, Vector2 position)
        {
            Game1.GraphicsGlobal.GraphicsDevice.SetRenderTargets(Game1.WaterWithoutLights);
            Game1.SpriteBatchGlobal.Begin(SpriteSortMode.Immediate, null, SamplerState.PointWrap);
            Effects.WaterEffect(new Vector2(128, 128), new Vector2(512, 512), new Vector2(Game1.Time * 5, Game1.Time * 5), position, Game1.WaterMask);
            Game1.SpriteBatchGlobal.Draw(Game1.Map, position: Vector2.Zero);
            Game1.SpriteBatchGlobal.End();
        }

        private static void DrawLayer1And2(IMap map)
        {
            Game1.GraphicsGlobal.GraphicsDevice.SetRenderTargets(Game1.TARGET_ALL);
            Game1.SpriteBatchGlobal.Begin(SpriteSortMode.Immediate, samplerState: SamplerState.PointWrap);
            Game1.SpriteBatchGlobal.Draw(Game1.Layer1, Vector2.Zero);
            Game1.SpriteBatchGlobal.Draw(Game1.Layer2, Vector2.Zero);
            Game1.SpriteBatchGlobal.End();
        }

        private static void DrawWaterMask(IMap map)
        {
            RenderTargetsSettings.SetAndClear(Game1.WaterMask, Color.Black);
            map.DrawWaterMask();
        }

        private static void DrawWaterMaskWorld(IMap map)
        {
            RenderTargetsSettings.SetAndClear(Game1.WaterMask, Color.Black);
            map.DrawWaterMask();
        }

        private static void DrawLayer2(IMap map)
        {
            RenderTargetsSettings.SetAndClear(Game1.Layer2, Color.Transparent);
            map.DrawLayer2();
        }

        private static void DrawLayer1(IMap map)
        {
            RenderTargetsSettings.SetAndClear(Game1.Layer1, Color.Transparent);
            map.Draw();
        }

        private static void DrawLights(IMap map)
        {
            RenderTargetsSettings.SetAndClear(Game1.Lights, new Color(map.LightLevel));
            Game1.SpriteBatchGlobal.Begin(SpriteSortMode.Immediate, BlendState.Additive, SamplerState.PointClamp, transformMatrix: Camera2DGame.GetViewMatrix());
            map.DrawLights();
            Game1.SpriteBatchGlobal.End();
            Game1.SpriteBatchGlobal.Begin(SpriteSortMode.Immediate, BlendState.Additive, SamplerState.PointClamp);
            Game1.SpriteBatchGlobal.Draw(Game1.LightNormalHolder, Vector2.Zero);
            Game1.SpriteBatchGlobal.End();
        }

        private static void DrawNormals(IMap map)
        {
            RenderTargetsSettings.SetAndClear(Game1.NormalMapBuffer, Color.Transparent);
            map.DrawNormal();
        }

        private static void DrawLightsNormals()
        {
            RenderTargetsSettings.SetAndClear(Game1.LightNormalHolder, Color.Black);
            Game1.SpriteBatchGlobal.Begin(SpriteSortMode.Immediate, BlendState.Additive, SamplerState.PointClamp);

            foreach(DecorationStatic decor in Game1.mapLive.mapDecorations)
            {
                if(decor.LightObj?.Position !=null && decor.LightObj?.LightColor != null)
                {
                    Game1.EffectNormalLight.Parameters["size"].SetValue(Globals.WinRenderSize);
                    Game1.EffectNormalLight.Parameters["lightColor"].SetValue(decor.LightObj.LightColor);
                    Game1.EffectNormalLight.Parameters["positionTex"].SetValue(Camera2DGame.Position);
                    Game1.EffectNormalLight.Parameters["light"].SetValue(decor.LightObj.Position);
                    Game1.EffectNormalLight.Parameters["normal"].SetValue(Game1.NormalMapBuffer);
                    Game1.EffectNormalLight.CurrentTechnique.Passes[0].Apply();
                    Game1.SpriteBatchGlobal.Draw(Game1.Textures["testaaa"], Vector2.Zero, Color.White);
                }
            }

            Game1.SpriteBatchGlobal.End();
        }

        private static void DrawLightsNormalsWorld()
        {
            RenderTargetsSettings.SetAndClear(Game1.LightNormalHolder, Color.Black);
            Game1.SpriteBatchGlobal.Begin(SpriteSortMode.Immediate, BlendState.Additive, SamplerState.PointClamp);

            foreach (DecorationStatic decor in Game1.mapLiveWorld.mapDecorations)
            {
                if (decor.LightObj?.Position != null && decor.LightObj?.LightColor != null)
                {
                    Game1.EffectNormalLight.Parameters["size"].SetValue(Globals.WinRenderSize);
                    Game1.EffectNormalLight.Parameters["lightColor"].SetValue(decor.LightObj.LightColor);
                    Game1.EffectNormalLight.Parameters["positionTex"].SetValue(Camera2DGame.Position);
                    Game1.EffectNormalLight.Parameters["light"].SetValue(decor.LightObj.Position);
                    Game1.EffectNormalLight.Parameters["normal"].SetValue(Game1.NormalMapBuffer);
                    Game1.EffectNormalLight.CurrentTechnique.Passes[0].Apply();
                    Game1.SpriteBatchGlobal.Draw(Game1.Textures["testaaa"], Vector2.Zero, Color.White);
                }
            }

            Game1.SpriteBatchGlobal.End();
        }

        private static void WaterAnimate()
        {
            RenderTargetsSettings.SetAndClear(Game1.WaterAnimateLeft, Color.Black);
            DrawAnimateWater.DrawLeft();
            RenderTargetsSettings.SetAndClear(Game1.WaterAnimateRight, Color.Black);
            DrawAnimateWater.DrawRight();
        }

        private static void WaterAnimateTop()
        {
            RenderTargetsSettings.SetAndClear(Game1.WaterAnimateLeftTop, Color.Transparent);
            DrawAnimateWaterOver.DrawLeft();
            RenderTargetsSettings.SetAndClear(Game1.WaterAnimateRightTop, Color.Transparent);
            DrawAnimateWaterOver.DrawRight();
        }

        public static void DrawWinTarget()
        {
            Game1.GraphicsGlobal.GraphicsDevice.SetRenderTargets(null);
            Game1.SpriteBatchGlobal.Begin(samplerState: state);
            Game1.SpriteBatchGlobal.Draw(Game1.Win, new Rectangle(Globals.WinOffset.X, Globals.WinOffset.Y, Globals.Winsize.X, Globals.Winsize.Y), new Rectangle(0, 0, (int)Globals.WinRenderSize.X, (int)Globals.WinRenderSize.Y), Color.White);
            MouseInput.DrawCursorMenu();
            Game1.SpriteBatchGlobal.End();
        }

        public static void DrawEditorWinTarget()
        {
            Game1.GraphicsGlobal.GraphicsDevice.SetRenderTargets(null);
            Game1.SpriteBatchGlobal.Begin(samplerState: state, blendState: BlendState.AlphaBlend, sortMode: SpriteSortMode.Immediate);
            Game1.SpriteBatchGlobal.Draw(Game1.EditorBackground, new Rectangle(Globals.WinOffset.X, Globals.WinOffset.Y, Globals.Winsize.X, Globals.Winsize.Y), new Rectangle(0, 0, (int)Globals.WinRenderSize.X, (int)Globals.WinRenderSize.Y), Color.White);

            Game1.SpriteBatchGlobal.Draw(Game1.cursor, MouseInput.MouseStateNew.Position.ToVector2(), origin: new Vector2(8), scale: new Vector2(1) * Globals.ScreenRatio.X);

            Game1.SpriteBatchGlobal.End();
        }

        private static void DrawFinalScreenTargetToGauss1()
        {
            Game1.GraphicsGlobal.GraphicsDevice.SetRenderTargets(Game1.ToPrepareGaussPass1);
            Game1.SpriteBatchGlobal.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointWrap);
            Game1.SpriteBatchGlobal.Draw((Game1.FinalScreen), Vector2.Zero);
            Game1.SpriteBatchGlobal.End();
        }
        private static void ProcessGaussHorizontal()
        {
            RenderTargetsSettings.SetAndClear(Game1.ToRenderFirstPass, Color.Black);
            Game1.GraphicsGlobal.GraphicsDevice.SetRenderTargets(Game1.ToProcessSecondPass);
            Game1.SpriteBatchGlobal.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointWrap);
            Effects.GaussHorizontalEffect(Game1.ToPrepareGaussPass1,Game1.Textures["WeaponMenuBackMask"]);
            Game1.SpriteBatchGlobal.Draw((Game1.ToRenderFirstPass), Vector2.Zero);
            Effects.ResetEffect3D();
            Game1.SpriteBatchGlobal.End();
        }
        private static void DrawPartMenu()
        {
            RenderTargetsSettings.SetAndClear(Game1.MenuParts, Color.Transparent);
            Game1.SpriteBatchGlobal.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointWrap);
            if (Game1.GameScreen == InGameScreen.assembly)
                UIAssembly.Instance.Draw();
            else if (Game1.GameScreen == InGameScreen.shop)
                UIShop.Instance.Draw();
            Game1.SpriteBatchGlobal.End();
        }
        private static void ProcessGaussVertical()
        {
            RenderTargetsSettings.SetAndClear(null, Color.Black);
            Game1.SpriteBatchGlobal.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, state);
            Game1.SpriteBatchGlobal.Draw((Game1.FinalScreen), new Rectangle(Globals.WinOffset.X, Globals.WinOffset.Y, Globals.Winsize.X, Globals.Winsize.Y), new Rectangle(0, 0, (int)Globals.WinRenderSize.X, (int)Globals.WinRenderSize.Y), Color.White);
            Effects.GaussVerticalEffect(Game1.ToProcessSecondPass, Game1.Textures["WeaponMenuBackMask"]);
            Game1.SpriteBatchGlobal.Draw((Game1.CanvasToSecondPass), new Rectangle(Globals.WinOffset.X, Globals.WinOffset.Y, Globals.Winsize.X, Globals.Winsize.Y), new Rectangle(0, 0, (int)Globals.WinRenderSize.X, (int)Globals.WinRenderSize.Y), Color.White);
            Effects.ResetEffect3D();
            Game1.SpriteBatchGlobal.End();
        }

        private static void DrawFinalWithoutGauss()
        {
            Game1.GraphicsGlobal.GraphicsDevice.SetRenderTargets(null);
            Game1.SpriteBatchGlobal.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, state);
            Game1.SpriteBatchGlobal.Draw((Game1.ToPrepareGaussPass1), new Rectangle(Globals.WinOffset.X, Globals.WinOffset.Y, Globals.Winsize.X, Globals.Winsize.Y), new Rectangle(0, 0, (int)Globals.WinRenderSize.X, (int)Globals.WinRenderSize.Y), Color.White);
            Game1.SpriteBatchGlobal.Draw(Game1.cursor, MouseInput.MouseStateNew.Position.ToVector2(), origin: new Vector2(8), scale: new Vector2(1) * Globals.ScreenRatio.X);
            //Game1.SpriteBatchGlobal.Draw(Game1.NormalMapBuffer, new Rectangle(Globals.WinOffset.X, Globals.WinOffset.Y, Globals.Winsize.X, Globals.Winsize.Y), new Rectangle(0, 0, (int)Globals.WinRenderSize.X, (int)Globals.WinRenderSize.Y), Color.White);
            Game1.SpriteBatchGlobal.End();
        }

        public static void DrawGame(IMap map)
        {
            WaterAnimate();
            WaterAnimateTop();
            DrawNormals(map);
            DrawLightsNormals();
            DrawLights(map);
            DrawLayer1(map);
            DrawLayer2(map);
            DrawWaterMask(map);
            DrawLayer1And2(map);
            DrawApplyLights();
            DrawWater(map, Camera2DGame.Position);
            DrawFinal(map);
            DrawFinalScreenTargetToGauss1();

            if (Game1.GameScreen != InGameScreen.none)
            {
                DrawPartMenu();
                ProcessGaussHorizontal();
                ProcessGaussVertical();
            }

            if (Game1.GameScreen == InGameScreen.shop)
            {
                Game1.SpriteBatchGlobal.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, state);
                Game1.SpriteBatchGlobal.Draw(Game1.Textures["ShopMenuBack"], new Rectangle(Globals.WinOffset.X, Globals.WinOffset.Y, Globals.Winsize.X, Globals.Winsize.Y), new Rectangle(0, 0, (int)Globals.WinRenderSize.X, (int)Globals.WinRenderSize.Y), Color.White);
                Game1.SpriteBatchGlobal.Draw(Game1.MenuParts, new Rectangle(Globals.WinOffset.X, Globals.WinOffset.Y, Globals.Winsize.X, Globals.Winsize.Y), new Rectangle(0, 0, (int)Globals.WinRenderSize.X, (int)Globals.WinRenderSize.Y), Color.White);
                Game1.SpriteBatchGlobal.Draw(Game1.cursor, MouseInput.MouseStateNew.Position.ToVector2(), origin: new Vector2(8), scale: new Vector2(1) * Globals.ScreenRatio.X);
                Game1.SpriteBatchGlobal.End();
            }
            else if (Game1.GameScreen ==  InGameScreen.assembly)
            {
                Game1.SpriteBatchGlobal.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, state);
                Game1.SpriteBatchGlobal.Draw(Game1.Textures["WeaponMenuBack"], new Rectangle(Globals.WinOffset.X, Globals.WinOffset.Y, Globals.Winsize.X, Globals.Winsize.Y), new Rectangle(0, 0, (int)Globals.WinRenderSize.X, (int)Globals.WinRenderSize.Y), Color.White);
                Game1.SpriteBatchGlobal.Draw(Game1.MenuParts, new Rectangle(Globals.WinOffset.X, Globals.WinOffset.Y, Globals.Winsize.X, Globals.Winsize.Y), new Rectangle(0, 0, (int)Globals.WinRenderSize.X, (int)Globals.WinRenderSize.Y), Color.White);
                Game1.SpriteBatchGlobal.Draw(Game1.cursor, MouseInput.MouseStateNew.Position.ToVector2(), origin: new Vector2(8), scale: new Vector2(1) * Globals.ScreenRatio.X);
                Game1.SpriteBatchGlobal.End();
            }
            else
            {
                DrawFinalWithoutGauss();
            }
        }

        public static void DrawWorld(IMap map)
        {
            DrawNormals(map);
            DrawLights(map);
            DrawLightsNormalsWorld();
            DrawLayer1(map);
            DrawLayer2(map);
            //DrawWaterMaskWorld(map);
            DrawLayer1And2(map);
            DrawApplyLightsWorld();
            //DrawWater(map, Camera2DWorld.Position);
            DrawFinalWorld(map);
            DrawFinalScreenTargetToGauss1();
            DrawFinalWithoutGauss();
        }
    }

    public static class GameUpdate
    {
        public static void TweakSound()
        {
            foreach (Sound snd3D in Game1.Sounds3D.Reverse<Sound>())
            {
                snd3D.Update(Game1.PlayerInstance.Boundary.Origin);

                if (snd3D.Stopped == true) Game1.Sounds3D.Remove(snd3D);
            }

            foreach (Sound snd in Game1.SoundsMain.Reverse<Sound>())
            {
                snd.Update(Game1.PlayerInstance.Boundary.Origin);

                if (snd.Stopped == true) Game1.SoundsMain.Remove(snd);
            }
        }

        public static void InWorldUpdate()
        {
            Game1.PlayerWorldInstance.Update(MouseInput.MouseRealPosGame());
            Camera2DWorld.Update();
            Game1.mapLiveWorld.Update();
        }

        public static void InGameUpdate()
        {
            Game1.mapLive.Update();
            Game1.PlayerInstance.Update(MouseInput.MouseRealPosGame());
            Camera2DGame.Update();
            UIPlayer.Update();
            Game1.PlayerInstance.UpdateWeapons(MouseInput.MouseRealPosGame());
        }
    }
}