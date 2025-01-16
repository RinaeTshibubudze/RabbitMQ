// See https://aka.ms/new-console-template for more information
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

Console.WriteLine("Welcome to the ticketing service");

var factory = new ConnectionFactory()
{
    HostName = "localhost",
    UserName = "guest",
    Password = "guest",
    VirtualHost = "/"
};

var conn = factory.CreateConnection();

using var channel = conn.CreateModel();


channel.QueueDeclare(
                    queue: "bookings",
                    durable: true,
                    exclusive: false,
                    autoDelete: false
                );

var consumer = new EventingBasicConsumer(channel);

consumer.Received += (model, eventArgs) =>
{
    // Getting my Bytes[]

    var body = eventArgs.Body.ToArray();

    var message = Encoding.UTF8.GetString(body);

    Console.WriteLine($"Received message: {message}");
};

channel.BasicConsume("bookings", true, consumer);

Console.ReadKey();

