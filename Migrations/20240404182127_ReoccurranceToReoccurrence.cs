using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PersonalFinances.Migrations
{
    /// <inheritdoc />
    public partial class ReoccurranceToReoccurrence : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Reoccurrance",
                table: "Transactions",
                newName: "Reoccurrence");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Reoccurrence",
                table: "Transactions",
                newName: "Reoccurrance");
        }
    }
}
