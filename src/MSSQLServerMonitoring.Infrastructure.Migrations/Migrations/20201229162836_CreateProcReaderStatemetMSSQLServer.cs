using Microsoft.EntityFrameworkCore.Migrations;

namespace MSSQLServerMonitoring.Infrastructure.Migrations.Migrations
{
    public partial class CreateProcReaderStatemetMSSQLServer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sp = @"CREATE PROCEDURE ReaderStatemetMSSQLServer "
                + "AS "
                + "DECLARE @target_data XML "
                + "SELECT  @target_data = CAST([t].[target_data] AS XML) "
                + "FROM    [sys].[dm_xe_sessions] AS s "
                + "JOIN    [sys].[dm_xe_session_targets] AS t "
                + "ON [t].[event_session_address] = [s].[address] "
                + "WHERE   [s].[name] = N'StatementEvents' AND "
                + "[t].[target_name] = N'ring_buffer'; "
                + "SELECT DATEADD(hh, DATEDIFF(hh, GETUTCDATE(), CURRENT_TIMESTAMP), n.value('(@timestamp)[1]', 'datetime2')) AS [TimeStamp], "
                + "n.value(N'(action[@name=\"attach_activity_id\"]/value)[1]', N'nvarchar(max)') AS [AttachActivityId], "
                + "n.value(N'(@name)[1]', N'varchar(50)') AS [EventName], "
                + "n.value(N'(@package)[1]', N'varchar(50)') AS [PackageName], "
                + "n.value(N'(action[@name=\"client_hostname\"]/value)[1]', N'nvarchar(max)') AS [ClientHn], "
                + "n.value(N'(action[@name=\"client_app_name\"]/value)[1]', N'nvarchar(4000)') AS [ClientAppName], "
                + "n.value(N'(action[@name=\"nt_username\"]/value)[1]', N'nvarchar(max)') AS [NtUserName], "
                + "n.value(N'(action[@name=\"database_id\"]/value)[1]', N'int') AS [DatabaseId], "
                + "n.value(N'(action[@name=\"database_name\"]/value)[1]', N'nvarchar(max)') AS [DatabaseName], "
                + "n.value(N'(data[@name=\"statement\"]/value)[1]', N'nvarchar(max)') AS [Statement], "
                + "CONVERT(DECIMAL(6,3),round(n.value(N'(data[@name=\"duration\"]/value)[1]', N'bigint')/1000000.0,3,1)) AS [Duration], "
                + "n.value(N'(data[@name=\"cpu_time\"]/value)[1]', N'bigint') AS [CpuTime], "
                + "n.value(N'(data[@name=\"physical_reads\"]/value)[1]', N'bigint') AS [PhysicalReads], "
                + "n.value(N'(data[@name=\"logical_reads\"]/value)[1]', N'bigint') AS [LogicalReads], "
                + "n.value(N'(data[@name=\"writes\"]/value)[1]', N'bigint') AS [Writes], "
                + "n.value(N'(data[@name=\"row_count\"]/value)[1]', N'bigint') AS [RowCount], "
                + "REPLACE(n.value(N'(action[@name=\"sql_text\"]/value)[1]', N'nvarchar(max)'), CHAR(10), CHAR(13)+CHAR(10)) AS [SqlText] "
                + "FROM @target_data.nodes('RingBufferTarget/event') AS the (n) ";

            migrationBuilder.Sql(sp);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
