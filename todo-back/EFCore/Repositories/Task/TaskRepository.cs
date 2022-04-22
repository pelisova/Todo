using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Core.DTOs.task;
using Core.Entities;
using EFCore.Context;
using EFCore.Pagination;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace EFCore.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public TaskRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PagedResponse<TodoTask>> CreateTask(int userId, TodoTask task, PaginationParams paginationParams)
        {
            await _context.Tasks.AddAsync(task);
            _context.SaveChangesAsync();
            return await this.GetTasksPagination(userId, paginationParams);
        }

        public async Task<TodoTask> GetTaskById(int id)
        {
            return await _context.Tasks.FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<List<TodoTask>> GetTasks(int userId)
        {
            return await _context.Tasks.Where(u => u.UserId == userId).OrderBy(t => t.Id).ToListAsync();
        }


        public async Task<PagedResponse<TodoTask>> UpdateTask(int userId, TodoTask task, PaginationParams paginationParams)
        {
            _context.Tasks.Update(task);
            await _context.SaveChangesAsync();
            return await this.GetTasksPagination(userId, paginationParams);
        }

        public async Task<PagedResponse<TodoTask>> DeleteTask(string taskId, int userId, PaginationParams paginationParams)
        {
            var task = await this.GetTaskById(Int32.Parse(taskId));
            if (task.UserId != userId) throw new Exception("You don't have permission for this action!");
            if (task == null) throw new Exception("Task is not found!");

            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();
            return await this.GetTasksPagination(userId, paginationParams);
        }

        public async Task<PagedResponse<TodoTask>> GetTasksPagination(int userId, PaginationParams paginationParams)
        {
            var query = _context.Tasks.OrderBy(t => t.Id).Where(u => u.UserId == userId).AsQueryable();
            // var tasks = query.ProjectTo<TaskDto>(_mapper.ConfigurationProvider).AsNoTracking();
            return await PagedResponse<TodoTask>.CreateAsync(query, paginationParams.PageNumber, paginationParams.PageSize);
        }
    }
}