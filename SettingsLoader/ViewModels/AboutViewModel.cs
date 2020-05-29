using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Diagnostics;
using System.IO;

namespace SettingsLoader.ViewModels
{
    public class AboutViewModel : Screen
    {
        public string Company { get; set; }
        public string Version { get; set; }
        public List<string> References { get; set; } = new List<string>();

        public AboutViewModel()
        {
        }

        protected override void OnInitialize()
        {
            base.OnInitialize();

            var assembly = Assembly.GetExecutingAssembly();
            var versionInfo = FileVersionInfo.GetVersionInfo(assembly.Location);

            Company = versionInfo.CompanyName;
            Version = versionInfo.FileVersion;

            var refs = assembly.GetReferencedAssemblies();

            foreach (var item in refs)
            {
                References.Add($"{item.Name} ({item.Version})");
            }
        }
    }
}
