using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartBiz.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddCurrencyToFinancialRecord : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Currency",
                table: "FinancialRecords",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Currency",
                table: "FinancialRecords");
        }
    }
}
