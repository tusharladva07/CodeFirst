using Microsoft.EntityFrameworkCore.Migrations;

namespace CodeFirst.Migrations
{
    public partial class RemoveMobilenoToAuthor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MobileNo",
                table: "Author");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MobileNo",
                table: "Author",
                nullable: true);
        }
    }
}
