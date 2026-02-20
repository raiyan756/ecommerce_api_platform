using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.InvalidModelStateResponseFactory = context =>
    {
        var error = context.ModelState
        .Where(e=>e.Value!=null && e.Value.Errors.Count>0)
        .Select(e => new
        {
            Field = e.Key,
            error =e.Value != null ? e.Value.Errors.Select(x=>x.ErrorMessage):new string[0].ToArray()
            
        }).ToList();

        return new BadRequestObjectResult(new
        {
            message = "Validation Error",
            Errors = error
        });
        
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
