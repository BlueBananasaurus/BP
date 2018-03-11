using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace Monogame_GL
{
    public class Setup
    {
        public Dictionary<string, Keys> ControlKeys;
        public float GlobalVolume;
        public bool IsFullScreen;
        public Point WindowSizeFullscreen;
        public Point WindowSizeWindowed;
        public byte Mute;

        public Setup()
        {
            ControlKeys = new Dictionary<string, Keys>();
            IsFullScreen = false;
        }

        public void LoadSetup()
        {
            Globals.LoadJson(ref Game1.STP, "Setup");
        }

        public void Save()
        {
            using (StreamWriter file = File.CreateText("Setup.json"))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Formatting = Formatting.Indented;

                serializer.Serialize(file, this);
            }
        }
    }
}