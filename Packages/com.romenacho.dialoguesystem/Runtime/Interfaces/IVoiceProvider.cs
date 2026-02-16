namespace DialogSystem.Interfaces
{
    public interface IVoiceProvider
    {
        void Play(string lineId);
        bool HasVoice(string lineId);
        void Stop();
    }
}
