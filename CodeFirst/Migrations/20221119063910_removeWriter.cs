using Microsoft.EntityFrameworkCore.Migrations;

namespace CodeFirst.Migrations
{
    public partial class removeWriter : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Writer",
                table: "Book");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Writer",
                table: "Book",
                nullable: true);
        }
    }
}
