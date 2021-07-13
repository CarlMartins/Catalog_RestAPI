using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Catalog.Api.DTOs;
using Catalog.Api.Entities;
using Catalog.Api.Extensions;
using Catalog.Api.Repositories;

namespace Catalog.Api.Controllers
{
  public class ItemsController : BaseApiController
  {
    private readonly IItemsRepository _repository;
    private readonly ILogger<ItemsController> _logger;

    public ItemsController(IItemsRepository repository, ILogger<ItemsController> logger)
    {
      _repository = repository;
      _logger = logger;
    }

    [HttpGet]
    public async Task<IEnumerable<ItemDto>> GetItemsAsync()
    {
      var items = (await _repository.GetItemsAsync())
        .Select(item => item.AsDto());

      _logger.LogInformation($"{DateTime.UtcNow.ToString("hh:mm:ss")}: Retrieved {items.Count()} items");
      return items;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ItemDto>> GetItemAsync(Guid id)
    {
      var item = await _repository.GetItemAsync(id);

      if (item is null)
      {
        return NotFound();
      }

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