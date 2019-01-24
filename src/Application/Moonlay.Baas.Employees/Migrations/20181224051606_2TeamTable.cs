using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Moonlay.Baas.Employees.Migrations
{
    public partial class _2TeamTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TeamMember",
                columns: table => new
                {
                    Identity = table.Column<Guid>(nullable: false),
                    EmployeeId = table.Column<Guid>(nullable: false),
                    TeamId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeamMember", x => x.Identity);
                });

            migrationBuilder.CreateTable(
                name: "Teams",
                columns: table => new
                {
                    Identity = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Status = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teams", x => x.Identity);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TeamMember");

            migrationBuilder.DropTable(
                name: "Teams");
        }
    }
}
