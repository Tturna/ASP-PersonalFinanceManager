using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PersonalFinances.Migrations
{
    /// <inheritdoc />
    public partial class EuroFloatsToDecimals : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "AmountEuro",
                table: "Transactions",
                type: "decimal(65,30)",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "float");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<float>(
                name: "AmountEuro",
                table: "Transactions",
                type: "float",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(65,30)");
        }
    }
}
