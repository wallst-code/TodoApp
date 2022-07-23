using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using ToDoLibrary.DataAccess;
using ToDoAPI.StartupConfig;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.AddStandardServices();
builder.AddCustomServices();
builder.AddAuthenticationServices();
builder.AddHealthCheckServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication(); // RULE: the order here is important Authentication before Authorization. 
app.UseAuthorization();

app.MapControllers();
app.MapHealthChecks("/health").AllowAnonymous();

app.Run();
