using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TasksBoard.Models
{
    [Table("Tasks")]
    public class TaskModel
    {
        [Key]
        public int Id { get; set; }

        public int ParentTaskId { get; set; }

        [Required(ErrorMessage = "Task name is required")]
        [StringLength(600, MinimumLength = 10, ErrorMessage = "Task name lenght from 10 to 600 symbols")]
        public string TaskName { get; set; }

        [Required(ErrorMessage = "Task content is required")]
        [StringLength(6000, MinimumLength = 10, ErrorMessage = "Task content must be filled")]
        public string TaskContent { get; set; }

        public string EmployeesList { get; set; }


        [DataType(DataType.Date)]
        public DateTime DateAdd { get; set; }

        public DateTime DateFinish { get; set; }

        [Required(ErrorMessage = "Plan hours is required")]
        public decimal PlanHours { get; set; }

        public decimal TotalHours { get; set; }

        [Required(ErrorMessage = "State is required")]
        public int State { get; set; }
    }
}