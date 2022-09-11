using Azure.Extensions.AspNetCore.Configuration.Secrets;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Google.Apis.Auth.AspNetCore3;
using MediatR;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Identity;
using System.Reflection;
//using Microsoft.AspNetCore.Identity;
using WebApi;
using WebApi.Hub;
using WebApi.Mock;
using WebApi.Service;

var builder = WebApplication.CreateBuilder(args);

builder.Host.ConfigureAppConfiguration((context, config)
    =>
        {
            var builtConfiguration = config.Build();
            string kvURL = builtConfiguration["KeyVaultConfig:KVUrl"];
            string tenantId = builtConfiguration["KeyVaultConfig:TenantId"];
            string clientId = builtConfiguration["KeyVaultConfig:ClientId"];
            string clientSecret = builtConfiguration["KeyVaultConfig:ClientSecretId"];

            var credential = new ClientSecretCredential(tenantId, clientId, clientSecret);
            var client = new SecretClient(new Uri(kvURL), credential);
            config.AddAzureKeyVault(client, new AzureKeyVaultConfigurationOptions());

        });


builder.Services.AddSignalR();



// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.OperationFilter<SwaggerFileOperationFilter>();
});
//builder.Services.AddApplication();

builder.Services.AddCors(options =>
{
    options.AddPolicy("ClientPermission", policy =>
    {
        policy.AllowAnyHeader()
            .AllowAnyMethod()
            .WithOrigins("http://localhost:3000")
            .AllowCredentials();
    });
});


//Identity 
//builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
//    .AddCookie();


//Mediator

builder.Services.AddMediatR(Assembly.GetExecutingAssembly());
//Mediator


builder.Services.AddAuthentication(options =>
{
    //options.DefaultAuthenticateScheme = GoogleDefaults.AuthenticationScheme;
    //options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;


    options.DefaultChallengeScheme = GoogleOpenIdConnectDefaults.AuthenticationScheme;
    // This forces forbid results to be handled by Google OpenID Handler, which checks if
    // extra scopes are required and does automatic incremental auth.
    options.DefaultForbidScheme = GoogleOpenIdConnectDefaults.AuthenticationScheme;
    // Default scheme that will handle everything else.
    // Once a user is authenticated, the OAuth2 token info is stored in cookies.
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;



}).
AddCookie()
.AddGoogleOpenIdConnect(options =>
{
    options.ClientId = "897095587174-rgveh4qsn4rr7mkiccq4ago41k2tv1dn.apps.googleusercontent.com";
    options.ClientSecret = "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";

});

//builder.Services
//    .AddAuthorization(o =>
//{
//    o.AddPolicy("FirstStepCompleted", policy => policy.RequireClaim("FirstStepCompleted"));
//    o.AddPolicy("Authorized", policy => policy.RequireClaim("Authorized"));
//});
var startup = new Startup(builder.Configuration);
startup.ConfigureServices(builder.Services); // calling ConfigureServices method
var app = builder.Build();
//startup.Configure(app, builder.Environment);



// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}


app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.MapHub<ChatHub>("/hubs/chat");
app.UseCors("ClientPermission");

//app.UseExtraRoutes();

app.Run();

public partial class Program { }