using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SettingsLoader.Services
{
    public class JsonFileService<T>
    {
        private readonly T _data;
        private readonly string _path;

        public JsonFileService(T data, string path)
        {
            if (data == null)
            {
                throw new ArgumentNullException(nameof(data));
            }
            _data = data;
            _path = path ?? throw new ArgumentNullException(nameof(path));
        }

        public T LoadData()
        {
            var file = new FileInfo(_path);

            if (!file.Exists)
            {
                throw new FileNotFoundException();
            }

            using (StreamReader sr = File.OpenText(_path))
            {
                var fileText = sr.ReadToEnd();

                if (file.Length == 0)
                {
                    throw new FileFormatException("File is empty.");
                }

                return JsonConvert.DeserializeObject<T>(fileText);
            }
        }

        public void SaveData(T data)
        {
            using (StreamWriter sw = File.CreateText(_path))
            {
                string output = JsonConvert.SerializeObject(data, Formatting.Indented);
                sw.Write(output);
            }
        }
    }
}
