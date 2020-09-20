namespace Zetta.Generics
{
    public delegate void LoadedDelegate();
    public delegate void SavedDelegate();

    public interface ISaveable
    {
        bool HasLoaded { get; }
        void Load(string path);
        void Save(string path);
        event LoadedDelegate Loaded;
        event SavedDelegate Saved;
    }
}