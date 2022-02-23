using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class TodoTask
    {
        public int Id { get; set; }
        [Required]
        public string? Text { get; set; }
        public bool Completed { get; set; }
        public int UserId {get; set;}
        public User? User {get; set;}
    }
}