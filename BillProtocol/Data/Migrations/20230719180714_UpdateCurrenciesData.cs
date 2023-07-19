using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BillProtocol.Data.Migrations
{
    public partial class UpdateCurrenciesData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("UPDATE Currencies SET Enabled=0  WHERE Code <> 'XRP'");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
