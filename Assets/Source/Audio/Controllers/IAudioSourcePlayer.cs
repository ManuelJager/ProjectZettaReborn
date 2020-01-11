namespace Zetta.Audio.Controllers
{
    public interface IAudioSourcePlayer
    {
        bool Prompt { get; }
        bool IsRunning { get; }
    }
}