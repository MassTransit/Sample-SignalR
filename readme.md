Sample SignalR
==============

This sample will show a variety of built in tools and techniques in MassTransit.

## Requirements ##

A message broker. This sample provides a docker-compose.yml which uses RabbitMq (broker) and MsSql (db). If you have rabbitMq locally installed, you can skip the first step.

## Easy Steps ##

1. in the directory with the docker-compose.yml run the command `docker-compose up -d`
2. After complete, give it a few seconds, and you can browse to  http://localhost:15672, and view the rabbitmq management console
3. go into the src/SampleSignalR.Service/ directory and type  `dotnet run --console`
4. in another command window, go into src/SampleSignalR.Mvc and type `dotnet run --launch-profile sample1`
5. in a thihrd command window, go into src/SampleSignalR.Mvc and type `dotnet run --launch-profile sample2`
6. Browse to http://localhost:5100
7. In another browser tab, go to http://localhost:5200

## Message from one tab to another, and console app ##

The reason we run two profiles is to simulate horizontal scaling. If you have two web hosts, any message sent on signalr backplane should be sent to all services on the backplane. And lastly, the console app can simulate a backend service that wants to send events directly to the SignalR javascript methods.

When you are all done, run `docker-compose down`
