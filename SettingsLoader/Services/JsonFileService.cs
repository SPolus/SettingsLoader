using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SettingsLoader.Services
{
    public class JsonFileService<T> : IFileService<T>
    {
        public IEnumerable<T> LoadData(string path)
        {
            var file = new FileInfo(path);

            if (!file.Exists)
            {
                throw new FileNotFoundException();
            }

            using (StreamReader sr = File.OpenText(path))
            {
                var fileText = sr.ReadToEnd();

                if (file.Length == 0)
                {
                    throw new FileFormatException("File is empty.");
                }

                return JsonConvert.DeserializeObject<IEnumerable<T>>(fileText);
            }
        }

        
        public void SaveData(string path, IEnumerable<T> data)
        {
            string dir = Path.GetDirectoryName(path);

            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }

            using (StreamWriter sw = File.CreateText(path))
            {
                string output = JsonConvert.SerializeObject(data, Formatting.Indented);
                sw.Write(output);
            }
        }
    }
}
