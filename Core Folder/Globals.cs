using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Newtonsoft.Json;
using System;
using System.IO;

namespace Monogame_GL
{
    public enum ItemHolderTypes { basic, purple, blue }

    public enum Ruleset { basic }

    public enum mechFacingLegs { left, right }

    public enum mechFacingCabin { left, right }

    public enum ZombieStates { left, stay, right }

    public enum monsters { zombie }

    public enum GrenadeClass { classic, electro, fire, nuke }

    public enum ButtonStates { none, hover, pressed, locked };

    public enum GraberState { none, hover, grabed };

    public enum Align { left, center };

    public enum ListItemStatesEnum { none, hover, selected };

    public enum Pickups { tissue, electronics, highTissue, highElectronics }

    public enum DoorTypes { old, basic }

    public enum TriggerTypes { triggerOld, triggerBase }

    public enum GlobalState { inMenu, inGame, inEditor, inWorld }

    public enum ButtonType { small, big, scrooll, check, radioBig, back, drop, inDrop }

    public enum CheckBoxType { classic, visibility, sound }

    public enum RadioType { classic, big }

    public enum FontType { small, big }

    public enum FileFolder { folder, json, file, drive }

    public enum textBoxType { number, text, fileName }

    public enum AllEntitiesEditor { player, mech, zombie }

    public enum GrabShow { byteValue, percentage, none }

    public enum LayerEditing { background, foreground, physics, noChoosed }

    public enum InGameScreen { none, shop, assembly }

    public enum Module { tier2, tier3, test }

    public enum Filtering { point, linear, anisotropic}

    public enum CollisionSides { bottom,top,left,right,none}

    public static class Globals
    {
        public const byte TileSize = 32;
        public static Random GlobalRandom { get; } = new Random();
        public static float Gravity = TileSize / 1024f;
        public static Vector2 WinRenderSize = new Vector2(1920f, 1080f);
        public static float UITransparencyFade = 256;
        public static Point Winsize;
        public static Point WinOffset;
        public static bool Horizontal = true;
        public static Vector2 ScreenRatio;
        public static float FrameRate;
        public static float BackButtonPosY = 768f + 128f;
        public static Vector2 MenuTitlePosition = new Vector2(256, 32);
        public static float ButtonXPos = 128;
        public static Color LightBlueText = new Color(129, 150, 159);
        public static Color LightGrayText = new Color(245, 245, 245);
        public static Color LightRedish = new Color(240, 180, 180);
        public static Color LightLocked = new Color(150, 150, 150);
        public static Filtering filter = Filtering.point;

        public static FourColorsButtons ColorRedish = new FourColorsButtons(LightRedish, LightRedish, LightRedish, LightRedish);
        public static FourColorsButtons ColorGrayBlue = new FourColorsButtons(LightGrayText, LightGrayText, LightBlueText, LightBlueText);
        public static FourColorsButtons ColorBlueGray = new FourColorsButtons(LightBlueText, LightGrayText, LightBlueText,  LightGrayText);

        public static void AddContentNpc(Inpc npc)
        {
            Game1.mapLive.MapNpcs.Add(npc);
        }

        public static void AddContenProp(IDecoration decor)
        {
            Game1.mapLive.mapDecorations.Add(decor);
        }

        public static void AddContenPropLayer2(IDecoration decor)
        {
            Game1.mapLive.mapDecorationsLayer2.Add(decor);
        }

        public static void AddTrigger(ITriggers trigger)
        {
            Game1.mapLive.mapTriggers.Add(trigger);
        }

        public static void AddPick(IPickable pick)
        {
            Game1.mapLive.mapPickables.Add(pick);
        }

        public static void HideWindow()
        {
            Game1.State = GlobalState.inGame;
        }

        public static void NewGame()
        {
            if (UIGameMenu.Instance.MapName.Valid == true)
            {
                try
                {
                    LoadJson(ref Game1.mapLiveWorld, UIGameMenu.Instance.MapName.Text);
                    Game1.State = GlobalState.inWorld;
                    Game1.GameInProgress = true;
                    UIGameMenu.Instance.MapError = false;
                    Game1.PlayerInstance = new Player();
                }
                catch(FileNotFoundException ex)
                {
                    UIGameMenu.Instance.MapError = true;
                }
                catch (Exception ex)
                {
                    UIGameMenu.Instance.MapError = true;
                }
            }
            else
            {
                UIGameMenu.Instance.MapError = true;
            }
        }

        public static void EditorEnter()
        {
            Game1.State = GlobalState.inEditor;
            Game1.EditInProgress = true;
        }

        public static void ChangeWindowTo(string key)
        {
            Game1.Windows[Game1.ActualWindow].Reset();
            Game1.ActualWindow = key;

            //if (Game1.GameInProgress == true && key == "Main")
            //    Game1.ActualWindow = "Continue";
        }

        public static void Mute()
        {
            Game1.STP.Mute = 0;
        }

        public static void Unmute()
        {
            Game1.STP.Mute = 1;
        }

        public static void OpenAssembly()
        {
            Game1.GameScreen = InGameScreen.assembly;
        }

        public static void OpenShop()
        {
            Game1.GameScreen = InGameScreen.shop;
        }

        public static void Quit()
        {
            Game1.Game1Self.Exit();
        }

        public static void FullscreenOn()
        {
            Game1.STP.IsFullScreen = true;
            Game1.STP.WindowSizeFullscreen = new Point(GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width, GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height);
            Game1.GraphicsGlobal.PreferredBackBufferHeight = Game1.STP.WindowSizeFullscreen.Y;
            Game1.GraphicsGlobal.PreferredBackBufferWidth = Game1.STP.WindowSizeFullscreen.X;
            Game1.GraphicsGlobal.IsFullScreen = true;
            Game1.GraphicsGlobal.ApplyChanges();
        }

