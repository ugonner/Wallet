using System.Text.Json;
using Microsoft.Extensions.Configuration;
using Shared;

namespace Services;

public class MessageSender<TMessage, TResult>
{
    public static async Task<GenericResult<TResult>> SendMessage(RequestMessageDTO<TMessage> messagePayload)
    {
        IConfiguration config = GlobalConfiguration.GlobalConfigurationSettings;
        var hostname = config.GetSection("rpc")["rpc_hostname"];
        var jsonMessage = JsonSerializer.Serialize(messagePayload);
        var result = new GenericResult<TResult>();
        TResult resObj;
        using (var rpcClinnt = new RabbitMqRpcClient(hostname: hostname))
        {
            var resultString = await rpcClinnt.CallAsync(jsonMessage);
            resObj = JsonSerializer.Deserialize<TResult>(resultString);
        }
        return result.Successed("success", 200, resObj);

    }
}