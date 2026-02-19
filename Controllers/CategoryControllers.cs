using Microsoft.AspNetCore.Mvc;
[ApiController]
[Route("/api/categories")]

public class CategoryControllers:ControllerBase
{
    private static List<Category> categories = new List<Category>(); 
    [HttpGet]
    public IActionResult GetCategories([FromQuery] string searchValue)
    {
          if(!string.IsNullOrEmpty(searchValue)){
        var filteredCategories = categories.Where(c=> c.CategoryName != null && c.CategoryName.Contains(searchValue, StringComparison.OrdinalIgnoreCase)).ToList();
        return Ok(filteredCategories);
    }
    
    return Ok(categories);
        
    }

    [HttpPut("{categoryId}")]
    public IActionResult UpdateCategories(Guid categoryId,[FromBody] Category updatedCategory)
    {
        var foundCategory = categories.FirstOrDefault(c=>c.CategoryId == categoryId);
    if (foundCategory == null){
        return NotFound("Category not found");
    
    }
    if (updatedCategory == null)
    {
        return BadRequest("Updated category data is required");
    }
    if(!string.IsNullOrEmpty(updatedCategory.CategoryName)){
        foundCategory.CategoryName = updatedCategory.CategoryName;
    }
    if(!string.IsNullOrEmpty(updatedCategory.Description)){
        foundCategory.Description = updatedCategory.Description;
    }
    return NoContent();
    
    }

    [HttpDelete("{categoryId}")]
    public IActionResult DeleteCategories(Guid categoryId)
    {
    var removeCateory = categories.FirstOrDefault(c => c.CategoryId == categoryId);  
    if (removeCateory != null)
    {
        categories.Remove(removeCateory);
        return Ok("Category removed");
    }
    return NotFound("Category not found");
    }

    [HttpPost]
    public IActionResult InsertCategory([FromBody] Category category)
    {
        if(string.IsNullOrEmpty(category.CategoryName)){
        return BadRequest("Category name is required");
    }
        if (string.IsNullOrEmpty(category.Description))
        {
            return BadRequest("Description is Required");
        }

    var newcat = new Category
    {
        CategoryId = Guid.NewGuid(),
        CategoryName = category.CategoryName,
        Description = category.Description,
        CreatedAt = DateTime.UtcNow
    };
    categories.Add(newcat);
    return Created($"/api/categories/{newcat.CategoryId}", newcat);
    }

    
}