using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using EFCore.Context;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace EFCore.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly DataContext _context;

        public TaskRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<List<TodoTask>> CreateTask(TodoTask task)
        {
            await _context.Tasks.AddAsync(task);
            _context.SaveChangesAsync();
             return await this.GetTasks();
        }

        public async Task<TodoTask> GetTaskById(int id)
        {
            return await _context.Tasks.FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<List<TodoTask>> GetTasks()
        {
            return await _context.Tasks.OrderBy(t => t.Id).ToListAsync();
        }

        
        public async Task<List<TodoTask>> UpdateTask(TodoTask task)
        {
            _context.Tasks.Update(task);
            await _context.SaveChangesAsync();
            return await this.GetTasks();
        }

        public async Task<List<TodoTask>> DeleteTask(int id)
        {
            var task = await this.GetTaskById(id);
            if(task == null) {
                throw new Exception("Task is not found!");    
            }
            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();
            return await this.GetTasks();
        }

        public async Task<List<TodoTask>> GetTasksPagination()
        {
            return await GetTasks();
        }
    }
}