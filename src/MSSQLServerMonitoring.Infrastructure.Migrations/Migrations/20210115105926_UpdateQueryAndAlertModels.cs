using Microsoft.EntityFrameworkCore.Migrations;

namespace MSSQLServerMonitoring.Infrastructure.Migrations.Migrations
{
    public partial class UpdateQueryAndAlertModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Query");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Alert");

            migrationBuilder.AddColumn<decimal>(
                name: "Duration",
                table: "Alert",
                type: "decimal(19, 4)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<long>(
                name: "LogicalReads",
                table: "Alert",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "Writes",
                table: "Alert",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Duration",
                table: "Alert");

            migrationBuilder.DropColumn(
                name: "LogicalReads",
                table: "Alert");

            migrationBuilder.DropColumn(
                name: "Writes",
                table: "Alert");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Query",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Alert",
                nullable: true);
        }
    }
}
