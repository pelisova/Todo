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

        public async Task<PagedResponse<TaskDto>> CreateTask(int userId, CreateTaskDto createTaskDto, PaginationParams paginationParams)
        {
            if (userId != createTaskDto.UserId) throw new Exception("Unauthorized request!");
            var task = _mapper.Map<TodoTask>(createTaskDto);
            return _mapper.Map<PagedResponse<TaskDto>>(await _taskRepository.CreateTask(userId, task, paginationParams));
        }

        public async Task<TaskDto> GetTaskById(int id)
        {
            return _mapper.Map<TaskDto>(await _taskRepository.GetTaskById(id));
        }

        public async Task<List<TaskDto>> GetTasks(int userId)
        {
            return _mapper.Map<List<TaskDto>>(await _taskRepository.GetTasks(userId));
        }

        public async Task<PagedResponse<TaskDto>> UpdateTask(int userId, UpdateTaskDto updateTaskDto, PaginationParams paginationParams)
        {
            if (userId != updateTaskDto.UserId) throw new Exception("You do not have access for this resource!");
            var task = await _taskRepository.GetTaskById(updateTaskDto.Id);
            var taskToUpdate = _mapper.Map<UpdateTaskDto, TodoTask>(updateTaskDto, task);
            return _mapper.Map<PagedResponse<TaskDto>>(await _taskRepository.UpdateTask(userId, taskToUpdate, paginationParams));
        }

        public async Task<PagedResponse<TaskDto>> DeleteTask(string taskId, int userId, PaginationParams paginationParams)
        {
            try
            {
                return _mapper.Map<PagedResponse<TaskDto>>(await _taskRepository.DeleteTask(taskId, userId, paginationParams));
            }
            catch (System.Exception ex)
            {

                throw ex;
            }
        }

        public async Task<PagedResponse<TaskDto>> GetTasksPagination(int userId, PaginationParams paginationParams)
        {
            return _mapper.Map<PagedResponse<TaskDto>>(await _taskRepository.GetTasksPagination(userId, paginationParams));
        }
    }
}