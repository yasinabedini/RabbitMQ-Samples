using RabbitMQ.Client;
using RabbitMQ.Client.Events;

var connectionFactory = new ConnectionFactory()
{
    HostName = "localhost"
};
using var connection = connectionFactory.CreateConnection();
using var channel = connection.CreateModel();

string exchangeName = "FanoutMailBoxExchange";
channel.ExchangeDeclare(exchangeName, ExchangeType.Fanout, false, false);

Console.Write("Enter Your Message : ");
string message = Console.ReadLine() ?? "Default message";

var messageByte = System.Text.Encoding.UTF8.GetBytes(message);

channel.BasicPublish(exchangeName, "", null, messageByte);
Console.WriteLine("Your Message Sent");
Console.ReadKey();