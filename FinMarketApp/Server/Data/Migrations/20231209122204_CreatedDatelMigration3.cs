using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinMarketApp.Server.Data.Migrations
{
    /// <inheritdoc />
    public partial class CreatedDatelMigration3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateCreated",
                table: "Expenses");

            migrationBuilder.DropColumn(
                name: "DateCreated",
                table: "BudgetGoals");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DateCreated",
                table: "Expenses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DateCreated",
                table: "BudgetGoals",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
