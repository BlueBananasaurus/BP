using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Monogame_GL
{
    public class MapEdit
    {
        public Point MapSize { get; set; }
        public string[,] BackgroundTileMap { get; set; }
        public string[,] ForegroundTileMap { get; set; }
        public string[,] FunctionTileMap { get; set; }
        public List<LineSegmentF> TileMapLines { get; set; }
        public List<IProjectile> MapProjectiles { get; set; }
        public List<IRectanglePhysics> MapMovables { get; set; }
        public List<Inpc> MapNpcs { get; set; }
        public List<Vector2> mapHits { get; set; }
        public List<ITriggers> mapTriggers { get; set; }
        public List<IEffect> mapLightings { get; set; }
        public List<IPickable> mapPickables { get; set; }
        public List<ParticleSmokeTrail> MapTrails { get; set; }
        public List<IParticle> mapParticles { get; set; }
        public List<IDecoration> mapDecorations { get; set; }
        public List<IDecoration> mapDecorationsForeground { get; set; }
        public List<CrateStatic> mapCrates { get; set; }
        public bool DrawFunctionMap { get; set; }
        public bool DrawBordersOfTiles { get; set; }
        public Vector3 LightLevel { get; set; }
        public MapTreeHolder MapTree { get; set; }
        public MapTreeHolder WaterTree { get; set; }
        public Texture2D Sky { get; set; }
        public bool ShowBoundary { get; set; }
        public bool ShowGround { get; set; }

        public List<MechEditor> mapEntities { get; set; }

        public static PlayerEditor Player { get; set; }

        public float TransparencyForeground { get; set; }
        public float TransparencyBackground { get; set; }

        public RectangleF MapBoundary { get { return new RectangleF(FunctionTileMap.GetLength(0) * Globals.TileSize, FunctionTileMap.GetLength(1) * Globals.TileSize, 0, 0); } }

        public MapEdit()
        {
            DrawFunctionMap = false;
            DrawBordersOfTiles = false;
            TileMapLines = new List<LineSegmentF>();
            MapProjectiles = new List<IProjectile>();
            MapMovables = new List<IRectanglePhysics>();
            mapHits = new List<Vector2>();
            MapNpcs = new List<Inpc>();
            mapTriggers = new List<ITriggers>();
            mapLightings = new List<IEffect>();
            mapPickables = new List<IPickable>();
            MapTrails = new List<ParticleSmokeTrail>();
            mapParticles = new List<IParticle>();
            mapDecorations = new List<IDecoration>();
            mapDecorationsForeground = new List<IDecoration>();
            mapCrates = new List<CrateStatic>();
            LightLevel = new Vector3(1f);
            Sky = null;
            ShowBoundary = false;
            ShowGround = true;
            MapSize = new Point(800, 600);
            TransparencyForeground = 1f;
            TransparencyBackground = 1f;

            mapEntities = new List<MechEditor>();
        }

        public void ChangeLightLevel(Vector3 light)
        {
            LightLevel = light;
        }

        public void ChangeSkytexture(Texture2D sky)
        {
            Sky = sky;
        }

        public void GenerateMapFunctionTrees()
        {
            MapTree = new MapTreeHolder((int)MapBoundary.Size.X, (int)MapBoundary.Size.Y);
            WaterTree = new MapTreeHolder((int)MapBoundary.Size.X, (int)MapBoundary.Size.Y);

            for (int y = 0; y < FunctionTileMap.GetLength(1); y++)
            {
                for (int x = 0; x < FunctionTileMap.GetLength(0); x++)
                {
                    if (FunctionTileMap[x, y] == "block")
                        MapTree.AddRectangle32(new Vector2(x, y) * Globals.TileSize);
                    if (FunctionTileMap[x, y] == "water")
                        WaterTree.AddRectangle32(new Vector2(x, y) * Globals.TileSize);
                }
            }
        }

        public void AddTileMap(string[,] background = null, string[,] foreground = null, string[,] function = null)
        {
            if (background != null)
            {
                BackgroundTileMap = background;
            }
            if (foreground != null)
            {
                ForegroundTileMap = foreground;
            }
            if (function != null)
            {
                FunctionTileMap = function;
            }
        }

        public void Update()
        {
            #region Updates

            //foreach (ITriggers trgr in mapTriggers)
            //{
            //    trgr.Update();
            //}

            //foreach (IRectangleGet mvbl in MapMovables)
            //{
            //    mvbl.Update();
            //}

            //foreach (Inpc npc in MapNpcs.Reverse<Inpc>())
            //{
            //    npc.Update(MapNpcs);
            //}

            //foreach (IEffect lght in mapLightings.Reverse<IEffect>())
            //{
            //    lght.Update();
            //}

            //foreach (IProjectile prjctl in MapProjectiles.Reverse<IProjectile>())
            //{
            //    prjctl.Update(MapMovables, Game1.mapLive.MapNpcs, Game1.mapLive.MapTree, Game1.mapLive.WaterTree);
            //}

            //foreach (IPickable pick in mapPickables.Reverse<IPickable>())
            //{
            //    pick.Update();
            //}

            //foreach (SmokeTrail trl in MapTrails.Reverse<SmokeTrail>())
            //{
            //    trl.Update();
            //}

            //foreach (IParticle prtcl in mapParticles.Reverse<IParticle>())
            //{
            //    prtcl.Update();
            //}

            #endregion Updates
        }

        public void DrawSky()
        {
            if (Sky != null)
                Game1.SpriteBatchGlobal.Draw(Sky, position: Vector2.Zero);
        }

        public void Draw()
        {
            //Game1.SpriteBatchGlobal.Begin(transformMatrix: Camera2DEditor.GetViewMatrix(), samplerState: SamplerState.PointWrap, sortMode: SpriteSortMode.Immediate);

            //if (ShowBoundary == true)
            //{
            //    Game1.EffectSlide.Parameters["UV"].SetValue(new Vector2(0, Game1.Time));
            //    Game1.EffectSlide.CurrentTechnique.Passes[0].Apply();
            //    Game1.SpriteBatch.Draw(Game1.boundary, position: new Vector2(-64, 0), sourceRectangle: new Rectangle(0, 0, 32, FunctionTileMap.GetLength(1) * Globals.TileSize / 2), scale: new Vector2(2));
            //    Game1.SpriteBatch.Draw(Game1.boundary, position: new Vector2(FunctionTileMap.GetLength(0) * Globals.TileSize, 0), sourceRectangle: new Rectangle(0, 0, 32, FunctionTileMap.GetLength(1) * Globals.TileSize / 2), scale: new Vector2(2), effects: SpriteEffects.FlipHorizontally);
            //    Game1.EffectBaseColor.CurrentTechnique.Passes[0].Apply();
            //}

            //if (ShowGround == true)
            //{
            //    Game1.SpriteBatch.Draw(Game1.tileTop, sourceRectangle: new Rectangle(0, 0, FunctionTileMap.GetLength(0) * Globals.TileSize / 2 + 1920 / 2, Globals.TileSize / 2), position: new Vector2(-1920 / 2, FunctionTileMap.GetLength(1) * Globals.TileSize), scale: new Vector2(2));
            //    Game1.SpriteBatch.Draw(Game1.Tile03, sourceRectangle: new Rectangle(0, 0, FunctionTileMap.GetLength(0) * Globals.TileSize / 2 + 1920 / 2, Globals.TileSize * 10), position: new Vector2(-1920 / 2, FunctionTileMap.GetLength(1) * Globals.TileSize + Globals.TileSize), scale: new Vector2(2));
            //}

            foreach (IDecoration dcr in mapDecorations)
            {
                dcr.Draw();
            }

            Effects.ColorEffect(new Vector4(1, 1, 1, TransparencyBackground * Convert.ToInt32(UIDrawing.Instance.BackgroundVisibility.State)));
            if (BackgroundTileMap != null)
            {
                for (int y = Math.Max((int)(Camera2DEditor.PositionPoint.Y / 32), 0); y < Math.Min((int)((Camera2DEditor.PositionPoint.Y + Globals.WinRenderSize.Y) / 32) + 1, BackgroundTileMap.GetLength(1)); y++)
                {
                    for (int x = Math.Max((int)(Camera2DEditor.PositionPoint.X / 32), 0); x < Math.Min((int)((Camera2DEditor.PositionPoint.X + Globals.WinRenderSize.X) / 32) + 1, BackgroundTileMap.GetLength(0)); x++)
                    {
                        if (Camera2DEditor.Position.Y - Globals.TileSize < y * Globals.TileSize && y * Globals.TileSize < Camera2DEditor.Position.Y + Globals.WinRenderSize.Y + Globals.TileSize)
                        {
                            if (Camera2DEditor.Position.X - Globals.TileSize < x * Globals.TileSize && x * Globals.TileSize < Camera2DEditor.Position.X + Globals.WinRenderSize.X + Globals.TileSize)
                            {
                                foreach (Tile tile in Game1.Tiles)
                                {
                                    if (BackgroundTileMap[x, y] == tile.Code)
                                        Game1.SpriteBatchGlobal.DrawAtlas(Game1.Textures[tile.TextureName], new Vector2(x * Globals.TileSize, y * Globals.TileSize), new Point(Globals.TileSize, Globals.TileSize), tile.IndexInPicture);
                                }

                                if (y > 0)
                                {
                                    if (FunctionTileMap[x, y] == "water" && FunctionTileMap[x, y - 1] != "water")
                                    {
                                        Game1.SpriteBatchGlobal.Draw(Game1.waterTop, new Vector2(x * Globals.TileSize, y * Globals.TileSize), scale: new Vector2(2));
                                    }
                                }
                            }
                        }
                    }
                }
            }
            Effects.ResetEffectSimple();

            foreach (MechEditor entity in mapEntities)
            {
                entity.Draw();
            }

            foreach (CrateStatic crt in mapCrates)
            {
                crt.Draw();
            }

            foreach (ITriggers trgr in mapTriggers)
            {
                trgr.Draw();
            }

            foreach (IRectanglePhysics mvbl in MapMovables)
            {
                mvbl.Draw();
            }

            foreach (IPickable pick in mapPickables)
            {
                pick.Draw();
            }

            foreach (Inpc npc in MapNpcs)
            {
                if (npc is PlayerTurret == false)
                    npc.Draw();
            }

            Player?.Draw();

            foreach (Inpc npc in MapNpcs)
            {
                if (npc is PlayerTurret == true)
                    npc.Draw();
            }

            foreach (IEffect lght in mapLightings)
            {
                lght.Draw();
            }

            foreach (ParticleSmokeTrail trl in MapTrails)
            {
                trl.Draw();
            }

            foreach (IParticle prtcl in mapParticles)
            {
                prtcl.Draw();
            }

            foreach (IProjectile prjctl in MapProjectiles)
            {
                prjctl.Draw();
            }

            Effects.ColorEffect(new Vector4(1, 1, 1, TransparencyForeground * Convert.ToInt32(UIDrawing.Instance.ForegroundVisibility.State)));
            if (ForegroundTileMap != null)
            {
                for (int y = Math.Max((int)(Camera2DEditor.PositionPoint.Y / 32), 0); y < Math.Min((int)((Camera2DEditor.PositionPoint.Y + Globals.WinRenderSize.Y) / 32) + 1, ForegroundTileMap.GetLength(1)); y++)
                {
                    for (int x = Math.Max((int)(Camera2DEditor.PositionPoint.X / 32), 0); x < Math.Min((int)((Camera2DEditor.PositionPoint.X + Globals.WinRenderSize.X) / 32) + 1, ForegroundTileMap.GetLength(0)); x++)
                    {
                        foreach (Tile tile in Game1.Tiles)
                        {
                            if (ForegroundTileMap[x, y] == tile.Code)
                                Game1.SpriteBatchGlobal.DrawAtlas(Game1.Textures[tile.TextureName], new Vector2(x * Globals.TileSize, y * Globals.TileSize), new Point(Globals.TileSize, Globals.TileSize), tile.IndexInPicture);
                        }
                    }
                }
            }
            Effects.ResetEffectSimple();

            if (FunctionTileMap != null)
            {
                for (int y = Math.Max((int)(Camera2DEditor.PositionPoint.Y / 32), 0); y < Math.Min((int)((Camera2DEditor.PositionPoint.Y + Globals.WinRenderSize.Y) / 32) + 1, FunctionTileMap.GetLength(1)); y++)
                {
                    for (int x = Math.Max((int)(Camera2DEditor.PositionPoint.X / 32), 0); x < Math.Min((int)((Camera2DEditor.PositionPoint.X + Globals.WinRenderSize.X) / 32) + 1, FunctionTileMap.GetLength(0)); x++)
                    {
                        if (Camera2DEditor.Position.Y - Globals.TileSize < y * Globals.TileSize && y * Globals.TileSize < Camera2DEditor.Position.Y + Globals.WinRenderSize.Y + Globals.TileSize)
                        {
                            if (Camera2DEditor.Position.X - Globals.TileSize < x * Globals.TileSize && x * Globals.TileSize < Camera2DEditor.Position.X + Globals.WinRenderSize.X + Globals.TileSize)
                            {
                                //if (FunctionTileMap[x, y] == "obstacle")
                                //    Game1.SpriteBatchGlobal.Draw(Game1.tileBack1, new Vector2(x * Globals.TileSize, y * Globals.TileSize));
                                //if (FunctionTileMap[x, y] == "water")
                                //    Game1.SpriteBatchGlobal.Draw(Game1.waterTex, new Vector2(x * Globals.TileSize, y * Globals.TileSize));
                            }
                        }
                    }
                }
            }
        }

        public void DrawLights()
        {
            Game1.SpriteBatchGlobal.Begin(SpriteSortMode.Immediate, BlendState.Additive, SamplerState.PointClamp, transformMatrix: Camera2DEditor.GetViewMatrix());

            foreach (IProjectile prjctl in MapProjectiles)
            {
                prjctl.DrawLight();
            }

            foreach (ITriggers trgr in mapTriggers)
            {
                trgr.DrawLight();
            }

            foreach (IEffect lght in mapLightings)
            {
                lght.DrawLight();
            }

            foreach (IDecoration dcr in mapDecorations)
            {
                dcr.DrawLight();
            }

            foreach (IParticle prtcl in mapParticles)
            {
                prtcl.DrawLight();
            }

            foreach (IPickable pck in mapPickables)
            {
                pck.DrawLight();
            }

            Game1.SpriteBatchGlobal.End();
        }
    }
}