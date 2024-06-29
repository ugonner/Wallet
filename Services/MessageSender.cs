using System.Text.Json;
using Microsoft.Extensions.Configuration;
using Shared;

namespace Services;

public class MessageSender<TMessage, TResult>
{
    public static async Task<GenericResult<TResult>> SendMessage(RequestMessageDTO<TMessage> messagePayload)
    {
        var result = new GenericResult<TResult>();

        try
        {
            IConfiguration config = GlobalConfiguration.GlobalConfigurationSettings;
            var hostname = config.GetSection("rpc")["rpc_hostname"];
            var jsonMessage = JsonSerializer.Serialize(messagePayload);
            GenericResult<TResult> resObj;
            using (var rpcClinnt = new RabbitMqRpcClient(hostname: hostname))
            {
                var resultString = await rpcClinnt.CallAsync(jsonMessage);
                resObj = JsonSerializer.Deserialize<GenericResult<TResult>>(resultString);
            }
            return resObj;

        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error sending and receiing result {ex.Message}");
            return result.Errored("something went wrong, communicating with service", 500);
        }
    }
}