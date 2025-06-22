using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace SmartBiz.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddIdToFinRecord : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_FinancialRecords",
                table: "FinancialRecords");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "FinancialRecords",
                type: "integer",
                nullable: false,
                defaultValue: 0)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_FinancialRecords",
                table: "FinancialRecords",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_FinancialRecords",
                table: "FinancialRecords");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "FinancialRecords");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FinancialRecords",
                table: "FinancialRecords",
                column: "Data");
        }
    }
}
