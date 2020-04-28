using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AttendeeTickerAPI.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Shift",
                columns: table => new
                {
                    ShiftID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ShiftName = table.Column<string>(maxLength: 50, nullable: true),
                    ShiftStart = table.Column<TimeSpan>(type: "time(0)", nullable: true),
                    ShiftEnd = table.Column<TimeSpan>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shift", x => x.ShiftID);
                });

            migrationBuilder.CreateTable(
                name: "Student",
                columns: table => new
                {
                    StudentID = table.Column<string>(maxLength: 300, nullable: false),
                    StudentLastName = table.Column<string>(maxLength: 20, nullable: true),
                    StudentFirstName = table.Column<string>(maxLength: 20, nullable: true),
                    Dob = table.Column<DateTime>(type: "date", nullable: true),
                    ClassName = table.Column<string>(maxLength: 50, nullable: true),
                    Nationality = table.Column<string>(maxLength: 20, nullable: true),
                    Phone = table.Column<string>(maxLength: 30, nullable: true),
                    PersonID = table.Column<string>(maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Student", x => x.StudentID);
                });

            migrationBuilder.CreateTable(
                name: "Subject",
                columns: table => new
                {
                    SubjectID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubjectName = table.Column<string>(maxLength: 50, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "date", nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 50, nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "date", nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subject", x => x.SubjectID);
                });

            migrationBuilder.CreateTable(
                name: "Teacher",
                columns: table => new
                {
                    TeacherID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TeacherName = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teacher", x => x.TeacherID);
                });

            migrationBuilder.CreateTable(
                name: "ComputerFile",
                columns: table => new
                {
                    FileID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FileUri = table.Column<string>(nullable: true),
                    FileName = table.Column<string>(maxLength: 50, nullable: true),
                    FileType = table.Column<string>(maxLength: 10, nullable: true),
                    StudentID = table.Column<string>(maxLength: 300, nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 50, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "date", nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 50, nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_File", x => x.FileID);
                    table.ForeignKey(
                        name: "FK_ComputerFile_Student",
                        column: x => x.StudentID,
                        principalTable: "Student",
                        principalColumn: "StudentID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SubjectClass",
                columns: table => new
                {
                    SubjectClassID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubjectClassName = table.Column<string>(maxLength: 50, nullable: true),
                    StartDate = table.Column<DateTime>(type: "date", nullable: true),
                    EndDate = table.Column<DateTime>(type: "date", nullable: true),
                    Status = table.Column<int>(nullable: true),
                    SubjectID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubjectClass", x => x.SubjectClassID);
                    table.ForeignKey(
                        name: "FK_SubjectClass_Subject",
                        column: x => x.SubjectID,
                        principalTable: "Subject",
                        principalColumn: "SubjectID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Attendance",
                columns: table => new
                {
                    AttendanceID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentID = table.Column<string>(maxLength: 300, nullable: true),
                    SubjectClassID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attendance", x => x.AttendanceID);
                    table.ForeignKey(
                        name: "FK_Attendance_Student",
                        column: x => x.StudentID,
                        principalTable: "Student",
                        principalColumn: "StudentID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Attendance_SubjectClass",
                        column: x => x.SubjectClassID,
                        principalTable: "SubjectClass",
                        principalColumn: "SubjectClassID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Event",
                columns: table => new
                {
                    EventID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ShiftID = table.Column<int>(nullable: true),
                    SubjectClassID = table.Column<int>(nullable: true),
                    DateTime = table.Column<DateTime>(type: "date", nullable: true),
                    TeacherID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Event", x => x.EventID);
                    table.ForeignKey(
                        name: "FK_Event_Shift",
                        column: x => x.ShiftID,
                        principalTable: "Shift",
                        principalColumn: "ShiftID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Event_SubjectClass",
                        column: x => x.SubjectClassID,
                        principalTable: "SubjectClass",
                        principalColumn: "SubjectClassID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Event_Teacher",
                        column: x => x.TeacherID,
                        principalTable: "Teacher",
                        principalColumn: "TeacherID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AttendanceDetails",
                columns: table => new
                {
                    AttendanceID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EventID = table.Column<int>(nullable: true),
                    IsAttended = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "FK_AttendanceDetails_Attendance",
                        column: x => x.AttendanceID,
                        principalTable: "Attendance",
                        principalColumn: "AttendanceID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AttendanceDetails_Event",
                        column: x => x.EventID,
                        principalTable: "Event",
                        principalColumn: "EventID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Attendance_StudentID",
                table: "Attendance",
                column: "StudentID");

            migrationBuilder.CreateIndex(
                name: "IX_Attendance_SubjectClassID",
                table: "Attendance",
                column: "SubjectClassID");

            migrationBuilder.CreateIndex(
                name: "IX_AttendanceDetails_AttendanceID",
                table: "AttendanceDetails",
                column: "AttendanceID");

            migrationBuilder.CreateIndex(
                name: "IX_AttendanceDetails_EventID",
                table: "AttendanceDetails",
                column: "EventID");

            migrationBuilder.CreateIndex(
                name: "IX_ComputerFile_StudentID",
                table: "ComputerFile",
                column: "StudentID");

            migrationBuilder.CreateIndex(
                name: "IX_Event_ShiftID",
                table: "Event",
                column: "ShiftID");

            migrationBuilder.CreateIndex(
                name: "IX_Event_SubjectClassID",
                table: "Event",
                column: "SubjectClassID");

            migrationBuilder.CreateIndex(
                name: "IX_Event_TeacherID",
                table: "Event",
                column: "TeacherID");

            migrationBuilder.CreateIndex(
                name: "IX_SubjectClass_SubjectID",
                table: "SubjectClass",
                column: "SubjectID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AttendanceDetails");

            migrationBuilder.DropTable(
                name: "ComputerFile");

            migrationBuilder.DropTable(
                name: "Attendance");

            migrationBuilder.DropTable(
                name: "Event");

            migrationBuilder.DropTable(
                name: "Student");

            migrationBuilder.DropTable(
                name: "Shift");

            migrationBuilder.DropTable(
                name: "SubjectClass");

            migrationBuilder.DropTable(
                name: "Teacher");

            migrationBuilder.DropTable(
                name: "Subject");
        }
    }
}
