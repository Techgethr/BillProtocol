using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BillProtocol.Data.Migrations
{
    public partial class InsertBaseData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData("InvoiceTypes", new string[]
                {"Id", "Name"},
                new object[]
                {
                    1,"Regular Invoice"
                });
            migrationBuilder.InsertData("InvoiceTypes", new string[]
                {"Id", "Name"},
                new object[]
                {
                    2,"Payment To Date"
                });
            migrationBuilder.InsertData("InvoiceTypes", new string[]
                {"Id", "Name"},
                new object[]
                {
                    3,"Escrow"
                });
            migrationBuilder.InsertData("InvoiceStatus", new string[]
                {"Id", "Name"},
                new object[]
                {
                    1,"Created"
                });
            migrationBuilder.InsertData("InvoiceStatus", new string[]
                {"Id", "Name"},
                new object[]
                {
                    2,"Rejected"
                });
            migrationBuilder.InsertData("InvoiceStatus", new string[]
                {"Id", "Name"},
                new object[]
                {
                    3,"Approved"
                });
            migrationBuilder.InsertData("InvoiceStatus", new string[]
                {"Id", "Name"},
                new object[]
                {
                    4,"Payed"
                });
            migrationBuilder.InsertData("Currencies", new string[]
                {"Id", "Name","Code","Enabled"},
                new object[]
                {
                    1,"XRP","XRP", true
                });
            migrationBuilder.InsertData("Currencies", new string[]
                {"Id", "Name","Code","Enabled"},
                new object[]
                {
                    2,"US Dollar","USD", true
                });
            migrationBuilder.InsertData("Currencies", new string[]
                {"Id", "Name","Code","Enabled"},
                new object[]
                {
                    3,"Euro","EUR", true
                });
            migrationBuilder.InsertData("Currencies", new string[]
                {"Id", "Name","Code","Enabled"},
                new object[]
                {
                    4,"Pound Sterling","GBP", true
                });
            migrationBuilder.InsertData("Countries", new string[]
                {"Id", "Name","Enabled"},
                new object[]
                {
                    1,"United States", true
                });
            migrationBuilder.InsertData("Countries", new string[]
                {"Id", "Name","Enabled"},
                new object[]
                {
                    2,"England", true
                });
            migrationBuilder.InsertData("Countries", new string[]
                {"Id", "Name","Enabled"},
                new object[]
                {
                    3,"Germany", true
                });
            migrationBuilder.InsertData("Countries", new string[]
                {"Id", "Name","Enabled"},
                new object[]
                {
                    4,"Spain", true
                });
            migrationBuilder.InsertData("Countries", new string[]
                {"Id", "Name","Enabled"},
                new object[]
                {
                    5,"Italy", true
                });
            migrationBuilder.InsertData("Countries", new string[]
                {"Id", "Name","Enabled"},
                new object[]
                {
                    6,"France", true
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
