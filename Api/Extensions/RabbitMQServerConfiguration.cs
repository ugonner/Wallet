using Services;

namespace Api.Extensions;

public static class RabbitMQServerConfiguration
{
    public static void ConfigureRabbitMQService(this IServiceCollection service) => service.AddHostedService<RabbitMqRpcServer>();
    public static void ConfigureRabbitMQServerInit(this IServiceCollection service, IConfiguration configuration)
    {
        var rpcSettings = configuration.GetSection("rpc");
        service.AddSingleton<RabbitMqRpcServer>(
            (opt) => new RabbitMqRpcServer(hostname: rpcSettings["rpc_hostname"], queueName: rpcSettings["rpc_queue"])
        );
    }
}