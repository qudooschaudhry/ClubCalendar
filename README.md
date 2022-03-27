This README is written using markdown. To get markdown support [Download] (https://marketplace.visualstudio.com/items?itemName=MadsKristensen.MarkdownEditor64) the extension and install it.*

https://razor.radzen.com/scheduler

# Introduction
This solution contains all source code and relevant assets for the Software Distribution Tool (SDT)

The software distribtion tool is a tool developed by MCS to allow for engineers to distribute various applications and their versions for installation on test stations.

# Local Developer Environment Setup

Install the following items

1. [Download](https://visualstudio.microsoft.com/) and install Visual studio 2022
2. [Download](https://azure.microsoft.com/en-us/features/storage-explorer/) and install Azure Storage Explorer.
3. [Download](https://docs.docker.com/desktop/windows/install/) and install Docker desktop for windows.
4. Install WSL, the instructions with a link is provided at the end of docker installation.
5. Download SQL Server docker image by running the following on Powershell
    `docker pull mcr.microsoft.com/mssql/server:2019-latest`
6. Start local SQL Server docker image by running the following command on Powershell

    `docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=Jm9h/{!zmUvZY62!" -p 1433:1433 --name LocalSql --hostname LocalSql -v c:/sqlserver/data:/var/opt/mssql/data -v c:/sqlserver/log:/var/opt/mssql/log -d mcr.microsoft.com/mssql/server:2019-latest`

7. Connect to your local DB using SQL Server Management Studio (SSMS) by using the following

    ```
        Server name:localhost,1433
        Authentication: SQL Server Authentication
        Login: sa
        Password: Jm9h/{!zmUvZY62!
    ```
**Note the comma between localhost and port number**

8. Download the Azure storage emulator (Azurite) docker image

    `docker pull mcr.microsoft.com/azure-storage/azurite`
9. Start the local storage emulator

    `docker run --name azurite -d --restart unless-stopped -p 10000:10000 -p 10001:10001 -p 10002:10002 -v c:/azurite:/data mcr.microsoft.com/azure-storage/azurite`

10. Open the Storage Explorer installed in step 2.
    1. Under Accounts, click Add an account
    2. Select storage account or service
    3. Select Connection string
    4. Give it a name like LocalStorageAccount and paste the following in the connection string
   
    `DefaultEndpointsProtocol=https;AccountName=devstoreaccount1;AccountKey=Eby8vdM02xNOcqFlqUwJPLlmEtlCDXJ1OUzFT50uSRZ6IFsuFq2UVErCz4I6tq/K1SZFPTOtr/KBHBeksoGMGw==;BlobEndpoint=https://127.0.0.1:10000/devstoreaccount1;QueueEndpoint=https://127.0.0.1:10001/devstoreaccount1;TableEndpoint=http://127.0.0.1:10002/devstoreaccount1;`

11. The application uses Azure Key Vault for application secrets (passwords, secrets etc.). To be able to run that locally, we need to follow these steps
      1. Provide your local machine name to DBA's so they can add it to the list of machines that have access to AKV
      2. Make sure you are connected to Pulse VPN.
      3. Make sure Visual studio is running as administrator.
      4. Add the following entry to your local host file (located in C:\Windows\System32\drivers\etc)
         `10.79.110.52    sqldatabasesdev-keyvault.vault.azure.net`
      5. Run the following on a powershell command line
        `resolve-dnsname "sqldatabasesdev-keyvault.vault.azure.net"`

# Solution Structure

The solution is structured based on a variation of the [clean architecture](https://blog.cleancoder.com/uncle-bob/2012/08/13/the-clean-architecture.html). This allows for good separation of concerns and allows us to test our code at various levels.

The solution is composed of the following projects






# migration
`Add-Migration InitialSchema -Project Connect.Infrastructure`
`Script-Migration -From InitialMigration' -To LatestMigration -Project Connect.Infrastructure`


 # docker

`docker pull mcr.microsoft.com/mssql/server:2019-latest`
`docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=!23qweASD" -p 14330:14330 --name LocalSql --hostname LocalSql -d mcr.microsoft.com/mssql/server:2019-latest`