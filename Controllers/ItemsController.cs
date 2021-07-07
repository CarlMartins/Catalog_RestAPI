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
  }
}