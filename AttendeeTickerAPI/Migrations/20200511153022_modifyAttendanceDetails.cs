using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AttendeeTickerAPI.Migrations
{
    public partial class modifyAttendanceDetails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AttendanceID",
                table: "AttendanceDetails");
            migrationBuilder.DropPrimaryKey(
                name: "PK_AttendanceDetail",
                table: "AttendanceDetails");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ShiftStart",
                table: "Shift",
                type: "dateTime",
                nullable: true,
                oldClrType: typeof(TimeSpan),
                oldType: "time(0)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ShiftEnd",
                table: "Shift",
                type: "dateTime",
                nullable: true,
                oldClrType: typeof(TimeSpan),
                oldType: "time",
                oldNullable: true);

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

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "ShiftStart",
                table: "Shift",
                type: "time(0)",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "dateTime",
                oldNullable: true);

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "ShiftEnd",
                table: "Shift",
                type: "time",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "dateTime",
                oldNullable: true);

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
