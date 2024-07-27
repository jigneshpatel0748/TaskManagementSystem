using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagementSystem.Data.Enums
{
    public enum Status
    {
        Idle = 1,
        Running = 2,
        Pending = 3,
        Complete = 4,
        Cancel = 5
    }
}
