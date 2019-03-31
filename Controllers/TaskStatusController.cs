using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TasksBoard.Services;
using TasksBoard.Models;

namespace TasksBoard.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskStatusController : ControllerBase
    {
        private readonly ITasksService _tasksService;

        public TaskStatusController (ITasksService tasksService)
        {
            _tasksService = tasksService;
        }

        // Get actual task statuses
        [HttpGet]
        public List<TaskStatusStruct> GetStatusList()
        {
            return TaskStatusModel.getStatusList();
        }

        [HttpGet("{id}")]
        public Task<IEnumerable<TaskModel>> Check(int id)
        {
            return _tasksService.getTree(id);
        }
    }
}