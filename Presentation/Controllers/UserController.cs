namespace Presentation.Controllers;

using Microsoft.AspNetCore.Mvc;
using Contracts.ServiceContracts;
using Shared;
using Shared.DTOs.UserDTOs;
using Services;

[ApiController]
[Route("{Controller}")]
public class UserController : ControllerBase
{
    IServiceManager _serviceManager;
    public UserController(IServiceManager serviceManager)
    {
        _serviceManager = serviceManager;
    }

    [HttpPost()]
    public async Task<IActionResult> Register([FromBody] UserDTO payload)
    {
        
        GenericResult<UserDTO> result = await _serviceManager.UserService.RegisterUser(payload);
        Console.WriteLine($"result: {result.Data}");
        return Ok(result);
    }

    [HttpGet()]
    public IActionResult Go()
    {
        Console.WriteLine("sending");
        return Ok("good luck");
    }


    [HttpGet("go")]
    public async Task<IActionResult> Login()
    {

        var res = await MessageSender<string, string>.SendMessage(new RequestMessageDTO<string>(){
            EventName = "ugonna hello",
            Data = "i am here"
        });
        return Ok(res);
        
        
    }

}