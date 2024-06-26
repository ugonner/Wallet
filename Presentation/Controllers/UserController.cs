namespace Presentation.Controllers;

using Microsoft.AspNetCore.Mvc;
using Contracts.ServiceContracts;
using Shared;
using Shared.DTOs.UserDTOs;

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

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody()] LoginDTO payload)
    {
        var res = await _serviceManager.UserService.LoginUser(payload);
        return Ok(res);
    }

}