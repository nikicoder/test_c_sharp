using System;

namespace TasksBoard.Models
{
    public class TaskOperationRequestModel
    {
        public int TaskId { get; set; }
        public string Action { get; set; }
        // New Status for change status action
        public int NewStatus { get; set; }
        // Total hours for close task action
        public float TotalHours { get; set; }
    }
}