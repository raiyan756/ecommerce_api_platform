using System.Diagnostics.CodeAnalysis;
using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

public class CategoryServices : ICategoryServices
{
     private static readonly List<Category> categories = new List<Category>();
     private IMapper _imaper;

     public CategoryServices(IMapper impaer)
    {
        _imaper = impaer;
    }

//Get all the categories
     public List<ReadCategoryDtos> GetAllCategories()
    {
       return _imaper.Map<List<ReadCategoryDtos>>(categories);
     }

//create new categories
        public ReadCategoryDtos CreateCategory (CreateCategoryDtos createCategoryDtos)
    {
     
    var newCategory = _imaper.Map<Category>(createCategoryDtos);
    newCategory.CategoryId = Guid.NewGuid();
    newCategory.CreatedAt = DateTime.UtcNow;

    categories.Add(newCategory);

    return _imaper.Map<ReadCategoryDtos>(newCategory);
     }

//get categories by id
    public ReadCategoryDtos? GetCategoryById (Guid categoryId)
    {

        var foundCategory = categories.FirstOrDefault(c=>c.CategoryId == categoryId);

        if (foundCategory == null)
        {
            return null;
            
        }

        return _imaper.Map<ReadCategoryDtos>(foundCategory);

        
    }

//update categories by id
    public ReadCategoryDtos? UpdateCategoryById (Guid CategoryId,UpdateCategoryDtos updateCategoryDtos)
    {
        var foundCategory = categories.FirstOrDefault(c=>c.CategoryId == CategoryId);

        if (foundCategory == null)
        {
            return null;
        }
       
     //  foundCategory.CategoryName = updateCategoryDtos.CategoryName;

       //foundCategory.Description = updateCategoryDtos.Description;

       _imaper.Map(updateCategoryDtos,foundCategory);

      return _imaper.Map<ReadCategoryDtos>(foundCategory);

       
        
    }
//delete categories by id
    public bool DeleteCategory(Guid CategoryId)
    {
         var foundCategory = categories.FirstOrDefault(c=>c.CategoryId == CategoryId);

        if (foundCategory != null)
        {
            categories.Remove(foundCategory);
            return true;
        }
        else
        {
            return false;
        }

        }


    
}