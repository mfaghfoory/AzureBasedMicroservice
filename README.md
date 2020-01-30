# AzureBasedMicroservice
Simple micro-service implementation based on Azure


## Architecture  And Design Patterns Which Have Been Used
#### 1 - CQRS
#### 2 - Microservices 
#### 3 - Unit Of Works

## Libraries Which Have Been Used

### Back-end part
#### 1 - [Ocelot](https://github.com/ThreeMammals/Ocelot) (As an API Gateway library)
#### 2 - [FluentValidation](https://fluentvalidation.net/) (For building strongly-typed validation rules)
#### 3 - [Swashbuckle](https://github.com/domaindrivendev/Swashbuckle.AspNetCore) (For API documentation based on Swagger and OpenAPI specification)
#### 4 - [MassTransit](https://masstransit-project.com/) (For creating distributed application based on both RabbitMQ/Microsoft Azure(Interchangeable) - because of the US sanctions, we are restricted to use Microsoft Azure services in Iran but as I already have studied about such services, I designed this part to be changable with RabbitMQ. So during the test time we can test it with rabbitmq and for production, we can set it to use Azure)
#### 5 - [SQLite](https://www.sqlite.org/index.html) For database
#### 6 - [Application Insight](https://docs.microsoft.com/en-us/azure/azure-monitor/app/app-insights-overview) Microsoft's application performance management solution for monitoring and analysing application data such as page load times, server performance counters, dependency wait times and more. Can be used with both azure and localhost and again due to the sanctions, I couldn't test it practically on Azure but it can be simply change to work with that.

### Front-end part
#### 1 - [Angular v8](https://angular.io/) For creating UI
#### 2 - [Bootstrap](https://getbootstrap.com/) For applying styles (The template is very simple but it can be improved if I decide to continue this project)

## How to start it

#### 1 - You have to run this command to create db. Make sure you have set "ApiGateway" project as Startup then put "AzureBasedMicroservice.EntityFramework" as default project in package manager console and then run update-database. It will create a database called "SampleDb.db" in drive D:\
#### 2 - After being sure from creating the database, set the solution to start multiple project. The projects we need to be run are "ApiGateway" and all three project located in "Services" directory. Then run the projects or press F5
#### 3 - Go to folder "frontend" and run "ng serve --open". Note: For running the front-end project, you have to have Angular installed on your local system so if you don't, please at first run this command "npm install -g @angular/cli" then repeat the step 3.
