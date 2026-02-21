public interface ICategoryServices
{
     Task< List<ReadCategoryDtos>> GetAllCategories();
     Task<ReadCategoryDtos> CreateCategory (CreateCategoryDtos createCategoryDtos);
    Task<ReadCategoryDtos?>  GetCategoryById (Guid categoryId);

    Task<ReadCategoryDtos?>  UpdateCategoryById (Guid CategoryId,UpdateCategoryDtos updateCategoryDtos);
     Task<bool> DeleteCategory  (Guid CategoryId);

}