public interface ICategoryServices
{
     Task< Pagination<ReadCategoryDtos>> GetAllCategories(int pageNumber ,int pageSize,string? search = null,string? sortOrder = null);
     Task<ReadCategoryDtos> CreateCategory (CreateCategoryDtos createCategoryDtos);
     Task<ReadCategoryDtos?>  GetCategoryById (Guid categoryId);

     Task<ReadCategoryDtos?>  UpdateCategoryById (Guid CategoryId,UpdateCategoryDtos updateCategoryDtos);
     Task<bool> DeleteCategory  (Guid CategoryId);

}