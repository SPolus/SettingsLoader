using System.Collections.Generic;

namespace SettingsLoader.Services
{
    public interface IFileService<T>
    {
        IEnumerable<T> LoadData(string path);
        void SaveData(string path, IEnumerable<T> data);
    }
}