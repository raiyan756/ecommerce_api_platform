using AutoMapper;

public class CategoryProfile:Profile
{
    public CategoryProfile()
    {
        CreateMap<Category,ReadCategoryDtos>();
        CreateMap<UpdateCategoryDtos,Category>();
        CreateMap<CreateCategoryDtos,Category>();
    }
    
}