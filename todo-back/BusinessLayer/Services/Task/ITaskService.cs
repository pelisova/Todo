using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLayer.Helpers;
using Core.DTOs.task;
using EFCore.Pagination;

namespace BusinessLayer.Services
{
    public interface ITaskService
    {
        Task<PagedList<TaskDto>> CreateTask(CreateTaskDto createTaskDto, PaginationParams paginationParams);
        Task<List<TaskDto>> GetTasks();
        Task<PagedListRepo<TaskDto>> GetTasksPagination(PaginationParamsRepo paginationParams);
        Task<TaskDto> GetTaskById(int id);
        Task<PagedList<TaskDto>> UpdateTask(int id, UpdateTaskDto updateTaskDto, PaginationParams paginationParams);
        Task<PagedList<TaskDto>> DeleteTask(int id, PaginationParams paginationParams);
    }
}