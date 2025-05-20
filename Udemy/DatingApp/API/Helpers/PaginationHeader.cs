namespace API.Helpers;

public class PaginationHeader(int currentPage, int itemsPerPage, int totalItems, int totalPage)
{
  public int CurrentPage { get; set; } = currentPage;
  public int ItemsPerPage { get; set; } = itemsPerPage;
  public int TotalItems { get; set; } = totalItems;
  public int TotalPage { get; set; } = totalPage;
}


