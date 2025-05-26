using Microsoft.EntityFrameworkCore;
namespace API.Helpers;

public class PagedList<E> : List<E>
{
  public PagedList(IEnumerable<E> items, int count, int pageNumber, int pageSize)
  {
    CurrentPage = pageNumber;
    TotalPages = (int)Math.Ceiling(count / (double)pageSize);
    PageSize = pageSize;
    TotalCount = count;
    AddRange(items);
  }
  public int CurrentPage { get; set; }
  public int TotalPages { get; set; }
  public int PageSize { get; set; }
  public int TotalCount { get; set; }

  public static async Task<PagedList<E>> CreateAsync(IQueryable<E> source,
  int pageNumber, int pageSize)
  {
    var count = await source.CountAsync();
    var items = await source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
    return new PagedList<E>(items, count, pageNumber, pageSize);

  }
}
