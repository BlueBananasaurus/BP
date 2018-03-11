using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Linq;

namespace Monogame_GL
{
    public class Player
    {
        public Vector2 Velocity;
        public bool Walking;
        public IPlayer CurretnObjectControl;
        public RectangleF curretnBoundary;

        public Player()
        {
            InVehicle = false;
            Speed = new Vector2(0.4f, 1.0f);
            Velocity = Vector2.Zero;

            Resolver = new CollisionResolver(Globals.TileSize);
            Tissue = 10000;
            Electronics = 10000;
            MaxHealth = 100;
            PotentialHealth = 100;
            PotentialShield = 100;
            Health = 100;
            maxShield = 100;
            Shield = 100;
            GrenadesCount = 3000;
            ControlsActive = true;
            Alive = true;
            ShowDestination = false;
            Carry = null;
            Weapons = new List<IWeapon>();
            MaxSpeed = new Vector2(0.6f);
            WeaponsAvailable = true;
            VestOn = true;
            GrenadeDmg = 100;
            Items = new List<PlayersItem>();

            Weapons.Add(new SimpleGun(1.0f, 0, 1000, this, 100, 10));
            Weapons.Add(new LightningGun(1.0f, 1000, 1000, this, 200, 20));
            Weapons.Add(new LaserGun(1.0f, 1000, 1000, this, 60, 30));
            Weapons.Add(new FlameThrower(1.0f, 1000, 1000, this, 100, 40));
            Weapons.Add(new RocketLauncher(1.6f, 100, 100, this, 100, 75));
            Weapons.Add(new EnergyLaucher(0.6f, 100, 100, this, 100, 125));
            Weapons.Add(new PlasmaGun(2.0f, 100, 100, this, 150, 60));

            CurrentWeapon = 0;
            CurrentWeaponObject = Weapons[CurrentWeapon];
            Boundary = new RectangleF(BuddyModule.PlayerBoundarySize, new Vector2(256, 256));
            CurretnObjectControl = new BuddyModule(this, new Vector2(512, 256));
        }

        public List<PlayersItem> Items { get; set; }
        public short GrenadeDmg { get; set; }
        public bool Alive { get; set; }
        public bool ControlsActive { get; set; }
        public bool InVehicle { get; set; }
        public bool ShowDestination { get; set; }
        public bool VestOn { get; set; }
        public bool WeaponsAvailable { get; set; }
        public CollisionResolver Resolver { get; set; }
        public float Health { get; set; }
        public float PotentialHealth { get; set; }
        public float PotentialShield { get; set; }
        public float Shield { get; set; }
        public Icarry Carry { get; set; }
        public int Electronics { get; set; }
        public int Tissue { get; set; }
        public int WalkChange { get; set; }
        public IWeapon CurrentWeaponObject { get; set; }
        public List<IWeapon> Weapons { get; set; }
        public RectangleF Boundary { get; set; }
        public sbyte CurrentWeapon { get; set; }
        public ushort GrenadesCount { get; set; }
        public ushort MaxHealth { get; set; }
        public ushort maxShield { get; set; }
        public Vector2 MaxSpeed { get; set; }
        public Vector2 Speed { get; set; }
        public Vector2 WeaponOrigin { get { return new Vector2(Boundary.Origin.X, Boundary.Origin.Y) + new Vector2(0, -16); } }

        public void AccesWeapons()
        {
            WeaponsAvailable = true;
        }

        public void LeftVehicle()
        {
            RectangleF curretnBoundary = Boundary;

            //if ((RayBarrel.ToAngle() * (180 / Math.PI)) + 180 > 90 && (RayBarrel.ToAngle() * (180 / Math.PI)) + 180 < 270)
            //    Game1.mapLive.MapNpcs.Add(new PlayerMech(Boundary.Position, mechFacing.right, Velocity));
            //else
            CurretnObjectControl.LeftVehicle(this);

            CurretnObjectControl = new BuddyModule(this, curretnBoundary.Origin);

            InVehicle = false;
        }

        public void DestroyVehicle()
        {
            RectangleF curretnBoundary = Boundary;

            //if ((RayBarrel.ToAngle() * (180 / Math.PI)) + 180 > 90 && (RayBarrel.ToAngle() * (180 / Math.PI)) + 180 < 270)
            //    Game1.mapLive.MapNpcs.Add(new PlayerMech(Boundary.Position, mechFacing.right, Velocity));
            //else

            CurretnObjectControl = new BuddyModule(this, curretnBoundary.Origin);
            Resolver.VerticalPressure = false;
            Resolver.HorizontalPressure = false;

            InVehicle = false;
        }

        public void AddElectronics(byte amount)
        {
            Electronics += amount;
        }

        public void Addhealth(ushort amount)
        {
            PotentialHealth += amount;
            if (PotentialHealth > MaxHealth)
                PotentialHealth = MaxHealth;
        }

        public void AddShield(ushort amount)
        {
            PotentialShield += amount;
            if (PotentialShield > maxShield)
                PotentialShield = maxShield;
        }

        public void AddTissue(byte amount)
        {
            Tissue += amount;
        }

