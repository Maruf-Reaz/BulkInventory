using Microsoft.EntityFrameworkCore.Migrations;

namespace InquiadTradingApp.Migrations
{
    public partial class dfkjnekjlrmg : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "KGAmount",
                table: "LocalSales");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "KGAmount",
                table: "LocalSales",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
