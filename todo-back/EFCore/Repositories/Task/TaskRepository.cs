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

        public async Task<PagedResponse<TodoTask>> CreateTask(TodoTask task, PaginationParams paginationParams)
        {
            await _context.Tasks.AddAsync(task);
            _context.SaveChangesAsync();
             return await this.GetTasksPagination(paginationParams);
        }

        public async Task<TodoTask> GetTaskById(int id)
        {
            return await _context.Tasks.FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<List<TodoTask>> GetTasks()
        {
            return await _context.Tasks.OrderBy(t => t.Id).ToListAsync();
        }

        
        public async Task<PagedResponse<TodoTask>> UpdateTask(TodoTask task, PaginationParams paginationParams)
        {
            _context.Tasks.Update(task);
            await _context.SaveChangesAsync();
            return await this.GetTasksPagination(paginationParams);
        }

        public async Task<PagedResponse<TodoTask>> DeleteTask(int id, PaginationParams paginationParams)
        {
            var task = await this.GetTaskById(id);
            if(task == null) {
                throw new Exception("Task is not found!");    
            }
            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();
            return await this.GetTasksPagination(paginationParams);
        }

        public async Task<PagedResponse<TodoTask>> GetTasksPagination(PaginationParams paginationParams)
        {
            var query = _context.Tasks.OrderBy(t => t.Id).AsQueryable();
            // var tasks = query.ProjectTo<TaskDto>(_mapper.ConfigurationProvider).AsNoTracking();
            return await PagedResponse<TodoTask>.CreateAsync(query, paginationParams.PageNumber, paginationParams.PageSize);
        }
    }
}