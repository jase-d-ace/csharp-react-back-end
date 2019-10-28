using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace helloapi.Migrations
{
    public partial class remove_movie_column : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "stars",
                table: "movies");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string[]>(
                name: "stars",
                table: "movies",
                nullable: true);
        }
    }
}
