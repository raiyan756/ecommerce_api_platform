var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


// Configure the HTTP request pipeline.

app.UseHttpsRedirection();



if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

List<Category> categories = new List<Category>(); 

app.MapGet("/", () =>
{
    return "Api is working!";
});
//read categories
app.MapGet("/api/categories", () =>
{
    
    return Results.Ok(categories);
});

app.MapPost("/api/categories", () =>
{

    var category = new Category
    {
        CategoryId = Guid.NewGuid(),
        CategoryName = "Electronics",
        Description = "Devices and gadgets",
        CreatedAt = DateTime.UtcNow
    };
    categories.Add(category);
    return Results.Created($"/api/categories/{category.CategoryId}", category);
});

app.MapDelete("/api/categories", () =>
{
    var removeCateory = categories.FirstOrDefault(c => c.CategoryId == Guid.Parse("60dac264-5c74-4b22-b002-81ae92fb659f"));  
    if (removeCateory != null)
    {
        categories.Remove(removeCateory);
        return Results.Ok("Category removed");
    }
    return Results.NotFound("Category not found");
});
// error found in the put method, it is not working, i will check it later and update it.
app.MapPut("/api/categories",()=>{

    var foundCategory = categories.FirstOrDefault(c=>c.CategoryId == Guid.Parse("60dac264-5c74-4b22-b002-81ae92fb659f"));
    if (foundCategory == null){
        return Results.NotFound("Category not found");
    
    }
    foundCategory.CategoryName = "Updated Electronics";
    foundCategory.Description = "Updated description for devices and gadgets";
    return Results.NoContent();
});
//

app.Run();

//dto is the template for the product.

public record Category
{
    
    public Guid CategoryId { get; set; }
    public string? CategoryName { get; set; }

    public string? Description { get; set; }

    public DateTime CreatedAt { get; set; }
}; 

//crud operations for the product.