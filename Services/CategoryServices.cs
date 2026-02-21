using Microsoft.AspNetCore.Mvc;

public class CategoryServices
{
     private static readonly List<Category> categories = new List<Category>();

     public List<ReadCategoryDtos> GetAllCategories()
    {
        return  categories.Select(c => new ReadCategoryDtos
        {
            CategoryId = c.CategoryId,
            CategoryName = c.CategoryName,
            Description = c.Description,
            CreatedAt = c.CreatedAt
              }).ToList();
        }


        public ReadCategoryDtos CreateCategory (CreateCategoryDtos createCategoryDtos)
    {
       var newcat = new Category
    {
        CategoryId = Guid.NewGuid(),
        CategoryName = createCategoryDtos.CategoryName,
        Description = createCategoryDtos.Description,
        CreatedAt = DateTime.UtcNow
        
    };

    categories.Add(newcat);

    return new ReadCategoryDtos
    {
        CategoryId = newcat.CategoryId,
        CategoryName = newcat.CategoryName,
        Description = newcat.Description,
        CreatedAt = newcat. CreatedAt
    }

                ;
    }

    public ReadCategoryDtos? GetCategoryById (Guid categoryId)
    {

        var foundCategory = categories.FirstOrDefault(c=>c.CategoryId == categoryId);

        if (foundCategory == null)
        {
            return null;
            
        }

        return new ReadCategoryDtos {
           CategoryId = foundCategory.CategoryId,
           Description = foundCategory.Description,
           CategoryName = foundCategory.CategoryName,
           CreatedAt = foundCategory.CreatedAt
           };


        
    }

    public ReadCategoryDtos? UpdateCategoryById (Guid CategoryId,UpdateCategoryDtos updateCategoryDtos)
    {
        var foundCategory = categories.FirstOrDefault(c=>c.CategoryId == CategoryId);

        if (foundCategory == null)
        {
            return null;
        }
       
       var updatedName = foundCategory.CategoryName = updateCategoryDtos.CategoryName;

      var updatedDescription =  foundCategory.Description = updateCategoryDtos.Description;

      return new ReadCategoryDtos
      {
        CategoryId = foundCategory.CategoryId,
        CategoryName = updatedName,
        Description = updatedDescription,
        CreatedAt = foundCategory.CreatedAt
      };

       
        
    }

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