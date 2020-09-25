using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjektAplikacjeInternetowe.Data.Migrations
{
    public partial class cart : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "movieName",
                table: "Cart",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "movieName",
                table: "Cart");
        }
    }
}