        public void Controling()
        {
            CurretnObjectControl.ControlPlayer(this);

            if (KeyboardInput.KeyboardStateNew.IsKeyDown(Game1.STP.ControlKeys["Grab"]) 
                && KeyboardInput.KeyboardStateOld.IsKeyUp(Game1.STP.ControlKeys["Grab"]))
            {
                bool justLeft = false;

                if (Carry != null)
                {
                    Carry.LetGo(Velocity);
                    Carry = null;
                }
                else
                {
                    if (Game1.PlayerInstance.InVehicle == true)
                    {
                        LeftVehicle();
                        justLeft = true;
                    }

                    foreach (Inpc npc in Game1.mapLive.MapNpcs.Reverse<Inpc>())
                    {
                        if (justLeft == false)
                        {
                            if (npc is Icarry && Game1.PlayerInstance.InVehicle == false)
                            {
                                if (CompareF.RectangleFVsRectangleF
                                    (Game1.PlayerInstance.Boundary, npc.Boundary) == true)
                                {
                                    Carry = (npc as Icarry);
                                    break;
                                }
                            }
                        }

                        if (InVehicle == false && justLeft == false)
                        {
                            if (npc is Ivehicle)
                            {
                                if (Carry == null)
                                {
                                    if (CompareF.RectangleFVsRectangleF
                                        (Game1.PlayerInstance.Boundary, npc.Boundary) == true)
                                    {
                                        curretnBoundary = Boundary;

                                        Game1.mapLive.MapNpcs.Remove(npc);
                                        if (npc is PlayerMech)
                                        {
                                            CurretnObjectControl = 
                                                new MechModule(this, npc.Boundary.Origin, (npc as Ivehicle).Health);
                                        }
                                        else if (npc is PlayerAircraft)
                                        {
                                            CurretnObjectControl = 
                                                new AircraftModule(this, npc.Boundary.Origin, (npc as Ivehicle).Health);
                                        }
                                        InVehicle = true;
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        public void TakeDamage(ushort amount)
        {
            CurretnObjectControl.TakeDamage(amount, this);
        }

        public void Draw()
        {
            Game1.SpriteBatchGlobal.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp, transformMatrix: Camera2DGame.GetViewMatrix());
            CurretnObjectControl.Draw(this);
            Game1.SpriteBatchGlobal.End();
        }

        public void DrawNormal()
        {
            Game1.SpriteBatchGlobal.Begin(SpriteSortMode.Immediate, BlendState.NonPremultiplied, SamplerState.LinearClamp, transformMatrix: Camera2DGame.GetViewMatrix());
            CurretnObjectControl.DrawNormal(this);
            Game1.SpriteBatchGlobal.End();
        }

        public void DrawLight()
        {
            CurretnObjectControl.DrawLight(this);
        }

        public void Kill()
        {
            Health = 0;
            PotentialHealth = 0;
            ControlsActive = false;
            Alive = false;
        }

        public void Aim(Vector2 realPos)
        {
        }

        public void NextWeapon()
        {
            CurrentWeapon++;
            if (CurrentWeapon == Weapons.Count)
                CurrentWeapon = 0;
            CurrentWeaponObject = Weapons[CurrentWeapon];
        }

        public void PreviousWeapon()
        {
            CurrentWeapon--;
            if (CurrentWeapon == -1)
                CurrentWeapon = (sbyte)(Weapons.Count - 1);
            CurrentWeaponObject = Weapons[CurrentWeapon];
        }

        public void Push(Vector2 velocity)
        {
            CurretnObjectControl.Push(this, velocity);
        }

        public void PutOnVest()
        {
            VestOn = true;
        }

        public void Update(Vector2 mousePos)
        {
            if (ControlsActive == true)
            {
                Controling();
            }

            CurretnObjectControl.PhysicsMove(this);

            if (MouseInput.MouseStateNew.LeftButton == ButtonState.Released)
            {
                (Weapons[2] as LaserGun).SetOff();
            }

            if (KeyboardInput.KeyboardStateOld.IsKeyUp(Keys.E) && KeyboardInput.KeyboardStateNew.IsKeyDown(Keys.E))
            {
                if (CurretnObjectControl is BuddyModule)
                {
                    foreach (ITriggers trgr in Game1.mapLive.mapTriggers)
                    {
                        if (CompareF.RectangleFVsRectangleF(Boundary, trgr.Boundary))
                        {
                            trgr.TriggerSwitch();
                        }
                    }

                    foreach (ICrate crt in Game1.mapLive.mapCrates.Reverse<ICrate>())
                    {
                        if (CompareF.RectangleFVsRectangleF(Boundary, crt.Boundary))
                        {
                            crt.Open();
                        }
                    }
                }
            }

            UpdateHealth();
            UpdateShield();

            CurretnObjectControl.HowILookCalc(this);

            if (Carry != null)
                Carry.Carry(Boundary.Origin);

            if ((CurretnObjectControl is BuddyModule) && (Resolver.VerticalPressure == true || Resolver.HorizontalPressure == true))
            {
                Kill();
            }
        }

        public void UpdateHealth()
        {
            if (Health < PotentialHealth)
            {
                Health += Game1.Delta / 100;
            }
            else
            {
                Health = PotentialHealth;
            }
        }

        public void UpdateShield()
        {
            if (Shield < PotentialShield)
            {
                Shield += Game1.Delta / 100;
            }
            else
            {
                Shield = PotentialShield;
            }
        }

        public void UpdateWeapons(Vector2 realPos)
        {
            CurretnObjectControl.LineAim(realPos, this);
            CurretnObjectControl.ShootWeapons(this, realPos);

            foreach (IWeapon wpn in Weapons)
            {
                wpn.Update();
            }
        }
    }
}