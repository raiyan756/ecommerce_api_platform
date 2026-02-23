using System.ComponentModel.Design;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
[ApiController]
[Route("/api/categories")]

public class CategoryControllers:ControllerBase
{

    private ICategoryServices _categoryServices;

    
   
    public CategoryControllers(ICategoryServices categoryServices)
    {
        _categoryServices = categoryServices;
        
    }
    
    [HttpGet]
    public async Task<IActionResult>  GetCategories([FromQuery] QueryParameter queryParameter )
    {
        /*
        if(!string.IsNullOrEmpty(searchValue)){
        var filteredCategories = categories.Where(c=> c.CategoryName != null && c.CategoryName.Contains(searchValue, StringComparison.OrdinalIgnoreCase)).ToList();
       return Ok(filteredCategories);
    }
    */
        var validatedQuery = queryParameter.Validate();
        var newCategories =await _categoryServices.GetAllCategories(validatedQuery);
    
       return Ok (ApiResponses<Pagination<ReadCategoryDtos>>.SuccessResponse(newCategories,200,"category created successfully"));
    }
    //get category by id
    [HttpGet("{categoryId:guid}")]
    public async Task<IActionResult> GetCategoryById(Guid categoryId)
    {
      var foundCategory = await _categoryServices.GetCategoryById(categoryId);

        if (foundCategory == null)
        {
            return NotFound (ApiResponses<object>.ErrorResponse(new List<string> {"Category with ID does not found"}, 404 , "Invalid request" ));
        }
    
        return Ok (ApiResponses<ReadCategoryDtos>.SuccessResponse(foundCategory,200,"category returned successfully"));
    }

    [HttpPut("{categoryId:guid}")]
    public async Task<IActionResult>  UpdateCategories(Guid categoryId,[FromBody] UpdateCategoryDtos updatedCategory)
    {
        var foundCategory =await _categoryServices.UpdateCategoryById(categoryId,updatedCategory);

        if (foundCategory == null)
        {
            return NotFound(ApiResponses<object>.ErrorResponse(new List<string>{"not found"},404,"Update is not successfull"));
        }
        else
        {
                   return Ok(ApiResponses<ReadCategoryDtos>.SuccessResponse(foundCategory,204,"category updated successfully"));

        }
    
    }

    [HttpDelete("{categoryId:guid}")]
    public async Task<IActionResult> DeleteCategories(Guid categoryId)
    {
       var removeCateory =await _categoryServices.DeleteCategory(categoryId);
   
    if (!removeCateory)
    {
        return NotFound(ApiResponses<object>.ErrorResponse(new List<string>{"No Matched Id dound to Delete"},404,"Deletation Failed"));
    }
    return Ok(ApiResponses<object>.SuccessResponse(null,204,"Category Deleted Successfully"));
    }

    [HttpPost]
    public async Task<IActionResult> InsertCategory([FromBody] CreateCategoryDtos category)
    {
       var newReadCategoryDtos  =await _categoryServices.CreateCategory(category);
    
    return Created(nameof(GetCategoryById),
    ApiResponses<ReadCategoryDtos>.SuccessResponse(newReadCategoryDtos,201,"category created successfully"));
    }

    
}