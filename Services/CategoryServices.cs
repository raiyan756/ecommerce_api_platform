using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

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
    public async Task<Pagination<ReadCategoryDtos>> GetAllCategories( QueryParameter queryParameter)
    {
        IQueryable <Category> query = _appDbContext.categories;
        /*
        if (!string.IsNullOrWhiteSpace(search.ToLower()))
        {
            query = query.Where(c=>c.CategoryName.ToLower().Contains(search) || c.Description.ToLower().Contains(search));
        }
        */
        //searching
        if (!string.IsNullOrWhiteSpace(queryParameter.search))
    {
    query = query.Where(c =>
        EF.Functions.Like(c.CategoryName, $"%{queryParameter.search}%") ||
        EF.Functions.Like(c.Description, $"%{queryParameter.search}%")
    );
     }

     //sorting
     if (string.IsNullOrWhiteSpace(queryParameter.sortOrder))
{
    query = query.OrderBy(c => c.CategoryName);
}
else
{
    var formattedQuery =queryParameter. sortOrder.Trim().ToLower();

    if (Enum.TryParse<SortOrder>(formattedQuery, true, out var parsedSortOrder))
    {
        query = parsedSortOrder switch
        {
            SortOrder.nameasc        => query.OrderBy(c => c.CategoryName),
            SortOrder.namedesc       => query.OrderByDescending(c => c.CategoryName),
            SortOrder.createdatasc   => query.OrderBy(c => c.CreatedAt),
            SortOrder.createdatdesc  => query.OrderByDescending(c => c.CreatedAt),

            _ => query.OrderBy(c => c.CategoryName)
        };
    }
    else
    {
        // invalid sortOrder value → default sorting
        query = query.OrderBy(c => c.CategoryName);
    }
}

       
        var totalCount = await query.CountAsync();
        var items = await query.Skip((queryParameter.pageNumber-1)*queryParameter.pageSize).Take(queryParameter.pageSize).ToListAsync();
        var results =  _mapper.Map<List<ReadCategoryDtos>>(items);
        return new Pagination<ReadCategoryDtos>
        {
            Items = results,
            totalCount = totalCount,
            pageNumber = queryParameter.pageNumber,
            pageSize = queryParameter.pageSize
            
        };

        
        
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