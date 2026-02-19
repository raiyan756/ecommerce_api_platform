using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
var app = builder.Build();


// Configure the HTTP request pipeline.

app.UseHttpsRedirection();



if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}



app.MapGet("/", () =>
{
    return "Api is working!";
});

app.MapControllers();
app.Run();
