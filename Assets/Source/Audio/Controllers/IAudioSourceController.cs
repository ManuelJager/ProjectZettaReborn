namespace Zetta.Audio.Controllers
{
    public interface IAudioSourceController : IAudioSourcePlayer
    {
        void Start();

        void Stop();
    }
}