@ECHO OFF

IF '%1' == '' GOTO migration_name_error

cd ..\..\src\ReceiptMonitoring.Infrastructure.Migrations
dotnet ef migrations add %1 || goto handle_error
exit /B 0

:migration_name_error
ECHO Error - migration name is not passed.
ECHO Example: %~nx0 InitialCreate
EXIT /B 1

GOTO handle_error

:handle_error
EXIT /B %ERRORLEVEL%