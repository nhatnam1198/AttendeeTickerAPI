using Microsoft.EntityFrameworkCore.Migrations;

namespace AttendeeTickerAPI.Migrations
{
    public partial class ModifyEventProperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Event",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Event");
        }
    }
}
