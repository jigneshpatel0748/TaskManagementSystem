using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagementSystem.Dto.Response
{
    public class ApiResponse<T>
    {
        public bool IsSuccess { get; set; }

        public T Data { get; set; }

        public string Message { get; set; }
    }
}
