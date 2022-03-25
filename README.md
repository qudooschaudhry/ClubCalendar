This README is written using markdown. To get markdown support [Download] (https://marketplace.visualstudio.com/items?itemName=MadsKristensen.MarkdownEditor64) the extension and install it.*

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

## MCS.SoftwareDistributor

This project contains the user interface of the Software distribution tool. It follows the asp.net mvc razor pages approach to building web based applications.

## MCS.SoftwareDistributor.API

This project contains an option for a RESTful API which can be consumed by other consumers outside of the software distribution tool.

## MCS.SoftwareDistributor.Domain

This project contains the domain model of the application. It contains all business logic and we should strive to push all business logic here.

## MCS.SoftwareDistributor.Infrastructure

This project contains infrastructure code that provides key capabilities used by the application, but it abstracts them out so we can build an application that is loosely coupled.

## MCS.SoftwareDistributor.ApplicationService

This project is the glue between domain and the infrastructure. The UI calls this for running Queries and Commands.

# Running the Application Locally

## First time setup

If this is your first time running the application, make sure you have run the pre-requisites, specifically you have a Local SQL server running as a docker image and you can connect to it through SSMS.


1. Open the following files.sql in the following folder and run them in the order of the timestamp (or sort by modified date and run from old to new)

    `~\SoftwareDistributionTool\scripts\ddl\`

    The files will be of this format `20220317141508_InitialCreate.sql`

2. Run the local seed script located in \scripts\dml\local_seed.sql

Once you are setup, you can run the application by pressing F5 (or ctrl + F5). Make sure the startup project is MCS.SoftwareDistributor

# Database Scripts

The application uses Entity Framework code first to design the model. To show how to generate the DDL scripts, follow this example

1. Let's say we want to add a new Field to the `ApplicationGroups` table called `LastModifiedBy`. Add the field in the ApplicationGroup class located under MCS.SoftwareDistributor.Domain like so

    `public int LastModifiedBy { get; set; }`

2. Open the Package Manager Console and run the following command to generate migrations

    `Add-Migration AddedLastModifiedDateToApplicationGroups -Project MCS.SoftwareDistributor.Infrastructure -StartupProject MCS.SoftwareDistributor`

    This will add the migration under the the Migrations folder

    `Script-Migration -From InitialCreate -To AddedLastModifiedDateToApplicationGroups -Project MCS.SoftwareDistributor.Infrastructure -StartupProject MCS.SoftwareDistributor -Idempotent`


    The switches represents the following

    - From: The migration we want to start from
    - To: The migration we want to  include up to
    - Project: The project containing the data context
    - Startup Project: the project the application starts up from
    - Idempotent: generates a script with no side effects

4. Once the script is generated, save it as a new .sql file in the following folder.  

    `~SoftwareDistributionTool\scripts\ddl`

    Use the migration name as the file name, for instance in our example use the generated time stamp like so <timestemp>_AddedLastModifiedDateToApplicationGroups.sql

5. This generated script can be run on the DEV database and handed of the SQL review team through existing process. 

--
Qudoos Chaudhry
(613) 302-3852




# migration
`Add-Migration InitialSchema -Project Connect.Infrastructure`
`Script-Migration -From InitialMigration' -To LatestMigration -Project Connect.Infrastructure`


 # docker

`docker pull mcr.microsoft.com/mssql/server:2019-latest`
`docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=!23qweASD" -p 14330:14330 --name LocalSql --hostname LocalSql -d mcr.microsoft.com/mssql/server:2019-latest`