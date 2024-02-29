using RabbitMQ.Client;

var connectionFactory = new ConnectionFactory()
{
    HostName = "localhost"
};

using var connection = connectionFactory.CreateConnection();
using var channel = connection.CreateModel();

string queueName = "testRabbitMQ";
channel.QueueDeclare(queueName, false, false, false, null);

Console.Write("Enter Yout Message : ");
string message = Console.ReadLine() ?? "Default Message";

var properties = channel.CreateBasicProperties();
properties.Persistent = true;

var messageBytes = System.Text.Encoding.UTF8.GetBytes(message);
channel.BasicPublish("", queueName, properties, messageBytes);

Console.WriteLine("Your Message Sent.....");
Console.WriteLine();
Console.WriteLine("Press Enter to close console....");
Console.ReadKey();