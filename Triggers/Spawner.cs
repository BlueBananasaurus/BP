using Microsoft.Xna.Framework;
using System;

namespace Monogame_GL
{
    public class Spawner : ITriggers
    {
        public string Name { get; set; }
        private Timer _timer;
        private monsters _monster;
        public RectangleF Boundary { set; get; }
        private float rotation;

        public Spawner(float delay, monsters monster, Vector2 position)
        {
            _timer = new Timer(delay, false);
            _monster = monster;
            Boundary = new RectangleF(new Vector2(96), position);
            rotation = 0;
        }

        public bool On
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public Vector2 Position
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public string Target
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public void Draw()
        {
            Game1.SpriteBatchGlobal.Draw(Game1.portal, Boundary.Position + new Vector2(0, 0), scale: new Vector2(2), origin: new Vector2(64, 64));
            Game1.SpriteBatchGlobal.Draw(Game1.portalThing, Boundary.Position + new Vector2(0, -96 - 3), scale: new Vector2(2), origin: new Vector2(50, 16), rotation: (float)(Globals.GlobalRandom.NextDouble() - 0.5f) / 8f - (float)Math.PI / 2);
            Game1.SpriteBatchGlobal.Draw(Game1.SpawnPanel, Boundary.Position + new Vector2(0, 0), scale: new Vector2(2), origin: new Vector2(31.5f, 31.5f), rotation: (float)(Globals.GlobalRandom.NextDouble() - 0.5f) / 12f);

            Game1.SpriteBatchGlobal.Draw(Game1.spawn, Boundary.Position, scale: new Vector2(2), rotation: rotation / 5f, origin: new Vector2(96 / 2, 96 / 2));
        }

        public void TriggerSwitch()
        {
            throw new NotImplementedException();
        }

        public void Update()
        {
            _timer.Update();
            rotation -= Game1.Delta / 10;

            if (_timer.Ready == true)
            {
                if (_monster == monsters.zombie)
                {
                    //Game1.Manager.Screens[Game1.ActualScreen].ScreenMap.MapNpcs.Add(new Zombie(Boundary.Position, new Vector2(0.5f, 0.5f), 1f, 100f));
                }
                _timer.Reset();
            }
        }

        public void DrawNormal()
        {
        }

        public void DrawLight()
        {
            Game1.SpriteBatchGlobal.Draw(Game1.PlayerLight, Boundary.Position - new Vector2(128));
        }
    }
}