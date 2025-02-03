namespace Assets.Scripts.Interfaces
{
    public interface IFileManager
    {
        void Save<T>(string FileName, T data) where T : class, new();
        T Load<T>(string FileName) where T : new();
    }
}
