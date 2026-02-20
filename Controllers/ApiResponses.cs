using System.Data.SqlTypes;

public class ApiResponses<T>
{
    public bool Success {get;set;}

    public string Message {get;set;} = string.Empty;

    public T? Data{get;set;}

    public List<String>? ErrorMessage {get;set;}

    public int StatusCode {get;set;}

    public DateTime TimeStamp {get;set;}

    //Constructor for generic api responses
    private ApiResponses(bool success,T data ,List<String> errorMessage, int statusCode , string message)
    {
        Success = success;
        Message = message;
        Data = data;
        ErrorMessage = errorMessage;
        StatusCode = statusCode;
        TimeStamp = DateTime.UtcNow;
        }

//now craeting a static method so that we can use it without using new keyword or not to create any new object of that constructor

    public static ApiResponses<T> SuccessResponse(T data , int statusCode , string message = "")
    {
       return new ApiResponses<T> (true,data,null,statusCode,message);
    }

//creating static method for ErrorResponse

     public static ApiResponses<T> ErrorResponse(List<String> errorMsg , int statusCode , string message = "")
   {

    return new ApiResponses<T>(false,default(T),errorMsg,statusCode,message);

   }

}