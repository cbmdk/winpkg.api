using System;
using System.Text.Json.Serialization;

namespace winstall.core
{
    public class WinPkg
    {
        public DateTime DateCreated { get; set; } = DateTime.Now;
        public string PackageId { get; set; } = Guid.NewGuid().ToString();
        [JsonPropertyName("Id")]
        public string  Pub { get; set; }
        public string  Version { get; set; }
        public string  Name { get; set; }
        public string Publisher { get; set; }
        public string AppMoniker { get; set; }
        //public Tag Tags { get; set; }
        public string License { get; set; }
        public string  Homepage { get; set; }
        public string LicenseUrl { get; set; }
        public string Description { get; set; }
        //public InstallData Installers { get; set; }

    }
}