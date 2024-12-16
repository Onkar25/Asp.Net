using System.Text.Json;
using API.Helpers;

namespace API.Extensions;

public static class HttpExtensions
{
public static void AddPaginationHeader<T>(this HttpResponse response , PagedList<T> data)
{
    var paginationHeader = new PaginationHeader(data.CurrentPage,data.PageSize,data.TotalCount,data.TotalPages);

    var jsonOption = new JsonSerializerOptions{PropertyNamingPolicy = JsonNamingPolicy.CamelCase};
    response.Headers.Append("Pagination",JsonSerializer.Serialize(paginationHeader,jsonOption));
    response.Headers.Append("Access-Control-Expose-Headers","Pagination");
    
}
}
