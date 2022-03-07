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
        Task<PagedResponse<TodoTask>> CreateTask(TodoTask task, PaginationParams paginationParams);
        Task<List<TodoTask>> GetTasks();
        Task<PagedResponse<TodoTask>> GetTasksPagination(PaginationParams paginationParams);
        Task<TodoTask> GetTaskById(int id);
        Task<PagedResponse<TodoTask>> UpdateTask(TodoTask task, PaginationParams paginationParams);
        Task<PagedResponse<TodoTask>> DeleteTask(int id, PaginationParams paginationParams);
    }
}