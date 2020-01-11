namespace Zetta.Generics
{
    public interface ISaveable
    {
        void Load(string path);

        void Save(string path);
    }
}