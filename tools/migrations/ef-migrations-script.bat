@ECHO OFF
@SET OLD_ENVIRONMENT=%ASPNETCORE_ENVIRONMENT%
IF '%1' == '' GOTO generate_script
ECHO Switch environment from %OLD_ENVIRONMENT% to %1
set ASPNETCORE_ENVIRONMENT=%1

:generate_script
echo Generate sql for environment %ASPNETCORE_ENVIRONMENT%
cd ..\..\src\ReceiptMonitoring.Infrastructure.Migrations
dotnet ef migrations script -o scripts.sql -i || goto handle_error
IF '%1' == '' GOTO exit
set ASPNETCORE_ENVIRONMENT=%OLD_ENVIRONMENT%
echo Revert environment to %ASPNETCORE_ENVIRONMENT%

:exit
exit /B 0

GOTO handle_error

:handle_error
EXIT /B %ERRORLEVEL%