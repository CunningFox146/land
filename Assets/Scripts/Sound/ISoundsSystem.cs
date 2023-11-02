namespace DefaultNamespace.Sound
{
    public interface ISoundsSystem
    {
        void PlaySound(string soundName);
        void StopSound(string soundName);
        void SetVolume(string soundName, float volume);
        void SetPitch(string soundName, float pitch);
    }
}