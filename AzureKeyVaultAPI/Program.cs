using Azure.Identity;
using AzureKeyVaultAPI.Service;
using Microsoft.Extensions.Azure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddAzureClients(AzureClientBuilderExtensions => {
    AzureClientBuilderExtensions.AddSecretClient(builder.Configuration.GetSection("AZVaultDBKeyUrl"));
});

builder.Services.AddSingleton<ISecretKeyService, SecretKeyService>();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseAuthorization();

app.MapControllers();

app.Run();
