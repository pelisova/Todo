using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLayer.Helpers;
using Core.DTOs.task;

namespace BusinessLayer.Services
{
    public interface ITaskService
    {
        Task<List<TaskDto>> CreateTask(CreateTaskDto createTaskDto);
        Task<List<TaskDto>> GetTasks();
        Task<PagedList<TaskDto>> GetTasksPagination(PaginationParams paginationParams);
        Task<TaskDto> GetTaskById(int id);
        Task<List<TaskDto>> UpdateTask(int id, UpdateTaskDto updateTaskDto);
        Task<List<TaskDto>> DeleteTask(int id);
    }
}