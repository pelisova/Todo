using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Messages;
using BusinessLayer.Services;
using Core.DTOs.task;
using EFCore.Pagination;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace API.Controllers
{
    public class TaskController : BaseApiController
    {
        private readonly ITaskService _taskService;

        public TaskController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpPost]
        public async Task<ActionResult<PagedResponse<TaskDto>>> CreateTask([FromBody] CreateTaskDto createTaskDto, [FromQuery] PaginationParams paginationParams)
        {
            var tasks = await _taskService.CreateTask(createTaskDto, paginationParams);
            Response.AddPaginationHeader(tasks.PaginationResult.CurrentPage, tasks.PaginationResult.PageSize, tasks.PaginationResult.TotalCount, tasks.PaginationResult.TotalPages);
            return (!tasks.Items.Any()) ? NotFound() : Ok(new ResponseMessageModel<PagedResponse<TaskDto>>("Task is successfully created!", tasks));
        }

        [HttpGet]
        public async Task<ActionResult<List<TaskDto>>> GetTasks()
        {
            var tasks = await _taskService.GetTasks();
            return (!tasks.Any()) ? NotFound("Tasks are not found!") : Ok(tasks);
        } 

        [HttpGet("paginate")]
        public async Task<ActionResult<PagedResponse<TaskDto>>> GetTasksPagination([FromQuery] PaginationParams paginationParams)
        {
            var tasks = await _taskService.GetTasksPagination(paginationParams);
            Response.AddPaginationHeader(tasks.PaginationResult.CurrentPage, tasks.PaginationResult.PageSize, tasks.PaginationResult.TotalCount, tasks.PaginationResult.TotalPages);
            return (!tasks.Items.Any()) ? NotFound("Tasks are not found!") : Ok(new ResponseMessageModel<PagedResponse<TaskDto>>("Ok", tasks));
        } 

        [HttpGet("{id}")]
        public async Task<ActionResult<TaskDto>> GetTaskById(int id)
        {
            var task = await _taskService.GetTaskById(id);
            return (task == null) ? NotFound("Task is not found!") : Ok(task);
        } 

        [HttpPatch("{id}")]
        public async Task<ActionResult<PagedResponse<TaskDto>>> UpdateTask(int id, [FromBody] UpdateTaskDto updateTaskDto, [FromQuery] PaginationParams paginationParams)
        {
            if(id != updateTaskDto.Id) return BadRequest("Invalid task to update!");

            try
            {
                // Console.Write(JsonConvert.SerializeObject(updateTaskDto));
                var tasks = await _taskService.UpdateTask(id, updateTaskDto, paginationParams);
                Response.AddPaginationHeader(tasks.PaginationResult.CurrentPage, tasks.PaginationResult.PageSize, tasks.PaginationResult.TotalCount, tasks.PaginationResult.TotalPages);
                return (!tasks.Items.Any()) ? NotFound("Oops! Task is not found!") :Ok(new ResponseMessageModel<PagedResponse<TaskDto>>("Ok", tasks));
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            } 
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<PagedResponse<TaskDto>>> DeleteTask(int id, [FromQuery] PaginationParams paginationParams)
        {
            try
            {
                var tasks = await _taskService.DeleteTask(id, paginationParams);
                Response.AddPaginationHeader(tasks.PaginationResult.CurrentPage, tasks.PaginationResult.PageSize, tasks.PaginationResult.TotalCount, tasks.PaginationResult.TotalPages);
                return Accepted(new ResponseMessageModel<PagedResponse<TaskDto>>("Task is successfully deleted!", tasks));
            }
            catch (System.Exception ex)
            {
 
                return BadRequest(ex.Message);
            }
           
        }
    }
}