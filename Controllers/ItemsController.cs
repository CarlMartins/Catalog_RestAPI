using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    public async Task<IEnumerable<ItemDto>> GetItemsAsync()
    {
      var items = (await _repository.GetItemsAsync())
        .Select(item => item.AsDto());
      return items;
    }

    [HttpGet("{id}")]
    public async Task<ItemDto> GetItemAsync(Guid id)
    {
      var item = await _repository.GetItemAsync(id);
      return item.AsDto();
    }

    [HttpPost]
    public async Task<ActionResult<ItemDto>> CreateItemAsync(CreateItemDto itemDto)
    {
      Item item = new()
      {
        Id = Guid.NewGuid(),
        Name = itemDto.Name,
        Price = itemDto.Price,
        CreatedDate = DateTimeOffset.UtcNow
      };

      await _repository.CreateItemAsync(item);

      return CreatedAtAction(nameof(GetItemAsync), new { item.Id }, item.AsDto());
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateItemAsync(Guid id, UpdateItemDto itemDto)
    {
      var existingItem = await _repository.GetItemAsync(id);

      if (existingItem is null)
      {
        return NotFound();
      }

      Item updatedItem = existingItem with
      {
        Name = itemDto.Name,
        Price = itemDto.Price
      };

      await _repository.UpdateItemAsync(updatedItem);

      return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteItem(Guid id)
    {
      var existingItem = await _repository.GetItemAsync(id);
      if (existingItem is null)
      {
        return NotFound();
      }

      await _repository.DeleteItemAsync(id);

      return NoContent();
    }
  }
}