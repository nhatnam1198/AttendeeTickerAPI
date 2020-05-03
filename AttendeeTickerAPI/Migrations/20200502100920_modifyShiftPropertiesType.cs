using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AttendeeTickerAPI.Migrations
{
    public partial class modifyShiftPropertiesType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
        }
    }
}
