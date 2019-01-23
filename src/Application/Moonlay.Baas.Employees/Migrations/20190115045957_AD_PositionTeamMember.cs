using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Moonlay.Baas.Employees.Migrations
{
    public partial class AD_PositionTeamMember : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "PositionId",
                table: "TeamMember",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "Position",
                columns: table => new
                {
                    Identity = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Status = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Position", x => x.Identity);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Position");

            migrationBuilder.DropColumn(
                name: "PositionId",
                table: "TeamMember");
        }
    }
}
