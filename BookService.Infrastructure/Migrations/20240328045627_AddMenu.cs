using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddMenu : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndWorkTimeUtc",
                table: "Restaurants");

            migrationBuilder.DropColumn(
                name: "StartWorkTimeUtc",
                table: "Restaurants");

            migrationBuilder.AddColumn<string>(
                name: "EndWorkTimeLocal",
                table: "Restaurants",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "MenuItems",
                table: "Restaurants",
                type: "jsonb",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StartWorkTimeLocal",
                table: "Restaurants",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndWorkTimeLocal",
                table: "Restaurants");

            migrationBuilder.DropColumn(
                name: "MenuItems",
                table: "Restaurants");

            migrationBuilder.DropColumn(
                name: "StartWorkTimeLocal",
                table: "Restaurants");

            migrationBuilder.AddColumn<TimeSpan>(
                name: "EndWorkTimeUtc",
                table: "Restaurants",
                type: "interval",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<TimeSpan>(
                name: "StartWorkTimeUtc",
                table: "Restaurants",
                type: "interval",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));
        }
    }
}
