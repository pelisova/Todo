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
        Task<PagedResponse<TaskDto>> CreateTask(int userId, CreateTaskDto createTaskDto, PaginationParams paginationParams);
        Task<List<TaskDto>> GetTasks();
        Task<PagedResponse<TaskDto>> GetTasksPagination(int userId, PaginationParams paginationParams);
        Task<TaskDto> GetTaskById(int id);
        Task<PagedResponse<TaskDto>> UpdateTask(int userId, UpdateTaskDto updateTaskDto, PaginationParams paginationParams);
        Task<PagedResponse<TaskDto>> DeleteTask(string taskId, int userId, PaginationParams paginationParams);
    }
}