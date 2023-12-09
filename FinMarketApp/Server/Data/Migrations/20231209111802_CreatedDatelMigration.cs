using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinMarketApp.Server.Data.Migrations
{
    /// <inheritdoc />
    public partial class CreatedDatelMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Date",
                table: "Expenses");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateCreated",
                table: "Expenses",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DateCreated",
                table: "BudgetGoals",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateCreated",
                table: "Expenses");

            migrationBuilder.DropColumn(
                name: "DateCreated",
                table: "BudgetGoals");

            migrationBuilder.AddColumn<string>(
                name: "Date",
                table: "Expenses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
