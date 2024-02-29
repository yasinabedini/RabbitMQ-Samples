using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

var connectionFactory = new ConnectionFactory()
{
    HostName = "localhost"
};

var connection = connectionFactory.CreateConnection();
var channel = connection.CreateModel();

string queueName = "testRabbitMQ";
channel.QueueDeclare(queueName, false, false, false, null);

var consumer = new EventingBasicConsumer(channel);
consumer.Received += Consumer_Recived;

channel.BasicConsume(queueName, false, consumer);

Console.WriteLine("Press Key To Exit.....");
Console.ReadLine();

void Consumer_Recived(object? sender, BasicDeliverEventArgs e)
{
    var body = e.Body.ToArray();
    var stringMessage = Encoding.UTF8.GetString(body);

    Console.WriteLine($"Message Recived : {stringMessage}");
    channel.BasicAck(e.DeliveryTag, false);
}