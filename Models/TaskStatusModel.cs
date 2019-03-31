using System;
using System.Linq;
using System.Collections.Generic;

namespace TasksBoard.Models
{
    interface ITaskStatusModel
    {
        // Task only assigned to user
        int statusAssigned { get; }
        // User performs this task
        int statusInWork { get; }
        // Task has been suspended
        int statusSuspended { get; }
        // Task has been completed
        int statusCompleted { get; }
        // Deleted task
        int statusDeleted { get; }
        bool isValidStatus (int status);
        bool allowChangeStatus (int fromStatus, int toStatus);
        List<TaskStatusStruct> getStatusList();
    }

    public struct TaskStatusStruct {
        public int code;
        public string name;
    };

    public class TaskStatusModel : ITaskStatusModel
    {
        // Status gradation:
        // 1-9: reserved for new task statuses like: isNewTask or needMoreInfo.
        // Task in status 1-9 NOT assigned to worker
        // 10-19 - assigned to workers, but work does not started
        // 20-50 - tasks in work
        // 99 - task completed
        // 0 - task suspended
        // statuses < 0 for cancelled, deleted tasks
        private static readonly int 
            _statusAssigned = 10,
            _statusInWork = 20,
            _statusSuspended = 0,
            _statusCompleted = 99,
            _statusDeleted = -1;

        private static readonly List<TaskStatusStruct> _statusList = new List<TaskStatusStruct> {
            new TaskStatusStruct { code = _statusAssigned, name = "assigned"},
            new TaskStatusStruct { code = _statusInWork, name = "in work"},
            new TaskStatusStruct { code = _statusSuspended, name = "suspended"},
            new TaskStatusStruct { code = _statusCompleted, name = "completed"},
            new TaskStatusStruct { code = _statusDeleted, name = "deleted"}
        };

        public int statusAssigned { get { return _statusAssigned; } private set {} }
        public int statusInWork { get { return _statusInWork; } private set {} }
        public int statusSuspended { get { return _statusSuspended; } private set {} }
        public int statusCompleted { get { return _statusCompleted; } private set {} }
        public int statusDeleted { get { return _statusDeleted; } private set {} }

        public static bool isValidStatus (int status)
        {
            return (_statusList.Where(p => p.code == status).Count() > 0);
        }

        bool ITaskStatusModel.isValidStatus(int status)
        {
            return isValidStatus(status);
        }
        
        public bool allowChangeStatus (int fromStatus, int toStatus)
        {
            // Dropped, cancelled or deleted tasks has status < 0
            // Change status for this tasks not 
            if(fromStatus < 0) {
                return false;
            }

            // If task completed change status not allowed
            if(fromStatus == _statusCompleted) {
                return false;
            }

            // The complete status may be set only after in work status
            if(toStatus == _statusCompleted 
                && fromStatus != _statusInWork) {
                return false;
            }

            // The suspended status may be set only after in work status
            if(toStatus == _statusSuspended 
                && fromStatus != _statusInWork) {
                return false;
            }

            return true;
        }

        public static List<TaskStatusStruct> getStatusList()
        {
            return _statusList;
        }

        List<TaskStatusStruct> ITaskStatusModel.getStatusList()
        {
            return getStatusList();
        }
    }
}