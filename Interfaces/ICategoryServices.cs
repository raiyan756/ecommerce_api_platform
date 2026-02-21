public interface ICategoryServices
{
     List<ReadCategoryDtos> GetAllCategories();
     ReadCategoryDtos CreateCategory (CreateCategoryDtos createCategoryDtos);
     ReadCategoryDtos? GetCategoryById (Guid categoryId);

     ReadCategoryDtos? UpdateCategoryById (Guid CategoryId,UpdateCategoryDtos updateCategoryDtos);
     bool DeleteCategory(Guid CategoryId);

}