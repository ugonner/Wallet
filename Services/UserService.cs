namespace Services;
using Contracts.RepositoryContracts;
using Contracts.ServiceContracts;
using Shared;
using Shared.DTOs.UserDTOs;
using AutoMapper;
using Entities;
using System.IdentityModel.Tokens.Jwt;

internal sealed class UserService : IUserService
{
    private readonly IRepositoryManager repositoryManager;
    private readonly IMapper mapper;

    public UserService(IRepositoryManager _repositoryManager, IMapper _mapper)
    {
        repositoryManager = _repositoryManager;
        mapper = _mapper;
    }

    public async Task<GenericResult<UserDTO>> RegisterUser(UserDTO user)
    {
        var mappedUser = mapper.Map<User>(user);
        repositoryManager.UserRepository.CreateUser(mappedUser);
        return new GenericResult<UserDTO>().Successed("user created", 201, user);
    }
    
    public async Task<GenericResult<TokenDTO>> LoginUser(LoginDTO user)
    {
        var usr = await repositoryManager.UserRepository.GetOne((User u) => (u.UserName == user.UserName && u.Password == user.Password));
        if(usr is null) return new GenericResult<TokenDTO>().Errored("no user found, check login credentials", 404);
        var jwtHandler = new JwtSecurityTokenHandler();
        
        return new GenericResult<TokenDTO>().Successed("user created", 200, new TokenDTO{Token = "ugoo"});
    }
}