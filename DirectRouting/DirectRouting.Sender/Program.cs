﻿using RabbitMQ.Client;
using RabbitMQ.Client.Events;

var connectionFactory = new ConnectionFactory()
{
    HostName = "localhost"
};
using var connection = connectionFactory.CreateConnection();
using var channel = connection.CreateModel();

string exchangeName = "mailBoxExchange";
channel.ExchangeDeclare(exchangeName, ExchangeType.Direct, false, false);

Console.Write("Enter Your Message : ");
string message = Console.ReadLine() ?? "Default message";

var messageByte = System.Text.Encoding.UTF8.GetBytes(message);

Console.Write("sms or email ? ");
string routeKey = Console.ReadLine() ?? "sms";

channel.BasicPublish(exchangeName, routeKey, null, messageByte);
Console.WriteLine("Your Message Sent");
Console.ReadKey();