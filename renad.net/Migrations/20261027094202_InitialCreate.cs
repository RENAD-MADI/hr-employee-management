using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace renad.net.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Department",
                columns: table => new
                {
                    Department_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Department_Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Department", x => x.Department_Id);
                });

            migrationBuilder.CreateTable(
                name: "Genders",
                columns: table => new
                {
                    Gender_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Gender_Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genders", x => x.Gender_Id);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Employee_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Employee_Name = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    Employee_Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Employee_Hire_Data = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Employee_Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Employee_Salary = table.Column<double>(type: "float", nullable: false),
                    Is_Active = table.Column<bool>(type: "bit", nullable: false),
                    Department_Id = table.Column<int>(type: "int", nullable: false),
                    Gender_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Employee_Id);
                    table.ForeignKey(
                        name: "FK_Employees_Department_Department_Id",
                        column: x => x.Department_Id,
                        principalTable: "Department",
                        principalColumn: "Department_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Employees_Genders_Gender_id",
                        column: x => x.Gender_id,
                        principalTable: "Genders",
                        principalColumn: "Gender_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Department",
                columns: new[] { "Department_Id", "Department_Name" },
                values: new object[,]
                {
                    { 1, "Human Resources" },
                    { 2, "Finance" },
                    { 3, "IT" },
                    { 4, "Sales" }
                });

            migrationBuilder.InsertData(
                table: "Genders",
                columns: new[] { "Gender_Id", "Gender_Name" },
                values: new object[,]
                {
                    { 1, "Male" },
                    { 2, "Female" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Employees_Department_Id",
                table: "Employees",
                column: "Department_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_Employee_Email",
                table: "Employees",
                column: "Employee_Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employees_Gender_id",
                table: "Employees",
                column: "Gender_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Department");

            migrationBuilder.DropTable(
                name: "Genders");
        }
    }
}
