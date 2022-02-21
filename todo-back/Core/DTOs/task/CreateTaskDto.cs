using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.DTOs.task
{
    public class CreateTaskDto
    {
        public string? Text { get; set; }
        public int UserId {get; set;}
        
    }
}