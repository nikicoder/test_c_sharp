using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TasksBoard.Models;

namespace TasksBoard.Services
{
    public interface ITasksService
    {
        Task<IEnumerable<TaskModel>> GetActualTasks();
        Task<TaskModel> GetTask(int id);
        Task<TaskModel> AddTask(TaskModel task);
        Task<TaskModel> UpdateTask(int id, TaskModel task);
        Task<bool> DeleteTask(int id);
        Task<bool> ChangeTaskStatus(int id, int status);
        Task<bool> CloseTask(int id, float TotalHours);
        Task<IEnumerable<TaskModel>> getTree(int id);
    }
}