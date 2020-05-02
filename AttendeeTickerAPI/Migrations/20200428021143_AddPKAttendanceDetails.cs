using Microsoft.EntityFrameworkCore.Migrations;

namespace AttendeeTickerAPI.Migrations
{
    public partial class AddPKAttendanceDetails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_AttendanceDetails_AttendanceID",
                table: "AttendanceDetails");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AttendanceDetail",
                table: "AttendanceDetails",
                column: "AttendanceID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_AttendanceDetail",
                table: "AttendanceDetails");

            migrationBuilder.CreateIndex(
                name: "IX_AttendanceDetails_AttendanceID",
                table: "AttendanceDetails",
                column: "AttendanceID");
        }
    }
}
