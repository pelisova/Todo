using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.DTOs.task
{
    public class UpdateTaskDto : CreateTaskDto
    {
        public int Id { get; set; }
        public bool Completed { get; set; }
    }
}