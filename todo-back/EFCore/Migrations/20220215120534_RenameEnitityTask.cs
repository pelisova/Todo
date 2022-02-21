using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EFCore.Migrations
{
    public partial class RenameEnitityTask : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "text",
                table: "Tasks",
                newName: "Text");

            migrationBuilder.RenameColumn(
                name: "completed",
                table: "Tasks",
                newName: "Completed");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Text",
                table: "Tasks",
                newName: "text");

            migrationBuilder.RenameColumn(
                name: "Completed",
                table: "Tasks",
                newName: "completed");
        }
    }
}
