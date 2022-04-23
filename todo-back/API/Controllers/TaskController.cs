using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Messages;
using BusinessLayer.Extensions;
using BusinessLayer.Services;
using Core.DTOs.task;
using Core.Entities;
using EFCore.Pagination;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace API.Controllers
{
    [Authorize]
    public class TaskController : BaseApiController
    {
        private readonly ITaskService _taskService;

        private readonly UserManager<User> _userManager;

        public TaskController(ITaskService taskService, UserManager<User> userManager)
        {
            _taskService = taskService;
            _userManager = userManager;
        }

        private async Task<int> GetUserId()
        {
            var email = User.GetUserEmail();
            var user = await _userManager.FindByEmailAsync(email);
            return user.Id;
        }

        [HttpPost]
        public async Task<ActionResult<PagedResponse<TaskDto>>> CreateTask([FromBody] CreateTaskDto createTaskDto, [FromQuery] PaginationParams paginationParams)
        {
            var userId = await this.GetUserId();
            try
            {
                var tasks = await _taskService.CreateTask(userId, createTaskDto, paginationParams);
                Response.AddPaginationHeader(tasks.PaginationResult.CurrentPage, tasks.PaginationResult.PageSize, tasks.PaginationResult.TotalCount, tasks.PaginationResult.TotalPages);
                return (!tasks.Items.Any()) ? NotFound() : Ok(new ResponseMessageModel<PagedResponse<TaskDto>>("Task is successfully created.", tasks));
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<List<TaskDto>>> GetTasks(int userId)
        {
            var tasks = await _taskService.GetTasks(userId);
            return (!tasks.Any()) ? NotFound("Tasks are not found!") : Ok(tasks);
        }

        [HttpGet("paginate")]
        public async Task<ActionResult<PagedResponse<TaskDto>>> GetTasksPagination([FromQuery] PaginationParams paginationParams)
        {
            var email = User.GetUserEmail();
            var user = await _userManager.FindByEmailAsync(email);

            var tasks = await _taskService.GetTasksPagination(user.Id, paginationParams);
            Response.AddPaginationHeader(tasks.PaginationResult.CurrentPage, tasks.PaginationResult.PageSize, tasks.PaginationResult.TotalCount, tasks.PaginationResult.TotalPages);
            return (!tasks.Items.Any()) ? NotFound("Tasks are not found!") : Ok(new ResponseMessageModel<PagedResponse<TaskDto>>("Ok", tasks));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TaskDto>> GetTaskById(int id)
        {
            var task = await _taskService.GetTaskById(id);
            return (task == null) ? NotFound("Task is not found!") : Ok(task);
        }

        [HttpPatch("{taskId}")]
        public async Task<ActionResult<PagedResponse<TaskDto>>> UpdateTask(string taskId, [FromBody] UpdateTaskDto updateTaskDto, [FromQuery] PaginationParams paginationParams)
        {
            var task_id = Int32.Parse(taskId);
            if (task_id != updateTaskDto.Id) return BadRequest("Invalid task to update!");
            var userId = await this.GetUserId();

            try
            {
                // Console.Write(JsonConvert.SerializeObject(updateTaskDto));
                var tasks = await _taskService.UpdateTask(userId, updateTaskDto, paginationParams);
                Response.AddPaginationHeader(tasks.PaginationResult.CurrentPage, tasks.PaginationResult.PageSize, tasks.PaginationResult.TotalCount, tasks.PaginationResult.TotalPages);
                return (!tasks.Items.Any()) ? NotFound("Oops! Task is not found!") : Ok(new ResponseMessageModel<PagedResponse<TaskDto>>("Task is updated.", tasks));
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpDelete("{taskId}")]
        public async Task<ActionResult<PagedResponse<TaskDto>>> DeleteTask(string taskId, [FromQuery] PaginationParams paginationParams)
        {
            var userId = await this.GetUserId();
            try
            {
                var tasks = await _taskService.DeleteTask(taskId, userId, paginationParams);
                Response.AddPaginationHeader(tasks.PaginationResult.CurrentPage, tasks.PaginationResult.PageSize, tasks.PaginationResult.TotalCount, tasks.PaginationResult.TotalPages);
                return Accepted(new ResponseMessageModel<PagedResponse<TaskDto>>("Task is successfully deleted.", tasks));
            }
            catch (System.Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }
    }
}