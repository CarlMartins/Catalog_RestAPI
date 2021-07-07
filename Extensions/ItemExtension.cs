using NET5_RestAPI.DTOs;
using NET5_RestAPI.Entities;

namespace NET5_RestAPI.Extensions
{
  public static class Extension
  {
    public static ItemDto AsDto(this Item item)
    {
      return new ItemDto
      {
        Id = item.Id,
        Name = item.Name,
        Price = item.Price,
        CreatedDate = item.CreatedDate
      };
    }
  }
}