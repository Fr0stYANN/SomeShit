using GraphQL.Server;
using Server.Business.Interfaces;
using Server.GraphQl.AppSchema;
using Server.GraphQl.Queries;
using GraphQL;
using GraphQL.SystemTextJson;
using Server.MSSQL.Repositories;
using GraphQL.Types;
using Server.GraphQl.GraphTypes;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Server.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<AppSchema>();
builder.Services.AddScoped<ITokenService, TokenService>();
//builder.Services.AddScoped<UserQueries>();
//builder.Services.AddScoped<UserType>();
//builder.Services.AddScoped<IDocumentExecuter, DocumentExecuter>();
builder.Services.AddGraphQL()
       .AddSystemTextJson()
       .AddGraphTypes(typeof(AppSchema), ServiceLifetime.Scoped)
        .AddErrorInfoProvider(opt => opt.ExposeExceptionStackTrace = true);
builder.Services.AddAuthentication(opt => {
    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "https://localhost:7301",
            ValidAudience = "https://localhost:5001",
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKey@345"))
        };
    });

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();
app.UseAuthorization();
app.UseAuthentication();
app.MapControllers();
app.UseGraphQL<AppSchema>();
app.Run();
