using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BusinessLayer.Helpers;
using Core.DTOs.task;
using Core.Entities;
using EFCore.Repositories;
using Newtonsoft.Json;

namespace BusinessLayer.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public TaskService(ITaskRepository taskRepository, IUserRepository userRepository, IMapper mapper)
        {
            _taskRepository = taskRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<List<TaskDto>> CreateTask(CreateTaskDto createTaskDto)
        {
            return _mapper.Map<List<TaskDto>>(await _taskRepository.CreateTask(_mapper.Map<TodoTask>(createTaskDto)));
        }

        public async Task<TaskDto> GetTaskById(int id)
        {
            return _mapper.Map<TaskDto>(await _taskRepository.GetTaskById(id));
        }

        public async Task<List<TaskDto>> GetTasks()
        {
            return _mapper.Map<List<TaskDto>>(await _taskRepository.GetTasks());
        }

        public async Task<List<TaskDto>> UpdateTask(int id, UpdateTaskDto updateTaskDto)
        {
            Console.WriteLine("Heloooo service!!");
            var task = await _taskRepository.GetTaskById(id);
            var user = await _userRepository.GetUserById(updateTaskDto.UserId);
            if(user.UserId != task.UserId) throw new Exception("You do not have access for this resource!");
            var taskToUpdate = _mapper.Map<UpdateTaskDto, TodoTask>(updateTaskDto, task);
            return _mapper.Map<List<TaskDto>>(await _taskRepository.UpdateTask(taskToUpdate));
        }

        public async Task<List<TaskDto>> DeleteTask(int id)
        {
            try
            {
               return _mapper.Map<List<TaskDto>>(await _taskRepository.DeleteTask(id));
            }
            catch (System.Exception ex)
            {
                
                throw ex;
            }   
        }

        public async Task<PagedList<TaskDto>> GetTasksPagination(PaginationParams paginationParams)
        {
            var tasks = (_mapper.Map<List<TaskDto>>(await _taskRepository.GetTasksPagination())).AsQueryable();
            return PagedList<TaskDto>.CreateAsync(tasks, paginationParams.PageNumber, paginationParams.PageSize);
        }
    }
}