using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TasksBoard.Migrations
{
    public partial class TasksTableAdd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tasks",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ParentTaskId = table.Column<int>(nullable: false),
                    TaskName = table.Column<string>(maxLength: 600, nullable: false),
                    TaskContent = table.Column<string>(maxLength: 6000, nullable: false),
                    EmployeesList = table.Column<string>(nullable: true),
                    DateAdd = table.Column<DateTime>(nullable: false),
                    DateFinish = table.Column<DateTime>(nullable: false),
                    PlanHours = table.Column<decimal>(nullable: false),
                    TotalHours = table.Column<decimal>(nullable: false),
                    State = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tasks", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tasks");
        }
    }
}
