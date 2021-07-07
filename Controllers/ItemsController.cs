using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using NET5_RestAPI.Entities;
using NET5_RestAPI.Repositories;

namespace NET5_RestAPI.Controllers
{
  public class ItemsController : BaseApiController
  {
    private readonly InMemItemsRepository _repository;

    public ItemsController()
    {
      _repository = new InMemItemsRepository();
    }

    [HttpGet]
    public IEnumerable<Item> GetItems()
    {
      var items = _repository.GetItems();
      return items;
    }

    [HttpGet("{id}")]
    public Item GetItem(Guid id)
    {
      var item = _repository.GetItem(id);
      return item;
    }
  }
}