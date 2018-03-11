using Microsoft.Xna.Framework;

namespace Monogame_GL
{
    public interface IPlayer
    {
        float MaxHealth { get; set; }

        float HealthMachine { get; set; }

        void LeftVehicle(Player player);

        void Push(Player player, Vector2 velocity);

        void HowILookCalc(Player player);

        void PhysicsMove(Player player);

        void LineAim(Vector2 realPos, Player player);

        void ShootWeapons(Player player, Vector2 realPos);

        void ThrowGrenade(Player player);

        void DrawNormal(Player player);

        void Draw(Player player);

        void TakeDamage(ushort amount, Player player);

        void ControlPlayer(Player player);

        void DrawLight(Player player);
    }
}