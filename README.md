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

### Front-end part
#### 1 - [Angular v8](https://angular.io/) For creating UI
#### 2 - [Bootstrap](https://getbootstrap.com/) For applying styles (The template is very simple but it can be improved if I decide to continue this project)
