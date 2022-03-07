using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.DTOs.task;
using EFCore.Pagination;

namespace BusinessLayer.Services
{
    public interface ITaskService
    {
        Task<PagedResponse<TaskDto>> CreateTask(CreateTaskDto createTaskDto, PaginationParams paginationParams);
        Task<List<TaskDto>> GetTasks();
        Task<PagedResponse<TaskDto>> GetTasksPagination(PaginationParams paginationParams);
        Task<TaskDto> GetTaskById(int id);
        Task<PagedResponse<TaskDto>> UpdateTask(int id, UpdateTaskDto updateTaskDto, PaginationParams paginationParams);
        Task<PagedResponse<TaskDto>> DeleteTask(int id, PaginationParams paginationParams);
    }
}