using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CodeFirst.Migrations
{
    public partial class AddPublishedDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "PublishedDate",
                table: "Book",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PublishedDate",
                table: "Book");
        }
    }
}
