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

    [HttpGet()]
    public async Task<IActionResult> Login([FromBody] LoginDTO payload)
    {
        
        GenericResult<TokenDTO> result = await _serviceManager.UserService.LoginUser(payload);
        Console.WriteLine($"result: {result.Data}");
        return Ok(result);
    }

}