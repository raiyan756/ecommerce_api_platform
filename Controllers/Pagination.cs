using System.Security.Cryptography.X509Certificates;

public class Pagination<T>
{
    public IEnumerable<T> Items{get;set;} = new List<T>();

    public int totalCount {get;set;}

    public int pageNumber {get;set;}

    public int pageSize {get;set;}

    public int totalPages => (int) Math.Ceiling((double) totalCount/pageSize); 
}