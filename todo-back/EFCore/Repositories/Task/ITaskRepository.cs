using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.DTOs.task;
using Core.Entities;

namespace EFCore.Repositories
{
    public interface ITaskRepository
    {
        Task<TodoTask> CreateTask(TodoTask task);
        Task<List<TodoTask>> GetTasks();
        Task<List<TodoTask>> GetTasksPagination();
        Task<TodoTask> GetTaskById(int id);
        Task<TodoTask> UpdateTask(TodoTask task);
        Task DeleteTask(int id);
    }
}