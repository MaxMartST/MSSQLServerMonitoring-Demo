@ECHO OFF

cd ..\..\src\ReceiptMonitoring.Infrastructure.Migrations
dotnet ef database update %1 || goto handle_error
exit /B 0

GOTO handle_error

:handle_error
EXIT /B %ERRORLEVEL%