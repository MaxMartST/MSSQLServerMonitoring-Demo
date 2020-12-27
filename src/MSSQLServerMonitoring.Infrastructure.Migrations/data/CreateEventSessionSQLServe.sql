IF EXISTS (SELECT *
      FROM sys.server_event_sessions
      WHERE name = 'StatementEvents')
BEGIN
    DROP EVENT SESSION [StatementEvents]
          ON SERVER;
END
GO

CREATE EVENT SESSION [StatementEvents] ON SERVER 
	ADD EVENT sqlserver.sql_statement_completed(    
		ACTION (
			sqlserver.sql_text, 
			sqlserver.tsql_stack, 
			sqlserver.client_app_name, 
			sqlserver.client_hostname, 
			sqlserver.nt_username,
			sqlserver.username,
			sqlserver.database_id,
			sqlserver.database_name,
			sqlserver.plan_handle
		)
	)
    ADD TARGET package0.ring_buffer
	WITH(
		STARTUP_STATE=OFF,
		MAX_MEMORY=4096 KB,
		EVENT_RETENTION_MODE=ALLOW_SINGLE_EVENT_LOSS,
		MAX_DISPATCH_LATENCY=1 SECONDS
	);
GO

ALTER EVENT SESSION [StatementEvents]
    ON SERVER
    STATE = START;