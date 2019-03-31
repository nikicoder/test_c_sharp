using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TasksBoard.Models;

namespace TasksBoard.Services
{
    public class TasksService : ITasksService
    {
        private readonly DatabaseContext _context;
        private readonly ITaskStatusModel _taskStatuses;

        public TasksService (DatabaseContext context)
        {
            _context = context;
            _taskStatuses = new TaskStatusModel();
        }

        public async Task<IEnumerable<TaskModel>> GetActualTasks()
        {
            var tasks = await _context.Tasks.Where(p => p.State >= 0).ToListAsync();

            return tasks;
        }

        public async Task<TaskModel> GetTask(int id)
        {
            var task = await _context.Tasks.FindAsync(id);

            return task;
        }

        public async Task<TaskModel> AddTask(TaskModel task)
        {
            task.DateAdd = DateTime.UtcNow;
            // hardcode to assigned state
            task.State = _taskStatuses.statusAssigned;

            _context.Tasks.Add(task);
            var result = await _context.SaveChangesAsync();

            if(result != 1) {
                // something wrong
            }

            return await GetTask(task.Id);
        }

        public async Task<TaskModel> UpdateTask(int id, TaskModel task)
        {
            var updateTask = _context.Tasks.Find(id);

            if(updateTask == null) {
                // exception
            }

            // Update only allowed props
            updateTask.TaskName = task.TaskName;
            updateTask.TaskContent = task.TaskContent;
            updateTask.ParentTaskId = task.ParentTaskId;
            updateTask.PlanHours = task.PlanHours;

            _context.Entry(updateTask).State = EntityState.Modified;
            _context.SaveChanges();

            return await GetTask(id);
        }

        public async Task<bool> DeleteTask(int id)
        {
            var deleteTask = await _context.Tasks.FindAsync(id);

            if(deleteTask == null) {
                // exception
            }

            deleteTask.State = _taskStatuses.statusDeleted;
            _context.Entry(deleteTask).State = EntityState.Modified;
            var result = await _context.SaveChangesAsync();

            if(result != 1) {
                // exception
            }

            return true;
        }

        public async Task<bool> ChangeTaskStatus(int id, int status)
        {
            var updateTask = await _context.Tasks.FindAsync(id);

            if(updateTask == null) {
                // exception
            }

            // Task status must be defined
            if(!_taskStatuses.isValidStatus(status)) {
                // exception
            }

            // Task change rules
            if(!_taskStatuses.allowChangeStatus(updateTask.State, status)) {
                // exception
            }

            // For closing task please use CloseTask() method
            if(status == _taskStatuses.statusCompleted) {
                // exception
            }

            updateTask.State = status;
            _context.Entry(updateTask).State = EntityState.Modified;
            var result = await _context.SaveChangesAsync();

            if(result != 1) {
                // exception
            }

            return true;
        }

        public async Task<bool> CloseTask(int id, float TotalHours)
        {
            var closeTask = await _context.Tasks.FindAsync(id);

            return true;
        }

        public async Task<IEnumerable<TaskModel>> getTree(int id)
        {
            var result = new List<TaskModel>();
            var stackIds = new List<int>();

            var rootTask = await _context.Tasks.FindAsync(id);
            
            if(rootTask == null) {
                // exception
            }

            result.Add(rootTask);

            // add element to scan for children
            stackIds.Add(rootTask.Id);
            
            int currentId;
            List<TaskModel> childItems;
            
            while(stackIds.Count() > 0) {
                currentId = stackIds.First();
                childItems = _context.Tasks.Where(p => p.ParentTaskId == currentId).ToList();
                
                if(childItems.Count() > 0) {
                    foreach(TaskModel item in childItems) {
                        stackIds.Add(item.Id);
                        result.Add(item);
                    }
                }

                stackIds.Remove(currentId);
            }

            return result;
        }
    }
}