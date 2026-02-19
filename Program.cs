using Microsoft.AspNetCore.Mvc;

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
app.MapGet("/api/categories", ([FromQuery] string? searchValue=" ") =>
{
    if(!string.IsNullOrEmpty(searchValue)){
        var filteredCategories = categories.Where(c=> c.CategoryName != null && c.CategoryName.Contains(searchValue, StringComparison.OrdinalIgnoreCase)).ToList();
        return Results.Ok(filteredCategories);
    }
    
    return Results.Ok(categories);
});

app.MapPost("/api/categories", ([FromBody] Category category) =>
{
    if(string.IsNullOrEmpty(category.CategoryName)){
        return Results.BadRequest("Category name is required");
    }

    var newcat = new Category
    {
        CategoryId = Guid.NewGuid(),
        CategoryName = category.CategoryName,
        Description = category.Description,
        CreatedAt = DateTime.UtcNow
    };
    categories.Add(newcat);
    return Results.Created($"/api/categories/{newcat.CategoryId}", newcat);
});

app.MapDelete("/api/categories/{categoryId:guid}", (Guid categoryId) =>
{
    var removeCateory = categories.FirstOrDefault(c => c.CategoryId == categoryId);  
    if (removeCateory != null)
    {
        categories.Remove(removeCateory);
        return Results.Ok("Category removed");
    }
    return Results.NotFound("Category not found");
});
// error found in the put method, it is not working, i will check it later and update it.
app.MapPut("/api/categories/{categoryId}",(Guid categoryId,[FromBody] Category updatedCategory)=>{

    var foundCategory = categories.FirstOrDefault(c=>c.CategoryId == categoryId);
    if (foundCategory == null){
        return Results.NotFound("Category not found");
    
    }
    if (updatedCategory == null)
    {
        return Results.BadRequest("Updated category data is required");
    }
    if(!string.IsNullOrEmpty(updatedCategory.CategoryName)){
        foundCategory.CategoryName = updatedCategory.CategoryName;
    }
    if(!string.IsNullOrEmpty(updatedCategory.Description)){
        foundCategory.Description = updatedCategory.Description;
    }
    ;
    return Results.NoContent();
});
//

app.Run();

//dto is the template for the product.

public record Category
{
    
    public Guid CategoryId { get; set; }
    public string? CategoryName { get; set; }

    public string? Description { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; }
}; 

//crud operations for the product.