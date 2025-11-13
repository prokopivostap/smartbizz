using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartBiz.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddSumForPurpose : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Sum",
                table: "FinancialRecords",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Sum",
                table: "FinancialRecords");
        }
    }
}
