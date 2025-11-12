using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartBiz.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddPurposeToFinancialRecord : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Purpose",
                table: "FinancialRecords",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Purpose",
                table: "FinancialRecords");
        }
    }
}
