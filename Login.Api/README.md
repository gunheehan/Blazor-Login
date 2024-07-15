# Sign API

### Starting SQL Server
```powershell
$sa_password = "[SA PASSWORD HERE]"
docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=Salin@2017" -e "MSSQL_PID=Developer"  -e "MSSQL_USER=SA" -p 2000:1433 -v sqlvolume:/var/opt/mssql -d --name=LoginDB mcr.microsoft.com/azure-sql-edge
docker run -e "ACCEPT_EULA=1" -e "MSSQL_SA_PASSWORD=Rajhans123@" -e "MSSQL_PID=Developer" -e "MSSQL_USER=SA" -p 1433:1433 -d --name=mssql mcr.microsoft.com/azure-sql-edge
docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=Salin@2017" -e "MSSQL_PID=Developer"  -e "MSSQL_USER=SA" -p 1433:1433 -v sqlvolume:/var/opt/mssql -d --name=LoginAPI mcr.microsoft.com/azure-sql-edge
```

### Setting the connection string to secret manager
```powershell
dotnet user-secrets set "ConnectionStrings:LoginContext" "Server=localhost; Database=LoginDB; User Id = sa; Password=Salin@2017; TrustServerCertificate=True"
dotnet user-secrets set "ConnectionStrings:LoginAPIContext" "Server=localhost; Database=LoginAPI; User Id = sa; Password=Salin@2017; TrustServerCertificate=True"
```

# MSSQL

### variable
```powershell
문자형 : ''
숫자형 : 데이터만 사용
```

### Data Inserts
```powershell
EX : INSERT INTO TABLE(FIELD1, FIELD2) VALUSE(VALUE1, VALUE2, VALUE3)
INSERT into LoginAPI.dbo.LoginInfos(Nickname, Email) values ('hea','hea@heahea.com')
```

### Data Update
```powershell
EX : UPDATE TABLE1 SET FIELD1 = VALUE1, FIELD2 = VALUE2... WHERE 조건 == 조건식
UPDATE LoginAPI.dbo.LoginInfos SET Nickname = 'Goen' WHERE Id=1
```