using Application;
using Infraestructure;
using Web.API;
using Web.API.Extensions;

var builder = WebApplication.CreateBuilder(args);


// injectando las dependencias
builder.Services
        .AddPresentation()
        .AddInfraestructure(builder.Configuration)
        .AddApplication();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.ApplyMigrations();
}

app.UseExceptionHandler("/error");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
