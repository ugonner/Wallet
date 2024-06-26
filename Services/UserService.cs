namespace Services;
using Contracts.RepositoryContracts;
using Contracts.ServiceContracts;
using Shared;
using Shared.DTOs.UserDTOs;
using AutoMapper;
using Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text.Json;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using System.Data.SqlTypes;
using System.Text;


internal sealed class UserService : IUserService
{
    private readonly IRepositoryManager repositoryManager;
    private readonly IMapper mapper;
    private User _user;

    public UserService(IRepositoryManager _repositoryManager, IMapper _mapper)
    {
        repositoryManager = _repositoryManager;
        mapper = _mapper;
    }


    public IConfiguration _configSettings = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json").Build();

    public async Task<GenericResult<UserDTO>> RegisterUser(UserDTO user)
    {
        var mappedUser = mapper.Map<User>(user);
        mappedUser.Password = BCrypt.Net.BCrypt.EnhancedHashPassword(mappedUser.Password);
        mappedUser.RoleId = 3;
        repositoryManager.UserRepository.CreateUser(mappedUser);
        await repositoryManager.Save();
        return new GenericResult<UserDTO>().Successed("user created", 201, user);
    }

    public async Task<GenericResult<TokenDTO>> LoginUser(LoginDTO user)
    {
        GenericResult<TokenDTO> result = new GenericResult<TokenDTO>();
        var usr = await repositoryManager.UserRepository.FindUser((User u) => (u.UserName == user.UserName));
        if (usr is null) return new GenericResult<TokenDTO>().Errored("no user found, check login credentials", 404);
        if(!BCrypt.Net.BCrypt.Verify(user.Password, usr.Password)) return result.Errored("Wrong credentials", 400);
        return result.Successed("user created", 200, new TokenDTO {
            Token = GenerateJwtToken(usr),
            RefreshToken = GenerateRefreshToken()
        });
    }

    public string GenerateJwtToken(User user)
    {

        var jwtHandler = new JwtSecurityTokenHandler();
        var jwtSettings = _configSettings.GetSection("jwt");

        var key = Encoding.ASCII.GetBytes($"{jwtSettings["key"]}");

        Claim[] claims = new Claim[]{
            new Claim(ClaimTypes.UserData, JsonSerializer.Serialize(user)),
            new Claim(ClaimTypes.Role, user.RoleId.ToString())
            };
        var tokenDescription = new SecurityTokenDescriptor()
        {
            Subject = new ClaimsIdentity(claims),
            Issuer = jwtSettings["issuer"],
            Audience = jwtSettings["audience"],
            Expires = DateTime.UtcNow.AddMinutes(30),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256),
        };
        var token = jwtHandler.CreateToken(tokenDescription);
        return jwtHandler.WriteToken(token);
    }

    public string GenerateRefreshToken() => BCrypt.Net.BCrypt.EnhancedHashPassword("haaneaanac");

    public async Task<GenericResult<string>> ReValidateRefreshToken(RefreshTokenDTO tokens)
    {
        var result = new GenericResult<string>();
        _user = await repositoryManager.UserRepository.FindOne((u) => u.RefreshToken == tokens.RefreshToken, false);
        if (_user == null) return result.Errored("no such refresh token exists", 404);


        var jwtSettings = _configSettings.GetSection("jwt");

        var key = Encoding.ASCII.GetBytes($"{jwtSettings["key"]}");

        var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();

        var validationParameters = new TokenValidationParameters()
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key)
        };

        SecurityToken securityToken;
        var principal = jwtSecurityTokenHandler.ValidateToken(tokens.Token, validationParameters, out securityToken);
        if (securityToken == null) return result.Errored("NOT A VALID REFRESH TOKEN", 400);
        return result.Successed("token valid", 200, principal.ToString());

    }

    public async Task<GenericResult<RefreshTokenDTO>> ReAssignTokens(RefreshTokenDTO tokens)
    {
        GenericResult<RefreshTokenDTO> result = new GenericResult<RefreshTokenDTO>();

        var res =  await ReValidateRefreshToken(tokens);
        var newRefreshToken = GenerateRefreshToken();
        if(res.Status)
        {
            _user.RefreshToken = newRefreshToken;
            repositoryManager.UserRepository.Update(_user);
            await repositoryManager.Save();
            return result.Successed("token updated", 200, new RefreshTokenDTO()
            {
                RefreshToken = newRefreshToken,
                Token = GenerateJwtToken(_user)
            });
        }
        return result.Errored(res.Message, res.StatusCode);

    }


}