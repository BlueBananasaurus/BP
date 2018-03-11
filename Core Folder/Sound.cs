using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;

namespace Monogame_GL
{
    public class Sound
    {
        private SoundEffectInstance soundEffectInstance { get; set; }
        private Vector2? _position;
        private static float _maxPan = 0.04f;
        public bool IsNew { get; private set; }
        public float VolumeOveral;

        public bool Stopped
        {
            get
            {
                if (soundEffectInstance.State != SoundState.Playing)
                    return true;
                return false;
            }
        }

        public Sound(SoundEffect effect, Vector2 soundPos,float volumeOveral)
        {
            soundEffectInstance = effect.CreateInstance();
            _position = soundPos;
            IsNew = true;
            VolumeOveral = volumeOveral;
        }

        public Sound(SoundEffect effect, Vector2 soundPos)
        {
            soundEffectInstance = effect.CreateInstance();
            _position = soundPos;
            IsNew = true;
            VolumeOveral = 1f;
        }

        public Sound(SoundEffect effect)
        {
            soundEffectInstance = effect.CreateInstance();
            _position = null;
            IsNew = true;
            VolumeOveral = 1f;
        }

        public void Play(float pitch = 0f)
        {
            soundEffectInstance.Play();
            soundEffectInstance.Pitch = pitch;
        }

        public void Stop()
        {
            soundEffectInstance.Stop();
        }

        public void Pause()
        {
            soundEffectInstance.Pause();
        }

        public void Change(float volume, float pan)
        {
            soundEffectInstance.Volume = volume;
            soundEffectInstance.Pan = pan;
        }

        public void Update(Vector2 listenerPos)
        {
            if (soundEffectInstance.State == SoundState.Playing)
            {
                if (_position != null)
                {
                    float pan = (_position.Value.X - listenerPos.X) / (2048f * 2);
                    float volume = 1f - LineSegmentF.Lenght(listenerPos, _position.Value) / 2048;

                    if (pan > _maxPan) pan = _maxPan;
                    if (pan < -_maxPan) pan = -_maxPan;

                    if (volume > 1f)
                        volume = 1f;
                    if (volume < 0)
                        volume = 0f;

                    Change(volume * MathHelper.Clamp(VolumeOveral, 0f, 1f), pan);
                }
            }

            IsNew = false;
        }

        public static void PlaySoundPositionVolume(Vector2 position, SoundEffect sound, float volume)
        {
            Sound toPlay = new Sound(sound, position, volume);

            if (Game1.Sounds3D.Count < 16)
            {
                Game1.Sounds3D.Add(toPlay);
                Game1.Sounds3D[Game1.Sounds3D.IndexOf(toPlay)].Play();
            }
        }

        public static void PlaySoundPosition(Vector2 position, SoundEffect sound, float pitch)
        {
            Sound toPlay = new Sound(sound, position);

            if (Game1.Sounds3D.Count < 16)
            {
                Game1.Sounds3D.Add(toPlay);
                Game1.Sounds3D[Game1.Sounds3D.IndexOf(toPlay)].Play();
                Game1.Sounds3D[Game1.Sounds3D.IndexOf(toPlay)].soundEffectInstance.Pitch = pitch;
            }
        }

        public static void PlaySoundPosition(Vector2 position, SoundEffect sound)
        {
            Sound toPlay = new Sound(sound, position);

            if (Game1.Sounds3D.Count < 16)
            {
                Game1.Sounds3D.Add(toPlay);
                Game1.Sounds3D[Game1.Sounds3D.IndexOf(toPlay)].Play();
            }
        }

        public static void PlaySound(SoundEffect sound, float pitch = 0f)
        {
            Sound toPlay = new Sound(sound);

            if (Game1.Sounds.Count < 16 && Game1.SoundsMain.Find(item => item.IsNew == true) == null)
            {
                Game1.SoundsMain.Add(toPlay);
                Game1.SoundsMain[Game1.SoundsMain.IndexOf(toPlay)].Play(pitch);
            }
        }

        public static void PlaySoundSimple(SoundEffect sound, float volume = 1, float pan = 0,float pitch = 0f)
        {
            try
            {
                sound.Play(volume, pan, pitch);
            }
            catch (NoAudioHardwareException ex)
            {

            }
        }
    }
}