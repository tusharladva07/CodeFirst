using Microsoft.EntityFrameworkCore.Migrations;

namespace CodeFirst.Migrations
{
    public partial class RemoveISBNToAuthor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ISBN",
                table: "Author");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ISBN",
                table: "Author",
                nullable: true);
        }
    }
}
