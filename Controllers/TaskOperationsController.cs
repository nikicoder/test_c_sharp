using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TasksBoard.Services;
using TasksBoard.Models;

namespace TasksBoard.Controllers
{
    [Route("api/task/manage")]
    [ApiController]
    public class TaskOperationsController : ControllerBase
    {
        private readonly ITasksService _tasksService;

        public TaskOperationsController (ITasksService tasksService)
        {
            _tasksService = tasksService;
        }

        // 
        [HttpPost]
        public async Task<ActionResult<bool>> GetStatusList(TaskOperationRequestModel request)
        {
            switch (request.Action)
            {
                case "changeStatus":
                    return await _tasksService.ChangeTaskStatus(request.TaskId, request.NewStatus);
                case "closeTask":
                    return await _tasksService.CloseTask(request.TaskId, request.TotalHours);
                default:
                    return BadRequest("Undefined action");
            }
        }
    }
}