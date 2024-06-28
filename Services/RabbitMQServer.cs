using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Text.Json;

namespace Services;
public class RabbitMqRpcServer : IHostedService
{
    private readonly IConnection _connection;
    private readonly IModel _channel;

    public RabbitMqRpcServer(string hostname, string queueName)
    {
        var factory = new ConnectionFactory() { HostName = hostname};
        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();
        _channel.QueueDeclare(queue: queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);

        }

    
    public Task StartAsync(CancellationToken cancellationToken)
    {
        var consumer = new EventingBasicConsumer(_channel);
        consumer.Received += (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            var props = ea.BasicProperties;
            var replyProps = _channel.CreateBasicProperties();
            replyProps.CorrelationId = props.CorrelationId;

            // Process the message (for demonstration, just echoing it back)
            var responseMessage = ProcessMessage(message);
            var responseBytes = Encoding.UTF8.GetBytes(responseMessage);

            _channel.BasicPublish(exchange: "", routingKey: props.ReplyTo, basicProperties: replyProps, body: responseBytes);
        };

        _channel.BasicConsume(queue: "rpc_queue", autoAck: true, consumer: consumer);
        return Task.CompletedTask;
    }

public Task StopAsync(CancellationToken cancellationToken)
{
    _channel.Close();
    _connection.Close();
    return Task.CompletedTask;
}
    private string ProcessMessage(string message)
    {
        // Add your processing logic here
        Console.WriteLine($"Received message: {message}");
        return $"Processed: {message}";
    }

    public void Close()
    {
        _channel.Close();
        _connection.Close();
    }
}
