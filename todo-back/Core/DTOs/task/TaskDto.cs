using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.DTOs.task
{
    public class TaskDto
    {
        public int Id { get; set; }
        public string? Text { get; set; }
        
        public bool Completed { get; set; }

        public int UserId {get; set;}
    }
}