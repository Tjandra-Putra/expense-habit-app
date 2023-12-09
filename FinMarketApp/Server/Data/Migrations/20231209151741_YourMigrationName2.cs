using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinMarketApp.Server.Data.Migrations
{
    /// <inheritdoc />
    public partial class YourMigrationName2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BudgetGoals_AspNetUsers_ApplicationUserId",
                table: "BudgetGoals");

            migrationBuilder.AlterColumn<string>(
                name: "ApplicationUserId",
                table: "BudgetGoals",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_BudgetGoals_AspNetUsers_ApplicationUserId",
                table: "BudgetGoals",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BudgetGoals_AspNetUsers_ApplicationUserId",
                table: "BudgetGoals");

            migrationBuilder.AlterColumn<string>(
                name: "ApplicationUserId",
                table: "BudgetGoals",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_BudgetGoals_AspNetUsers_ApplicationUserId",
                table: "BudgetGoals",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