        public static void FullscreenOff()
        {
            Game1.STP.IsFullScreen = false;
            Game1.STP.WindowSizeWindowed = new Point(1920 / 2,1080 / 2);
            Game1.GraphicsGlobal.PreferredBackBufferHeight = Game1.STP.WindowSizeWindowed.Y;
            Game1.GraphicsGlobal.PreferredBackBufferWidth = Game1.STP.WindowSizeWindowed.X;
            Game1.GraphicsGlobal.IsFullScreen = false;
            Game1.GraphicsGlobal.ApplyChanges();
        }

        public static void LoadJson<T>(ref T type, string link)
        {
            string textLinks = "";
            using (StreamReader sr = new StreamReader(link + ".json"))
            {
                textLinks = sr.ReadToEnd();
            }

            type = JsonConvert.DeserializeObject<T>(textLinks, new JsonSerializerSettings
            { TypeNameHandling = TypeNameHandling.Auto });
        }

        public static void SaveJson<T>(T type, string link, string name)
        {
            using (StreamWriter file = File.CreateText(Path.Combine(@link, name + ".json")))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Formatting = Formatting.None;
                serializer.TypeNameHandling = TypeNameHandling.All;

                serializer.Serialize(file, type);
            }
        }

        public static void TakeScreenShot(GraphicsDevice device)
        {
            if (KeyboardInput.KeyboardStateOld.IsKeyUp(Game1.STP.ControlKeys["Screenshot"]) == true && KeyboardInput.KeyboardStateNew.IsKeyDown(Game1.STP.ControlKeys["Screenshot"]) == true)
            {
                string name = "screenshot_";
                int number = 0;

                while (File.Exists(name + number + ".png") == true)
                {
                    number++;
                }

                using (FileStream stream = new FileStream(name + number + ".png", FileMode.Create))
                {
                    Game1.FinalScreen.SaveAsPng(stream, Game1.FinalScreen.Width, Game1.FinalScreen.Height);
                    Game1.InfoList.Insert(0, new UIInformer(name + number + " saved"));
                }
            }
        }

        public static void Window_ClientSizeChanged(object sender, EventArgs e)
        {
            if (Game1.Game1Self.Window.ClientBounds.Size.X >= Game1.Game1Self.Window.ClientBounds.Size.Y * (Globals.WinRenderSize.X / Globals.WinRenderSize.Y))
            {
                Horizontal = true;
            }
            else
            {
                Horizontal = false;
            }

            if (Horizontal)
            {
                Winsize.Y = Game1.Game1Self.Window.ClientBounds.Size.Y;
                Winsize.X = (int)(Winsize.Y * (WinRenderSize.X / WinRenderSize.Y));
            }
            else
            {
                Winsize.X = Game1.Game1Self.Window.ClientBounds.Size.X;
                Winsize.Y = (int)(Winsize.X / (WinRenderSize.X / WinRenderSize.Y));
            }

            if (Horizontal)
            {
                WinOffset.X = Game1.Game1Self.Window.ClientBounds.Width / 2 - Winsize.X / 2;
                WinOffset.Y = 0;
            }
            else
            {
                WinOffset.Y = Game1.Game1Self.Window.ClientBounds.Height / 2 - Winsize.Y / 2;
                WinOffset.X = 0;
            }

            Game1.GraphicsGlobal.PreferredBackBufferWidth = Winsize.X;
            Game1.GraphicsGlobal.PreferredBackBufferHeight = Winsize.Y;

            ScreenRatio = new Vector2(Winsize.X, Winsize.Y) / new Vector2(WinRenderSize.X, WinRenderSize.Y);

            Camera2DEditor.MoveBy(Vector2.Zero);

            if (Game1.GraphicsGlobal.IsFullScreen == false)
            {
                Game1.STP.WindowSizeWindowed = new Point(Game1.Game1Self.Window.ClientBounds.Size.X, Game1.Game1Self.Window.ClientBounds.Size.Y);
                Game1.STP.Save();
            }
        }

        public static void ToogleFulscreen()
        {
            if (KeyboardInput.KeyboardStateOld.IsKeyUp(Game1.STP.ControlKeys["Toogle fullscreen"]) && KeyboardInput.KeyboardStateNew.IsKeyDown(Game1.STP.ControlKeys["Toogle fullscreen"]))
            {
                if (Game1.STP.IsFullScreen == true)
                {
                    FullscreenOff();
                    UIGraphicsMenu.Instance.FullScreen.SetState(false);
                }
                else
                {
                    FullscreenOn();
                    UIGraphicsMenu.Instance.FullScreen.SetState(true);
                }

                Game1.GraphicsGlobal.ApplyChanges();
            }
        }

        public static void Escape()
        {
            if (KeyboardInput.KeyboardStateOld.IsKeyUp(Keys.Escape) == true && KeyboardInput.KeyboardStateNew.IsKeyDown(Keys.Escape) == true)
            {
                if (Game1.ActualWindow != "Main" || Game1.State == GlobalState.inGame || Game1.State == GlobalState.inEditor || Game1.State == GlobalState.inWorld)
                {
                    Game1.State = GlobalState.inMenu;

                    //if (Game1.State == GlobalState.inGame)
                    //    ChangeWindowTo("Continue");
                    //else
                        ChangeWindowTo("Main");
                }
                else
                {
                    Quit();
                }
            }
        }
    }
}