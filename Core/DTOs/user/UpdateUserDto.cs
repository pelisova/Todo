using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.DTOs
{
    public class UpdateUserDto : CreateUserDto
    {
           public int Id { get; set; }
    }
}