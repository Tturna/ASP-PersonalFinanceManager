using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PersonalFinances.Migrations
{
    /// <inheritdoc />
    public partial class CentsToEuros : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AmountEuroCents",
                table: "Transactions");

            migrationBuilder.AddColumn<float>(
                name: "AmountEuro",
                table: "Transactions",
                type: "float",
                nullable: false,
                defaultValue: 0f);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AmountEuro",
                table: "Transactions");

            migrationBuilder.AddColumn<int>(
                name: "AmountEuroCents",
                table: "Transactions",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
