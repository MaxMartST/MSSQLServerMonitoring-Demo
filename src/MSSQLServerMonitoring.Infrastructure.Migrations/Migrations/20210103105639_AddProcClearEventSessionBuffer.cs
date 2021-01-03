using Microsoft.EntityFrameworkCore.Migrations;

namespace MSSQLServerMonitoring.Infrastructure.Migrations.Migrations
{
    public partial class AddProcClearEventSessionBuffer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sp = @"CREATE PROCEDURE ClearEventSessionBuffer "
                + "AS "
                + "ALTER EVENT SESSION [StatementEvents] " 
                + "ON SERVER STATE = STOP; "
                + "ALTER EVENT SESSION [StatementEvents] "
                + "ON SERVER DROP TARGET package0.ring_buffer; "
                + "ALTER EVENT SESSION [StatementEvents] "
                + "ON SERVER ADD TARGET package0.ring_buffer "
                + "WITH( "
                + "STARTUP_STATE=OFF, "
                + "MAX_MEMORY=4096 KB, "
                + "EVENT_RETENTION_MODE=ALLOW_SINGLE_EVENT_LOSS, "
                + "MAX_DISPATCH_LATENCY=1 SECONDS, "
                + "TRACK_CAUSALITY = ON ); "
                + "ALTER EVENT SESSION [StatementEvents] "
                + "ON SERVER STATE = START; ";

            migrationBuilder.Sql(sp);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
