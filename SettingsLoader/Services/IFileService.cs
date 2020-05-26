namespace SettingsLoader.Services
{
    public interface IFileService<T>
    {
        T LoadData(string path);
        void SaveData(string path, T data);
    }
}