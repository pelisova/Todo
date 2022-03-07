using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Core.DTOs.task;
using Core.Entities;
using EFCore.Pagination;
using EFCore.Repositories;

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

        public async Task<PagedResponse<TaskDto>> CreateTask(CreateTaskDto createTaskDto, PaginationParams paginationParams)
        {
            var task = _mapper.Map<TodoTask>(createTaskDto);
            return _mapper.Map<PagedResponse<TaskDto>>(await _taskRepository.CreateTask(task, paginationParams));
        }

        public async Task<TaskDto> GetTaskById(int id)
        {
            return _mapper.Map<TaskDto>(await _taskRepository.GetTaskById(id));
        }

        public async Task<List<TaskDto>> GetTasks()
        {
            return _mapper.Map<List<TaskDto>>(await _taskRepository.GetTasks());
        }

        public async Task<PagedResponse<TaskDto>> UpdateTask(int id, UpdateTaskDto updateTaskDto, PaginationParams paginationParams)
        {
            var task = await _taskRepository.GetTaskById(id);
            var user = await _userRepository.GetUserById(updateTaskDto.UserId);
            if(user.UserId != task.UserId) throw new Exception("You do not have access for this resource!");
            var taskToUpdate = _mapper.Map<UpdateTaskDto, TodoTask>(updateTaskDto, task);
            return _mapper.Map<PagedResponse<TaskDto>>(await _taskRepository.UpdateTask(taskToUpdate, paginationParams));
        }

        public async Task<PagedResponse<TaskDto>> DeleteTask(int id, PaginationParams paginationParams)
        {
            try
            {
               return _mapper.Map<PagedResponse<TaskDto>>(await _taskRepository.DeleteTask(id, paginationParams));
            }
            catch (System.Exception ex)
            {
                
                throw ex;
            }   
        }

        public async Task<PagedResponse<TaskDto>> GetTasksPagination(PaginationParams paginationParams)
        {
            return _mapper.Map<PagedResponse<TaskDto>>(await _taskRepository.GetTasksPagination(paginationParams));
        }
    }
}