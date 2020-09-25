using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjektAplikacjeInternetowe.Data.Migrations
{
    public partial class booked : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MovieName",
                table: "BookingTable",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MovieName",
                table: "BookingTable");
        }
    }
}
