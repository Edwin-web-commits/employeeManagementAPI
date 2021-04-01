using Microsoft.EntityFrameworkCore.Migrations;

namespace EmployeeManagementAPI.Migrations
{
    public partial class SeedingData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Departments",
                columns: new[] { "Id", "DepartmentName" },
                values: new object[] { 1, "Human Resource" });

            migrationBuilder.InsertData(
                table: "Departments",
                columns: new[] { "Id", "DepartmentName" },
                values: new object[] { 2, "Sales" });

            migrationBuilder.InsertData(
                table: "Departments",
                columns: new[] { "Id", "DepartmentName" },
                values: new object[] { 3, "Customs" });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "Address", "DepartmentID", "Email", "EmployeeName", "IdentityNo", "Position", "Salary" },
                values: new object[] { 3, "27 Nelson Mandel strt", 1, "sharonmoropane04@gmail.com", "Sharon Motlokwa", "9709294764082", "Supervisor", 50000.0 });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "Address", "DepartmentID", "Email", "EmployeeName", "IdentityNo", "Position", "Salary" },
                values: new object[] { 2, "29 Nelson Mandel strt", 2, "edwinmoropane03@gmail.com", "Edwin Motlokwa", "9909294764082", "Manager", 70000.0 });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "Address", "DepartmentID", "Email", "EmployeeName", "IdentityNo", "Position", "Salary" },
                values: new object[] { 1, "23 Nelson Mandel strt", 3, "edwinmoropane02@gmail.com", "Moropane Motlokwa", "9509294764082", "Supervisor", 50000.0 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
