namespace Contracts.ServiceContracts;
using Shared.DTOs.UserDTOs;
using Shared;
using Entities;

public interface IUserService
{
    public Task<GenericResult<UserDTO>> RegisterUser(UserDTO user);
    public Task<GenericResult<TokenDTO>> LoginUser(LoginDTO user);

    public string GenerateJwtToken(User user);
    public string GenerateRefreshToken();

    public  Task<GenericResult<string>> ReValidateRefreshToken(RefreshTokenDTO tokens);
    public Task<GenericResult<RefreshTokenDTO>> ReAssignTokens(RefreshTokenDTO tokens);
}