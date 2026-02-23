using Microsoft.AspNetCore.Mvc;

public interface ICategoryServices
{
     Task<Pagination<ReadCategoryDtos>> GetAllCategories(QueryParameter  queryParameter);
     Task<ReadCategoryDtos> CreateCategory (CreateCategoryDtos createCategoryDtos);
     Task<ReadCategoryDtos?>  GetCategoryById (Guid categoryId);

     Task<ReadCategoryDtos?>  UpdateCategoryById (Guid CategoryId,UpdateCategoryDtos updateCategoryDtos);
     Task<bool> DeleteCategory  (Guid CategoryId);

}