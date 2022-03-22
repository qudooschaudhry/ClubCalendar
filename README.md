# migration
`Add-Migration InitialSchema -Project Connect.Infrastructure`
`Script-Migration -From InitialMigration' -To LatestMigration -Project Connect.Infrastructure`


 # docker

`docker pull mcr.microsoft.com/mssql/server:2019-latest`
`docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=!23qweASD" -p 14330:14330 --name LocalSql --hostname LocalSql -d mcr.microsoft.com/mssql/server:2019-latest`