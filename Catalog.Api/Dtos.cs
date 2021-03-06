using System;
using System.ComponentModel.DataAnnotations;

namespace Catalog.Api.Dtos
{
  public record ItemDto(
    Guid Id,
    string Name,
    string Description,
    decimal Price,
    DateTimeOffset CreatedDate
    );

  public record CreateItemDto(
    [Required] string Name,
    [Range(1, 1000)] string Description,
    decimal Price
    );

  public record UpdateItemDto(
  [Required] string Name,
  [Range(1, 1000)] string Description,
  decimal Price
  );
}