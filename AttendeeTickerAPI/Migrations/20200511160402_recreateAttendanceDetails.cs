using Microsoft.EntityFrameworkCore.Migrations;

namespace AttendeeTickerAPI.Migrations
{
    public partial class recreateAttendanceDetails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_AttendanceDetail",
                table: "AttendanceDetails");

            migrationBuilder.AlterColumn<int>(
                name: "AttendanceID",
                table: "AttendanceDetails",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "AttendanceDetailsID",
                table: "AttendanceDetails",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AttendanceDetails",
                table: "AttendanceDetails",
                column: "AttendanceDetailsID");

            migrationBuilder.CreateIndex(
                name: "IX_AttendanceDetails_AttendanceID",
                table: "AttendanceDetails",
                column: "AttendanceID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_AttendanceDetails",
                table: "AttendanceDetails");

            migrationBuilder.DropIndex(
                name: "IX_AttendanceDetails_AttendanceID",
                table: "AttendanceDetails");

            migrationBuilder.DropColumn(
                name: "AttendanceDetailsID",
                table: "AttendanceDetails");

            migrationBuilder.AlterColumn<int>(
                name: "AttendanceID",
                table: "AttendanceDetails",
                type: "int",
                nullable: false,
                oldClrType: typeof(int))
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AttendanceDetail",
                table: "AttendanceDetails",
                column: "AttendanceID");
        }
    }
}
