public class QueryParameter
{
    private const int maxPageSize = 50;

    public int pageNumber { get; set; } = 1;
    public int pageSize { get; set; } = 6;

    public string? search { get; set; }
    public string? sortOrder { get; set; }

    public QueryParameter Validate()
    {
        if (pageNumber < 1)
        {
            pageNumber = 1;
        }

        if (pageSize < 1)
        {
            pageSize = 6;
        }

        if (pageSize > maxPageSize)
        {
            pageSize = maxPageSize;
        }

        return this;
    }
}