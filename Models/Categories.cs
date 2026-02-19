using System.Runtime.CompilerServices;

public class Category
{
    
    public Guid CategoryId { get; set; }
    public string? CategoryName { get; set; }

    public string? Description { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; }
}; 