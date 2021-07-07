using System;
using System.Collections.Generic;
using NET5_RestAPI.Entities;

namespace NET5_RestAPI.Repositories
{
  public interface IItemsRepository
  {
    Item GetItem(Guid id);
    IEnumerable<Item> GetItems();
  }
}