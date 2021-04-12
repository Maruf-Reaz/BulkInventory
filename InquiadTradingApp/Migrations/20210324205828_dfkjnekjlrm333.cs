using Microsoft.EntityFrameworkCore.Migrations;

namespace InquiadTradingApp.Migrations
{
    public partial class dfkjnekjlrm333 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Amount",
                table: "LocalSales",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Amount",
                table: "LocalSales");
        }
    }
}
