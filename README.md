# dotnetCore-rabbitMQ-massTransit
This project is to demonstrate how to configure RabbitMQ using Masstransit and dotnet core 7.0

# Pre-requisites
1. Install Dotnet SDK 7.0
2. Install Docker Desktop for Windows

## RabbitMQ Setup
1. Run the following command to start the RabbitMQ server
```docker compose up```
2. Open the RabbitMQ management console at http://localhost:15672
3. Login with the following credentials
```username: guest```
```password: guest```

## Project Setup
1. Run the following command to restore the packages
```dotnet restore```
2. Run the following command to build the project
```dotnet build```
3. Run the following command to run the project
```dotnet run```

### using vscode
1. Open the folder in vscode
2. Goto Run and Debug
3. Select the Web/worker project and click on the play button

## Testing the project
1. Open the RabbitMQ management console at http://localhost:15672
2. Login with the following credentials
```username: guest```
```password: guest```
3. Goto Queues and click on the queue name
4. launch https://localhost:5026
5. Enter the message and click on publish a message
6. You should see the message in the integrated terminal

## To test Deduplication
1. launch https://localhost:5026
2. stop the worker
3. Enter the message and click on publish a message
4. Click the button again with same message
5. You should see the error in web page and console
