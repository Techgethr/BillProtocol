using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BillProtocol.Data.Migrations
{
    public partial class CreateLedgerSequenceColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "LedgerSequence",
                table: "Invoices",
                type: "bigint",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LedgerSequence",
                table: "Invoices");
        }
    }
}
