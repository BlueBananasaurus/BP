using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Monogame_GL
{
    public class Trigger : ITriggers
    {
        public string Name { get; set; }
        public Vector2 Position { get; set; }
        public bool On { get; set; }
        public RectangleF Boundary { get; set; }
        public string Target { get; set; }
        private Texture2D _texOnLight;
        private Texture2D _texOffLight;
        private TriggerTypes _type;
        private bool _lockAfterUse;
        private bool _locked;

        public Trigger(Vector2 position, string target, bool startOn, TriggerTypes type, bool lockAfterUse = false)
        {
            Position = position;
            Target = target;
            On = startOn;
            if (type == TriggerTypes.triggerOld)
            {
                Boundary = new RectangleF(64, 64, position.X, position.Y);
            }
            if (type == TriggerTypes.triggerBase)
            {
                Boundary = new RectangleF(48, 48, position.X, position.Y);
            }
            _texOnLight = Game1.triggerOnLight;
            _texOffLight = Game1.triggerOffLight;
            _type = type;
            _lockAfterUse = lockAfterUse;
            _locked = false;
        }

        public void Update()
        {
        }

        public void TriggerSwitch()
        {
            if (On == true)
            {
                SetOff();
            }
            else
            {
                SetOn();
            }
        }

        public void SetOff()
        {
            if (_locked == false)
            {
                foreach (IRectanglePhysics recGet in Game1.mapLive.MapMovables)
                {
                    if (recGet.Name == Target)
                    {
                        recGet.SetOff();
                    }
                }

                Sound.PlaySoundPosition(Boundary.Origin, Game1.Sounds["Button"]);
                On = false;
                if (_lockAfterUse == true)
                    _locked = true;
            }
        }

        public void SetOn()
        {
            if (_locked == false)
            {
                foreach (IRectanglePhysics recGet in Game1.mapLive.MapMovables)
                {
                    if (recGet.Name == Target)
                    {
                        recGet.SetOn();
                    }
                }

                Sound.PlaySoundPosition(Boundary.Origin, Game1.Sounds["Button"]);
                On = true;
                if (_lockAfterUse == true)
                    _locked = true;
            }
        }

        public void Draw()
        {
            DrawEntities.DrawTrigger(Position, On, _type);
        }

        public void DrawNormal()
        {
            Game1.SpriteBatchGlobal.Draw(Game1.Textures["triggerNormal"], Position);
        }

        public void DrawLight()
        {
            if (On)
                Game1.SpriteBatchGlobal.Draw(_texOnLight, Boundary.Origin - new Vector2(128), scale: new Vector2(2));
            else
                Game1.SpriteBatchGlobal.Draw(_texOffLight, Boundary.Origin - new Vector2(128), scale: new Vector2(2));
        }
    }
}