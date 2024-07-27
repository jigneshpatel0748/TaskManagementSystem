using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagementSystem.Data.Base
{
    public class AppSettings
    {
        public string? DbTimeoutInSecond { get; set; }
        public string Environment { get; set; } = String.Empty;
        public string Secret { get; set; } = String.Empty;
        public string DatabaseString { get; set; } = String.Empty;
    }
}
