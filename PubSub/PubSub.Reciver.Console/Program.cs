﻿using RabbitMQ.Client;
using RabbitMQ.Client.Events;

var connectionFactory = new ConnectionFactory()
{
    HostName = "localhost"
};
using var connection = connectionFactory.CreateConnection();
using var channel = connection.CreateModel();

string exchangeName = "FanoutMailBoxExchange";
channel.ExchangeDeclare(exchangeName, ExchangeType.Fanout, false, false);

var queueName = channel.QueueDeclare().QueueName;
channel.QueueBind(queueName, exchangeName, "");

var consumer = new EventingBasicConsumer(channel);
consumer.Received += Consumer_Received;

channel.BasicConsume(queueName, false, consumer);

Console.WriteLine("Press Key To Exit.....");
Console.ReadLine();
void Consumer_Received(object? sender, BasicDeliverEventArgs e)
{
    var body = e.Body.ToArray();
    string messageString = System.Text.Encoding.UTF8.GetString(body);
    Console.WriteLine($"Console Recived : {messageString}");
}