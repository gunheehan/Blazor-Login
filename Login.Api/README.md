# Sign API

## Starting SQL Server
```powershell
$sa_password = "[SA PASSWORD HERE]"
docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=Salin@2017" -e "MSSQL_PID=Developer"  -e "MSSQL_USER=SA" -p 2000:1433 -v sqlvolume:/var/opt/mssql -d --name=LoginDB mcr.microsoft.com/azure-sql-edge
docker run -e "ACCEPT_EULA=1" -e "MSSQL_SA_PASSWORD=Rajhans123@" -e "MSSQL_PID=Developer" -e "MSSQL_USER=SA" -p 1433:1433 -d --name=mssql mcr.microsoft.com/azure-sql-edge
docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=Salin@2017" -e "MSSQL_PID=Developer"  -e "MSSQL_USER=SA" -p 1433:1433 -v sqlvolume:/var/opt/mssql -d --name=LoginAPI mcr.microsoft.com/azure-sql-edge

```

## Setting the connection string to secret manager
```powershell
dotnet user-secrets set "ConnectionStrings:LoginContext" "Server=localhost; Database=LoginDB; User Id = sa; Password=Salin@2017; TrustServerCertificate=True"
dotnet user-secrets set "ConnectionStrings:LoginAPIContext" "Server=localhost; Database=LoginAPI; User Id = sa; Password=Salin@2017; TrustServerCertificate=True"
```