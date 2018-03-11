using Microsoft.Xna.Framework;
using System;

namespace Monogame_GL
{
    public class Door : IRectanglePhysics
    {
        public Vector2 Position;
        private Movable _door;
        private float _speed;
        private float _speedMax;
        private DoorTypes _type;
        public Door(Vector2 position, float speed, string name, bool on, DoorTypes type)
        {
            Name = name;
            _door = new Movable(new RectangleF(32, 128, position.X, position.Y));
            Position = position;
            _speedMax = speed;
            On = on;
            _type = type;
        }

        public string Name { get; }
        public bool On { get; set; }
        public RectangleF Boundary
        {
            get
            {
                return new RectangleF(_door.Boundary.Size, new Vector2((float)Math.Round(_door.Boundary.Position.X), (float)Math.Round(_door.Boundary.Position.Y)));
            }
            set
            {
                _door.Boundary = value;
            }
        }

        public Vector2 velocity
        {
            get
            {
                return new Vector2(0, _speed);
            }
        }
        public void Draw()
        {
            switch (_type)
            {
                case DoorTypes.old:
                    Game1.SpriteBatchGlobal.Draw(Game1.doorRack, position: Position - new Vector2(0, 128), scale: new Vector2(2));
                    _door.Draw(Game1.door);
                    break;

                case DoorTypes.basic:
                    _door.Draw(Game1.Textures["DoorBase"]);
                    break;
            }
        }

        public void DrawNormal() { }

        public void SetOff()
        {
            On = false;
        }

        public void SetOn()
        {
            On = true;
        }

        public void Update()
        {
            if (On == false)
            {
                _speed = _speedMax;
                _door.Update(new Vector2(0, _speed));
                {
                    if (_door.Boundary.Position.Y >= Position.Y)
                    {
                        _speed = 0;
                        _door.Boundary.Position = new Vector2(_door.Boundary.Position.X, Position.Y);
                    }
                }
            }
            else
            {
                _speed = -_speedMax;
                _door.Update(new Vector2(0, _speed));

                if (_door.Boundary.Position.Y <= Position.Y - 128)
                {
                    _speed = 0;
                    _door.Boundary.Position = new Vector2(_door.Boundary.Position.X, Position.Y - 128);
                }
            }
        }
    }
}