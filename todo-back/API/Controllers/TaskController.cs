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
        public async Task<ActionResult<List<TaskDto>>> CreateTask([FromBody] CreateTaskDto createTaskDto)
        {
            var tasks= await _taskService.CreateTask(createTaskDto);
            return (!tasks.Any()) ? NotFound() : Created("Task is successfully created", tasks);
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
                Console.Write(JsonConvert.SerializeObject(updateTaskDto));
                var newTasks = await _taskService.UpdateTask(id, updateTaskDto);
                return (!newTasks.Any()) ? NotFound("Oops! Task is not found!") : Created("Task is successfully updated", newTasks);   
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            } 
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<TaskDto>>> DeleteTask(int id)
        {
            try
            {
                var tasks = await _taskService.DeleteTask(id);
                return Accepted("Task is successfully deleted!", tasks);
            }
            catch (System.Exception ex)
            {
 
                return BadRequest(ex.Message);
            }
           
        }
    }
}