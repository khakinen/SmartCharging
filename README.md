## Architecture
The clean implementation is achieved by using Hexagonal Architecture (Ports and Adapters) which helps keeping domain super clean along with addressing separation of concerns principle.  Meanwhile some design patterns like CQRS, Decorator, Repository, Unit Of Work are applied.

## Domain Logic
All required fuctionalities are fulfilled with domin services. On the other side there are two levels of validation applied in the solution.
* Mediator pipelines : Basic validations with meaningful error messages to make sure requests contain required input values.
* Decorators : Decorator pattern is applied through domain logic services to make sure domain required validations are always in place. 


## Test
All required domain logic is covered with unit tests. Test project is placed in the Domain folder.


## Database
A docker compose file (docker-compose.yml) is placed in the root folder. To have a quick SQL Server running, make sure first docker desktop is installed, then executing 'docker compose up' command in the root folder will provide SQL server up and running.
On the other hand,
Migrations are used in the Data layer which means there is no need to run sql script other than creating database.
Database creation could be easily achieved by executing this script:
* CREATE DATABASE SmartCharging

The rest of the table creations will be taken care of by EF migration. Application will create all required tables in the SmartCharging database.


## Swagger 
Swagger page is accessible with https://localhost:5001/swagger/index.html  Nevertheless open-api file is placed in root folder.

## Performance and maintenance
The architecture is designed on Enterprise level which will be able to handle any number of concurrent requests with running multiple instances behind a load balancer. Thanks to the applied UnitOfWork pattern, Data consistency will be solid.

## To run
Make sure that net6.0 was already installed. 
After having an SQL Server up and running, executing 'dotnet run' command will spin up the web service application.
* src/Applications/WebApi> dotnet run




