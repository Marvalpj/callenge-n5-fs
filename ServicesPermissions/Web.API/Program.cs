using Application;
using Infraestructure;
using Web.API;

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
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
