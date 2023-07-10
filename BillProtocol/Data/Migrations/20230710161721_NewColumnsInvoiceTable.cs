using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BillProtocol.Data.Migrations
{
    public partial class NewColumnsInvoiceTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "InvoiceStatusComments",
                table: "Invoices",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "InvoiceStatusId",
                table: "Invoices",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalAmount",
                table: "Invoices",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_InvoiceStatusId",
                table: "Invoices",
                column: "InvoiceStatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_InvoiceStatus_InvoiceStatusId",
                table: "Invoices",
                column: "InvoiceStatusId",
                principalTable: "InvoiceStatus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_InvoiceStatus_InvoiceStatusId",
                table: "Invoices");

            migrationBuilder.DropIndex(
                name: "IX_Invoices_InvoiceStatusId",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "InvoiceStatusComments",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "InvoiceStatusId",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "TotalAmount",
                table: "Invoices");
        }
    }
}
