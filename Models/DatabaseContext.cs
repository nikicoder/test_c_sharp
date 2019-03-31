using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace TasksBoard.Models
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext (DbContextOptions<DatabaseContext> options)
            : base(options)
        {
        }

        public DbSet<TasksBoard.Models.TaskModel> Tasks { get; set; }
    }
}