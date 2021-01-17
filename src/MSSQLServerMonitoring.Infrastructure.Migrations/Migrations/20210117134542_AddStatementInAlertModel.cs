using Microsoft.EntityFrameworkCore.Migrations;

namespace MSSQLServerMonitoring.Infrastructure.Migrations.Migrations
{
    public partial class AddStatementInAlertModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AttachActivityId",
                table: "Alert",
                newName: "Statement");

            migrationBuilder.AddColumn<string>(
                name: "QueryId",
                table: "Alert",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "QueryId",
                table: "Alert");

            migrationBuilder.RenameColumn(
                name: "Statement",
                table: "Alert",
                newName: "AttachActivityId");
        }
    }
}
