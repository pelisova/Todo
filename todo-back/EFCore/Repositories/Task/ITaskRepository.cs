using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.DTOs.task;
using Core.Entities;
using EFCore.Pagination;

namespace EFCore.Repositories
{
    public interface ITaskRepository
    {
        Task<PagedResponse<TodoTask>> CreateTask(int userId, TodoTask task, PaginationParams paginationParams);
        Task<List<TodoTask>> GetTasks(int userId);
        Task<PagedResponse<TodoTask>> GetTasksPagination(int userId, PaginationParams paginationParams);
        Task<TodoTask> GetTaskById(int id);
        Task<PagedResponse<TodoTask>> UpdateTask(int userId, TodoTask task, PaginationParams paginationParams);
        Task<PagedResponse<TodoTask>> DeleteTask(string taskId, int userId, PaginationParams paginationParams);
    }
}