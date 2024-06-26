namespace Contracts.ServiceContracts;
using Shared.DTOs.UserDTOs;
using Shared;
public interface IUserService
{
    public Task<GenericResult<UserDTO>> RegisterUser(UserDTO user);
    public Task<GenericResult<TokenDTO>> LoginUser(LoginDTO user);
}