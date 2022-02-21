using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Core.DTOs.task;
using Core.Entities;

namespace BusinessLayer.Mappings
{
    public class MappingTasks : Profile
    {
        public MappingTasks()
        {
            CreateMap<CreateTaskDto, TodoTask>();
            CreateMap<TodoTask, TaskDto>();
            CreateMap<UpdateTaskDto, TodoTask>();
        }
    }
}