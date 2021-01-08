using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MSSQLServerMonitoring.Infrastructure.Migrations.Migrations
{
    public partial class QueryModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Query",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    TimeStamp = table.Column<DateTime>(nullable: false),
                    AttachActivityId = table.Column<string>(nullable: true),
                    EventName = table.Column<string>(nullable: true),
                    PackageName = table.Column<string>(nullable: true),
                    ClientHn = table.Column<string>(nullable: true),
                    ClientAppName = table.Column<string>(nullable: true),
                    NtUserName = table.Column<string>(nullable: true),
                    DatabaseId = table.Column<int>(nullable: false),
                    DatabaseName = table.Column<string>(nullable: true),
                    Statement = table.Column<string>(nullable: true),
                    Duration = table.Column<decimal>(type: "decimal(19, 4)", nullable: false),
                    CpuTime = table.Column<long>(nullable: false),
                    PhysicalReads = table.Column<long>(nullable: false),
                    LogicalReads = table.Column<long>(nullable: false),
                    Writes = table.Column<long>(nullable: false),
                    RowCount = table.Column<long>(nullable: false),
                    SqlText = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Query", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Query");
        }
    }
}
