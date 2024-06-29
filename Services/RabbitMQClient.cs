using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Services;
using System;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

public class RabbitMqRpcClient : IDisposable
{
    private readonly IConnection _connection;
    private readonly IModel _channel;
    private readonly string _replyQueueName;
    private readonly EventingBasicConsumer _consumer;
    private readonly IBasicProperties _props;
    private readonly TaskCompletionSource<string> _responseTask;

    private readonly  string _consumerQueueName = GlobalConfiguration.GlobalConfigurationSettings.GetSection("rpc")["rpc_queue"];
    public RabbitMqRpcClient(string hostname)
    {
        try
        {
            
        var factory = new ConnectionFactory() { HostName = hostname };
        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();
        _replyQueueName = _channel.QueueDeclare().QueueName;
        _consumer = new EventingBasicConsumer(_channel);
        _responseTask = new TaskCompletionSource<string>();

        var correlationId = Guid.NewGuid().ToString();
        _props = _channel.CreateBasicProperties();
        _props.CorrelationId = correlationId;
        _props.ReplyTo = _replyQueueName;

        _consumer.Received += (model, ea) =>
        {
            if (ea.BasicProperties.CorrelationId == correlationId)
            {
                var response = Encoding.UTF8.GetString(ea.Body.ToArray());
                _responseTask.SetResult(response);
            }
        };

        _channel.BasicConsume(consumer: _consumer, queue: _replyQueueName, autoAck: true);
        }
        catch(Exception ex)
        {
            Console.WriteLine($"Error instantiating client {ex.Message}");
        }
    }

    public async Task<string> CallAsync(string message)
    {
        var body = Encoding.UTF8.GetBytes(message);
        
        _channel.BasicPublish(exchange: "", routingKey: _consumerQueueName, basicProperties: _props, body: body);

        return await _responseTask.Task;
    }

    public void Dispose()
    {
        _channel.Close();
        _connection.Close();
    }
}
