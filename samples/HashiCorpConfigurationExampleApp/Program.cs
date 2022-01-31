using HashiCorpConfiguration.Extensions;
using HashiCorpConfiguration.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Configuration.AddVaultConfiguration(new VaultConfigSettings
{
    //Set up your vault settings here
    Token = "Your Token",
    ServerUriWithPort = "Your port",
    MountPoint = "Your mount point",
    Path = "Path to your secrets"
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();