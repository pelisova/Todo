using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLayer.Helpers;
using BusinessLayer.Services;
using Core.DTOs.task;
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
        public async Task<ActionResult<TaskDto>> CreateTask([FromBody] CreateTaskDto createTaskDto)
        {
            var task = await _taskService.CreateTask(createTaskDto);
            return (task == null) ? NotFound() : Created("Task is successfully created", task);
        }

        [HttpGet]
        public async Task<ActionResult<List<TaskDto>>> GetTasks()
        {
            var tasks = await _taskService.GetTasks();
            return (!tasks.Any()) ? NotFound("Tasks are not found!") : Ok(tasks);
        } 

        [HttpGet("paginate")]
        public async Task<ActionResult<PagedList<TaskDto>>> GetTasksPagination([FromQuery] PaginationParams paginationParams)
        {
            var tasks = await _taskService.GetTasksPagination(paginationParams);
            Response.AddPaginationHeader(tasks.CurrentPage, tasks.PageSize, tasks.TotalCount, tasks.TotalPages);
            return (!tasks.Any()) ? NotFound("Tasks are not found!") : Ok(tasks);
        } 

        [HttpGet("{id}")]
        public async Task<ActionResult<TaskDto>> GetTaskById(int id)
        {
            var task = await _taskService.GetTaskById(id);
            return (task == null) ? NotFound("Task is not found!") : Ok(task);
        } 

        [HttpPatch("{id}")]
        public async Task<ActionResult<List<TaskDto>>> UpdateTask(int id, [FromBody] UpdateTaskDto updateTaskDto)
        {
            if(id != updateTaskDto.Id) return BadRequest("Invalid task to update!");

            try
            {
                // Console.Write(JsonConvert.SerializeObject(updateTaskDto));
                var newTask = await _taskService.UpdateTask(id, updateTaskDto);
                return (newTask == null) ? NotFound("Oops! Task is not found!") : Created("Task is successfully updated", newTask);   
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            } 
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTask(int id)
        {
            try
            {
                await _taskService.DeleteTask(id);
                return StatusCode(202, "Task is successfully deleted!");
            }
            catch (System.Exception ex)
            {
 
                return BadRequest(ex.Message);
            }
           
        }
    }
}