using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Monogame_GL
{
    public class Game1 : Game
    {
        #region basicEffect
        public static Matrix View = Camera2DGame.GetViewMatrix();
        public static Matrix projection = Matrix.CreateOrthographicOffCenter(0, Globals.WinRenderSize.X, Globals.WinRenderSize.Y, 0, 0, -1);
        public static Matrix World = Camera2DGame.GetViewMatrix();
        public static BasicEffect BscEffect;
        #endregion

        #region init
        public static GraphicsDeviceManager GraphicsGlobal { get; private set; }
        public static SpriteBatch SpriteBatchGlobal { get; private set; }
        public static Game1 Game1Self { get; private set; }
        public static ContentManager ContentGlobal { get; private set; }
        #endregion

        #region resources
        private static Dictionary<string, string> LinksToTextures;
        public static Dictionary<string, Texture2D> Textures;

        private static Dictionary<string, string> LinksToSounds;
        public static Dictionary<string, SoundEffect> Sounds;

        public static Dictionary<string, IWindow> Windows;

        public static List<Tile> Tiles;
        public static List<Tile> TilesPhysics;
        #endregion

        #region content declaration

        public static SoundEffect soundVortex { get; private set; }
        public static SoundEffect soundExplosion { get; private set; }
        public static SoundEffect soundElectro { get; private set; }
        public static SoundEffect soundTeleport { get; private set; }
        public static SoundEffect soundHit { get; private set; }
        public static SoundEffect soundjump { get; private set; }
        public static SoundEffect soundCoin { get; private set; }
        public static SoundEffect sound { get; private set; }
        public static SoundEffect soundGrenadeHit { get; private set; }
        public static SoundEffect soundDamage { get; private set; }
        public static SoundEffect soundSaw { get; private set; }
        public static SoundEffect soundFlamb { get; private set; }
        public static SoundEffect soundPlasmaRifleShoot { get; private set; }
        public static SoundEffect soundSelect { get; private set; }
        public static SoundEffect soundMechWalk { get; private set; }

        public static Texture2D laserDest { get; private set; }
        public static Texture2D roomBase { get; private set; }
        public static Texture2D podBase { get; private set; }
        public static Texture2D TestRec { get; private set; }
        public static Texture2D rainDrop { get; private set; }
        public static Texture2D electronics { get; private set; }
        public static Texture2D LootCrateStain { get; private set; }
        public static Texture2D plasmaLight { get; private set; }
        public static Texture2D FireSmall2LightHalo { get; private set; }
        public static Texture2D FireSmall2 { get; private set; }
        public static Texture2D muzzlePlasmaLight { get; private set; }
        public static Texture2D muzzlePlasma { get; private set; }
        public static Texture2D PlasmaGunUp { get; private set; }
        public static Texture2D PlasmaGunDown { get; private set; }
        public static Texture2D muzzleSimpleLight { get; private set; }
        public static Texture2D plasma { get; private set; }
        public static Texture2D plasmaGun { get; private set; }
        public static Texture2D FireLightHalo { get; private set; }
        public static Texture2D hotLight { get; private set; }
        public static Texture2D projectileLight { get; private set; }
        public static Texture2D LaserLight { get; private set; }
        public static Texture2D controlBackHover { get; private set; }
        public static Texture2D controlBack { get; private set; }
        public static Texture2D Info { get; private set; }
        public static Texture2D saveBackSelected { get; private set; }
        public static Texture2D saveBack { get; private set; }
        public static Texture2D bubble { get; private set; }
        public static Texture2D door { get; private set; }
        public static Texture2D doorRack { get; private set; }
        public static Texture2D FireSmallLight { get; private set; }
        public static Texture2D playerTurretHead { get; private set; }
        public static Texture2D playerTurretBase { get; private set; }
        public static Texture2D waterTop { get; private set; }
        public static Texture2D FlameProjectile { get; private set; }
        public static Texture2D flameThrower { get; private set; }
        public static Texture2D flameThrowerUp { get; private set; }
        public static Texture2D flameThrowerDown { get; private set; }
        public static Texture2D turretBase { get; private set; }
        public static Texture2D turretHead { get; private set; }
        public static Texture2D triggerOffLight { get; private set; }
        public static Texture2D triggerOnLight { get; private set; }
        public static Texture2D tileMask { get; private set; }
        public static Texture2D fireBig { get; private set; }
        public static Texture2D boundary { get; private set; }
        public static Texture2D sawNut { get; private set; }
        public static Texture2D liftRail { get; private set; }
        public static Texture2D lampGroundLight { get; private set; }
        public static Texture2D lampGround { get; private set; }
        public static Texture2D LightRocket { get; private set; }
        public static Texture2D PlayerLight { get; private set; }
        public static Texture2D zombieFrozen { get; private set; }
        public static Texture2D Letters { get; private set; }
        public static Texture2D Tile03 { get; private set; }
        public static Texture2D backgroundWin { get; private set; }
        public static Texture2D tissue { get; private set; }
        public static Texture2D flamb { get; private set; }
        public static Texture2D transporter { get; private set; }
        public static Texture2D SpawnPanel { get; private set; }
        public static Texture2D grenade { get; private set; }
        public static Texture2D zombieShirtBlue { get; private set; }
        public static Texture2D well { get; private set; }
        public static Texture2D portalThing { get; private set; }
        public static Texture2D portal { get; private set; }
        public static Texture2D zombieShirt { get; private set; }
        public static Texture2D cacti1 { get; private set; }
        public static Texture2D numbersMedium { get; private set; }
        public static Texture2D projectilGunDown { get; private set; }
        public static Texture2D projectilGunUp { get; private set; }
        public static Texture2D debugTex { get; private set; }
        public static Texture2D playerTex { get; private set; }
        public static Texture2D tileTex { get; private set; }
        public static Texture2D waterTex { get; private set; }
        public static Texture2D platformTex { get; private set; }
        public static Texture2D triggerOffTex { get; private set; }
        public static Texture2D triggerOnTex { get; private set; }
        public static Texture2D debug_longTex { get; private set; }
        public static Texture2D LightTex { get; private set; }
        public static Texture2D LightLineTex { get; private set; }
        public static Texture2D debug_thin { get; private set; }
        public static Texture2D smoke { get; private set; }
        public static Texture2D projectilTex { get; private set; }
        public static Texture2D projectil { get; private set; }
        public static Texture2D haloProjectil { get; private set; }
        public static Texture2D particle { get; private set; }
        public static Texture2D flame { get; private set; }
        public static Texture2D waterStore { get; private set; }
        public static Texture2D healthBar { get; private set; }
        public static Texture2D barBackground { get; private set; }
        public static Texture2D healthBarBackground { get; private set; }
        public static Texture2D projectilGun { get; private set; }
        public static Texture2D rocketLauncher { get; private set; }
        public static Texture2D laserGun { get; private set; }
        public static Texture2D laserGunUp { get; private set; }
        public static Texture2D laserGunDown { get; private set; }
        public static Texture2D shield { get; private set; }
        public static Texture2D explosion { get; private set; }
        public static Texture2D lightingGun { get; private set; }
        public static Texture2D lightingGunUp { get; private set; }
        public static Texture2D energyLauncher { get; private set; }
        public static Texture2D cursor { get; private set; }
        public static Texture2D health { get; private set; }
        public static Texture2D numbers { get; private set; }
        public static Texture2D smokeeExpl { get; private set; }
        public static Texture2D foot { get; private set; }
        public static Texture2D footAir { get; private set; }
        public static Texture2D footWalk { get; private set; }
        public static Texture2D laserDestHalo { get; private set; }
        public static Texture2D muzzleSimple { get; private set; }
        public static Texture2D rocketLauncherFlame { get; private set; }
        public static Texture2D smokeSmall { get; private set; }
        public static Texture2D enemyPiece { get; private set; }
        public static Texture2D shieldBar { get; private set; }
        public static Texture2D sky { get; private set; }
        public static Texture2D zombie { get; private set; }
        public static Texture2D spawn { get; private set; }
        public static Texture2D shieldBarBackground { get; private set; }
        public static Texture2D tileMiddle { get; private set; }
        public static Texture2D tileTop { get; private set; }
        public static Texture2D white { get; private set; }
        public static Texture2D palm { get; private set; }
        public static Texture2D hot { get; private set; }
        public static Texture2D lightingGunDown { get; private set; }
        public static Texture2D energyLauncherUp { get; private set; }
        public static Texture2D energyLauncherDown { get; private set; }
        public static Texture2D saw { get; private set; }
        public static Texture2D template { get; private set; }
        public static Texture2D flameParticle { get; private set; }
        public static Texture2D TESTLIGHT { get; private set; }
        public static Texture2D flameLight { get; private set; }
        public static Texture2D tileBack1 { get; private set; }
        public static Texture2D tileBackLeft { get; private set; }
        public static Texture2D Transition { get; private set; }
        public static Texture2D SmokeBig { get; private set; }
        public static Texture2D liftEnd { get; private set; }
        public static Texture2D Flake { get; private set; }
        #endregion conntent declaration

        #region content 2
        public static SpriteFont FontDebug { get; set; }

        public static Effect EffectBaseColor { get; set; }
        public static Effect EffectColors { get; set; }
        public static Effect EffectBlur { get; set; }
        public static Effect EffectLights { get; set; }
        public static Effect EffectSlide { get; set; }
        public static Effect EffectWater { get; set; }
        public static Effect EffectAlpha { get; set; }
        public static Effect EffectNormalLight { get; set; }
        public static Effect EffectRotateNormals { get; set; }
        public static Effect EffectVertexNormal { get; set; }
        public static Effect EffectBaseColorSimple { get; set; }

        public static RenderTarget2D TARGET_ALL;
        public static RenderTarget2D Layer1;
        public static RenderTarget2D Layer2;
        public static RenderTarget2D GaussFinal;
        public static RenderTarget2D Win;
        public static RenderTarget2D EditorMenu;
        public static RenderTarget2D Lights;
        public static RenderTarget2D Map;
        public static RenderTarget2D FinalScreen;
        public static RenderTarget2D WaterMask;
        public static RenderTarget2D WaterWithoutLights;
        public static RenderTarget2D ToPrepareGaussPass1;
        public static RenderTarget2D ToRenderFirstPass;
        public static RenderTarget2D ToProcessSecondPass;
        public static RenderTarget2D LightNormalHolder;
        #endregion

        public static List<RenderTarget2D> EditorEntitiesTargets;

        public static RenderTarget2D EditorLayer1;
        public static RenderTarget2D EditorBackground;
        public static RenderTarget2D MenuParts;

        public static List<Sound> Sounds3D;
        public static List<Sound> SoundsMain;
        public static List<UIInformer> InfoList;

        public static Map mapLive;
        public static MapWorld mapLiveWorld;
        public static RenderTarget2D CanvasToSecondPass { get; private set; }
        public static RenderTarget2D WaterBlur { get; private set; }
        public static RenderTarget2D WaterAfterBlur { get; private set; }
        public static RenderTarget2D NormalMapBuffer { get; private set; }

        public static Player PlayerInstance;
        public static PlayerWorld PlayerWorldInstance;
        public static string ActualWindow;
        public static GlobalState State;
        public static bool GameInProgress;
        public static bool EditInProgress;
        public static Setup STP = new Setup();
        public static bool EditorMenuOpen;
        public static float Delta;
        public static float Time;

        public static InGameScreen GameScreen { get; set; }
        public static RenderTarget2D WaterAnimateLeft { get; set; }
        public static RenderTarget2D WaterAnimateRight { get; set; }
        public static RenderTarget2D WaterAnimateLeftTop { get; set; }
        public static RenderTarget2D WaterAnimateRightTop { get; set; }

        public Game1()
        {
            GraphicsGlobal = new GraphicsDeviceManager(this);
            Game1Self = this;
            IsFixedTimeStep = true;
            Window.AllowUserResizing = true;
            Content.RootDirectory = "Content";

            ContentGlobal = Content;
            STP.LoadSetup();
            GraphicsGlobal.IsFullScreen = STP.IsFullScreen;

            if (STP.IsFullScreen == true)
            {
                GraphicsGlobal.PreferredBackBufferHeight = STP.WindowSizeFullscreen.Y;
                GraphicsGlobal.PreferredBackBufferWidth = STP.WindowSizeFullscreen.X;
            }
            else
            {
                GraphicsGlobal.PreferredBackBufferHeight = STP.WindowSizeWindowed.Y;
                GraphicsGlobal.PreferredBackBufferWidth = STP.WindowSizeWindowed.X;
            }

            GraphicsGlobal.SynchronizeWithVerticalRetrace = true;
            GraphicsGlobal.ApplyChanges();
        }

        protected override void Initialize()
        {
            #region Load content
            GameScreen = InGameScreen.none;
            Globals.LoadJson(ref LinksToTextures, "Textures");

            Textures = new Dictionary<string, Texture2D>();

            foreach (KeyValuePair<string, string> entry in LinksToTextures)
            {
                Texture2D temp = Content.Load<Texture2D>(entry.Value);
                Textures.Add(entry.Key, temp);
            }

            Globals.LoadJson(ref LinksToSounds, "Sounds");

            Sounds = new Dictionary<string, SoundEffect>();

            foreach (KeyValuePair<string, string> entry in LinksToSounds)
            {
                SoundEffect temp = Content.Load<SoundEffect>(entry.Value);
                Sounds.Add(entry.Key, temp);
            }
            #endregion

            #region Tiles

            Tiles = new List<Tile>();
            Tiles.Add(new Tile("TilesSand", new Point(0, 0), "sandLeftTop"));
            Tiles.Add(new Tile("TilesSand", new Point(1, 0), "sandMiddleTop"));
            Tiles.Add(new Tile("TilesSand", new Point(2, 0), "sandRightTop"));

            Tiles.Add(new Tile("TilesSand", new Point(0, 1), "sandLeftMiddle"));
            Tiles.Add(new Tile("TilesSand", new Point(1, 1), "sandMiddleMiddle"));
            Tiles.Add(new Tile("TilesSand", new Point(2, 1), "sandRightMiddle"));

            Tiles.Add(new Tile("TilesSand", new Point(0, 2), "sandLeftBottom"));
            Tiles.Add(new Tile("TilesSand", new Point(1, 2), "sandMiddleBottom"));
            Tiles.Add(new Tile("TilesSand", new Point(2, 2), "sandRightBottom"));

            Tiles.Add(new Tile("TilesSand", new Point(3, 0), "sandVertical"));
            Tiles.Add(new Tile("TilesSand", new Point(3, 1), "sandHorizontal"));
            Tiles.Add(new Tile("TilesSand", new Point(3, 2), "sand"));

            Tiles.Add(new Tile("TilesSand", new Point(4, 0), "sandtTop"));
            Tiles.Add(new Tile("TilesSand", new Point(4, 1), "sandLeft"));

            Tiles.Add(new Tile("TilesSand", new Point(5, 0), "sandBottom"));
            Tiles.Add(new Tile("TilesSand", new Point(5, 1), "sandRight"));
            //-------------------------------------------------------------
            Tiles.Add(new Tile("TilesDirt", new Point(0, 0), "dirtLeftTop"));
            Tiles.Add(new Tile("TilesDirt", new Point(1, 0), "dirtMiddleTop"));
            Tiles.Add(new Tile("TilesDirt", new Point(2, 0), "dirtRightTop"));

            Tiles.Add(new Tile("TilesDirt", new Point(0, 1), "dirtLeftMiddle"));
            Tiles.Add(new Tile("TilesDirt", new Point(1, 1), "dirtMiddleMiddle"));
            Tiles.Add(new Tile("TilesDirt", new Point(2, 1), "dirtRightMiddle"));

            Tiles.Add(new Tile("TilesDirt", new Point(0, 2), "dirtLeftBottom"));
            Tiles.Add(new Tile("TilesDirt", new Point(1, 2), "dirtMiddleBottom"));
            Tiles.Add(new Tile("TilesDirt", new Point(2, 2), "dirtRightBottom"));

            Tiles.Add(new Tile("TilesDirt", new Point(3, 0), "dirtVertical"));
            Tiles.Add(new Tile("TilesDirt", new Point(3, 1), "dirtHorizontal"));
            Tiles.Add(new Tile("TilesDirt", new Point(3, 2), "dirt"));

            Tiles.Add(new Tile("TilesDirt", new Point(4, 0), "dirtTop"));
            Tiles.Add(new Tile("TilesDirt", new Point(4, 1), "dirtLeft"));

            Tiles.Add(new Tile("TilesDirt", new Point(5, 0), "dirtBottom"));
            Tiles.Add(new Tile("TilesDirt", new Point(5, 1), "dirtRight"));
            //------------------------------------------------------------
            Tiles.Add(new Tile("TilesSteel", new Point(0, 0), "steelLeftTop"));
            Tiles.Add(new Tile("TilesSteel", new Point(1, 0), "steelMiddleTop"));
            Tiles.Add(new Tile("TilesSteel", new Point(2, 0), "steelRightTop"));

            Tiles.Add(new Tile("TilesSteel", new Point(0, 1), "steelLeftMiddle"));
            Tiles.Add(new Tile("TilesSteel", new Point(1, 1), "steelMiddleMiddle"));
            Tiles.Add(new Tile("TilesSteel", new Point(2, 1), "steelRightMiddle"));

            Tiles.Add(new Tile("TilesSteel", new Point(0, 2), "steelLeftBottom"));
            Tiles.Add(new Tile("TilesSteel", new Point(1, 2), "steelMiddleBottom"));
            Tiles.Add(new Tile("TilesSteel", new Point(2, 2), "steelRightBottom"));

            Tiles.Add(new Tile("TilesSteel", new Point(3, 0), "steelVertical"));
            Tiles.Add(new Tile("TilesSteel", new Point(3, 1), "steelHorizontal"));
            Tiles.Add(new Tile("TilesSteel", new Point(3, 2), "steel"));

            Tiles.Add(new Tile("TilesSteel", new Point(4, 0), "steelTop"));
            Tiles.Add(new Tile("TilesSteel", new Point(4, 1), "steelLeft"));

            Tiles.Add(new Tile("TilesSteel", new Point(5, 0), "steelBottom"));
            Tiles.Add(new Tile("TilesSteel", new Point(5, 1), "steelRight"));

            TilesPhysics = new List<Tile>();
            TilesPhysics.Add(new Tile("TilesPhysics", new Point(0, 0), "obstacle"));
            TilesPhysics.Add(new Tile("TilesPhysics", new Point(1, 0), "water"));

            using (StreamWriter file = File.CreateText("Tiles.json"))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Formatting = Formatting.Indented;

                serializer.Serialize(file, Tiles);
            }

            #endregion Tiles

            ActualWindow = "Main";
            State = GlobalState.inMenu;
            mapLive = new Map();
            mapLiveWorld = new MapWorld();
            GameInProgress = false;
            EditInProgress = false;
            PlayerInstance = new Player();
            PlayerWorldInstance = new PlayerWorld();

            EditorMenuOpen = true;

            Sounds3D = new List<Sound>();
            SoundsMain = new List<Sound>();
            InfoList = new List<UIInformer>();

            short[,] tempMap = new short[0, 0];
            Globals.LoadJson(ref tempMap, "mapWorld");
            mapLiveWorld.AddTileMap(null, null, tempMap);

            short[,] tempMap2 = new short[0, 0];
            Globals.LoadJson(ref tempMap2, "mapWorldGraphic");
            mapLiveWorld.AddTileMap(tempMap2);

            #region AddTileMapPhysics

            string text = "";
            using (StreamReader sr = new StreamReader("mapG.json"))
            { text = sr.ReadToEnd(); }
            short[,] mapa = JsonConvert.DeserializeObject<short[,]>(text);
            mapLive.AddTileMap(null, null, mapa);

            #endregion AddTileMapPhysics

            #region AddTileMap

            string text2 = "";
            using (StreamReader sr = new StreamReader("map.json"))
            { text2 = sr.ReadToEnd(); }
            short[,] mapa2 = JsonConvert.DeserializeObject<short[,]>(text2);
            mapLive.AddTileMap(mapa2, null, null);

            #endregion AddTileMap

            mapLive.GenerateMapAndWater();

            #region RenderTargets

            TARGET_ALL = new RenderTarget2D(GraphicsGlobal.GraphicsDevice, (int)Globals.WinRenderSize.X, (int)Globals.WinRenderSize.Y);
            Layer1 = new RenderTarget2D(GraphicsGlobal.GraphicsDevice, (int)Globals.WinRenderSize.X, (int)Globals.WinRenderSize.Y);
            Layer2 = new RenderTarget2D(GraphicsGlobal.GraphicsDevice, (int)Globals.WinRenderSize.X, (int)Globals.WinRenderSize.Y);
            Win = new RenderTarget2D(GraphicsGlobal.GraphicsDevice, (int)Globals.WinRenderSize.X, (int)Globals.WinRenderSize.Y, false, SurfaceFormat.Color, DepthFormat.Depth24, 1, RenderTargetUsage.PreserveContents);
            EditorMenu = new RenderTarget2D(GraphicsGlobal.GraphicsDevice, (int)Globals.WinRenderSize.X, (int)Globals.WinRenderSize.Y, false, SurfaceFormat.Color, DepthFormat.Depth24, 1, RenderTargetUsage.PreserveContents);
            Lights = new RenderTarget2D(GraphicsGlobal.GraphicsDevice, (int)Globals.WinRenderSize.X, (int)Globals.WinRenderSize.Y);
            Map = new RenderTarget2D(GraphicsGlobal.GraphicsDevice, (int)Globals.WinRenderSize.X, (int)Globals.WinRenderSize.Y);
            FinalScreen = new RenderTarget2D(GraphicsGlobal.GraphicsDevice, (int)Globals.WinRenderSize.X, (int)Globals.WinRenderSize.Y);
            WaterMask = new RenderTarget2D(GraphicsGlobal.GraphicsDevice, (int)Globals.WinRenderSize.X, (int)Globals.WinRenderSize.Y);
            WaterWithoutLights = new RenderTarget2D(GraphicsGlobal.GraphicsDevice, (int)Globals.WinRenderSize.X, (int)Globals.WinRenderSize.Y);
            EditorBackground = new RenderTarget2D(GraphicsGlobal.GraphicsDevice, (int)Globals.WinRenderSize.X, (int)Globals.WinRenderSize.Y);
            MenuParts = new RenderTarget2D(GraphicsGlobal.GraphicsDevice, (int)Globals.WinRenderSize.X, (int)Globals.WinRenderSize.Y, false, SurfaceFormat.Color, DepthFormat.Depth24, 1, RenderTargetUsage.PreserveContents);
            ToRenderFirstPass = new RenderTarget2D(GraphicsGlobal.GraphicsDevice, (int)Globals.WinRenderSize.X, (int)Globals.WinRenderSize.Y);
            ToPrepareGaussPass1 = new RenderTarget2D(GraphicsGlobal.GraphicsDevice, (int)Globals.WinRenderSize.X, (int)Globals.WinRenderSize.Y);
            ToProcessSecondPass = new RenderTarget2D(GraphicsGlobal.GraphicsDevice, (int)Globals.WinRenderSize.X, (int)Globals.WinRenderSize.Y);
            CanvasToSecondPass = new RenderTarget2D(GraphicsGlobal.GraphicsDevice, (int)Globals.WinRenderSize.X, (int)Globals.WinRenderSize.Y);
            WaterAnimateLeft = new RenderTarget2D(GraphicsGlobal.GraphicsDevice, Globals.TileSize, Globals.TileSize * 2, false, SurfaceFormat.Color, DepthFormat.Depth24, 1, RenderTargetUsage.PreserveContents);
            WaterAnimateRight = new RenderTarget2D(GraphicsGlobal.GraphicsDevice, Globals.TileSize, Globals.TileSize * 2, false, SurfaceFormat.Color, DepthFormat.Depth24, 1, RenderTargetUsage.PreserveContents);
            WaterAnimateLeftTop = new RenderTarget2D(GraphicsGlobal.GraphicsDevice, Globals.TileSize, Globals.TileSize * 2, false, SurfaceFormat.Color, DepthFormat.Depth24, 1, RenderTargetUsage.PreserveContents);
            WaterAnimateRightTop = new RenderTarget2D(GraphicsGlobal.GraphicsDevice, Globals.TileSize, Globals.TileSize * 2, false, SurfaceFormat.Color, DepthFormat.Depth24, 1, RenderTargetUsage.PreserveContents);
            NormalMapBuffer = new RenderTarget2D(GraphicsGlobal.GraphicsDevice, (int)Globals.WinRenderSize.X, (int)Globals.WinRenderSize.Y);
            LightNormalHolder = new RenderTarget2D(GraphicsGlobal.GraphicsDevice, (int)Globals.WinRenderSize.X, (int)Globals.WinRenderSize.Y, false, SurfaceFormat.Color, DepthFormat.Depth24, 1, RenderTargetUsage.PreserveContents);

            EditorEntitiesTargets = new List<RenderTarget2D>();

            EditorLayer1 = new RenderTarget2D(GraphicsGlobal.GraphicsDevice, (int)Globals.WinRenderSize.X, (int)Globals.WinRenderSize.Y);

            #endregion RenderTargets

            Globals.Winsize = new Point((int)Globals.WinRenderSize.X, (int)Globals.WinRenderSize.Y);
            Globals.WinOffset = new Point(0, 0);
            Window.ClientSizeChanged += new EventHandler<EventArgs>(Globals.Window_ClientSizeChanged);

            Windows = new Dictionary<string, IWindow>();
            Windows.Add("Main", UIGameMenu.Instance);
            Windows.Add("Load", UILoadMenu.Instance);
            Windows.Add("Options", UIOptionsMenu.Instance);
            Windows.Add("Controls", UIControlsMenu.Instance);
            Windows.Add("Graphics", UIGraphicsMenu.Instance);
            Windows.Add("Audio", UIAudio.Instance);
            Windows.Add("Continue", UIContinueMenu.Instance);


            BscEffect = new BasicEffect(GraphicsGlobal.GraphicsDevice);

            BscEffect.TextureEnabled = true;
            BscEffect.LightingEnabled = false;
            BscEffect.AmbientLightColor = new Vector3(1f);

            UIGraphicsMenu.Instance.FullScreen.SetState(STP.IsFullScreen);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            #region Load
            EffectColors = Content.Load<Effect>("Effects/ColorManipulation");
            EffectBaseColor = Content.Load<Effect>("Effects/BaseColor");
            EffectBlur = Content.Load<Effect>("Effects/Gauss");
            EffectLights = Content.Load<Effect>("Effects/Lights");
            EffectSlide = Content.Load<Effect>("Effects/Slide");
            EffectWater = Content.Load<Effect>("Effects/Water");
            EffectAlpha = Content.Load<Effect>("Effects/AlphaManipulation");
            EffectNormalLight = Content.Load<Effect>("Effects/NormalLight");
            EffectRotateNormals = Content.Load<Effect>("Effects/RotateNormals");
            EffectVertexNormal = Content.Load<Effect>("Effects/vertexNormal");
            EffectBaseColorSimple = Content.Load<Effect>("Effects/BaseColorSimple");

            soundVortex = Content.Load<SoundEffect>("Sounds/vortex");
            soundExplosion = Content.Load<SoundEffect>("Sounds/explosion");
            soundElectro = Content.Load<SoundEffect>("Sounds/electro");
            soundTeleport = Content.Load<SoundEffect>("Sounds/teleport");
            soundHit = Content.Load<SoundEffect>("Sounds/hit");
            soundjump = Content.Load<SoundEffect>("Sounds/jump");
            soundCoin = Content.Load<SoundEffect>("Sounds/coin");
            sound = Content.Load<SoundEffect>("Sounds/sound");
            soundGrenadeHit = Content.Load<SoundEffect>("Sounds/grenadeHit");
            soundDamage = Content.Load<SoundEffect>("Sounds/damage");
            soundSaw = Content.Load<SoundEffect>("Sounds/saw");
            soundFlamb = Content.Load<SoundEffect>("Sounds/flamb");
            soundPlasmaRifleShoot = Content.Load<SoundEffect>("Sounds/PlasmaRifleShoot");
            soundSelect = Content.Load<SoundEffect>("Sounds/select");
            soundMechWalk = Content.Load<SoundEffect>("Sounds/mechWalk");

            laserDest = Content.Load<Texture2D>("Sprites/Test/laserDest");
            roomBase = Content.Load<Texture2D>("Sprites/Test/roomBase");
            podBase = Content.Load<Texture2D>("Sprites/Test/podBase");
            TestRec = Content.Load<Texture2D>("Sprites/Test/TestRec");
            rainDrop = Content.Load<Texture2D>("Sprites/Test/rainDrop");
            electronics = Content.Load<Texture2D>("Sprites/Test/gem");
            LootCrateStain = Content.Load<Texture2D>("Sprites/Test/LootCrateStain");
            plasmaLight = Content.Load<Texture2D>("Sprites/Test/plasmaLight");
            FireSmall2LightHalo = Content.Load<Texture2D>("Sprites/Test/FireSmall2LightHalo");
            FireSmall2 = Content.Load<Texture2D>("Sprites/Test/SmokeSmall2");
            muzzlePlasmaLight = Content.Load<Texture2D>("Sprites/Test/muzzlePlasmaLight");
            muzzlePlasma = Content.Load<Texture2D>("Sprites/Test/muzzlePlasma");
            PlasmaGunUp = Content.Load<Texture2D>("Sprites/Test/PlasmaGunUp");
            PlasmaGunDown = Content.Load<Texture2D>("Sprites/Test/PlasmaGunDown");
            muzzleSimpleLight = Content.Load<Texture2D>("Sprites/Test/muzzleSimpleLight");
            plasma = Content.Load<Texture2D>("Sprites/Test/plasma");
            plasmaGun = Content.Load<Texture2D>("Sprites/Test/plasmaGun");
            FireLightHalo = Content.Load<Texture2D>("Sprites/Test/FireSmallLightHalo");
            hotLight = Content.Load<Texture2D>("Sprites/Test/hotLight");
            projectileLight = Content.Load<Texture2D>("Sprites/Test/projectileLight");
            LaserLight = Content.Load<Texture2D>("Sprites/Test/LaserLight");
            controlBackHover = Content.Load<Texture2D>("Sprites/Test/controlBackHover");
            controlBack = Content.Load<Texture2D>("Sprites/Test/controlBack");
            Info = Content.Load<Texture2D>("Sprites/Test/Info");
            saveBackSelected = Content.Load<Texture2D>("Sprites/Test/saveBackSelected");
            saveBack = Content.Load<Texture2D>("Sprites/Test/saveBack");
            bubble = Content.Load<Texture2D>("Sprites/Test/bubble");
            door = Content.Load<Texture2D>("Sprites/Test/door");
            doorRack = Content.Load<Texture2D>("Sprites/Test/doorRack");
            FireSmallLight = Content.Load<Texture2D>("Sprites/Test/FireSmallLight");
            playerTurretHead = Content.Load<Texture2D>("Sprites/Test/playerTurretHead");
            playerTurretBase = Content.Load<Texture2D>("Sprites/Test/playerTurretBase");
            waterTop = Content.Load<Texture2D>("Sprites/Test/waterTop");
            FlameProjectile = Content.Load<Texture2D>("Sprites/Test/FlameProjectile");
            flameThrower = Content.Load<Texture2D>("Sprites/Test/flameThrower");
            flameThrowerUp = Content.Load<Texture2D>("Sprites/Test/flameThrowerUp");
            flameThrowerDown = Content.Load<Texture2D>("Sprites/Test/flameThrowerDown");
            turretHead = Content.Load<Texture2D>("Sprites/Test/turretHead");
            turretBase = Content.Load<Texture2D>("Sprites/Test/turretBase");
            tileMask = Content.Load<Texture2D>("Sprites/Test/tileMask");
            boundary = Content.Load<Texture2D>("Sprites/Test/boundary");
            sawNut = Content.Load<Texture2D>("Sprites/Test/sawNut");
            PlayerLight = Content.Load<Texture2D>("Sprites/Test/LightPlayer");
            zombieFrozen = Content.Load<Texture2D>("Sprites/Test/zombieFrozen");
            Letters = Content.Load<Texture2D>("Sprites/Test/letters");
            Tile03 = Content.Load<Texture2D>("Sprites/Test/Tile03");
            tissue = Content.Load<Texture2D>("Sprites/Test/coin");
            template = Content.Load<Texture2D>("Sprites/Test/template");
            flamb = Content.Load<Texture2D>("Sprites/Test/flamb");
            transporter = Content.Load<Texture2D>("Sprites/Test/transporter");
            SpawnPanel = Content.Load<Texture2D>("Sprites/Test/SpawnPanel");
            lightingGunUp = Content.Load<Texture2D>("Sprites/Test/lightingGunUp");
            lightingGunDown = Content.Load<Texture2D>("Sprites/Test/lightingGunDown");
            white = Content.Load<Texture2D>("Sprites/Test/white");
            grenade = Content.Load<Texture2D>("Sprites/Test/grenade");
            zombieShirtBlue = Content.Load<Texture2D>("Sprites/Test/zombieShirtBlue");
            portalThing = Content.Load<Texture2D>("Sprites/Test/portalThing");
            portal = Content.Load<Texture2D>("Sprites/Test/portal");
            sky = Content.Load<Texture2D>("Sprites/Test/sky");
            projectilGunDown = Content.Load<Texture2D>("Sprites/Test/projectilGunDown");
            projectilGunUp = Content.Load<Texture2D>("Sprites/Test/projectilGunUp");
            debugTex = Content.Load<Texture2D>("Sprites/Test/debug_4x4");
            playerTex = Content.Load<Texture2D>("Sprites/Test/player");
            waterTex = Content.Load<Texture2D>("Sprites/Test/water");
            platformTex = Content.Load<Texture2D>("Sprites/Test/platform");
            FontDebug = Content.Load<SpriteFont>("Fonts/FontDebug");
            triggerOffTex = Content.Load<Texture2D>("Sprites/Test/off");
            triggerOnTex = Content.Load<Texture2D>("Sprites/Test/on");
            debug_longTex = Content.Load<Texture2D>("Sprites/Test/debug_long");
            LightTex = Content.Load<Texture2D>("Sprites/Test/Light");
            LightLineTex = Content.Load<Texture2D>("Sprites/Test/LightLine");
            debug_thin = Content.Load<Texture2D>("Sprites/Test/debug_thin");
            projectilTex = Content.Load<Texture2D>("Sprites/Test/Projectil");
            smoke = Content.Load<Texture2D>("Sprites/Test/smoke");
            flame = Content.Load<Texture2D>("Sprites/Test/flame");
            haloProjectil = Content.Load<Texture2D>("Sprites/Test/halo");
            projectil = Content.Load<Texture2D>("Sprites/Test/projectil2");
            particle = Content.Load<Texture2D>("Sprites/Test/particle");
            healthBar = Content.Load<Texture2D>("Sprites/Test/healthBar");
            barBackground = Content.Load<Texture2D>("Sprites/Test/healthBar2");
            healthBarBackground = Content.Load<Texture2D>("Sprites/Test/healthBarBackground");
            projectilGun = Content.Load<Texture2D>("Sprites/Test/projectilGun");
            lightingGun = Content.Load<Texture2D>("Sprites/Test/lightingGun");
            rocketLauncher = Content.Load<Texture2D>("Sprites/Test/rocketLauncher");
            laserGun = Content.Load<Texture2D>("Sprites/Test/laserGun");
            energyLauncher = Content.Load<Texture2D>("Sprites/Test/energyLauncher");
            energyLauncherUp = Content.Load<Texture2D>("Sprites/Test/energyLauncherUp");
            energyLauncherDown = Content.Load<Texture2D>("Sprites/Test/energyLauncherDown");
            cursor = Content.Load<Texture2D>("Sprites/Test/cursor");
            health = Content.Load<Texture2D>("Sprites/Test/health");
            shield = Content.Load<Texture2D>("Sprites/Test/shield");
            numbers = Content.Load<Texture2D>("Sprites/Test/numbers");
            smokeeExpl = Content.Load<Texture2D>("Sprites/Test/smokeExpl");
            foot = Content.Load<Texture2D>("Sprites/Test/foot");
            footAir = Content.Load<Texture2D>("Sprites/Test/footAir");
            tileMiddle = Content.Load<Texture2D>("Sprites/Test/tileMiddle");
            tileTop = Content.Load<Texture2D>("Sprites/Test/tileTop");
            enemyPiece = Content.Load<Texture2D>("Sprites/Test/enemyPiece");
            spawn = Content.Load<Texture2D>("Sprites/Test/spawn");
            laserDestHalo = Content.Load<Texture2D>("Sprites/Test/laserDestHalo");
            muzzleSimple = Content.Load<Texture2D>("Sprites/Test/muzzleSImple");
            rocketLauncherFlame = Content.Load<Texture2D>("Sprites/Test/rocketLauncherFlame");
            smokeSmall = Content.Load<Texture2D>("Sprites/Test/smokeSmall");
            hot = Content.Load<Texture2D>("Sprites/Test/hot");
            saw = Content.Load<Texture2D>("Sprites/Test/saw");
            shieldBarBackground = Content.Load<Texture2D>("Sprites/Test/shieldBarBackground");
            shieldBar = Content.Load<Texture2D>("Sprites/Test/shieldBar");
            numbersMedium = Content.Load<Texture2D>("Sprites/Test/numbersMedium");
            zombie = Content.Load<Texture2D>("Sprites/Test/zombie");
            zombieShirt = Content.Load<Texture2D>("Sprites/Test/zombieShirt");
            laserGunUp = Content.Load<Texture2D>("Sprites/Test/laserGunUp");
            laserGunDown = Content.Load<Texture2D>("Sprites/Test/laserGunDown");
            backgroundWin = Content.Load<Texture2D>("Sprites/Test/backgroundWin");
            LightRocket = Content.Load<Texture2D>("Sprites/Test/LightRocket");
            TESTLIGHT = Content.Load<Texture2D>("Sprites/Test/TESTLIGHT");
            lampGround = Content.Load<Texture2D>("Sprites/Test/lampGround");
            lampGroundLight = Content.Load<Texture2D>("Sprites/Test/lampGroundLight");
            flameLight = Content.Load<Texture2D>("Sprites/Test/flameLight");
            tileBack1 = Content.Load<Texture2D>("Sprites/Test/tileBack1");
            tileBackLeft = Content.Load<Texture2D>("Sprites/Test/tileBackLeft");
            Transition = Content.Load<Texture2D>("Sprites/Test/Transition");
            footWalk = Content.Load<Texture2D>("Sprites/Test/footWalk");
            SmokeBig = Content.Load<Texture2D>("Sprites/Test/SmokeBig");
            liftRail = Content.Load<Texture2D>("Sprites/Test/liftRail");
            liftEnd = Content.Load<Texture2D>("Sprites/Test/liftEnd");
            Flake = Content.Load<Texture2D>("Sprites/Test/Flake");
            fireBig = Content.Load<Texture2D>("Sprites/Test/fireSmall");
            triggerOnLight = Content.Load<Texture2D>("Sprites/Test/triggerOnLight");
            triggerOffLight = Content.Load<Texture2D>("Sprites/Test/triggerOffLight");

            #endregion Load

            mapLive.ChangeLightLevel(new Vector3(0.25f));
            mapLiveWorld.ChangeLightLevel(new Vector3(1f));
            mapLive.ShowBoundary = true;
            PlayerInstance.PutOnVest();
            PlayerInstance.AccesWeapons();

            SpriteBatchGlobal = new SpriteBatch(GraphicsDevice);

            AddEntity(DrawEntities.DrawBuddyDefault, new Vector2(104, 132));
            AddEntity(DrawEntities.DrawMechDefault, new Vector2(64, 64));
            AddEntity(DrawEntities.DrawSawDefault,new Vector2(128, 128));
            AddEntity(DrawEntities.DrawPlayerTurretDefault,new Vector2(128, 128));
            AddEntity(DrawEntities.DrawTriggerDefault,new Vector2(96));
            AddEntity(DrawEntities.DrawHealth,new Vector2(128));
            AddEntity(DrawEntities.DrawShield,new Vector2(128));

            //AddEntity(() => DrawEntities.DrawMech(new RectangleF(64 * 2, 96 * 2, 64, 64), 0, false, Vector2.Zero, CollisionResolver.Standing));
            //AddEntity(() => DrawEntities.DrawPlayerTurret(new RectangleF(new Vector2(36, 64), new Vector2(110, 192)), 0f, 0f));
            //AddEntity(() => DrawEntities.DrawCrate(new Vector2(96, 192)));
            //AddEntity(() => DrawEntities.DrawHealth(new Vector2(128), new Vector2(2)));
            //AddEntity(() => DrawEntities.DrawShield(new Vector2(128), new Vector2(2)));
            //AddEntity(() => DrawEntities.DrawGear(new Vector2(128), new Vector2(2)));
            //AddEntity(() => DrawEntities.DrawSaw(new Vector2(128), 0));
            //AddEntity(() => DrawEntities.DrawZombie(false, CollisionResolver.Standing, 0, new RectangleF(24 * 2, 62 * 2, 104, 132), 0, Vector2.Zero));
            //AddEntity(() => DrawEntities.DrawTrigger(new Vector2(96), true, TriggerTypes.triggerOld));

            //mapLive.MapMovables.Add(new Elevator(new Vector2(700, 1000), 0.05f, "lol", new Vector2(1400, 1600)));

            //mapLive.MapMovables.Add(new Elevator(new Vector2(1400, 0), 0.1f, "lol", new Vector2(1400, 1400)));

            //mapLive.MapMovables.Add(new Elevator(new Vector2(1200, 1400), 0.1f, "lol", new Vector2(1800, 200)));

            //mapLive.MapNpcs.Add(new Turret(new Vector2(1280 + 512, 800), 250));

            //mapLive.mapDecorations.Add(new DecorationStatic(new Vector2(2048, 1024 + 32), new TexPos("sandLamp", Vector2.Zero), new TexPos("sandLampNormal", Vector2.Zero), isFront: true, light: new Light(new Vector2(1024 * 2, 1024), new Vector4(2, 2, 1, 1), "flareSand", new Vector2(-64 + 16, -64 + 16 + 32))));

            //mapLive.MapNpcs.Add(new Battery(new Vector2(800)));
            //mapLive.mapTriggers.Add(new BatteryTrigger(new Vector2(1800, 1200), "lol"));

            //mapLive.mapCrates.Add(new CrateStatic(new Vector2(800, 1280 + 64), 5, 5, 5, 5));
            //mapLive.MapNpcs.Add(new Crate(new Vector2(1800, 800), 5, 5, 5, 5));

            //Globals.SaveJson(mapLive, "", "map1");

            //mapLiveWorld.Cities.Add(new City(new Vector2(32, 32), "map1", false));
            //mapLiveWorld.Cities.Add(new City(new Vector2(32, 96), "map2", false));
            //mapLiveWorld.Cities.Add(new City(new Vector2(128, 64), "", true));

            //Globals.SaveJson(mapLiveWorld, "", "world");
        }

        public delegate void DrawThing(Vector2 position);

        public void AddEntity(DrawThing method, Vector2 position)
        {
            EditorEntitiesTargets.Add(new RenderTarget2D(GraphicsGlobal.GraphicsDevice, 256, 256, false, SurfaceFormat.Color, DepthFormat.Depth24, 1, RenderTargetUsage.PreserveContents));
            GraphicsDevice.SetRenderTarget(EditorEntitiesTargets[EditorEntitiesTargets.Count - 1]);
            SpriteBatchGlobal.Begin(samplerState: SamplerState.PointWrap, blendState: BlendState.AlphaBlend, sortMode: SpriteSortMode.Immediate);
            method(position);
            SpriteBatchGlobal.End();
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            Delta = gameTime.ElapsedGameTime.Milliseconds;
            Time = (float)gameTime.TotalGameTime.TotalSeconds;

            DrawAnimateWater.Update();
            DrawAnimateWaterOver.Update();

            SoundEffect.MasterVolume = STP.GlobalVolume * STP.Mute;

            MouseInput.NewUpdate();
            KeyboardInput.NewUpdate();

            if (KeyboardInput.KeyPressed(Keys.R))
            {
                Globals.AddContentNpc(new Zombie(new Vector2(1024, 256)));
            }

            if (KeyboardInput.KeyPressed(Keys.Tab))
            {
                GameScreen = InGameScreen.none;
            }

            if (KeyboardInput.KeyPressed(Keys.H))
            {
                PlayerInstance.Alive = true;
                PlayerInstance.ControlsActive = true;
            }

            Globals.Escape();
            Globals.TakeScreenShot(GraphicsGlobal.GraphicsDevice);
            Globals.ToogleFulscreen();

            if (State == GlobalState.inMenu) Windows[ActualWindow].Update();
            else if (State == GlobalState.inGame)
            {
                if (GameScreen == InGameScreen.none)
                    GameUpdate.InGameUpdate();
                else if (GameScreen == InGameScreen.assembly)
                    UIAssembly.Instance.Update();
                else if (GameScreen == InGameScreen.shop)
                    UIShop.Instance.Update();
            }
            else if (State == GlobalState.inWorld) GameUpdate.InWorldUpdate();
            else if (State == GlobalState.inEditor) Editor.Update();

            GameUpdate.TweakSound();

            foreach (UIInformer info in InfoList.Reverse<UIInformer>()) info.Update();

            KeyboardInput.OldUpdate();
            MouseInput.OldUpdate();

            View = Camera2DGame.GetViewMatrix();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            Globals.FrameRate = 1 / (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (State == GlobalState.inWorld)
            {
                GameDraw.DrawWorld(mapLiveWorld);
            }
            else if (State == GlobalState.inGame)
            {
                GameDraw.DrawGame(mapLive);
            }
            else if (State == GlobalState.inMenu)
            {
                GameDraw.DrawWin();
                GameDraw.DrawWinTarget();
            }
            else if (State == GlobalState.inEditor)
            {
                if (EditorMenuOpen == true)
                {
                    GameDraw.DrawEditorWin();
                    GameDraw.DrawWinTarget();
                }
                else
                {
                    GameDraw.DrawEditor();
                    GameDraw.DrawEditorWinTarget();
                }
            }

            base.Draw(gameTime);
        }
    }
}