using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Net;
using System.Security.Claims;
using System.Text;
using WebApiDempApp.Data;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<WebApiDempAppContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("WebApiDempAppContext") ?? throw new InvalidOperationException("Connection string 'WebApiDempAppContext' not found.")));


// Add services to the container.
#region --------- Add JWT authentication----------------------
var key = Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:SecretKey"]);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme) //Specify Authentication Scheme
.AddJwtBearer(options =>
{ //The token is validated by the authentication middleware based on the configuration specified in AddJwtBearer()
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        //This section specifies what fields are used to validate token
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false
    };

    options.Events = new JwtBearerEvents
    {       
        OnTokenValidated = async context =>
        {
            var req = context.Request;
            Console.Write("Token successfully validated");
        },
        OnChallenge = async context =>
        {
            context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            context.Response.ContentType = "text/plain";
            await context.Response.WriteAsync("Not Authorized");
            context.HandleResponse();           
        }
    };

});
builder.Services.AddHttpClient();
builder.Services.AddAuthorization();
#endregion 

builder.Services.AddControllers();



// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();//This is required to Make API calls to Authentication API 

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
