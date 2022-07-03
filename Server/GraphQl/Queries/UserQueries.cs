using GraphQL.Types;
using Server.Business.Interfaces;
using Server.GraphQl.GraphTypes;
using GraphQL;
using GraphQL.Utilities;
using Server.Business.Entities;
using Server.GraphQl.InputTypes;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Server.Services;

namespace Server.GraphQl.Queries
{
    public class UserQueries : ObjectGraphType
    {
        private readonly IUserRepository userRepository;
        private readonly ITokenService tokenService;
        public UserQueries(IUserRepository userRepository, ITokenService tokenService)
        {
            this.userRepository = userRepository;
            this.tokenService = tokenService;
            Field<UserType>(
                 "UserById",
                 "Returns User By Id",
                 new QueryArguments(new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "userId" }),
                 resolve: context =>
                 {
                     var userId = context.GetArgument<int>("userId");
                     User user = userRepository.GetById(userId);
                     return user;
                 });
            Field<AuthenticatedResponseType>(
                "LoginUser",
                "LoginsUserAndReturnsToken",
                new QueryArguments(new QueryArgument<NonNullGraphType<LoginInputType>> { Name = "loginInput" }),
                resolve: context =>
                {
                    var userInput = context.GetArgument<LoginInput>("loginInput");
                    //var user = userRepository.GetByEmail(userInput.Email);
                    //var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKey@345"));
                    //var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
                    //var tokenOptions = new JwtSecurityToken(
                    //    issuer: "https://localhost:7301",
                    //    audience: "https://localhost:5001",
                    //    claims: new List<Claim>(),
                    //    expires: DateTime.Now.AddMinutes(5),
                    //    signingCredentials: signinCredentials
                    //);
                    //var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, userInput.Email),
                        new Claim(ClaimTypes.Role, "User")
                    };
                    var accessToken = tokenService.GenerateAccessToken(claims);
                    var refreshToken = tokenService.GenerateRefreshToken();
                    return new AuthenticatedResponse()
                    {
                        Token = accessToken.Token,
                        RefreshToken = refreshToken,
                        ValidTo = accessToken.ExpiryDate
                    };
                });
            Field<AuthenticatedResponseType>(
                "RefreshToken",
                "Refreshes token when date expires",
                new QueryArguments(new QueryArgument<NonNullGraphType<RefreshTokenInputType>> { Name = "refreshInput" }),
                 resolve: context =>
                 {
                     var refreshTokenInput = context.GetArgument<RefreshTokenInput>("refreshInput");
                     var accessToken = refreshTokenInput.AccesToken;
                     var refreshToken = refreshTokenInput.RefreshToken;
                     var principal = tokenService.GetPrincipalFromExpiredToken(accessToken);
                     var userEmail = principal.Identity.Name;
                     var user = userRepository.GetByEmail(userEmail);
                     var newAccesToken = tokenService.GenerateAccessToken(principal.Claims);
                     var newRefreshToken = tokenService.GenerateRefreshToken();
                     return new AuthenticatedResponse()
                     {
                         Token = newAccesToken.Token,
                         RefreshToken = newRefreshToken,
                         ValidTo = newAccesToken.ExpiryDate
                     };
                 });
        }
    }
}
