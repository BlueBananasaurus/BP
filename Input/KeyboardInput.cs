namespace Monogame_GL
{
    using Microsoft.Xna.Framework.Input;
    using System.Collections.Generic;

    public static class KeyboardInput
    {
        public static KeyboardState KeyboardStateNew { get; private set; }
        public static KeyboardState KeyboardStateOld { get; private set; }

        public static List<Keys> GetPressedKeys()
        {
            List<Keys> keysNew = new List<Keys>();
            keysNew.AddRange(KeyboardStateNew.GetPressedKeys());
            List<Keys> keysOld = new List<Keys>();
            keysOld.AddRange(KeyboardStateOld.GetPressedKeys());

            foreach (Keys key in keysOld)
            {
                keysNew.Remove(key);
            }

            return keysNew;
        }

        public static bool KeyPressed(Keys key)
        {
            if (KeyboardStateNew.IsKeyDown(key) && KeyboardStateOld.IsKeyUp(key))
            {
                return true;
            }
            return false;
        }

        public static void NewUpdate()
        {
            KeyboardStateNew = Keyboard.GetState();
        }

        public static void OldUpdate()
        {
            KeyboardStateOld = KeyboardStateNew;
        }
    }
}