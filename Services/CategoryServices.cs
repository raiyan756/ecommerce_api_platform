using AutoMapper;
using Microsoft.EntityFrameworkCore;

public class CategoryServices : ICategoryServices
{
    private readonly IMapper _mapper;
    private readonly AppDbContext _appDbContext;

    public CategoryServices(AppDbContext appDbContext, IMapper mapper)
    {
        _mapper = mapper;
        _appDbContext = appDbContext;
    }

    // Get all categories
    public async Task<List<ReadCategoryDtos>> GetAllCategories()
    {
        var categories = await _appDbContext.categories.ToListAsync();
        return _mapper.Map<List<ReadCategoryDtos>>(categories);
    }

    // Create new category
    public async Task<ReadCategoryDtos> CreateCategory(CreateCategoryDtos createCategoryDtos)
    {
        var newCategory = _mapper.Map<Category>(createCategoryDtos);
        newCategory.CategoryId = Guid.NewGuid();
        newCategory.CreatedAt = DateTime.UtcNow;

        _appDbContext.categories.Add(newCategory);
        await _appDbContext.SaveChangesAsync();  // ✅ MUST

        return _mapper.Map<ReadCategoryDtos>(newCategory);
    }

    // Get category by id
    public async Task<ReadCategoryDtos?> GetCategoryById(Guid categoryId)
    {
        var foundCategory = await _appDbContext.categories
            .FirstOrDefaultAsync(c => c.CategoryId == categoryId);

        if (foundCategory == null)
            return null;

        return _mapper.Map<ReadCategoryDtos>(foundCategory);
    }

    // Update category
    public async Task<ReadCategoryDtos?> UpdateCategoryById(Guid categoryId, UpdateCategoryDtos updateCategoryDtos)
    {
        var foundCategory = await _appDbContext.categories
            .FirstOrDefaultAsync(c => c.CategoryId == categoryId);

        if (foundCategory == null)
            return null;

        _mapper.Map(updateCategoryDtos, foundCategory);

        await _appDbContext.SaveChangesAsync();  // ✅ MUST

        return _mapper.Map<ReadCategoryDtos>(foundCategory);
    }

    // Delete category
    public async Task<bool> DeleteCategory(Guid categoryId)
    {
        var foundCategory = await _appDbContext.categories
            .FirstOrDefaultAsync(c => c.CategoryId == categoryId);

        if (foundCategory == null)
            return false;

        _appDbContext.categories.Remove(foundCategory);
        await _appDbContext.SaveChangesAsync();  // ✅ MUST

        return true;
    }
}