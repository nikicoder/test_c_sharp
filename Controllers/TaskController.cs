using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TasksBoard.Services;
using TasksBoard.Models;
using TasksBoard.Heplers;

namespace TasksBoard.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ITasksService _tasksService;

        public TaskController (ITasksService tasksService)
        {
            _tasksService = tasksService;
        }

        [HttpGet]
        public async Task<IEnumerable<TaskModel>> Get()
        {
            var result = await _tasksService.GetActualTasks();

            return result;
        }

        [HttpGet("tree")]
        public async Task<IEnumerable<TasksTreeItem<TaskModel>>> GetTree()
        {
            var tasks = await _tasksService.GetActualTasks();

            return tasks.GenerateTree(c => c.Id, c => c.ParentTaskId);
        }

        // GET single task data
        [HttpGet("{id}")]
        public async Task<ActionResult<TaskModel>> GetTask(int id)
        {
            var task = await _tasksService.GetTask(id);

            if (task == null)
            {
                return NotFound();
            }

            return task;
        }

        [HttpPost]
        public async Task<ActionResult<TaskModel>> InsertTask(TaskModel task)
        {
            return await _tasksService.AddTask(task);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<TaskModel>> UpdateTask(int id, TaskModel task)
        {
            return await _tasksService.UpdateTask(id, task);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteTask(int id)
        {
            var result = await _tasksService.DeleteTask(id);

            return result;
        }
    }
}