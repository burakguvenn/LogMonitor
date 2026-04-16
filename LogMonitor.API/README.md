Step 1 : docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=YourStrong!Pass123" -p 1433:1433 -d mcr.microsoft.com/mssql/server:2022-latest

Step 2 : dotnet restore

Step 3 : cd LogMonitor.API
dotnet ef database update

Step 4 : dotnet run
