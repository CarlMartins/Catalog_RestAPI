using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using NET5_RestAPI.DTOs;
using NET5_RestAPI.Entities;
using NET5_RestAPI.Extensions;
using NET5_RestAPI.Repositories;

namespace NET5_RestAPI.Controllers
{
  public class ItemsController : BaseApiController
  {
    private readonly IItemsRepository _repository;

    public ItemsController(IItemsRepository repository)
    {
      _repository = repository;
    }

    [HttpGet]
    public IEnumerable<ItemDto> GetItems()
    {
      var items = _repository.GetItems().Select(item => item.AsDto());
      return items;
    }

    [HttpGet("{id}")]
    public ItemDto GetItem(Guid id)
    {
      var item = _repository.GetItem(id);
      return item.AsDto();
    }

    [HttpPost]
    public ActionResult<ItemDto> CreateItem(CreateItemDto itemDto)
    {
      Item item = new()
      {
        Id = Guid.NewGuid(),
        Name = itemDto.Name,
        Price = itemDto.Price,
        CreatedDate = DateTimeOffset.UtcNow
      };

      _repository.CreateItem(item);

      return CreatedAtAction(nameof(GetItem), new { item.Id }, item.AsDto());
    }

    [HttpPut("{id}")]
    public ActionResult UpdateItem(Guid id, UpdateItemDto itemDto)
    {
      var existingItem = _repository.GetItem(id);

      if (existingItem is null)
      {
        return NotFound();
      }

      Item updatedItem = existingItem with
      {
        Name = itemDto.Name,
        Price = itemDto.Price
      };

      _repository.UpdateItem(updatedItem);

      return NoContent();
    }

    [HttpDelete("{id}")]
    public ActionResult DeleteItem(Guid id)
    {
      var existingItem = _repository.GetItem(id);
      if (existingItem is null)
      {
        return NotFound();
      }

      _repository.DeleteItem(id);

      return NoContent();
    }
  }
}