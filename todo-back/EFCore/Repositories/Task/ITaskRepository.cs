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
        Task<List<TodoTask>> CreateTask(TodoTask task);
        Task<List<TodoTask>> GetTasks();
        Task<PagedListRepo<TaskDto>> GetTasksPagination(PaginationParamsRepo pagination);
        Task<TodoTask> GetTaskById(int id);
        Task<List<TodoTask>> UpdateTask(TodoTask task);
        Task<List<TodoTask>> DeleteTask(int id);
    }
}