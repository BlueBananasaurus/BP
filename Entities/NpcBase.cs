using Microsoft.Xna.Framework;

namespace Monogame_GL
{
    public abstract class NpcBase : PhysicEntity
    {
        public float Health { get; protected set; }
        public float Tint { get; protected set; }

        public void ApplyDamage(short projectile)
        {
            KineticDamage(projectile);
        }

        public NpcBase()
        {
            Tint = 1f;
        }

        public void KineticDamage(short damage)
        {
            if (damage > 0)
            {
                Health -= damage;
                Tint = 0f;
                PlayDamageSound();
            }
        }

        public void Stun()
        {
        }

        public void Kill()
        {
        }

        public virtual void DrawNormal()
        {

        }

        private void PlayDamageSound()
        {
            Sound.PlaySoundPosition(Boundary.Origin, Game1.soundDamage);
        }
    }
}