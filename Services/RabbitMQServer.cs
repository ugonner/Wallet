using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Shared;
using System;
using System.Text;
using System.Text.Json;

namespace Services;
public class RabbitMqRpcServer : IHostedService
{

    private readonly IConnection _connection;
    private readonly IModel _channel;
    private readonly string _queueName;


    public RabbitMqRpcServer()
    {
        try
        {
            var configuration = GlobalConfiguration.GlobalConfigurationSettings;
            var hostname = configuration.GetSection("rpc")["rpc_hostname"];
            var queueName = configuration.GetSection("rpc")["rpc_queue"];
            _queueName = queueName;
            Console.WriteLine($"CONNECTING RABBITMQ ON: {_queueName}");

            //ProduceQueue(hostname: hostname, queueName: queueName);
            var factory = new ConnectionFactory() { HostName = hostname };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.QueueDeclare(queue: queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);

        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error connecting {ex.Message}");
        }
    }

    private Task StartServer()
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

        _channel.BasicConsume(queue: _queueName, autoAck: true, consumer: consumer);
        return Task.CompletedTask;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        await StartServer();
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
        var res = new GenericResult<string>().Successed("take your message", 200, "done testing");
        return JsonSerializer.Serialize(res);
    }

    public void Close()
    {
        _channel.Close();
        _connection.Close();
    }
}
