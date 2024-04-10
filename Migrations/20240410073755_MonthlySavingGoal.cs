using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PersonalFinances.Migrations
{
    /// <inheritdoc />
    public partial class MonthlySavingGoal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "MonthlySavingGoal",
                table: "Users",
                type: "decimal(65,30)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MonthlySavingGoal",
                table: "Users");
        }
    }
}
