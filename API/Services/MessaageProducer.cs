using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using RabbitMQ.Client;


namespace API.Services
{
    public class MessaageProducer : IMessageProducer
    {
        public void SendingMessage<T>(T message)
        {
            var factory = new ConnectionFactory()
            {
                HostName = "localhost",
                UserName = "guest",
                Password = "guest",
                VirtualHost = "/"
            };

            var conn = factory.CreateConnection();

            using var channel = conn.CreateModel();


            channel.QueueDeclare("bookings",
                                 durable: true,
                                 exclusive: true
                                 );

            var json = JsonSerializer.Serialize(message);
            var body = Encoding.UTF8.GetBytes(json);

            channel.BasicPublish("", "bookings", body: body);

        }
    }
}