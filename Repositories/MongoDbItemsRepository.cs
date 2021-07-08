using System;
using System.Collections.Generic;
using MongoDB.Driver;
using NET5_RestAPI.Entities;

namespace NET5_RestAPI.Repositories
{
  public class MongoDbItemsRepository : IItemsRepository
  {
    private const string _databaseName = "catalog";
    private const string _collectionName = "items";
    private readonly IMongoCollection<Item> _itemsCollection;

    public MongoDbItemsRepository(IMongoClient mongoClient)
    {
      IMongoDatabase database = mongoClient.GetDatabase(_databaseName);
      _itemsCollection = database.GetCollection<Item>(_collectionName);
    }
    public void CreateItem(Item item)
    {
      throw new NotImplementedException();
    }

    public void DeleteItem(Guid id)
    {
      throw new NotImplementedException();
    }

    public Item GetItem(Guid id)
    {
      throw new NotImplementedException();
    }

    public IEnumerable<Item> GetItems()
    {
      throw new NotImplementedException();
    }

    public void UpdateItem(Item item)
    {
      throw new NotImplementedException();
    }
  }
}