using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<CategoryServices>();
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.InvalidModelStateResponseFactory = context =>
    {
        var error = context.ModelState
        .Where(e=>e.Value!=null && e.Value.Errors.Count>0)
        .SelectMany(e => e.Value.Errors.Select(x =>x.ErrorMessage)).ToList();

        return new BadRequestObjectResult(ApiResponses<object>.ErrorResponse(error,400,"Validation error"));
    };
    
});
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
