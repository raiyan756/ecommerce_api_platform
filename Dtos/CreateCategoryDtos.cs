using System.ComponentModel.DataAnnotations;

public class CreateCategoryDtos
{
      [Required(ErrorMessage ="Category name is required")]
       public string? CategoryName { get; set; }

      public string? Description { get; set; } = string.Empty;
}